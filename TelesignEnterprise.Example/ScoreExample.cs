using Newtonsoft.Json.Linq;

namespace TelesignEnterprise.Example
{
    public class ScoreExample
    {
        private readonly string _ApiKey;
        private readonly string _CustomerId;
        private readonly string _PhoneNumber;
        private readonly string _EmailAddress;
        private readonly Dictionary<string, string> _OptionalParams;

        public ScoreExample(
            string apiKey,
            string customerId,
            string phoneNumber,
            string emailAddress,
            Dictionary<string, string>? optionalParams = null)
        {
            _ApiKey = apiKey;
            _CustomerId = customerId;
            _PhoneNumber = phoneNumber;
            _EmailAddress = emailAddress;
            _OptionalParams = optionalParams ?? new Dictionary<string, string>();
        }

        public void CheckPhoneNumberRiskLevel()
        {
            Console.WriteLine("*** Send score request by checking phone number risk level ***");
            string accountLifecycleEvent = "sign-in";

            try
            {
                var scoreClient = new ScoreClient(_CustomerId, _ApiKey);

                var telesignResponse = scoreClient.Score(
                    _PhoneNumber,
                    accountLifecycleEvent,
                    _OptionalParams
                );

                if (telesignResponse != null && telesignResponse.OK && telesignResponse.Json != null)
                {
                    JObject json = telesignResponse.Json;
                    JObject? risk = json["risk"] as JObject;

                    if (risk != null)
                    {
                        string level = risk["level"]?.ToString() ?? "unknown";
                        string recommendation = risk["recommendation"]?.ToString() ?? "none";

                        Console.WriteLine("Request successful.");
                        Console.WriteLine("Success from Score!!!");
                        Console.WriteLine("StatusCode: 200");
                        
                        Console.WriteLine($"Phone number: {_PhoneNumber}");
                        Console.WriteLine($"Risk level: {level}");
                        Console.WriteLine($"Recommendation: {recommendation}");
                        return;
                    }
                }

                Console.WriteLine("Risk information is not available or request failed.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}");
            }
        }

        public async Task CheckPhoneNumberRiskLevelAsync()
        {
            Console.WriteLine("*** Send score request by checking phone number risk level asynchronously ***");
            string accountLifecycleEvent = "sign-in";

            try
            {
                var scoreClient = new ScoreClient(_CustomerId, _ApiKey);

                var telesignResponse = await scoreClient.ScoreAsync(
                    _PhoneNumber,
                    accountLifecycleEvent,
                    _OptionalParams
                );

                if (telesignResponse != null && telesignResponse.OK && telesignResponse.Json != null)
                {
                    JObject json = telesignResponse.Json;
                    JObject? risk = json["risk"] as JObject;

                    if (risk != null)
                    {
                        string level = risk["level"]?.ToString() ?? "unknown";
                        string recommendation = risk["recommendation"]?.ToString() ?? "none";

                        Console.WriteLine("Request successful.");
                        Console.WriteLine("Success from Score!!!");
                        Console.WriteLine("StatusCode: 200");
                        
                        Console.WriteLine($"Phone number: {_PhoneNumber}");
                        Console.WriteLine($"Risk level: {level}");
                        Console.WriteLine($"Recommendation: {recommendation}");
                        return;
                    }
                }

                Console.WriteLine("Risk information is not available or request failed.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}");
            }
        }

        public void CheckEmailAddressRiskLevel()
        {
            Console.WriteLine("*** Sending Score request to check email address risk level ***");
            const string accountLifecycleEvent = "create";

            try
            {
                var scoreClient = new ScoreClient(_CustomerId, _ApiKey);
                Telesign.RestClient.TelesignResponse telesignResponse = scoreClient.EmailIntelligence(
                    _EmailAddress,
                    accountLifecycleEvent,
                    _OptionalParams
                );

                if (telesignResponse != null && telesignResponse.OK && telesignResponse.Json != null)
                {
                    JObject json = telesignResponse.Json;
                    JObject? risk = (json["intelligence_details"]?["risk"] as JObject)
                        ?? (json["risk"] as JObject);

                    if (risk != null)
                    {
                        string level = risk["level"]?.ToString() ?? "unknown";
                        string recommendation = risk["recommendation"]?.ToString() ?? "none";

                        Console.WriteLine("Request successful.");
                        Console.WriteLine("Success from Score!!!");
                        Console.WriteLine("StatusCode: 200");
                        
                        Console.WriteLine($"Email address: {_EmailAddress}");
                        Console.WriteLine($"Risk level: {level}");
                        Console.WriteLine($"Recommendation: {recommendation}");
                        return;
                    }
                }

                Console.WriteLine("Risk information is not available or request failed.");
                PrintDetailedResponse(telesignResponse);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}");
            }
        }
        
        private void PrintDetailedResponse(Telesign.RestClient.TelesignResponse? response)
        {
            if (response is null)
            {
                Console.WriteLine("No response received.");
                return;
            }

            Console.WriteLine($"Status Code: {response.StatusCode}");
            Console.WriteLine($"OK: {response.OK}");
            if (response.Json != null)
            {
                Console.WriteLine("Response JSON:");
                Console.WriteLine(response.Json.ToString());
            }
            else
            {
                Console.WriteLine("No JSON content in response.");
            }
        }

    }
}