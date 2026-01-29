using System.Collections.Generic;
using System.Threading.Tasks;
using Opc.Ua;
using Opc.Ua.Client;

namespace Diagraph.ResmarkApi.Services;

public class OpcUaNodeExplorerService
{
    private readonly ClientService _clientService;

    public OpcUaNodeExplorerService(ClientService clientService)
    {
        _clientService = clientService;
    }

    public async Task<IList<ReferenceDescription>> BrowseAsync(string ipAddress, int port, NodeId startNodeId)
    {
        using var session = await _clientService.OpenOpcuaSessionAsync("explorer", ipAddress, port).ConfigureAwait(false);

        var browser = new Browser(session)
        {
            BrowseDirection = BrowseDirection.Forward,
            IncludeSubtypes = true,
            NodeClassMask = (int)NodeClass.Object | (int)NodeClass.Variable | (int)NodeClass.Method,
            ResultMask = (uint)BrowseResultMask.All
        };

        var references = browser.Browse(startNodeId);
        session.Close();
        return references;
    }

    public async Task<CallResponse> CallAsync(string ipAddress, int port, CallMethodRequest request)
    {
        using var session = await _clientService.OpenOpcuaSessionAsync("methodExplorer", ipAddress, port).ConfigureAwait(false);
        var methods = new CallMethodRequestCollection { request };
        session.Call(null, methods, out var results, out var diagnosticInfos);

        var response = new CallResponse
        {
            ResponseHeader = new ResponseHeader { ServiceResult = StatusCodes.Good },
            Results = results,
            DiagnosticInfos = diagnosticInfos
        };

        session.Close();
        return response;
    }
}
