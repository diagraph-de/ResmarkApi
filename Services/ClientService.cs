using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Workstation.ServiceModel.Ua;
using Workstation.ServiceModel.Ua.Channels;
using ApplicationDescription = Workstation.ServiceModel.Ua.ApplicationDescription;
using ApplicationType = Workstation.ServiceModel.Ua.ApplicationType;

namespace Diagraph.ResmarkApi.Services;

public class ClientService
{
    private readonly ApplicationDescription _clientDescription;

    public ClientService()
    {
        var name = Assembly.GetExecutingAssembly().GetName().Name;
        _clientDescription = new ApplicationDescription
        {
            ApplicationName = name,
            ApplicationUri = "urn:" + Dns.GetHostName() + ":" + name,
            ApplicationType = ApplicationType.Client
        };
    }

    public async Task<ClientSessionChannel> OpenOpcuaChannelAsync(string channelId, string ipAddress, int port)
    {
        var channel = CreateChannel(channelId, ipAddress, port);
        await channel.OpenAsync();
        return channel;
    }

    private ClientSessionChannel CreateChannel(string channelId, string ipAddress, int opcuaPort,
        uint timeout = 10000u)
    {
        var channel = new ClientSessionChannel(_clientDescription, null, new AnonymousIdentity(),
            $"opc.tcp://{ipAddress}:{opcuaPort}", null, null, new ClientSessionChannelOptions
            {
                TimeoutHint = timeout
            });
        return channel;
    }
}