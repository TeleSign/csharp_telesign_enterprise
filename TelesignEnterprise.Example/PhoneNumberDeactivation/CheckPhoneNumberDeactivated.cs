using Newtonsoft.Json.Linq;
using System;
using Telesign;

namespace TelesignEnterprise.Example.PhoneNumberDeactivation
{
    public class CheckPhoneNumberDeactivated
    {
        public static void Main(string[] args)
        {
            string customerId = "FFFFFFFF-EEEE-DDDD-1234-AB1234567890";
            string apiKey = "EXAMPLETE8sTgg45yusumoN6BYsBVkh+yRJ5czgsnCehZaOYldPJdmFh6NeX8kunZ2zU1YWaUw/0wV6xfw==";

            string phoneNumber = "phone_number";
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
