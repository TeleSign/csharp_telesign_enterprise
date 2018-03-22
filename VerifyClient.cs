using System.Collections.Generic;
using System.Net;
using Telesign;

namespace TelesignEnterprise
{
    
    public class VerifyClient : RestClient, IVerifyClient
    {
        private const string VERIFY_SMS_RESOURCE = "/v1/verify/sms";
        private const string VERIFY_VOICE_RESOURCE = "/v1/verify/call";
        private const string VERIFY_SMART_RESOURCE = "/v1/verify/smart";
        private const string VERIFY_PUSH_RESOURCE = "/v2/verify/push";
        private const string VERIFY_STATUS_RESOURCE = "/v1/verify/{0}";
        private const string VERIFY_COMPLETION_RESOURCE = "/v1/verify/completion/{0}";

        public VerifyClient(string customerId,
                            string apiKey)
                : base(customerId,
                       apiKey,
                       restEndpoint: "https://rest-ww.telesign.com")
        { }

        public VerifyClient(string customerId,
                             string apiKey,
                             string restEndpoint)
            : base(customerId,
                   apiKey,
                   restEndpoint)
        { }

        public VerifyClient(string customerId,
                             string apiKey,
                             string restEndpoint,
                             int timeout,
                             WebProxy proxy,
                             string proxyUsername,
                             string proxyPassword)
            : base(customerId,
                   apiKey,
                   restEndpoint: restEndpoint,
                   timeout: timeout,
                   proxy: proxy,
                   proxyUsername: proxyUsername,
                   proxyPassword: proxyPassword)
        { }
        
        public TelesignResponse Sms(string phoneNumber, Dictionary<string, string> parameters = null)
        {
            if (parameters == null)
            {
                parameters = new Dictionary<string, string>();
            }

            parameters.Add("phone_number", phoneNumber);

            return this.Post(VERIFY_SMS_RESOURCE, parameters);
        }
        
        public TelesignResponse Voice(string phoneNumber, Dictionary<string, string> parameters = null)
        {
            if (parameters == null)
            {
                parameters = new Dictionary<string, string>();
            }

            parameters.Add("phone_number", phoneNumber);

            return this.Post(VERIFY_VOICE_RESOURCE, parameters);
        }

        public TelesignResponse Smart(string phoneNumber, string ucid, Dictionary<string, string> parameters = null)
        {
            if (parameters == null)
            {
                parameters = new Dictionary<string, string>();
            }

            parameters.Add("phone_number", phoneNumber);
            parameters.Add("ucid", ucid);

            return this.Post(VERIFY_SMART_RESOURCE, parameters);
        }
        
        public TelesignResponse Push(string phoneNumber, string ucid, Dictionary<string, string> parameters = null)
        {
            if (parameters == null)
            {
                parameters = new Dictionary<string, string>();
            }

            parameters.Add("phone_number", phoneNumber);
            parameters.Add("ucid", ucid);

            return this.Post(VERIFY_PUSH_RESOURCE, parameters);
        }
        
        public TelesignResponse Status(string referenceId, Dictionary<string, string> parameters = null)
        {
            return this.Get(string.Format(VERIFY_STATUS_RESOURCE, referenceId), parameters);
        }
        
        public TelesignResponse Completion(string referenceId, Dictionary<string, string> parameters = null)
        {
            return this.Put(string.Format(VERIFY_COMPLETION_RESOURCE, referenceId), parameters);
        }
    }
}
