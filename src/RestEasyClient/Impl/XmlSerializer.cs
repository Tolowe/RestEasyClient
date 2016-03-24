using RestEasyClient.Config;
using RestEasyClient.Extensions;

namespace RestEasyClient.Impl
{
    public class XmlSerializer : ISerializer
    {
        public ContentType ContentType
        {
            get
            {
                return ContentType.Xml;
            }
        }

        public T Deserialize<T>(string obj)
        {
            return obj.FromXml<T>();
        }

        public string Serialize<T>(T obj)
        {
            return obj.ToXml();
        }
    }
}