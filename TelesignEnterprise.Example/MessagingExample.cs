using System;

namespace TelesignEnterprise.Example;

public class MessagingExample(string apiKey, string customerId, string phoneNumber)
{
    private readonly string _ApiKey = apiKey;
    private readonly string _CustomerId = customerId;
    private readonly string _PhoneNumber = phoneNumber;

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
}
