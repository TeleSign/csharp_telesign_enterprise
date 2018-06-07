using System.Net;
using _MessagingClient = Telesign.MessagingClient;

namespace TelesignEnterprise
{
    public class MessagingClient : _MessagingClient
    {
        public MessagingClient(string customerId,
            string apiKey)
            : base(customerId,
                apiKey,
                "https://rest-ww.telesign.com")
        { }

        public MessagingClient(string customerId,
            string apiKey,
            string restEndpoint)
            : base(customerId,
                apiKey,
                restEndpoint)
        { }

        public MessagingClient(string customerId,
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