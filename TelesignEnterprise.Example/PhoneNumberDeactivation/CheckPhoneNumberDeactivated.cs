using Newtonsoft.Json.Linq;
using System;
using Telesign;

namespace TelesignEnterprise.Example.PhoneNumberDeactivation
{
    public class CheckPhoneNumberDeactivated
    {
        public static void Main(string[] args)
        {
            string customerId = Environment.GetEnvironmentVariable("CUSTOMER_ID");
            string apiKey = Environment.GetEnvironmentVariable("API_KEY");

            string phoneNumber = Environment.GetEnvironmentVariable("PHONE_NUMBER");
            string ucid = "ATCK";

            try
            {
                PhoneIdClient phoneIdClient = new PhoneIdClient(customerId, apiKey);
                RestClient.TelesignResponse telesignResponse = phoneIdClient.NumberDeactivation(phoneNumber, ucid);

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

            Console.WriteLine("Press any key to quit.");
            Console.ReadKey();
        }
    }
}
