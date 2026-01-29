using System;
using System.Threading.Tasks;
using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Configuration;

namespace Diagraph.ResmarkApi.Services;

public class ClientService
{
    private readonly ApplicationConfiguration _config;

    public ClientService()
    {
        _config = CreateConfiguration();
    }

    public async Task<Session> OpenOpcuaSessionAsync(string printerId, string ipAddress, int port)
    {
        var endpointUrl = $"opc.tcp://{ipAddress}:{port}";
        var uri = new Uri(endpointUrl);

        var endpointConfiguration = EndpointConfiguration.Create(_config);
        endpointConfiguration.OperationTimeout = 15000;

        using (var discovery = DiscoveryClient.Create(uri, endpointConfiguration))
        {
            var endpoints = await discovery.GetEndpointsAsync(null).ConfigureAwait(false);
            var selectedEndpoint = CoreClientUtils.SelectEndpoint(_config, uri.ToString(), false, 15000);


            var endpointConfig = EndpointConfiguration.Create(_config);
            var endpoint = new ConfiguredEndpoint(null, selectedEndpoint, endpointConfig);

            var session = await Session.Create(
                _config,
                endpoint,
                false,
                $"ResmarkApi:{printerId}",
                60000,
                new UserIdentity(new AnonymousIdentityToken()),
                null
            ).ConfigureAwait(false);

            return session;
        }

    }

    private static ApplicationConfiguration CreateConfiguration()
    {
        var config = new ApplicationConfiguration
        {
            ApplicationName = "ResmarkApi",
            ApplicationType = ApplicationType.Client,
            ApplicationUri = "urn:ResmarkApi",
            SecurityConfiguration = new SecurityConfiguration
            {
                ApplicationCertificate = new CertificateIdentifier(),
                AutoAcceptUntrustedCertificates = true,
                RejectSHA1SignedCertificates = false,
                AddAppCertToTrustedStore = false
            },
            TransportQuotas = new TransportQuotas
            {
                OperationTimeout = 60000,
                SecurityTokenLifetime = 3600000
            },
            ClientConfiguration = new ClientConfiguration
            {
                DefaultSessionTimeout = 60000
            }
        };

        config.Validate(ApplicationType.Client).GetAwaiter().GetResult();
        config.CertificateValidator.CertificateValidation += (_, e) => { e.Accept = true; };
        return config;
    }
}
