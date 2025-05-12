using System.Net;
using System.Reflection;
using _VoiceClient = Telesign.VoiceClient;

namespace TelesignEnterprise
{
    public class VoiceClient : _VoiceClient
    {
        public VoiceClient(string customerId,
            string apiKey)
            : base(customerId,
                apiKey,
                "https://rest-ww.telesign.com",
                "csharp_telesign_enterprise",
                Assembly.GetAssembly(typeof(VoiceClient)).GetName().Version.ToString(),
                Assembly.GetAssembly(typeof(_VoiceClient)).GetName().Version.ToString())
        { }

        public VoiceClient(string customerId,
            string apiKey,
            string restEndpoint)
            : base(customerId,
                apiKey,
                restEndpoint,
                "csharp_telesign_enterprise",
                Assembly.GetAssembly(typeof(VoiceClient)).GetName().Version.ToString(),
                Assembly.GetAssembly(typeof(_VoiceClient)).GetName().Version.ToString())
        { }

        public VoiceClient(string customerId,
            string apiKey,
            string restEndpoint,
            int timeout,
            WebProxy proxy,
            string proxyUsername,
            string proxyPassword)
            : base(customerId,
                apiKey,
                restEndpoint,
                timeout: timeout,
                proxy: proxy,
                proxyUsername: proxyUsername,
                proxyPassword: proxyPassword,
                "csharp_telesign_enterprise",
                Assembly.GetAssembly(typeof(VoiceClient)).GetName().Version.ToString(),
                Assembly.GetAssembly(typeof(_VoiceClient)).GetName().Version.ToString())
        { }

    }
}