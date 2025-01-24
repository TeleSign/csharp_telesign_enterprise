using System;
using System.Collections.Generic;
using Telesign;

namespace TelesignEnterprise.Example.VerifyVoice
{
    class SendCustomVoiceCallWithTextToSpeech
    {
        static void Main(string[] args)
        {
            string customerId = Environment.GetEnvironmentVariable("CUSTOMER_ID");
            string apiKey = Environment.GetEnvironmentVariable("API_KEY");

            string phoneNumber = Environment.GetEnvironmentVariable("PHONE_NUMBER");
            string verifyCode = "12345";
            string ttsMessage = string.Format("Hello, your code is {0}. Once again, your code is {1}. Goodbye.", verifyCode, verifyCode);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
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
