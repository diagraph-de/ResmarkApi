// Copyright (c) Converter Systems LLC. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Workstation.ServiceModel.Ua;

/// <summary>
///     Represents an event.
/// </summary>
public class BaseEvent
{
    [EventField(ObjectTypeIds.BaseEventType, "EventId")]
    public byte[]? EventId { get; set; }

    [EventField(ObjectTypeIds.BaseEventType, "EventType")]
    public NodeId? EventType { get; set; }

    [EventField(ObjectTypeIds.BaseEventType, "SourceName")]
    public string? SourceName { get; set; }

    [EventField(ObjectTypeIds.BaseEventType, "Time")]
    public DateTime Time { get; set; }

    [EventField(ObjectTypeIds.BaseEventType, "Message")]
    public LocalizedText? Message { get; set; }

    [EventField(ObjectTypeIds.BaseEventType, "Severity")]
    public ushort Severity { get; set; }
}