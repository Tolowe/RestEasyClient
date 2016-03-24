using RestEasyClient.Config;

namespace RestEasyClient
{
    public interface ISerializer
    {
        ContentType ContentType { get; }

        string Serialize<T>(T obj);

        T Deserialize<T>(string obj);
    }
}