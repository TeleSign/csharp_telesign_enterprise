using System;
using System.Threading.Tasks;
using Telesign;

namespace TelesignEnterprise.Example
{
    public class AppVerifyExamples
    {
        private readonly string _ApiKey;
        private readonly string _CustomerId;
        private readonly string _PhoneNumber;
        private readonly AppVerifyClient _appVerifyClient;

        public AppVerifyExamples(string customerId, string apiKey, string phoneNumber)
        {
            _CustomerId = customerId;
            _ApiKey = apiKey;
            _PhoneNumber = phoneNumber;
            _appVerifyClient = new AppVerifyClient(_CustomerId, _ApiKey);
        }

        /// <summary>
        /// Initiates the verification call to the phone number.
        /// </summary>
        public void InitiateVerification()
        {
            Console.WriteLine("*** Initiate Verification ***");
            try
            {
                Telesign.RestClient.TelesignResponse response = _appVerifyClient.Initiate(_PhoneNumber);
                Console.WriteLine($"Status Code: {response.StatusCode}");
                Console.WriteLine($"Response Body: {Environment.NewLine + response.Body}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during Initiate: {ex.Message}");
            }
        }

        /// <summary>
        /// Asynchronously initiates the verification call to the phone number.
        /// </summary>
        public async Task InitiateVerificationAsync()
        {
            Console.WriteLine("*** Initiate Verification (Async) ***");
            try
            {
                Telesign.RestClient.TelesignResponse response = await _appVerifyClient.InitiateAsync(_PhoneNumber);
                Console.WriteLine($"Status Code: {response.StatusCode}");
                Console.WriteLine($"Response Body: {Environment.NewLine + response.Body}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during InitiateAsync: {ex.Message}");
            }
        }

        /// <summary>
        /// Finalizes the verification using the reference ID.
        /// </summary>
        /// <param name="referenceId">Reference ID from initiate response</param>
        public void FinalizeVerification(string referenceId)
        {
            Console.WriteLine("*** Finalize Verification ***");
            try
            {
                Telesign.RestClient.TelesignResponse response = _appVerifyClient.Finalize(referenceId);
                Console.WriteLine($"Status Code: {response.StatusCode}");
                Console.WriteLine($"Response Body: {Environment.NewLine + response.Body}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during Finalize: {ex.Message}");
            }
        }

        /// <summary>
        /// Asynchronously finalizes the verification using the reference ID.
        /// </summary>
        /// <param name="referenceId">Reference ID from initiate response</param>
        public async Task FinalizeVerificationAsync(string referenceId)
        {
            Console.WriteLine("*** Finalize Verification (Async) ***");
            try
            {
                Telesign.RestClient.TelesignResponse response = await _appVerifyClient.FinalizeAsync(referenceId);
                Console.WriteLine($"Status Code: {response.StatusCode}");
                Console.WriteLine($"Response Body: {Environment.NewLine + response.Body}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during FinalizeAsync: {ex.Message}");
            }
        }

        /// <summary>
        /// Reports an unknown caller ID issue.
        /// </summary>
        /// <param name="referenceId">Reference ID from initiate response</param>
        /// <param name="unknownCallerId">Unknown caller ID to report</param>
        public void ReportUnknownCallerId(string referenceId, string unknownCallerId)
        {
            Console.WriteLine("*** Report Unknown Caller ID ***");
            try
            {
                Telesign.RestClient.TelesignResponse response = _appVerifyClient.ReportUnknownCallerId(referenceId, unknownCallerId);
                Console.WriteLine($"Status Code: {response.StatusCode}");
                Console.WriteLine($"Response Body: {Environment.NewLine + response.Body}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during ReportUnknownCallerId: {ex.Message}");
            }
        }

