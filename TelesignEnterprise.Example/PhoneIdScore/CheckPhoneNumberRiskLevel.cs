using System;
using Telesign;

namespace TelesignEnterprise.Example.PhoneIdScore
{
    public class CheckPhoneNumberRiskLevel
    {
        public static void Main(string[] args)
        {
            string customerId = Environment.GetEnvironmentVariable("CUSTOMER_ID");
            string apiKey = Environment.GetEnvironmentVariable("API_KEY");

            string phoneNumber = Environment.GetEnvironmentVariable("PHONE_NUMBER");
            string ucid = "BACF";

            try
            {
                PhoneIdClient phoneIdClient = new PhoneIdClient(customerId, apiKey);
                RestClient.TelesignResponse telesignResponse = phoneIdClient.Score(phoneNumber, ucid);

                if (telesignResponse.OK)
                {
                    Console.WriteLine(string.Format("Phone number {0} has a '{1}' risk level and the recommendation is to '{2}' the transaction.",
                            phoneNumber,
                            telesignResponse.Json["risk"]["level"],
                            telesignResponse.Json["risk"]["recommendation"]));
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
