using RestEasyClient.Config;
using System;
using System.Net.Http;

namespace RestEasyClient.Impl
{
    public class GatewayFactory : IGatewayFactory
    {
        private readonly HttpClient _httpClient;
        private readonly GatewayConfiguration _gatewayConfiguration;
        private readonly ISerializer _serializer;

        public GatewayFactory(string ProtocolAndDomain)
            : this(new GatewayConfiguration(ProtocolAndDomain))
        { }

        public GatewayFactory(GatewayConfiguration GatewayConfiguration)
        {
            CheckConfigurationForErrors(GatewayConfiguration);
            _gatewayConfiguration = GatewayConfiguration;
            _httpClient = GetHttpClient(_gatewayConfiguration);
            _serializer = GetSerializer(_gatewayConfiguration.ContentType);
        }

        private HttpClient GetHttpClient(GatewayConfiguration GatewayConfiguration)
        {
            var httpClient = new HttpClient();
            var baseUri = (GatewayConfiguration.ProtocolAndDomain.EndsWith(@"/") ? GatewayConfiguration.ProtocolAndDomain : GatewayConfiguration.ProtocolAndDomain + @"/");
            httpClient.BaseAddress = new Uri(baseUri);
            return httpClient;
        }

        private ISerializer GetSerializer(ContentType ContentType)
        {
            ISerializer serializer;
            switch (ContentType)
            {
                case ContentType.Json:
                    serializer = new JsonSerializer();
                    break;

                case ContentType.Xml:
                    serializer = new XmlSerializer();
                    break;

                default:
                    serializer = new JsonSerializer();
                    break;
            };
            return serializer;
        }

        private void CheckConfigurationForErrors(GatewayConfiguration GatewayConfiguration)
        {
            if (GatewayConfiguration == null) throw new ArgumentNullException("Configuration can't be null!");
        }

        public ICqrsGateway<T> GetCqrsGateway<T>()
        {
            return GetCqrsGateway<T>(_gatewayConfiguration);
        }

        public ICqrsGateway<T> GetCqrsGateway<T>(GatewayConfiguration GatewayConfiguration)
        {
            if (ReferenceEquals(GatewayConfiguration, _gatewayConfiguration))
            {
                return new CqrsGateway<T>(_httpClient, _serializer);
            }
            CheckConfigurationForErrors(GatewayConfiguration);
            var httpClient = GetHttpClient(GatewayConfiguration);
            var serializer = GetSerializer(GatewayConfiguration.ContentType);
            return new CqrsGateway<T>(httpClient, serializer);
        }
    }
}