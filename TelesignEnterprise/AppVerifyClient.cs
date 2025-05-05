using System.Net;
using System.Reflection;
using _AppVerifyClient = Telesign.AppVerifyClient;

namespace TelesignEnterprise
{
    public class AppVerifyClient : _AppVerifyClient
    {
        public AppVerifyClient(string customerId,
            string apiKey)
            : base(customerId,
                apiKey,
                "https://rest-ww.telesign.com",
                "csharp_telesign_enterprise",
                Assembly.GetAssembly(typeof(AppVerifyClient)).GetName().Version.ToString(),
                Assembly.GetAssembly(typeof(_AppVerifyClient)).GetName().Version.ToString())
        { }

        public AppVerifyClient(string customerId,
            string apiKey,
            string restEndpoint)
            : base(customerId,
                apiKey,
                restEndpoint,
                "csharp_telesign_enterprise",
                Assembly.GetAssembly(typeof(AppVerifyClient)).GetName().Version.ToString(),
                Assembly.GetAssembly(typeof(_AppVerifyClient)).GetName().Version.ToString())
        { }

        public AppVerifyClient(string customerId,
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
                Assembly.GetAssembly(typeof(AppVerifyClient)).GetName().Version.ToString(),
                Assembly.GetAssembly(typeof(_AppVerifyClient)).GetName().Version.ToString())
        { }
    }
}