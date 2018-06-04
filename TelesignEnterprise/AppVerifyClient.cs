using System.Net;
using _AppVerifyClient = Telesign.AppVerifyClient;

namespace TelesignEnterprise
{
    public class AppVerifyClient : _AppVerifyClient
    {
        public AppVerifyClient(string customerId,
            string apiKey)
            : base(customerId,
                apiKey,
                "https://rest-ww.telesign.com")
        { }

        public AppVerifyClient(string customerId,
            string apiKey,
            string restEndpoint)
            : base(customerId,
                apiKey,
                restEndpoint)
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
                proxyPassword: proxyPassword)
        { }
    }
}