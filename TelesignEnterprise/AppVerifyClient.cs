using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Telesign;
using _AppVerifyClient = Telesign.AppVerifyClient;

namespace TelesignEnterprise
{
    public class AppVerifyClient : _AppVerifyClient
    {
        private const string APP_VERIFY_BASE_RESOURCE = "/v1/verify/auto/voice";
        private const string INITIATE_RESOURCE = APP_VERIFY_BASE_RESOURCE + "/initiate";
        private const string FINALIZE_RESOURCE = APP_VERIFY_BASE_RESOURCE + "/finalize";
        private const string FINALIZE_CALLERID_RESOURCE = APP_VERIFY_BASE_RESOURCE + "/finalize/callerid";
        private const string FINALIZE_TIMEOUT_RESOURCE = APP_VERIFY_BASE_RESOURCE + "/finalize/timeout";
        private const string TRANSACTION_STATUS_RESOURCE = APP_VERIFY_BASE_RESOURCE; // + "/{reference_id}" appended dynamically

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

        /// <summary>
        /// Initiates the verification process by sending a voice call with a verification code to the specified phone number.
        /// </summary>
        /// <param name="phoneNumber">The phone number to verify</param>
        /// <returns>A TelesignResponse containing the API response</returns>
        public RestClient.TelesignResponse Initiate(string phoneNumber)
        {
            var parameters = new Dictionary<string, string>
            {
                { "phone_number", phoneNumber }
            };
            return Post(INITIATE_RESOURCE, parameters);
        }

        /// <summary>
        /// Asynchronously initiates the verification process by sending a voice call with a verification code to the specified phone number.
        /// </summary>
        /// <param name="phoneNumber">The phone number to verify</param>
        /// <returns>A Task representing the asynchronous operation, with a TelesignResponse containing the API response.</returns>
        public Task<RestClient.TelesignResponse> InitiateAsync(string phoneNumber)
        {
            var parameters = new Dictionary<string, string>
            {
                { "phone_number", phoneNumber }
            };
            return PostAsync(INITIATE_RESOURCE, parameters);
        }

        /// <summary>
        /// Finalizes the verification by submitting the verification code and the reference ID of the transaction.
        /// </summary>
        /// <param name="referenceId">The reference ID of the verification transaction</param>
        /// <returns>A TelesignResponse containing the API response</returns>
        public RestClient.TelesignResponse Finalize(string referenceId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "reference_id", referenceId }
            };
            return Post(FINALIZE_RESOURCE, parameters);
        }

        /// <summary>
        /// Asynchronously finalizes the verification by submitting the verification code and the reference ID of the transaction.
        /// </summary>
        /// <param name="referenceId">The reference ID of the verification transaction</param>
        /// <returns>A Task representing the asynchronous operation, with a TelesignResponse containing the API response.</returns>
        public Task<RestClient.TelesignResponse> FinalizeAsync(string referenceId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "reference_id", referenceId }
            };
            return PostAsync(FINALIZE_RESOURCE, parameters);
        }

        /// <summary>
        /// Reports an unknown caller ID issue when the caller ID prefix does not match the expected prefix.
        /// </summary>
        /// <param name="referenceId">The reference ID of the verification transaction</param>
        /// <param name="unknownCallerId">The unknown caller ID to report.</param>
        /// <returns>A TelesignResponse containing the API response.</returns>
        public RestClient.TelesignResponse ReportUnknownCallerId(string referenceId, string unknownCallerId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "reference_id", referenceId },
                { "unknown_caller_id", unknownCallerId }
            };
            return Post(FINALIZE_CALLERID_RESOURCE, parameters);
        }

        /// <summary>
        /// Asynchronously reports an unknown caller ID issue when the caller ID prefix does not match the expected prefix.
        /// </summary>
        /// <param name="referenceId">The reference ID of the verification transaction.</param>
        /// <param name="unknownCallerId">The unknown caller ID to report</param>
        /// <returns>A Task representing the asynchronous operation, with a TelesignResponse containing the API response.</returns>
        public Task<RestClient.TelesignResponse> ReportUnknownCallerIdAsync(string referenceId, string unknownCallerId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "reference_id", referenceId },
                { "unknown_caller_id", unknownCallerId }
            };
            return PostAsync(FINALIZE_CALLERID_RESOURCE, parameters);
        }

        /// <summary>
        /// Reports a timeout issue when the verification call does not reach the handset within the specified time.
        /// </summary>
        /// <param name="referenceId">The reference ID of the verification transaction</param>
        /// <returns>A TelesignResponse containing the API response</returns>
        public RestClient.TelesignResponse ReportTimeout(string referenceId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "reference_id", referenceId }
            };
            return Post(FINALIZE_TIMEOUT_RESOURCE, parameters);
        }

        /// <summary>
        /// Asynchronously reports a timeout issue when the verification call does not reach the handset within the specified time.
        /// </summary>
        /// <param name="referenceId">The reference ID of the verification transaction</param>
        /// <returns>A Task representing the asynchronous operation, with a TelesignResponse containing the API response</returns>
        public Task<RestClient.TelesignResponse> ReportTimeoutAsync(string referenceId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "reference_id", referenceId }
            };
            return PostAsync(FINALIZE_TIMEOUT_RESOURCE, parameters);
        }

        /// <summary>
        /// Retrieves the status and details of a verification transaction by reference ID.
        /// </summary>
        /// <param name="referenceId">The reference ID of the verification transaction</param>
        /// <returns>A TelesignResponse containing the API response with transaction status</returns>
        public RestClient.TelesignResponse GetTransactionStatus(string referenceId)
        {
            var resource = $"{TRANSACTION_STATUS_RESOURCE}/{referenceId}";
            return Get(resource, null);
        }

        /// <summary>
        /// Asynchronously retrieves the status and details of a verification transaction by reference ID.
        /// </summary>
        /// <param name="referenceId">The reference ID of the verification transaction</param>
        /// <returns>A Task representing the asynchronous operation, with a TelesignResponse containing the API response with transaction status</returns>
        public Task<RestClient.TelesignResponse> GetTransactionStatusAsync(string referenceId)
        {
            var resource = $"{TRANSACTION_STATUS_RESOURCE}/{referenceId}";
            return GetAsync(resource, null);
        }
    }
}
