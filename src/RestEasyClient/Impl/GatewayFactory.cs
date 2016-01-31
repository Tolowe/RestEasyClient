using System;
using System.Net.Http;

namespace RestEasyClient.Impl
{
    public class GatewayFactory : IGatewayFactory
    {
        private readonly HttpClient _httpClient;

        public GatewayFactory(string ProtocolAndDomain)
        {
            _httpClient = new HttpClient();
            var baseUri = (ProtocolAndDomain.EndsWith(@"/") ? ProtocolAndDomain : ProtocolAndDomain + @"/");
            _httpClient.BaseAddress = new Uri(baseUri);
        }

        public ICqrsGateway<T> GetCqrsGateway<T>()
        {
            return new CqrsGateway<T>(_httpClient);
        }
    }
}