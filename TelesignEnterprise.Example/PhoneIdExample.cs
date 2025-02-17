using System;
using Newtonsoft.Json.Linq;

namespace TelesignEnterprise.Example;

public class PhoneIdExample(string apiKey, string customerId, string phoneNumber)
{
    private readonly string _ApiKey = apiKey;
    private readonly string _CustomerId = customerId;
    private readonly string _PhoneNumber = phoneNumber;

    public void CheckPhoneNumberRiskLevel()
    {
        Console.WriteLine("***Check phone number risk level***");
        string ucid = "BACF";

        try
        {
            PhoneIdClient phoneIdClient = new(_CustomerId, _ApiKey);
            Telesign.RestClient.TelesignResponse telesignResponse = phoneIdClient.Score(_PhoneNumber, ucid);
            Console.WriteLine($"Http Status code: {telesignResponse.StatusCode}");
            Console.WriteLine($"Response body: {Environment.NewLine + telesignResponse.Body}");

            if (telesignResponse.OK)
            {
                Console.WriteLine(string.Format("Phone number {0} has a '{1}' risk level and the recommendation is to '{2}' the transaction.",
                        _PhoneNumber,
                        telesignResponse.Json["risk"]["level"],
                        telesignResponse.Json["risk"]["recommendation"]));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public void CheckPhoneNumberDeactivated()
    {
        string ucid = "ATCK";

        try
        {
            Console.WriteLine("***Check phone number deactivated***");
            PhoneIdClient phoneIdClient = new(_CustomerId, _ApiKey);
            Telesign.RestClient.TelesignResponse telesignResponse = phoneIdClient.NumberDeactivation(_PhoneNumber, ucid);
            Console.WriteLine($"Http Status code: {telesignResponse.StatusCode}");
            Console.WriteLine($"Response body: {Environment.NewLine + telesignResponse.Body}");

            if (telesignResponse.OK)
            {
                if (telesignResponse.Json["number_deactivation"]["last_deactivated"].Type != JTokenType.Null)
                {
                    Console.WriteLine(string.Format("Phone number {0} was last deactivated {1}.",
                            telesignResponse.Json["number_deactivation"]["number"],
                            telesignResponse.Json["number_deactivation"]["last_deactivated"]));
                }
                else
                {
                    Console.WriteLine(string.Format("Phone number {0} has not been deactivated.",
                            telesignResponse.Json["number_deactivation"]["number"]));
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}
