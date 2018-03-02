﻿using System;
using System.Collections.Generic;
using Telesign;

namespace TelesignEnterprise.Example.VerifyCompletion
{
    class ReportCompletionAfterReceivingVoiceCall
    {
        public static void Main(string[] args)
        {
            string customerId = "FFFFFFFF-EEEE-DDDD-1234-AB1234567890";
            string apiKey = "EXAMPLETE8sTgg45yusumoN6BYsBVkh+yRJ5czgsnCehZaOYldPJdmFh6NeX8kunZ2zU1YWaUw/0wV6xfw==";

            string phoneNumber = "phone_number";
            string verifyCode = "12345";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters["verify_code"] = verifyCode;

            try
            {
                VerifyClient verifyClient = new VerifyClient(customerId, apiKey);
                RestClient.TelesignResponse telesignResponse = verifyClient.Voice(phoneNumber, parameters);

                string referenceId = telesignResponse.Json["reference_id"].ToString();

                Console.WriteLine("Please enter your verification code:");
                string code = Console.ReadLine().Trim();

                if (verifyCode == code)
                {
                    Console.WriteLine("Your code is correct.");

                    telesignResponse = verifyClient.Completion(referenceId);

                    if (telesignResponse.OK && telesignResponse.Json["status"]["code"].ToString() == "1900")
                    {
                        Console.WriteLine("Completion successfully reported.");
                    }
                    else
                    {
                        Console.WriteLine("Error reporting completion.");
                    }
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