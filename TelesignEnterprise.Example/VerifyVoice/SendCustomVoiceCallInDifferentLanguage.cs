using System;
using System.Collections.Generic;
using Telesign;

namespace TelesignEnterprise.Example.VerifyVoice
{
    class SendCustomVoiceCallInDifferentLanguage
    {
        static void Main(string[] args)
        {
            string customerId = Environment.GetEnvironmentVariable("CUSTOMER_ID");
            string apiKey = Environment.GetEnvironmentVariable("API_KEY");

            string phoneNumber = Environment.GetEnvironmentVariable("PHONE_NUMBER");
            string language = "fr-FR";
            string ttsMessage = "Votre code de vérification Widgets 'n' More est $$CODE$$.";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("language", language);
            parameters.Add("tts_message", ttsMessage);

            try
            {
                VerifyClient verifyClient = new VerifyClient(customerId, apiKey);
                RestClient.TelesignResponse telesignResponse = verifyClient.Voice(phoneNumber, parameters);
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
