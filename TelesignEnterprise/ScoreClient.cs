using System.Net;
using System.Reflection;
using _ScoreClient = Telesign.ScoreClient;

namespace TelesignEnterprise
{
    public class ScoreClient : _ScoreClient
    {
        public ScoreClient(string customerId,
            string apiKey)
            : base(customerId,
                apiKey,
                "https://rest-ww.telesign.com",
                "csharp_telesign_enterprise",
                Assembly.GetAssembly(typeof(ScoreClient)).GetName().Version.ToString(),
                Assembly.GetAssembly(typeof(_ScoreClient)).GetName().Version.ToString())
        { }

        public ScoreClient(string customerId,
            string apiKey,
            string restEndpoint)
            : base(customerId,
                apiKey,
                restEndpoint,
                "csharp_telesign_enterprise",
                Assembly.GetAssembly(typeof(ScoreClient)).GetName().Version.ToString(),
                Assembly.GetAssembly(typeof(_ScoreClient)).GetName().Version.ToString())
        { }

        public ScoreClient(string customerId,
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
                Assembly.GetAssembly(typeof(ScoreClient)).GetName().Version.ToString(),
                Assembly.GetAssembly(typeof(_ScoreClient)).GetName().Version.ToString())
        { }
    }
}