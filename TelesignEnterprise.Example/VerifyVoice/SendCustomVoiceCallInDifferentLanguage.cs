using System;
using System.Collections.Generic;
using Telesign;

namespace TelesignEnterprise.Example.VerifyVoice
{
    class SendCustomVoiceCallInDifferentLanguage
    {
        static void Main(string[] args)
        {
            string customerId = "FFFFFFFF-EEEE-DDDD-1234-AB1234567890";
            string apiKey = "EXAMPLETE8sTgg45yusumoN6BYsBVkh+yRJ5czgsnCehZaOYldPJdmFh6NeX8kunZ2zU1YWaUw/0wV6xfw==";

            string phoneNumber = "phone_number";
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
