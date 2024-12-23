using System;
using System.Collections.Generic;
using Telesign;

namespace TelesignEnterprise.Example.VerifyOmnichannel
{
    class VerificationSmsOwnOTP
    {
        static void Main(string[] args)
        {
            string customerId = Environment.GetEnvironmentVariable("CUSTOMER_ID");
            string apiKey = Environment.GetEnvironmentVariable("API_KEY");

            string phoneNumber = Environment.GetEnvironmentVariable("PHONE_NUMBER");
            string verifyCode = "12345";

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("security_factor", verifyCode);

            try
            {
                VerifyClient verifyClient = new VerifyClient(customerId, apiKey);
                RestClient.TelesignResponse telesignResponse = verifyClient.CreateVerificationProcess(phoneNumber, parameters);

                Console.WriteLine("Please enter your verification code:");
                string code = Console.ReadLine().Trim();

                if (verifyCode == code)
                {
                    Console.WriteLine("Your code is correct.");
                }
                else
                {
                    Console.WriteLine("Your code is incorrect.");
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
