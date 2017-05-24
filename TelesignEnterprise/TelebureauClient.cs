using System.Collections.Generic;
using System.Net;
using Telesign;

namespace TelesignEnterprise
{
    /// <summary>
    /// TeleBureau is a service is based on TeleSign's watchlist, which is a proprietary database containing verified phone
    /// numbers of users known to have committed online fraud.TeleSign crowd-sources this information from its customers.
    /// Participation is voluntary, but you have to contribute in order to benefit.
    /// </summary>
    public class TelebureauClient : RestClient
    {
        private const string TELEBUREAU_CREATE_RESOURCE = "/v1/telebureau/event";
        private const string TELEBUREAU_RETRIEVE_RESOURCE = "/v1/telebureau/event/{0}";
        private const string TELEBUREAU_DELETE_RESOURCE = "/v1/telebureau/event/{0}";

        public TelebureauClient(string customerId,
                                 string apiKey)
            : base(customerId,
                   apiKey,
                   restEndpoint: "https://rest-ww.telesign.com")
        { }

        public TelebureauClient(string customerId,
                             string apiKey,
                             string restEndpoint)
            : base(customerId,
                   apiKey,
                   restEndpoint)
        { }

        public TelebureauClient(string customerId,
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

        /// <summary>
        /// Creates a telebureau event corresponding to supplied data.
        /// <p>
        /// See https://developer.telesign.com/docs/telebureau-api for detailed API documentation.
        /// </summary>
        public TelesignResponse CreateEvent(string phoneNumber, string fraudType, string occurredAt, Dictionary<string, string> parameters = null)
        {
            if (parameters == null)
            {
                parameters = new Dictionary<string, string>();
            }

            parameters.Add("phone_number", phoneNumber);
            parameters.Add("fraud_type", fraudType);
            parameters.Add("occurred_at", occurredAt);

            return this.Post(TELEBUREAU_CREATE_RESOURCE, parameters);
        }

        /// <summary>
        /// Creates a telebureau event corresponding to supplied data.
        ///
        /// See https://developer.telesign.com/docs/telebureau-api for detailed API documentation.
        /// </summary>
        public TelesignResponse RetrieveEvent(string referenceId, Dictionary<string, string> parameters = null)
        {
            return this.Get(string.Format(TELEBUREAU_RETRIEVE_RESOURCE, referenceId), parameters);
        }

        /// <summary>
        /// Deletes a previously submitted fraud event. You make this call in your web application after completion of the
        /// create transaction for a telebureau event.
        ///
        /// See https://developer.telesign.com/docs/telebureau-api for detailed API documentation.
        /// </summary>>
        public TelesignResponse DeleteEvent(string referenceId, Dictionary<string, string> parameters = null)
        {
            return this.Delete(string.Format(TELEBUREAU_DELETE_RESOURCE, referenceId), parameters);
        }
    }
}
