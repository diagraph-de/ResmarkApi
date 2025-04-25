// Copyright (c) Converter Systems LLC. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Microsoft.Extensions.Logging;
using Workstation.Collections;
using Workstation.ServiceModel.Ua.Channels;

namespace Workstation.ServiceModel.Ua;

/// <summary>
///     A base class that subscribes to receive data changes and events from an OPC UA server.
/// </summary>
public abstract class SubscriptionBase : INotifyPropertyChanged, INotifyDataErrorInfo, ISetDataErrorInfo
{
    private readonly ActionBlock<PublishResponse> actionBlock;
    private readonly UaApplication application;
    private readonly string? endpointUrl;
    private readonly ErrorsContainer<string> errors;
    private readonly uint keepAliveCount = ClientSessionChannel.DefaultKeepaliveCount;
    private readonly uint lifetimeCount;
    private readonly ILogger? logger;
    private readonly MonitoredItemBaseCollection monitoredItems = new();
    private readonly IProgress<CommunicationState> progress;
    private readonly double publishingInterval = ClientSessionChannel.DefaultPublishingInterval;
    private readonly CancellationTokenSource stateMachineCts;
    private readonly Task stateMachineTask;
    private volatile ClientSessionChannel? innerChannel;
    private volatile bool isPublishing;
    private PropertyChangedEventHandler? propertyChanged;
    private CommunicationState state = CommunicationState.Created;
    private volatile uint subscriptionId;
    private volatile TaskCompletionSource<bool> whenSubscribed;
    private volatile TaskCompletionSource<bool> whenUnsubscribed;

    /// <summary>
    ///     Initializes a new instance of the <see cref="SubscriptionBase" /> class.
    /// </summary>
    public SubscriptionBase()
        : this(UaApplication.Current)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="SubscriptionBase" /> class.
    /// </summary>
    /// <param name="application">The UaApplication.</param>
    public SubscriptionBase(UaApplication? application)
    {
        this.application = application ?? throw new ArgumentNullException(nameof(application));
        this.application.Completion.ContinueWith(t => stateMachineCts?.Cancel());
        logger = this.application.LoggerFactory?.CreateLogger(GetType());
        errors = new ErrorsContainer<string>(p => ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(p)));
        progress = new Progress<CommunicationState>(s => State = s);
        propertyChanged += OnPropertyChanged;
        whenSubscribed = new TaskCompletionSource<bool>();
        whenUnsubscribed = new TaskCompletionSource<bool>();
        whenUnsubscribed.TrySetResult(true);

        // register the action to be run on the ui thread, if there is one.
        if (SynchronizationContext.Current != null)
            actionBlock = new ActionBlock<PublishResponse>(pr => OnPublishResponse(pr),
                new ExecutionDataflowBlockOptions
                {
                    SingleProducerConstrained = true, TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext()
                });
        else
            actionBlock = new ActionBlock<PublishResponse>(pr => OnPublishResponse(pr),
                new ExecutionDataflowBlockOptions { SingleProducerConstrained = true });

        // read [Subscription] attribute.
        var typeInfo = GetType().GetTypeInfo();
        var sa = typeInfo.GetCustomAttribute<SubscriptionAttribute>();
        if (sa != null)
        {
            endpointUrl = sa.EndpointUrl;
            publishingInterval = sa.PublishingInterval;
            keepAliveCount = sa.KeepAliveCount;
            lifetimeCount = sa.LifetimeCount;
        }

