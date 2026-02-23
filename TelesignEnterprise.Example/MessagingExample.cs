using System;
using System.Collections.Generic;
using System.Threading; 
using Newtonsoft.Json.Linq;

namespace TelesignEnterprise.Example;

public class MessagingExample(string apiKey, string customerId, string phoneNumber)
{
    private readonly string _ApiKey = apiKey;
    private readonly string _CustomerId = customerId;
    private readonly string _PhoneNumber = phoneNumber;
    private string? _referenceId;

    public void SendMessage()
    {
        Console.WriteLine("***Send message***");
        string message = "You're scheduled for a dentist appointment at 2:30PM.";
        string messageType = "ARN";
        try
        {
            MessagingClient messagingClient = new(_CustomerId, _ApiKey);
            Telesign.RestClient.TelesignResponse telesignResponse = messagingClient.Message(_PhoneNumber, message, messageType);
            Console.WriteLine($"Http Status code: {telesignResponse.StatusCode}");
            Console.WriteLine($"Response body: {Environment.NewLine + telesignResponse.Body}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public void SendMessageWithVerificationCode()
    {
        Console.WriteLine();
        Console.WriteLine("***Send message with verification code***");
        string verifyCode = "12345";
        string message = string.Format("Your code is {0}", verifyCode);
        string messageType = "OTP";

        try
        {
            MessagingClient messagingClient = new(_CustomerId, _ApiKey);
            Telesign.RestClient.TelesignResponse telesignResponse = messagingClient.Message(_PhoneNumber, message, messageType);
            Console.WriteLine($"Http Status code: {telesignResponse.StatusCode}");
            Console.WriteLine($"Response body: {Environment.NewLine + telesignResponse.Body}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public void SendOmniMessageRcsChannel()
    {
        Console.WriteLine();
        Console.WriteLine("***Send advance message (RCS channel)***");
        string message = "You're scheduled for a dentist appointment at 2:30PM.";

        Dictionary<string, object> parameters = new()
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
                    { "phone_number", _PhoneNumber }
                }
            },
            {
                "channels", new List<Dictionary<string, object>>()
                {
                    new ()
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
            MessagingClient messagingClient = new(_CustomerId, _ApiKey);
            Telesign.RestClient.TelesignResponse telesignResponse = messagingClient.OmniMessage(parameters);
            Console.WriteLine($"Http Status code: {telesignResponse.StatusCode}");
            Console.WriteLine($"Response body: {Environment.NewLine + telesignResponse.Body}");

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public void SendOmniMessageSmsChannel()
    {
        Console.WriteLine();
        Console.WriteLine("***Send advance message (SMS channel)***");
        string message = "You're scheduled for a dentist appointment at 2:30PM.";

        Dictionary<string, object> parameters = new()
        {
            {
                "message", new Dictionary<string,object>()
                {
                    {
                        "sms", new Dictionary<string, object>()
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
                    { "phone_number", _PhoneNumber }
                }
            },
            {
                "channels", new List<Dictionary<string, object>>()
                {
                    new ()
                    {
                        { "channel", "sms" },
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
            MessagingClient messagingClient = new(_CustomerId, _ApiKey);
            Telesign.RestClient.TelesignResponse telesignResponse = messagingClient.OmniMessage(parameters);
            Console.WriteLine($"Http Status code: {telesignResponse.StatusCode}");
            Console.WriteLine($"Response body: {Environment.NewLine + telesignResponse.Body}");

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public void SendOmniMessageSmsRcsWithParams()
    {
        Console.WriteLine();
        Console.WriteLine("***Send advance message (SMS and RCS channels)***");
        string message = "You're scheduled for a dentist appointment at 2:30PM.";

        Dictionary<string, object> parameters = new()
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
                    },
                    {
                        "sms", new Dictionary<string, object>()
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
                    { "phone_number", _PhoneNumber }
                }
            },
            {
                "channels", new List<Dictionary<string, object>>()
                {
                    new ()
                    {
                        { "channel", "rcs" },
                        { "fallback_time", 300 }
                    },
                    new ()
                    {
                        { "channel", "sms" },
                        { "fallback_time", 300 }
                    }
                }
            },
            {
                "message_type", "ARN"
            },
            {
                "originating_ip", "127.0.0.1"
            },
            {
                "account_lifecycle_event", "create"
            }
        };
        try
        {
            MessagingClient messagingClient = new MessagingClient(_CustomerId, _ApiKey);
            Telesign.RestClient.TelesignResponse telesignResponse = messagingClient.OmniMessage(parameters);
            Console.WriteLine($"Http Status code: {telesignResponse.StatusCode}");
            Console.WriteLine($"Response body: {Environment.NewLine + telesignResponse.Body}");

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public void CheckChannelCapability()
    {
        Console.WriteLine();
        Console.WriteLine("***Check channel capability***");
        try
        {
            var client = new MessagingClient(_CustomerId, _ApiKey);
            var response = client.CheckPhoneNumberChannelCapability("rcs", _PhoneNumber);
            Console.WriteLine($"Status code: {response.StatusCode}");
            Console.WriteLine($"Response: {response.Body}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public void CheckRbmCapability()
    {
        Console.WriteLine();
        Console.WriteLine("***Check RBM capability***");
        string agentId = "test_rbm_agent_id";
        try
        {
            var client = new MessagingClient(_CustomerId, _ApiKey);
            var response = client.CheckPhoneNumberRBMCapability(_PhoneNumber, agentId);
            Console.WriteLine($"Status code: {response.StatusCode}");
            Console.WriteLine($"Response: {response.Body}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public void GetMessagingStatus()
    {
        Console.WriteLine();
        Console.WriteLine("***Send OmniMessage and check status***");
        string message = "You're scheduled for a dentist appointment at 2:30PM.";

        var parameters = new Dictionary<string, object>
        {
            {
                "message", new Dictionary<string, object>
                {
                    {
                        "sms", new Dictionary<string, object>
                        {
                            { "template", "text" },
                            { 
                                "parameters", new Dictionary<string, string>
                                {
                                    { "text", message }
                                }
                            }
                        }
                    }
                }
            },
            {
                "recipient", new Dictionary<string, string>
                {
                    { "phone_number", _PhoneNumber }
                }
            },
            {
                "channels", new List<Dictionary<string, object>>
                {
                    new Dictionary<string, object>
                    {
                        { "channel", "sms" },
                        { "fallback_time", 300 }
                    }
                }
            },
            { "message_type", "ARN" }
        };

        try
        {
            MessagingClient messagingClient = new(_CustomerId, _ApiKey);
        
            Console.WriteLine(" Sending OmniMessage...");
            Telesign.RestClient.TelesignResponse sendResponse = messagingClient.OmniMessage(parameters);
        
            Console.WriteLine($"Send - Status code: {sendResponse.StatusCode}");
            Console.WriteLine($"Send - Response: {Environment.NewLine + sendResponse.Body}");

            if (sendResponse.StatusCode == 200)
            {
                var json = JObject.Parse(sendResponse.Body);
                _referenceId = json["reference_id"]!.ToString();
                Console.WriteLine($"Extracted reference_id: {_referenceId}");

                Thread.Sleep(3000);

                Console.WriteLine($" Checking status for {_referenceId}...");
                var statusResponse = messagingClient.GetMessagingStatus(_referenceId);
            
                Console.WriteLine($"Status check - Code: {statusResponse.StatusCode}");
                Console.WriteLine($"Status check - Response: {Environment.NewLine + statusResponse.Body}");

                try
                {
                    var statusJson = JObject.Parse(statusResponse.Body);
                    var statusCode = statusJson["status"]?["code"]?.ToString() ?? "unknown";
                    var description = statusJson["status"]?["description"]?.ToString() ?? "no description";
                
                    Console.WriteLine($"Final Status: {statusCode} - {description}");
                
                    switch (statusCode)
                    {
                        case "3000":
                            Console.WriteLine("Delivered - The message was delivered to the end user.");
                        break;
                        case "3001" :
                            Console.WriteLine("Message in progress");
                        break;
                        case "3002":
                            Console.WriteLine("Delivered to Gateway - The message is in the process of delivery.");
                        break;
                        case "3003":
                            Console.WriteLine("Delivery error - The message could not be delivered to the end-user.");
                        break;
                        case "3005":
                            Console.WriteLine("Message is read - The message has been read by the end user.");
                        break;
                        case "3006":
                            Console.WriteLine("None of the channels is enabled for the customer");
                        break;
                        default:
                            Console.WriteLine($"Unknown status: {statusCode}");
                        break;
                    }
                }
                catch
                {
                    Console.WriteLine("Could not parse status JSON");
                }
            }
            else
            {
                Console.WriteLine("Send failed - cannot check status");
                PrintErrorDetails(sendResponse);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }

    public void ManageTemplates()
    {
        Console.WriteLine();
        Console.WriteLine("***Template Management***");
        try
        {
            var client = new MessagingClient(_CustomerId, _ApiKey);
            string channel = "sms";
            string templateName = $"test_template_{DateTime.Now:yyMMddHHmm}";

            CreateTemplate(client, channel, templateName);
            Thread.Sleep(1000);

            GetTemplate(client, channel, templateName);
            Thread.Sleep(1000);

            DeleteTemplate(client, channel, templateName);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Template management error: {e.Message}");
        }
    }

    public void CreateTemplate(MessagingClient client, string channel, string templateName)
    {
        Console.WriteLine();
        Console.WriteLine($"Creating '{templateName}'...");
    
        var createParams = new Dictionary<string, object>
        {
            {"name", templateName},
            {"type", "standard"},
            {"channel", channel},
            { 
                "content", new List<Dictionary<string, object>>
                {
                    new Dictionary<string, object>
                    {
                        {
                            "body", new Dictionary<string, object>
                            {
                                {"type", "text"},
                                {"text", "Your testorder {{1}} has shipped to {{2}}."}
                            }
                        }
                    }
                }
            }
        };
    
        var createResponse = client.CreateMsgTemplate(createParams);
        Console.WriteLine($"   Create result: {createResponse.StatusCode}");
        Console.WriteLine($"   Create response: {createResponse.Body}");
        if (createResponse.StatusCode != 200) PrintErrorDetails(createResponse);
    }

    public void GetTemplate(MessagingClient client, string channel, string templateName)
    {
        Console.WriteLine();
        Console.WriteLine($"Testing GetTemplate('{templateName}')...");
        var getResponse = client.GetMsgTemplate(channel, templateName);
        Console.WriteLine($"   Template details: {getResponse.StatusCode}");
        Console.WriteLine($"   Template response: {getResponse.Body}");
        if (getResponse.StatusCode != 200) PrintErrorDetails(getResponse);
    }

    public void DeleteTemplate(MessagingClient client, string channel, string templateName)
    {
        Console.WriteLine();
        Console.WriteLine($"Testing DeleteTemplate('{templateName}')...");
        var deleteResponse = client.DeleteMsgTemplate(channel, templateName);
        Console.WriteLine($"   Delete result: {deleteResponse.StatusCode}");
        Console.WriteLine($"   Delete response: {deleteResponse.Body}");
        if (deleteResponse.StatusCode != 200) PrintErrorDetails(deleteResponse);
    }

    private static void PrintErrorDetails(Telesign.RestClient.TelesignResponse response)
    {
        try 
        {
            var json = JObject.Parse(response.Body);
            var updatedOn = json["status"]?["updated_on"]?.ToString() ?? "2024-11-12T13:08:37.000000Z";
            var statusCode = json["status"]?["code"]?.ToString() ?? "unknown";
            var description = json["status"]?["description"]?.ToString() ?? "no description";
            var lastChannel = json["status"]?["last_channel"]?.ToString() ?? "whatsapp";
            Console.WriteLine($"    → Error {statusCode}: {description}");
        }
        catch 
        {
            Console.WriteLine($"    → Raw error: {response.Body}");
        }
    }
}
