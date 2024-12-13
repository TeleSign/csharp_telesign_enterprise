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
            string customerId = "FFFFFFFF-EEEE-DDDD-1234-AB1234567890";
            string apiKey = "EXAMPLETE8sTgg45yusumoN6BYsBVkh+yRJ5czgsnCehZaOYldPJdmFh6NeX8kunZ2zU1YWaUw/0wV6xfw==";

            string phoneNumber = "phone_number";
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
