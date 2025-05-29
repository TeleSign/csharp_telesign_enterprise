using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TelesignEnterprise.Example
{
    public class OmniVerifyExample
    {
        private readonly string _ApiKey;
        private readonly string _CustomerId;
        private readonly string _PhoneNumber;

        public OmniVerifyExample(string apiKey, string customerId, string phoneNumber)
        {
            _ApiKey = apiKey;
            _CustomerId = customerId;
            _PhoneNumber = phoneNumber;
        }


        /// <summary>
        /// Retrieve the data for the reference_id using the new OmniVerifyClient RetrieveVerificationProcess.
        /// </summary>
        public void RetrieveVerificationProcess(string referenceId)
        {
            Console.WriteLine("***Retrieve verification process (OmniVerifyClient)***");

            try
            {
                var omniClient = new OmniVerifyClient(_CustomerId, _ApiKey);
                var response = omniClient.RetrieveVerificationProcess(referenceId);

                if (response != null)
                {
                    Console.WriteLine($"HTTP Status Code: {response.StatusCode}");
                    Console.WriteLine($"Response Body:\n{response.Body}");
                }
                else
                {
                    Console.WriteLine("No response received.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving verification process: {ex.Message}");
            }
        }


        /// <summary>
        /// Send OTP code asynchronously with OmniVerifyClient
        /// </summary>
        public async Task SendVerificationAndValidateAsync()
        {
            Console.WriteLine("***Send OTP code asynchronously with OmniVerifyClient***");
            string verifyCode = "12345";

            var bodyParams = new Dictionary<string, object>
            {
                { "security_factor", verifyCode },
                { "verification_policy", new List<Dictionary<string, object>>
                    {
                        new Dictionary<string, object> { { "method", "sms" }, { "fallback_time", 30 } }
                    }
                }
            };

            try
            {
                var omniClient = new OmniVerifyClient(_CustomerId, _ApiKey);
                var response = await omniClient.CreateVerificationProcessAsync(_PhoneNumber, bodyParams);

                Console.WriteLine($"HTTP Status Code: {response.StatusCode}");
                Console.WriteLine($"Response Body:\n{response.Body}");

                if (response != null && response.Json != null && response.Json.ContainsKey("reference_id"))
                {
                    Console.WriteLine("Please enter your verification code:");
                    string? userCode = Console.ReadLine();

                    if (!string.IsNullOrEmpty(userCode) && userCode.Trim() == verifyCode)
                    {
                        Console.WriteLine("Verification successful!");
                    }
                    else
                    {
                        Console.WriteLine("Incorrect verification code or no input.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in async verification: {ex.Message}");
            }
        }


        /// <summary>
        /// Sends an OTP code via SMS using the new OmniVerifyClient.
        /// </summary>
        public void SendOmniVerificationSmsChannel()
        {
            Console.WriteLine("***Send OTP code with new verify API (OmniVerifyClient)***");
            
            string verifyCode = "12345"; 
            string? referenceId = null;

            try
            {
                var bodyParams = new Dictionary<string, object>
                {
                    { "security_factor", verifyCode },
                    { "verification_policy", new List<Dictionary<string, object>>
                        {
                            new Dictionary<string, object>
                            {
                                { "method", "sms" },
                                { "fallback_time", 30 }
                            }
                        }
                    }
                };

                OmniVerifyClient omniClient = new(_CustomerId, _ApiKey);
                Telesign.RestClient.TelesignResponse response = omniClient.CreateVerificationProcess(_PhoneNumber, bodyParams);
                
                Console.WriteLine($"HTTP Status Code: {response.StatusCode}");
                Console.WriteLine($"Response Body:\n{response.Body}");

                if (response != null && response.StatusCode == 200 && response.Json != null && response.Json.ContainsKey("reference_id"))
                {
                    referenceId = response.Json["reference_id"]?.ToString();
                    Console.WriteLine($"HTTP Status Code: {response.StatusCode}");
                    Console.WriteLine($"Verification Reference ID: {referenceId}");
                    Console.WriteLine("verification_policy:" + Newtonsoft.Json.JsonConvert.SerializeObject(bodyParams["verification_policy"]));
                    
                    Console.WriteLine("Please enter your verification code:");
                    string? userCode = Console.ReadLine()?.Trim();

                    if (!string.IsNullOrEmpty(userCode) && userCode.Trim() == verifyCode)
                    {
                        Console.WriteLine("Verification successful!");
                    }
                    else
                    {
                        Console.WriteLine("Incorrect verification code");
                    }
                }
                else
                {
                    Console.WriteLine("Verification request failed. Check:");
                    Console.WriteLine("- API credentials");
                    Console.WriteLine("- Phone number format (E.164)");
                    Console.WriteLine("- Account permissions for Verify API");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Critical error: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
