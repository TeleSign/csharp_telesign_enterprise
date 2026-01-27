using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TelesignEnterprise.Example;

public class PhoneIdExample(string apiKey, string customerId, string phoneNumber)
{
    private readonly string _ApiKey = apiKey;
    private readonly string _CustomerId = customerId;
    private readonly string _PhoneNumber = phoneNumber;

    public void CheckPhoneIdPath()
    {
        Console.WriteLine("***Check Phone ID (Path-based POST)***");
        try
        {
            PhoneIdClient phoneIdClient = new(_CustomerId, _ApiKey);

            var parameters = new Dictionary<string, object>
            {
                { "account_lifecycle_event", "sign-in" },
                { "external_id", "example_external_id" },
                { "originating_ip", "203.0.113.42" },
                { "addons", new Dictionary<string, object> { { "consent", new Dictionary<string, int> { { "method", 1 } } } } }
            };

            var response = phoneIdClient.PhoneIdPath(_PhoneNumber, parameters);

            Console.WriteLine($"Http Status code: {response.StatusCode}");
            try
            {

                Console.WriteLine(JToken.Parse(response.Body).ToString(Formatting.Indented));
            }
            catch (JsonReaderException)
            {
                Console.WriteLine(response.Body);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }


    public void CheckPhoneIdBody()
    {
        Console.WriteLine("***Check Phone ID (Payload-based POST)***");
        try
        {
            PhoneIdClient phoneIdClient = new(_CustomerId, _ApiKey);

            var parameters = new Dictionary<string, object>
            {
                { "phone_number", _PhoneNumber }, 
                { "consent", new Dictionary<string, int> { { "method", 1 } } }
            };

            var response = phoneIdClient.PhoneIdBody(_PhoneNumber, parameters);

            Console.WriteLine($"Http Status code: {response.StatusCode}");
            try
            {

                Console.WriteLine(JToken.Parse(response.Body).ToString(Formatting.Indented));
            }
            catch (JsonReaderException)
            {
                Console.WriteLine(response.Body);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

}
