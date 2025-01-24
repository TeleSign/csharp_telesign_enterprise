using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using _MessagingClient = Telesign.MessagingClient;

namespace TelesignEnterprise
{
    public class MessagingClient : _MessagingClient
    {
        private const string OMNI_MESSAGING_RESOURCE = "/v1/omnichannel";
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
        /// <summary>
        /// Send a message to the target recipient using any of Telesign's supported channels.
        /// 
        /// See  https://developer.telesign.com/enterprise/reference/sendadvancedmessage for detailed API documentation.
        /// </summary>
        public TelesignResponse OmniMessage(Dictionary<string,object> parameters)
        {
            return Post(OMNI_MESSAGING_RESOURCE, parameters);
        }
        /// <summary>
        /// Send a message to the target recipient using any of Telesign's supported channels.
        /// 
        /// See  https://developer.telesign.com/enterprise/reference/sendadvancedmessage for detailed API documentation.
        /// </summary>
        public Task<TelesignResponse> OmniMessageAsync(Dictionary<string,object> parameters)
        {
            return PostAsync(OMNI_MESSAGING_RESOURCE, parameters);
        }
    }
}