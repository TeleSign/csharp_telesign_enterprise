using System;
using System.Collections.Generic;
using Telesign;

namespace TelesignEnterprise.Example.VerifyVoice
{
    class SendCustomVoiceCallWithTextToSpeech
    {
        static void Main(string[] args)
        {
            string customerId = "FFFFFFFF-EEEE-DDDD-1234-AB1234567890";
            string apiKey = "EXAMPLETE8sTgg45yusumoN6BYsBVkh+yRJ5czgsnCehZaOYldPJdmFh6NeX8kunZ2zU1YWaUw/0wV6xfw==";

            string phoneNumber = "phone_number";
            string verifyCode = "12345";
            string ttsMessage = string.Format("Hello, your code is {0}. Once again, your code is {1}. Goodbye.", verifyCode, verifyCode);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters["tts_message"] = ttsMessage;

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
