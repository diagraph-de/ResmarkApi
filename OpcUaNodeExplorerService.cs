using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workstation.ServiceModel.Ua;
using Workstation.ServiceModel.Ua.Channels;

namespace ResmarkApi.PrinterApi.Shared.Services;

public class OpcUaNodeExplorerService
{
    private readonly ClientService _clientService;

    public OpcUaNodeExplorerService(ClientService clientService)
    {
        _clientService = clientService;
    }
    private static async Task List()
    {
        var clientService = new ClientService();
        var explorerService = new OpcUaNodeExplorerService(clientService);
        var opcUaAddress = "192.168.174.225";

        Console.WriteLine("Auflisten aller Nodes:");
        var nodes = await explorerService.ListAllNodesAsync(opcUaAddress);
        foreach (var node in nodes) Console.WriteLine(node);

        Console.WriteLine("\nGenerieren von Beispielaufrufen für Methoden:");
        await explorerService.GenerateMethodExamplesAsync(opcUaAddress);
    }

    public async Task<List<NodeId>> ListAllNodesAsync(string opcUaAddress)
    {
        var nodes = new List<NodeId>();
        var channel =
            await _clientService.OpenOpcuaChannelAsync("explorer", opcUaAddress,
                ResmarkPrinterService.DefaultOpcuaPort);
        await ExploreNodesRecursively(channel, new NodeId(ObjectIds.ObjectsFolder), nodes);

        await channel.CloseAsync();

        return nodes;
    }

    public async Task GenerateMethodExamplesAsync(string opcUaAddress)
    {
        var nodes = await ListAllNodesAsync(opcUaAddress);

        foreach (var nodeId in nodes)
        {
            var methods = await GetMethodsOfNodeAsync(opcUaAddress, nodeId);

            foreach (var method in methods)
            {
                Console.WriteLine($"Beispielaufruf für Methode {method}:");
                var exampleCall = CreateCallMethodRequest(method, 1);
                Console.WriteLine(exampleCall);
            }
        }
    }

    private async Task ExploreNodesRecursively(ClientSessionChannel channel, NodeId nodeId, List<NodeId> nodes)
    {
        var browseRequest = new BrowseRequest
        {
            NodesToBrowse = new[]
            {
                new BrowseDescription
                {
                    NodeId = nodeId,
                    BrowseDirection = BrowseDirection.Forward,
                    NodeClassMask = (uint)NodeClass.Method,
                    ResultMask = (uint)BrowseResultMask.All
                }
            }
        };

        var browseResponse = await channel.BrowseAsync(browseRequest);

        foreach (var result in browseResponse.Results)
        foreach (var reference in result.References)
        {
            nodes.Add(reference.NodeId.NodeId);
            await ExploreNodesRecursively(channel, reference.NodeId.NodeId, nodes);
        }
    }

    private async Task<List<NodeId>> GetMethodsOfNodeAsync(string opcUaAddress, NodeId nodeId)
    {
        var methods = new List<NodeId>();
        var channel = await _clientService.OpenOpcuaChannelAsync("methodExplorer", opcUaAddress,
            ResmarkPrinterService.DefaultOpcuaPort);

        var browseRequest = new BrowseRequest
        {
            NodesToBrowse = new[]
            {
                new BrowseDescription
                {
                    NodeId = nodeId, BrowseDirection = BrowseDirection.Forward, NodeClassMask = (uint)NodeClass.Method,
                    ResultMask = (uint)BrowseResultMask.All
                }
            }
        };

        var browseResponse = await channel.BrowseAsync(browseRequest);

        foreach (var result in browseResponse.Results)
        foreach (var reference in result.References)
            if (reference.NodeClass == NodeClass.Method)
                methods.Add(reference.NodeId.NodeId);

        await channel.CloseAsync();

        return methods;
    }

    private CallMethodRequest CreateCallMethodRequest(NodeId methodId, params object[] args)
    {
        return new CallMethodRequest
        {
            MethodId = methodId,
            ObjectId = NodeId.Parse(ObjectIds.ObjectsFolder),
            InputArguments = args.Select(arg => new Variant(arg)).ToArray()
        };
    }
}