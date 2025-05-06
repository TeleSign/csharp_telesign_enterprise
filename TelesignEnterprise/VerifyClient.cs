using System.Collections.Generic;
using System.Net;
using System.Reflection;
using Telesign;

namespace TelesignEnterprise
{
    /// <summary>
    /// The Verify API delivers phone-based verification and two-factor authentication using a time-based, one-time passcode
    /// sent via SMS message or Voice call Notification.
    /// </summary>
    public class VerifyClient : RestClient
    {

        private const string VERIFY_SMS_RESOURCE = "/v1/verify/sms";
        private const string VERIFY_VOICE_RESOURCE = "/v1/verify/call";
        private const string VERIFY_SMART_RESOURCE = "/v1/verify/smart";
        private const string VERIFY_STATUS_RESOURCE = "/v1/verify/{0}";
        private const string VERIFY_COMPLETION_RESOURCE = "/v1/verify/completion/{0}";

        public VerifyClient(string customerId,
                            string apiKey)
                : base(customerId,
                   apiKey,
                   restEndpoint: "https://rest-ww.telesign.com",
                   source: "csharp_telesign_enterprise",
                   sdkVersionOrigin: Assembly.GetAssembly(typeof(VerifyClient)).GetName().Version.ToString(),
                   sdkVersionDependency: Assembly.GetAssembly(typeof(RestClient)).GetName().Version.ToString())
        { }

        public VerifyClient(string customerId,
                             string apiKey,
                             string restEndpoint)
            : base(customerId,
                   apiKey,
                   restEndpoint,
                   source: "csharp_telesign_enterprise",
                   sdkVersionOrigin: Assembly.GetAssembly(typeof(VerifyClient)).GetName().Version.ToString(),
                   sdkVersionDependency: Assembly.GetAssembly(typeof(RestClient)).GetName().Version.ToString())
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
                   proxyPassword: proxyPassword,
                   source: "csharp_telesign_enterprise",
                   sdkVersionOrigin: Assembly.GetAssembly(typeof(VerifyClient)).GetName().Version.ToString(),
                   sdkVersionDependency: Assembly.GetAssembly(typeof(RestClient)).GetName().Version.ToString())
        { }

        /// <summary>
        /// The SMS Verify API delivers phone-based verification and two-factor authentication using a time-based,
        /// one-time passcode sent over SMS.
        /// 
        /// See https://developer.telesign.com/docs/rest_api-verify-sms for detailed API documentation.
        /// </summary>
        public TelesignResponse Sms(string phoneNumber, Dictionary<string, string> parameters = null)
        {
            if (parameters == null)
            {
                parameters = new Dictionary<string, string>();
            }

            parameters.Add("phone_number", phoneNumber);

            return this.Post(VERIFY_SMS_RESOURCE, parameters);
        }

        /// <summary>
        /// The Voice Verify API delivers patented phone-based verification and two-factor authentication using a one-time
        /// passcode sent over verify_voice message.
        /// 
        /// See https://developer.telesign.com/docs/rest_api-verify-call for detailed API documentation.
        /// </summary>
        public TelesignResponse Voice(string phoneNumber, Dictionary<string, string> parameters = null)
        {
            if (parameters == null)
            {
                parameters = new Dictionary<string, string>();
            }

            parameters.Add("phone_number", phoneNumber);

            return this.Post(VERIFY_VOICE_RESOURCE, parameters);
        }

        /// <summary>
        /// The Smart Verify web service simplifies the process of verifying user identity by integrating several TeleSign
        /// web services into a single API call. This eliminates the need for you to make multiple calls to the TeleSign
        /// Verify resource.
        /// 
        /// See https://developer.telesign.com/docs/rest_api-smart-verify for detailed API documentation.
        /// </summary>
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

        /// <summary>
        /// Retrieves the verification result for any verify resource.
        /// 
        /// See https://developer.telesign.com/docs/rest_api-verify-transaction-callback for detailed API documentation.
        /// </summary>
        public TelesignResponse Status(string referenceId, Dictionary<string, string> parameters = null)
        {
            return this.Get(string.Format(VERIFY_STATUS_RESOURCE, referenceId), parameters);
        }

        /// <summary>
        /// Notifies TeleSign that a verification was successfully delivered to the user in order to help improve the
        /// quality of message delivery routes.
        /// 
        /// See https://developer.telesign.com/docs/completion-service-for-verify-products for detailed API documentation.
        /// </summary>
        public TelesignResponse Completion(string referenceId, Dictionary<string, string> parameters = null)
        {
            return this.Put(string.Format(VERIFY_COMPLETION_RESOURCE, referenceId), parameters);
        }
        public TelesignResponse CreateVerificationProcess(string phoneNumber, Dictionary<string, object> parameters = null)
        {
            OmniVerifyClient verifyClientNew = new OmniVerifyClient(this.customerId, this.apiKey);
            return verifyClientNew.CreateVerificationProcess(phoneNumber, parameters);
        }
    }

    internal class OmniVerifyClient : RestClient
    {
        private const string VERIFY_OMNICHANNEL_RESOURCE = "/verification";
        public OmniVerifyClient(string customerId,
                            string apiKey)
                : base(customerId,
                       apiKey,
                       restEndpoint: "https://verify.telesign.com",
                   source: "csharp_telesign_enterprise",
                   sdkVersionOrigin: Assembly.GetAssembly(typeof(VerifyClient)).GetName().Version.ToString(),
                   sdkVersionDependency: Assembly.GetAssembly(typeof(RestClient)).GetName().Version.ToString())
        { }

        public OmniVerifyClient(string customerId,
                             string apiKey,
                             string restEndpoint)
            : base(customerId,
                   apiKey,
                   restEndpoint,
                   source: "csharp_telesign_enterprise",
                   sdkVersionOrigin: Assembly.GetAssembly(typeof(VerifyClient)).GetName().Version.ToString(),
                   sdkVersionDependency: Assembly.GetAssembly(typeof(RestClient)).GetName().Version.ToString())
        { }

        public OmniVerifyClient(string customerId,
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
                   proxyPassword: proxyPassword,
                   source: "csharp_telesign_enterprise",
                   sdkVersionOrigin: Assembly.GetAssembly(typeof(VerifyClient)).GetName().Version.ToString(),
                   sdkVersionDependency: Assembly.GetAssembly(typeof(RestClient)).GetName().Version.ToString())
        { }
        public TelesignResponse CreateVerificationProcess(string phoneNumber, Dictionary<string, object> parameters = null)
        {
            if (parameters == null)
                parameters = new Dictionary<string, object>();

            parameters.Add("recipient", new Dictionary<string, string>
            {
                { "phone_number", phoneNumber }
            });

            if (!parameters.ContainsKey("verification_policy"))
                parameters.Add("verification_policy", new List<Dictionary<string, object>>()
                {
                    new Dictionary<string, object>
                    {
                        { "method", "sms" },
                        { "fallback_time", 30 }
                    }
                });
            return this.Post(VERIFY_OMNICHANNEL_RESOURCE, parameters);
        }
    }
}