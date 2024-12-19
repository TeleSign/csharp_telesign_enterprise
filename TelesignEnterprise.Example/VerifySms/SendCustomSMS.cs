using System;
using System.Collections.Generic;
using Telesign;

namespace TelesignEnterprise.Example.VerifySms
{
    class SendCustomSMS
    {
        static void Main(string[] args)
        {
            string customerId = Environment.GetEnvironmentVariable("CUSTOMER_ID");
            string apiKey = Environment.GetEnvironmentVariable("API_KEY");

            string phoneNumber = Environment.GetEnvironmentVariable("PHONE_NUMBER");
            string template = "Your Widgets 'n' More verification code is $$CODE$$.";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("template", template);

            try
            {
                VerifyClient verifyClient = new VerifyClient(customerId, apiKey);
                RestClient.TelesignResponse telesignResponse = verifyClient.Sms(phoneNumber, parameters);
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
