using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telesign;

namespace TelesignEnterprise.Example.MessagingOmnichannel
{
    internal class MessagingRcs
    {
        static void Main(string[] args)
        {
            string customerId = Environment.GetEnvironmentVariable("CUSTOMER_ID");
            string apiKey = Environment.GetEnvironmentVariable("API_KEY");

            string phoneNumber = Environment.GetEnvironmentVariable("PHONE_NUMBER");
            string message = "You're scheduled for a dentist appointment at 2:30PM.";

            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {
                    "message", new Dictionary<string,object>()
                    {
                        {
                            "rcs", new Dictionary<string, object>()
                            {
                                { "template", "text" },
                                {
                                    "parameters", new Dictionary<string, string>()
                                    {
                                        { "text", message }
                                    }
                                }
                            }
                        }
                    }
                },
                {
                    "recipient", new Dictionary<string,string>()
                    {
                        { "phone_number", phoneNumber }
                    }
                },
                {
                    "channels", new List<Dictionary<string, object>>()
                    {
                        new Dictionary<string, object>()
                        {
                            { "channel", "rcs" },
                            { "fallback_time", 300 }
                        }
                    }
                },
                {
                    "message_type", "ARN"
                }
            };
            try
            {
                MessagingClient messagingClient = new MessagingClient(customerId, apiKey);
                RestClient.TelesignResponse telesignResponse = messagingClient.OmniMessage(parameters);

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
