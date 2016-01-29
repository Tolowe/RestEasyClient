namespace RestEasyClient
{
    public interface IGatewayFactory
    {
        ICqrsGateway<T> GetCqrsGateway<T>();
    }
}