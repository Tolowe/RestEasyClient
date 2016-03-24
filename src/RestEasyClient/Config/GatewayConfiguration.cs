using System;

namespace RestEasyClient.Config
{
    public class GatewayConfiguration
    {
        public GatewayConfiguration(string protocolAndDomain)
        {
            if (protocolAndDomain == string.Empty) throw new ArgumentNullException("You must provide a base url!");
            ProtocolAndDomain = protocolAndDomain;
            ContentType = ContentType.Json;
        }

        public ContentType ContentType { get; set; }
        public string ProtocolAndDomain { get; set; }
    }
}