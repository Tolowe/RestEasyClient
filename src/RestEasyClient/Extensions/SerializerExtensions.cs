using Newtonsoft.Json;
using System.IO;
using System.Xml.Serialization;

namespace RestEasyClient.Extensions
{
    internal static class SerializerExtensions
    {
        internal static string ToXml<T>(this T obj)
        {
            if (obj == null) return string.Empty;
            using (var stringwriter = new StringWriter())
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stringwriter, obj);
                return stringwriter.ToString();
            }
        }

        internal static T FromXml<T>(this string obj)
        {
            if (obj == string.Empty) return default(T);
            using (var stringReader = new StringReader(obj))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stringReader);
            }
        }

        internal static string ToJson<T>(this T obj)
        {
            if (obj == null) return string.Empty;
            return JsonConvert.SerializeObject(obj);
        }

        internal static T FromJson<T>(this string obj)
        {
            if (obj == string.Empty) return default(T);
            return JsonConvert.DeserializeObject<T>(obj);
        }
    }
}