        // read [MonitoredItem] attributes.
        foreach (var propertyInfo in typeInfo.DeclaredProperties)
        {
            var mia = propertyInfo.GetCustomAttribute<MonitoredItemAttribute>();
            if (mia == null || string.IsNullOrEmpty(mia.NodeId)) continue;

            MonitoringFilter? filter = null;
            if (mia.AttributeId == AttributeIds.Value && (mia.DataChangeTrigger != DataChangeTrigger.StatusValue ||
                                                          mia.DeadbandType != DeadbandType.None))
                filter = new DataChangeFilter
                {
                    Trigger = mia.DataChangeTrigger, DeadbandType = (uint)mia.DeadbandType,
                    DeadbandValue = mia.DeadbandValue
                };

            var propType = propertyInfo.PropertyType;
            if (propType == typeof(DataValue))
            {
                monitoredItems.Add(new DataValueMonitoredItem(
                    this,
                    propertyInfo,
                    ExpandedNodeId.Parse(mia.NodeId),
                    indexRange: mia.IndexRange,
                    attributeId: mia.AttributeId,
                    samplingInterval: mia.SamplingInterval,
                    filter: filter,
                    queueSize: mia.QueueSize,
                    discardOldest: mia.DiscardOldest));
                continue;
            }

            if (propType == typeof(BaseEvent) || propType.GetTypeInfo().IsSubclassOf(typeof(BaseEvent)))
            {
                monitoredItems.Add(new EventMonitoredItem(
                    this,
                    propertyInfo,
                    ExpandedNodeId.Parse(mia.NodeId),
                    indexRange: mia.IndexRange,
                    attributeId: mia.AttributeId,
                    samplingInterval: mia.SamplingInterval,
                    filter: new EventFilter { SelectClauses = EventHelper.GetSelectClauses(propType) },
                    queueSize: mia.QueueSize,
                    discardOldest: mia.DiscardOldest));
                continue;
            }

            if (propType == typeof(ObservableQueue<DataValue>))
            {
                monitoredItems.Add(new DataValueQueueMonitoredItem(
                    this,
                    propertyInfo,
                    ExpandedNodeId.Parse(mia.NodeId),
                    indexRange: mia.IndexRange,
                    attributeId: mia.AttributeId,
                    samplingInterval: mia.SamplingInterval,
                    filter: filter,
                    queueSize: mia.QueueSize,
                    discardOldest: mia.DiscardOldest));
                continue;
            }

            if (propType.IsConstructedGenericType && propType.GetGenericTypeDefinition() == typeof(ObservableQueue<>))
            {
                var elemType = propType.GenericTypeArguments[0];
                if (elemType == typeof(BaseEvent) || elemType.GetTypeInfo().IsSubclassOf(typeof(BaseEvent)))
                {
                    monitoredItems.Add((MonitoredItemBase)Activator.CreateInstance(
                        typeof(EventQueueMonitoredItem<>).MakeGenericType(elemType),
                        this,
                        propertyInfo,
                        ExpandedNodeId.Parse(mia.NodeId),
                        mia.AttributeId,
                        mia.IndexRange,
                        MonitoringMode.Reporting,
                        mia.SamplingInterval,
                        new EventFilter { SelectClauses = EventHelper.GetSelectClauses(elemType) },
                        mia.QueueSize,
                        mia.DiscardOldest)!);
                    continue;
                }
            }

            monitoredItems.Add((MonitoredItemBase)Activator.CreateInstance(
                typeof(ValueMonitoredItem<>).MakeGenericType(propType),
                this,
                propertyInfo,
                ExpandedNodeId.Parse(mia.NodeId),
                mia.AttributeId,
                mia.IndexRange,
                MonitoringMode.Reporting,
                mia.SamplingInterval,
                filter,
                mia.QueueSize,
                mia.DiscardOldest)!);
        }