        public void InitiateAndReportUnknownCallerId()
        {
            Console.WriteLine("*** Initiate Verification (for Unknown Caller ID Test) ***");
            var response = _appVerifyClient.Initiate(_PhoneNumber);
            Console.WriteLine($"Initiate Response Body: {Environment.NewLine + response.Body}");

            try
            {
                var json = Newtonsoft.Json.Linq.JObject.Parse(response.Body ?? "{}");
                string? referenceId = json["reference_id"]?.ToString();
                string? prefix = json["prefix"]?.ToString();

                if (string.IsNullOrWhiteSpace(referenceId))
                {
                    Console.WriteLine("No reference_id returned. Aborting.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(prefix))
                {
                    Console.WriteLine("No prefix returned to create unknown caller ID. Aborting.");
                    return;
                }
                string unknownCallerId = prefix + "99999";//unknown caller ID differing from prefix to simulate mismatch

                Console.WriteLine("*** Report Unknown Caller ID (Test) ***");
                var unknownResponse = _appVerifyClient.ReportUnknownCallerId(referenceId, unknownCallerId);
                Console.WriteLine($"Report Unknown Caller ID Status Code: {unknownResponse.StatusCode}");
                Console.WriteLine($"Report Unknown Caller ID Response Body: {Environment.NewLine + unknownResponse.Body}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during InitiateAndReportUnknownCallerId: {ex.Message}");
            }
        }

        /// <summary>
        /// Asynchronously reports an unknown caller ID issue.
        /// </summary>
        /// <param name="referenceId">Reference ID from initiate response</param>
        /// <param name="unknownCallerId">Unknown caller ID to report</param>
        public async Task ReportUnknownCallerIdAsync(string referenceId, string unknownCallerId)
        {
            Console.WriteLine("*** Report Unknown Caller ID (Async) ***");
            try
            {
                Telesign.RestClient.TelesignResponse response = await _appVerifyClient.ReportUnknownCallerIdAsync(referenceId, unknownCallerId);
                Console.WriteLine($"Status Code: {response.StatusCode}");
                Console.WriteLine($"Response Body: {Environment.NewLine + response.Body}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during ReportUnknownCallerIdAsync: {ex.Message}");
            }
        }

        /// <summary>
        /// Reports a timeout issue.
        /// </summary>
        /// <param name="referenceId">Reference ID from initiate response</param>
        public void ReportTimeout(string referenceId)
        {
            Console.WriteLine("*** Report Timeout ***");
            try
            {
                Telesign.RestClient.TelesignResponse response = _appVerifyClient.ReportTimeout(referenceId);
                Console.WriteLine($"Status Code: {response.StatusCode}");
                Console.WriteLine($"Response Body: {Environment.NewLine + response.Body}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during ReportTimeout: {ex.Message}");
            }
        }
        public void InitiateAndReportTimeout()
        {
            Console.WriteLine("*** Initiate Verification (for Timeout Test) ***");
            var response = _appVerifyClient.Initiate(_PhoneNumber);
            Console.WriteLine($"Initiate Response Body: {Environment.NewLine + response.Body}");

            try
            {
                var json = Newtonsoft.Json.Linq.JObject.Parse(response.Body ?? "{}");
                string? referenceId = json["reference_id"]?.ToString();

                if (string.IsNullOrWhiteSpace(referenceId))
                {
                    Console.WriteLine("No reference_id returned. Aborting.");
                    return;
                }
                Console.WriteLine("*** Report Timeout (Flow Test) ***");
                var timeoutResponse = _appVerifyClient.ReportTimeout(referenceId);
                Console.WriteLine($"Timeout Status Code: {timeoutResponse.StatusCode}");
                Console.WriteLine($"Timeout Response Body: {Environment.NewLine + timeoutResponse.Body}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during InitiateAndReportTimeout: {ex.Message}");
            }
        }


        /// <summary>
        /// Asynchronously reports a timeout issue.
        /// </summary>
        /// <param name="referenceId">Reference ID from initiate response</param>
        public async Task ReportTimeoutAsync(string referenceId)
        {
            Console.WriteLine("*** Report Timeout (Async) ***");
            try
            {
                Telesign.RestClient.TelesignResponse response = await _appVerifyClient.ReportTimeoutAsync(referenceId);
                Console.WriteLine($"Status Code: {response.StatusCode}");
                Console.WriteLine($"Response Body: {Environment.NewLine + response.Body}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during ReportTimeoutAsync: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets the transaction status by reference ID.
        /// </summary>
        /// <param name="referenceId">Reference ID from initiate response</param>
        public void GetTransactionStatus(string referenceId)
        {
            Console.WriteLine("*** Get Transaction Status ***");
            try
            {
                Telesign.RestClient.TelesignResponse response = _appVerifyClient.GetTransactionStatus(referenceId);
                Console.WriteLine($"Status Code: {response.StatusCode}");
                Console.WriteLine($"Response Body: {Environment.NewLine + response.Body}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during GetTransactionStatus: {ex.Message}");
            }
        }

        /// <summary>
        /// Asynchronously gets the transaction status by reference ID.
        /// </summary>
        /// <param name="referenceId">Reference ID from initiate response</param>
        public async Task GetTransactionStatusAsync(string referenceId)
        {
            Console.WriteLine("*** Get Transaction Status (Async) ***");
            try
            {
                Telesign.RestClient.TelesignResponse response = await _appVerifyClient.GetTransactionStatusAsync(referenceId);
                Console.WriteLine($"Status Code: {response.StatusCode}");
                Console.WriteLine($"Response Body: {Environment.NewLine + response.Body}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during GetTransactionStatusAsync: {ex.Message}");
            }
        }
    }
}
