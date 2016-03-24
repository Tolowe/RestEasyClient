using RestEasyClient.Config;
using RestEasyClient.Extensions;

namespace RestEasyClient.Impl
{
    public class JsonSerializer : ISerializer
    {
        public ContentType ContentType
        {
            get
            {
                return ContentType.Json;
            }
        }

        public T Deserialize<T>(string obj)
        {
            return obj.FromJson<T>();
        }

        public string Serialize<T>(T obj)
        {
            return obj.ToJson();
        }
    }
}