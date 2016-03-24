using RestEasyClient.Config;

namespace RestEasyClient
{
    public interface IGatewayFactory
    {
        ICqrsGateway<T> GetCqrsGateway<T>();

        ICqrsGateway<T> GetCqrsGateway<T>(GatewayConfiguration GatewayConfiguration);
    }
}