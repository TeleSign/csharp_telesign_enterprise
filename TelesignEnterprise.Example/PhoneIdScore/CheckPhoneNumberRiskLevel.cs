using System;
using Telesign;

namespace TelesignEnterprise.Example.PhoneIdScore
{
    public class CheckPhoneNumberRiskLevel
    {
        public static void Main(string[] args)
        {
            string customerId = "FFFFFFFF-EEEE-DDDD-1234-AB1234567890";
            string apiKey = "EXAMPLETE8sTgg45yusumoN6BYsBVkh+yRJ5czgsnCehZaOYldPJdmFh6NeX8kunZ2zU1YWaUw/0wV6xfw==";

            string phoneNumber = "phone_number";
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
