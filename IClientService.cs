using System.Threading.Tasks;
using Workstation.ServiceModel.Ua.Channels;

namespace Diagraph.PCD.Opcua.Interfaces;

public interface IClientService
{
    Task<ClientSessionChannel> OpenOpcuaChannelAsync(string channelId, string ipAddress, int port);
}