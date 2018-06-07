using System.Collections.Generic;
using System.Net;
using _PhoneIdClient = Telesign.PhoneIdClient;

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
        private const string PHONEID_CONTACT_RESOURCE = "/v1/phoneid/contact/{0}";
        private const string PHONEID_LIVE_RESOURCE = "/v1/phoneid/live/{0}";
        private const string PHONEID_NUMBER_DEACTIVATION_RESOURCE = "/v1/phoneid/number_deactivation/{0}";

        public PhoneIdClient(string customerId,
                             string apiKey)
            : base(customerId,
                   apiKey,
                   "https://rest-ww.telesign.com")
        { }

        public PhoneIdClient(string customerId,
                             string apiKey,
                             string restEndpoint)
            : base(customerId,
                   apiKey,
                   restEndpoint)
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
                   proxyPassword: proxyPassword)
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
        /// The PhoneID Contact API delivers contact information related to the subscriber's phone number to provide another
        /// set of indicators for established risk engines.
        ///
        /// See https://developer.telesign.com/docs/rest_api-phoneid-contact for detailed API documentation.
        /// </summary>
        public TelesignResponse Contact(string phoneNumber, string ucid, Dictionary<string, string> parameters = null)
        {
            if (parameters == null)
            {
                parameters = new Dictionary<string, string>();
            }

            parameters.Add("ucid", ucid);

            return this.Get(string.Format(PHONEID_CONTACT_RESOURCE, phoneNumber), parameters);
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
        /// The PhoneID Number Deactivation API determines whether a phone number has been deactivated and when, based on
        /// carriers' phone number data and TeleSign's proprietary analysis.
        ///
        /// See https://developer.telesign.com/docs/rest_api-phoneid-number-deactivation for detailed API documentation.
        /// </summary>
        public TelesignResponse NumberDeactivation(string phoneNumber, string ucid, Dictionary<string, string> parameters = null)
        {
            if (parameters == null)
            {
                parameters = new Dictionary<string, string>();
            }

            parameters.Add("ucid", ucid);

            return this.Get(string.Format(PHONEID_NUMBER_DEACTIVATION_RESOURCE, phoneNumber), parameters);
        }
    }
}
