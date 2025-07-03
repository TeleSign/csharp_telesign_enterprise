using System.Collections.Generic;
using System.Net;
using System.Reflection;
using _PhoneIdClient = Telesign.PhoneIdClient;
using System;
using System.Threading.Tasks;

namespace TelesignEnterprise
{
    /// <summary>
    /// PhoneID is a set of REST APIs that deliver deep phone number data attributes that help optimize the end user
    /// verification process and evaluate risk.
    /// 
    /// TeleSign PhoneID provides a wide range of risk assessment indicators on the number to help confirm user identity,
    /// delivering real-time decision making throughout the number lifecycle and ensuring only legitimate users are
    /// creating accounts and accessing your applications.
    /// </summary>
    public class PhoneIdClient : _PhoneIdClient
    {
        private const string PHONEID_STANDARD_RESOURCE = "/v1/phoneid/standard/{0}";
        private const string PHONEID_SCORE_RESOURCE = "/v1/phoneid/score/{0}";
        private const string PHONEID_LIVE_RESOURCE = "/v1/phoneid/live/{0}";
        private const string PHONEID_PATH_RESOURCE = "/v1/phoneid/{0}";
        private const string PHONEID_PAYLOAD_RESOURCE = "/v1/phoneid";

        public PhoneIdClient(string customerId,
                             string apiKey)
            : base(customerId,
                   apiKey,
                   "https://rest-ww.telesign.com",
                "csharp_telesign_enterprise",
                Assembly.GetAssembly(typeof(PhoneIdClient)).GetName().Version.ToString(),
                Assembly.GetAssembly(typeof(_PhoneIdClient)).GetName().Version.ToString())
        { }

        public PhoneIdClient(string customerId,
                             string apiKey,
                             string restEndpoint)
            : base(customerId,
                   apiKey,
                   restEndpoint,
                "csharp_telesign_enterprise",
                Assembly.GetAssembly(typeof(PhoneIdClient)).GetName().Version.ToString(),
                Assembly.GetAssembly(typeof(_PhoneIdClient)).GetName().Version.ToString())
        { }

        public PhoneIdClient(string customerId,
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
                Assembly.GetAssembly(typeof(PhoneIdClient)).GetName().Version.ToString(),
                Assembly.GetAssembly(typeof(_PhoneIdClient)).GetName().Version.ToString())
        { }

        /// <summary>
        /// PhoneID is a set of REST APIs that deliver deep phone number data attributes that help optimize the end user
        /// verification process and evaluate risk.
        ///
        /// TeleSign PhoneID provides a wide range of risk assessment indicators on the number to help confirm user identity,
        /// delivering real-time decision making throughout the number lifecycle and ensuring only legitimate users are
        /// creating accounts and accessing your applications.
        /// </summary>
        public TelesignResponse Standard(string phoneNumber, Dictionary<string, string> parameters = null)
        {
            return this.Get(string.Format(PHONEID_STANDARD_RESOURCE, phoneNumber), parameters);
        }

        /// <summary>
        /// Score is an API that delivers reputation scoring based on phone number intelligence, traffic patterns, machine
        /// learning, and a global data consortium.
        ///
        /// See https://developer.telesign.com/docs/rest_api-phoneid-score for detailed API documentation.
        /// </summary>
        public TelesignResponse Score(string phoneNumber, string ucid, Dictionary<string, string> parameters = null)
        {
            if (parameters == null)
            {
                parameters = new Dictionary<string, string>();
            }

            parameters.Add("ucid", ucid);

            return this.Get(string.Format(PHONEID_SCORE_RESOURCE, phoneNumber), parameters);
        }

        /// <summary>
        /// The PhoneID Live API delivers insights such as whether a phone is active or disconnected, a device is reachable
        /// or unreachable and its roaming status.
        ///
        /// See https://developer.telesign.com/docs/rest_api-phoneid-live for detailed API documentation.
        /// </summary>
        public TelesignResponse Live(string phoneNumber, string ucid, Dictionary<string, string> parameters = null)
        {
            if (parameters == null)
            {
                parameters = new Dictionary<string, string>();
            }

            parameters.Add("ucid", ucid);

            return this.Get(string.Format(PHONEID_LIVE_RESOURCE, phoneNumber), parameters);
        }

        /// <summary>
        /// Returns detailed information about a phone number, including its carrier, location, and more, by specifying the phone number in the path.
        /// 
        /// See https://developer.telesign.com/enterprise/reference/submitphonenumberforidentity for detailed API documentation.  
        /// </summary>
        public TelesignResponse PhoneIdPath(string phoneNumber, Dictionary<string, object> parameters = null)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                throw new ArgumentNullException(nameof(phoneNumber));

            if (parameters == null)
                parameters = new Dictionary<string, object>();

            if (!parameters.ContainsKey("consent"))
                parameters["consent"] = new Dictionary<string, int> { { "method", 1 } };

            string resourcePath = string.Format(PHONEID_PATH_RESOURCE, phoneNumber);

            return this.Post(resourcePath, parameters);
        }

        /// <summary>
        /// Returns detailed information about a phone number asynchronously, including its carrier, location, and more, by specifying the phone number in the path.
        /// 
        /// See https://developer.telesign.com/enterprise/reference/submitphonenumberforidentity for detailed API documentation.  
        /// </summary>
        public async Task<TelesignResponse> PhoneIdPathAsync(string phoneNumber, Dictionary<string, object> parameters = null)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                throw new ArgumentNullException(nameof(phoneNumber));

            if (parameters == null)
                parameters = new Dictionary<string, object>();

            if (!parameters.ContainsKey("consent"))
                parameters["consent"] = new Dictionary<string, int> { { "method", 1 } };

            string resourcePath = string.Format(PHONEID_PATH_RESOURCE, phoneNumber);

            return await this.PostAsync(resourcePath, parameters).ConfigureAwait(false);
        }


        /// <summary>
        /// Returns detailed information about a phone number, including its carrier, location, and more, by providing the phone number in the request body.
        /// 
        /// See https://developer.telesign.com/enterprise/reference/submitphonenumberforidentityalt for detailed API documentation.  
        /// </summary>
        public TelesignResponse PhoneIdBody(string phoneNumber, Dictionary<string, object> parameters = null)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                throw new ArgumentNullException(nameof(phoneNumber));

            if (parameters == null)
                parameters = new Dictionary<string, object>();

            parameters["phone_number"] = phoneNumber;

            if (!parameters.ContainsKey("consent"))
            {
                parameters["consent"] = new Dictionary<string, int> { { "method", 1 } };
            }

            return this.Post(PHONEID_PAYLOAD_RESOURCE, parameters);
        }

        /// <summary>
        /// Returns detailed information about a phone number asynchronously, including its carrier, location, and more, by providing the phone number in the request body.
        /// 
        /// See https://developer.telesign.com/enterprise/reference/submitphonenumberforidentityalt for detailed API documentation.  
        /// </summary>
        public async Task<TelesignResponse> PhoneIdBodyAsync(string phoneNumber, Dictionary<string, object> parameters = null)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                throw new ArgumentNullException(nameof(phoneNumber));

            if (parameters == null)
                parameters = new Dictionary<string, object>();

            parameters["phone_number"] = phoneNumber;

            if (!parameters.ContainsKey("consent"))
            {
                parameters["consent"] = new Dictionary<string, int> { { "method", 1 } };
            }

            return await this.PostAsync(PHONEID_PAYLOAD_RESOURCE, parameters).ConfigureAwait(false);
        }
    }
}