        stateMachineCts = new CancellationTokenSource();
        stateMachineTask = Task.Run(() => StateMachineAsync(stateMachineCts.Token));
    }

    /// <summary>
    ///     Gets the <see cref="CommunicationState" />.
    /// </summary>
    public CommunicationState State
    {
        get => state;
        private set => SetProperty(ref state, value);
    }

    /// <summary>
    ///     Gets the current subscription Id.
    /// </summary>
    public uint SubscriptionId => state == CommunicationState.Opened ? subscriptionId : 0u;

    /// <summary>
    ///     Gets the inner channel.
    /// </summary>
    protected ClientSessionChannel InnerChannel
    {
        get
        {
            if (innerChannel == null) throw new ServiceResultException(StatusCodes.BadServerNotConnected);

            return innerChannel;
        }
    }

    /// <summary>
    ///     Occurs when the validation errors have changed for a property or entity.
    /// </summary>
    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    /// <summary>
    ///     Gets a value indicating whether the entity has validation errors.
    /// </summary>
    public bool HasErrors => errors.HasErrors;

    /// <summary>
    ///     Gets the validation errors for a specified property or for the entire entity.
    /// </summary>
    /// <param name="propertyName">
    ///     The name of the property to retrieve validation errors for, or null or System.String.Empty
    ///     to retrieve entity-level errors.
    /// </param>
    /// <returns>The validation errors for the property or entity.</returns>
    public IEnumerable GetErrors(string? propertyName)
    {
        return errors.GetErrors(propertyName);
    }

    /// <inheritdoc />
    public event PropertyChangedEventHandler? PropertyChanged
    {
        add
        {
            var flag = propertyChanged?.GetInvocationList().Length == 1;
            propertyChanged += value;
            if (flag)
            {
                whenUnsubscribed = new TaskCompletionSource<bool>();
                whenSubscribed.TrySetResult(true);
            }
        }

        remove
        {
            propertyChanged -= value;
            if (propertyChanged?.GetInvocationList().Length == 1)
            {
                whenSubscribed = new TaskCompletionSource<bool>();
                whenUnsubscribed.TrySetResult(true);
            }
        }
    }

    /// <summary>
    ///     Sets the validation errors for a specified property or for the entire entity.
    /// </summary>
    /// <param name="propertyName">The name of the property, or null or System.String.Empty to set entity-level errors.</param>
    /// <param name="errors">The validation errors for the property or entity.</param>
    void ISetDataErrorInfo.SetErrors(string propertyName, IEnumerable<string>? errors)
    {
        this.errors.SetErrors(propertyName, errors);
    }

    /// <summary>
    ///     Requests a Refresh of all Conditions.
    /// </summary>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    public async Task<StatusCode> ConditionRefreshAsync()
    {
        if (State != CommunicationState.Opened) return StatusCodes.BadServerNotConnected;

        return await InnerChannel.ConditionRefreshAsync(SubscriptionId);
    }

    /// <summary>
    ///     Acknowledges a condition.
    /// </summary>
    /// <param name="condition">an AcknowledgeableCondition.</param>
    /// <param name="comment">a comment.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    public async Task<StatusCode> AcknowledgeAsync(AcknowledgeableCondition condition, LocalizedText? comment = null)
    {
        if (condition == null) throw new ArgumentNullException(nameof(condition));

        if (State != CommunicationState.Opened) return StatusCodes.BadServerNotConnected;

        return await InnerChannel.AcknowledgeAsync(condition, comment);
    }

    /// <summary>
    ///     Confirms a condition.
    /// </summary>
    /// <param name="condition">an AcknowledgeableCondition.</param>
    /// <param name="comment">a comment.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    public async Task<StatusCode> ConfirmAsync(AcknowledgeableCondition condition, LocalizedText? comment = null)
    {
        if (condition == null) throw new ArgumentNullException(nameof(condition));

        if (State != CommunicationState.Opened) return StatusCodes.BadServerNotConnected;

        return await InnerChannel.ConfirmAsync(condition, comment);
    }

    /// <summary>
    ///     Sets the property value and notifies listeners that the property value has changed.
    /// </summary>
    /// <typeparam name="T">Type of the property.</typeparam>
    /// <param name="storage">Reference to a storage field.</param>
    /// <param name="value">The new value.</param>
    /// <param name="propertyName">
    ///     Name of the property used to notify listeners. This
    ///     value is optional and can be provided automatically when invoked from compilers that
    ///     support CallerMemberName.
    /// </param>
    /// <returns>True if the value changed, otherwise false.</returns>
    protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null)
    {
        if (Equals(storage, value)) return false;

        storage = value;
        NotifyPropertyChanged(propertyName);
        return true;
    }

    /// <summary>
    ///     Notifies listeners that the property value has changed.
    /// </summary>
    /// <param name="propertyName">
    ///     Name of the property used to notify listeners. This
    ///     value is optional and can be provided automatically when invoked from compilers
    ///     that support <see cref="T:System.Runtime.CompilerServices.CallerMemberNameAttribute" />.
    /// </param>
    protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    ///     Handle PublishResponse message.
    /// </summary>
    /// <param name="publishResponse">The publish response.</param>
    private void OnPublishResponse(PublishResponse publishResponse)
    {
        isPublishing = true;
        try
        {
            // loop thru all the notifications
            var nd = publishResponse.NotificationMessage?.NotificationData;
            if (nd == null) return;

            foreach (var n in nd)
            {
                // if data change.
                var dcn = n as DataChangeNotification;
                if (dcn?.MonitoredItems != null)
                {
                    foreach (var min in dcn.MonitoredItems)
                    {
                        if (min?.Value == null)
                        {
                            logger?.LogError("One of the monitored item notifications is null");
                            continue;
                        }

                        if (monitoredItems.TryGetValueByClientId(min.ClientHandle, out var item))
                            try
                            {
                                item.Publish(min.Value);
                            }
                            catch (Exception ex)
                            {
                                logger?.LogError($"Error publishing value for NodeId {item.NodeId}. {ex.Message}");
                            }
                    }

                    continue;
                }

                // if event.
                var enl = n as EventNotificationList;
                if (enl?.Events != null)
                    foreach (var efl in enl.Events)
                    {
                        if (efl?.EventFields == null)
                        {
                            logger?.LogError("One of the event field list is null");
                            continue;
                        }

                        if (monitoredItems.TryGetValueByClientId(efl.ClientHandle, out var item))
                            try
                            {
                                item.Publish(efl.EventFields);
                            }
                            catch (Exception ex)
                            {
                                logger?.LogError($"Error publishing event for NodeId {item.NodeId}. {ex.Message}");
                            }
                    }
            }
        }
        finally
        {
            isPublishing = false;
        }
    }

    /// <summary>
    ///     Handles PropertyChanged event. If the property is associated with a MonitoredItem, writes the property value to the
    ///     node of the server.
    /// </summary>
    /// <param name="sender">the sender.</param>
    /// <param name="e">the event.</param>
    private async void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (isPublishing || string.IsNullOrEmpty(e.PropertyName)) return;

        if (monitoredItems.TryGetValueByName(e.PropertyName, out var item))
            if (item.TryGetValue(out var value))
            {
                StatusCode statusCode;
                try
                {
                    var writeRequest = new WriteRequest
                    {
                        NodesToWrite = new[]
                        {
                            new WriteValue
                            {
                                NodeId = ExpandedNodeId.ToNodeId(item.NodeId, InnerChannel.NamespaceUris),
                                AttributeId = item.AttributeId, IndexRange = item.IndexRange, Value = value
                            }
                        }
                    };
                    var writeResponse = await InnerChannel.WriteAsync(writeRequest).ConfigureAwait(false);
                    statusCode = writeResponse?.Results?[0] ?? StatusCodes.BadDataEncodingInvalid;
                }
                catch (ServiceResultException ex)
                {
                    statusCode = ex.StatusCode;
                }
                catch (Exception)
                {
                    statusCode = StatusCodes.BadServerNotConnected;
                }

                item.OnWriteResult(statusCode);
                if (StatusCode.IsBad(statusCode))
                    logger?.LogError(
                        $"Error writing value for {item.NodeId}. {StatusCodes.GetDefaultMessage(statusCode)}");
            }
    }

    /// <summary>
    ///     Signals the channel state is Closing.
    /// </summary>
    /// <param name="channel">The session channel. </param>
    /// <param name="token">A cancellation token. </param>
    /// <returns>A task.</returns>
    private async Task WhenChannelClosingAsync(ClientSessionChannel channel, CancellationToken token = default)
    {
        var tcs = new TaskCompletionSource<bool>();
        EventHandler handler = (o, e) => { tcs.TrySetResult(true); };
        using (token.Register(state => ((TaskCompletionSource<bool>)state!).TrySetCanceled(), tcs, false))
        {
            try
            {
                channel.Closing += handler;
                if (channel.State == CommunicationState.Opened) await tcs.Task;
            }
            finally
            {
                channel.Closing -= handler;
            }
        }
    }

    /// <summary>
    ///     The state machine manages the state of the subscription.
    /// </summary>
    /// <param name="token">A cancellation token.</param>
    /// <returns>A task.</returns>
    private async Task StateMachineAsync(CancellationToken token = default)
    {
        while (!token.IsCancellationRequested)
        {
            await whenSubscribed.Task;

            progress.Report(CommunicationState.Opening);

            try
            {
                if (endpointUrl is null)
                    throw new InvalidOperationException(
                        "The endpointUrl field must not be null. Please, use the Subscription attribute properly.");

                // get a channel.
                innerChannel = await application.GetChannelAsync(endpointUrl, token);

                try
                {
                    // create the subscription.
                    var subscriptionRequest = new CreateSubscriptionRequest
                    {
                        RequestedPublishingInterval = publishingInterval,
                        RequestedMaxKeepAliveCount = keepAliveCount,
                        RequestedLifetimeCount = Math.Max(lifetimeCount, 3 * keepAliveCount),
                        PublishingEnabled = true
                    };
                    var subscriptionResponse =
                        await innerChannel.CreateSubscriptionAsync(subscriptionRequest).ConfigureAwait(false);

                    // link up the dataflow blocks
                    var id = subscriptionId = subscriptionResponse.SubscriptionId;
                    var linkToken = innerChannel.LinkTo(actionBlock, pr => pr.SubscriptionId == id);

                    try
                    {
                        // create the monitored items.
                        var items = monitoredItems.ToList();
                        if (items.Count > 0)
                        {
                            var requests = items.Select(m => new MonitoredItemCreateRequest
                            {
                                ItemToMonitor = new ReadValueId
                                {
                                    NodeId = ExpandedNodeId.ToNodeId(m.NodeId, InnerChannel.NamespaceUris),
                                    AttributeId = m.AttributeId, IndexRange = m.IndexRange
                                },
                                MonitoringMode = m.MonitoringMode,
                                RequestedParameters = new MonitoringParameters
                                {
                                    ClientHandle = m.ClientId, DiscardOldest = m.DiscardOldest, QueueSize = m.QueueSize,
                                    SamplingInterval = m.SamplingInterval, Filter = m.Filter
                                }
                            }).ToArray();

                            //split requests array to MaxMonitoredItemsPerCall chunks
                            var maxmonitoreditemspercall = 100;
                            MonitoredItemCreateRequest[] requests_chunk;
                            int chunk_size;
                            for (var i_chunk = 0; i_chunk < requests.Length; i_chunk += maxmonitoreditemspercall)
                            {
                                chunk_size = Math.Min(maxmonitoreditemspercall, requests.Length - i_chunk);
                                requests_chunk = new MonitoredItemCreateRequest[chunk_size];
                                Array.Copy(requests, i_chunk, requests_chunk, 0, chunk_size);

                                var itemsRequest = new CreateMonitoredItemsRequest
                                {
                                    SubscriptionId = id,
                                    ItemsToCreate = requests_chunk
                                };
                                var itemsResponse = await innerChannel.CreateMonitoredItemsAsync(itemsRequest);

                                if (itemsResponse.Results is { } results)
                                    for (var i = 0; i < results.Length; i++)
                                    {
                                        var item = items[i];
                                        var result = results[i];

                                        if (result is null)
                                        {
                                            logger?.LogError(
                                                $"Error creating MonitoredItem for {item.NodeId}. The result is null.");
                                            continue;
                                        }

                                        item.OnCreateResult(result);
                                        if (StatusCode.IsBad(result.StatusCode))
                                            logger?.LogError(
                                                $"Error creating MonitoredItem for {item.NodeId}. {StatusCodes.GetDefaultMessage(result.StatusCode)}");
                                    }
                            }
                        }

                        progress.Report(CommunicationState.Opened);

                        // wait here until channel is closing, unsubscribed or token cancelled.
                        try
                        {
                            await Task.WhenAny(
                                WhenChannelClosingAsync(innerChannel, token),
                                whenUnsubscribed.Task);
                        }
                        catch
                        {
                        }
                        finally
                        {
                            progress.Report(CommunicationState.Closing);
                        }
                    }
                    catch (Exception ex)
                    {
                        logger?.LogError($"Error creating MonitoredItems. {ex.Message}");
                        progress.Report(CommunicationState.Faulted);
                    }
                    finally
                    {
                        linkToken.Dispose();
                    }

                    if (innerChannel.State == CommunicationState.Opened)
                        try
                        {
                            // delete the subscription.
                            var deleteRequest = new DeleteSubscriptionsRequest
                            {
                                SubscriptionIds = new[] { id }
                            };
                            await innerChannel.DeleteSubscriptionsAsync(deleteRequest);
                        }
                        catch (Exception ex)
                        {
                            logger?.LogError($"Error deleting subscription. {ex.Message}");
                            await Task.Delay(2000);
                        }

                    progress.Report(CommunicationState.Closed);
                }
                catch (Exception ex)
                {
                    logger?.LogError($"Error creating subscription. {ex.Message}");
                    progress.Report(CommunicationState.Faulted);
                    await Task.Delay(2000);
                }
            }
            catch (Exception ex)
            {
                logger?.LogTrace($"Error getting channel. {ex.Message}");
                progress.Report(CommunicationState.Faulted);
                await Task.Delay(2000);
            }
        }
    }
}