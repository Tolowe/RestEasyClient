using System;

namespace RestEasyClient.Impl
{
    public class GatewayFactory : IGatewayFactory
    {
        private readonly IRestHttpClient _restHttpClient;

        public GatewayFactory(string ProtocolAndDomain)
        {
            _restHttpClient = new RestHttpClient();
            var baseUri = (ProtocolAndDomain.EndsWith(@"/") ? ProtocolAndDomain : ProtocolAndDomain + @"/");
            _restHttpClient.BaseAddress = new Uri(baseUri);
        }

        public ICqrsGateway<T> GetCqrsGateway<T>()
        {
            return new CqrsGateway<T>(_restHttpClient);
        }
    }
}