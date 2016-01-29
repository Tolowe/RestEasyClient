namespace RestEasyClient.Impl
{
    public class GatewayFactory : IGatewayFactory
    {
        private readonly string _protocolAndDomain;

        public GatewayFactory(string ProtocolAndDomain)
        {
            _protocolAndDomain = ProtocolAndDomain;
        }

        public ICqrsGateway<T> GetCqrsGateway<T>()
        {
            return new CqrsGateway<T>(_protocolAndDomain);
        }
    }
}