using System;

namespace TelesignEnterprise.Example;

public class VerifyExample(string apiKey, string customerId, string phoneNumber)
{
    private readonly string _ApiKey = apiKey;
    private readonly string _CustomerId = customerId;
    private readonly string _PhoneNumber = phoneNumber;

    public void ReportCompletionAfterReceivingSMS()
    {
        Console.WriteLine("***Report completion after receiving an SMS***");
        string verifyCode = "12345";

        Dictionary<string, string> parameters = [];
        parameters.Add("verify_code", verifyCode);

        try
        {
            VerifyClient verifyClient = new(_CustomerId, _ApiKey);
            Telesign.RestClient.TelesignResponse telesignResponse = verifyClient.Sms(_PhoneNumber, parameters);
            Console.WriteLine($"Http Status code: {telesignResponse.StatusCode}");
            Console.WriteLine($"Response body: {Environment.NewLine + telesignResponse.Body}");

            string referenceId = telesignResponse.Json["reference_id"].ToString();

            Console.WriteLine("Please enter your verification code:");
            string? code = Console.ReadLine();

            if (!string.IsNullOrEmpty(code))
            {
                if (verifyCode == code.Trim())
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
            else {
                Console.WriteLine("Code validation ommited");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public void ReportCompletionAfterReceivingVoiceCall()
    {
        Console.WriteLine("***Report completion after receiving a voice call***");
        string verifyCode = "12345";

        Dictionary<string, string> parameters = [];
        parameters.Add("verify_code", verifyCode);

        try
        {
            VerifyClient verifyClient = new(_CustomerId, _ApiKey);
            Telesign.RestClient.TelesignResponse telesignResponse = verifyClient.Voice(_PhoneNumber, parameters);
            Console.WriteLine($"Http Status code: {telesignResponse.StatusCode}");
            Console.WriteLine($"Response body: {Environment.NewLine + telesignResponse.Body}");

            string referenceId = telesignResponse.Json["reference_id"].ToString();

            Console.WriteLine("Please enter your verification code:");
            string? code = Console.ReadLine();

            if (!string.IsNullOrEmpty(code))
            {
                if (verifyCode == code.Trim())
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
            else
            {
                Console.WriteLine("Code validation ommited");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public void SendOmniVerificationSmsChannel()
    {
        Console.WriteLine("***Send OTP code with new verify API***");
        string verifyCode = "12345";

        Dictionary<string, object> parameters = [];
        parameters.Add("security_factor", verifyCode);

        try
        {
            VerifyClient verifyClient = new(_CustomerId, _ApiKey);
            Telesign.RestClient.TelesignResponse telesignResponse = verifyClient.CreateVerificationProcess(_PhoneNumber, parameters);
            Console.WriteLine($"Http Status code: {telesignResponse.StatusCode}");
            Console.WriteLine($"Response body: {Environment.NewLine + telesignResponse.Body}");

            Console.WriteLine("Please enter your verification code:");
            string? code = Console.ReadLine();
            if (!string.IsNullOrEmpty(code)) 
            {
                if (verifyCode == code.Trim())
                {
                    Console.WriteLine("Your code is correct.");
                }
                else
                {
                    Console.WriteLine("Your code is incorrect.");
                }
            }
            else
            {
                Console.WriteLine("Code validation ommited");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public void SendCustomVerifySMS()
    {
        Console.WriteLine("***Send custom verify sms***");
        string template = "Your Widgets 'n' More verification code is $$CODE$$.";

        Dictionary<string, string> parameters = [];
        parameters.Add("template", template);

        try
        {
            VerifyClient verifyClient = new(_CustomerId, _ApiKey);
            Telesign.RestClient.TelesignResponse telesignResponse = verifyClient.Sms(_PhoneNumber, parameters);
            Console.WriteLine($"Http Status code: {telesignResponse.StatusCode}");
            Console.WriteLine($"Response body: {Environment.NewLine + telesignResponse.Body}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public void SendCustomVerifySMSInDifferentLanguage()
    {
        Console.WriteLine("***Send custom verify sms in diferent lenguage***");
        string template = "Votre code de vérification Widgets 'n' More est $$CODE$$.";

        Dictionary<string, string> parameters = [];
        parameters.Add("template", template);

        try
        {
            VerifyClient verifyClient = new(_CustomerId, _ApiKey);
            Telesign.RestClient.TelesignResponse telesignResponse = verifyClient.Sms(_PhoneNumber, parameters);
            Console.WriteLine($"Http Status code: {telesignResponse.StatusCode}");
            Console.WriteLine($"Response body: {Environment.NewLine + telesignResponse.Body}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public void SendSMSWithVerificationCode()
    {
        Console.WriteLine("***Send SMS with verification code***");
        string verifyCode = "12345";

        Dictionary<string, string> parameters = [];
        parameters.Add("verify_code", verifyCode);

        try
        {
            VerifyClient verifyClient = new(_CustomerId, _ApiKey);
            Telesign.RestClient.TelesignResponse telesignResponse = verifyClient.Sms(_PhoneNumber, parameters);
            Console.WriteLine($"Http Status code: {telesignResponse.StatusCode}");
            Console.WriteLine($"Response body: {Environment.NewLine + telesignResponse.Body}");

            Console.WriteLine("Please enter your verification code:");
            string? code = Console.ReadLine();

            if (!string.IsNullOrEmpty(code))
            {
                if (verifyCode == code.Trim())
                {
                    Console.WriteLine("Your code is correct.");
                }
                else
                {
                    Console.WriteLine("Your code is incorrect.");
                }
            }
            else
            {
                Console.WriteLine("Code validation ommited");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public void SendCustomVerifyVoiceCallInDifferentLanguage()
    {
        Console.WriteLine("***Send custom verify voice call in diferent lenguage***");
        string language = "fr-FR";
        string ttsMessage = "Votre code de vérification Widgets 'n' More est $$CODE$$.";

        Dictionary<string, string> parameters = [];
        parameters.Add("language", language);
        parameters.Add("tts_message", ttsMessage);

        try
        {
            VerifyClient verifyClient = new(_CustomerId, _ApiKey);
            Telesign.RestClient.TelesignResponse telesignResponse = verifyClient.Voice(_PhoneNumber, parameters);
            Console.WriteLine($"Http Status code: {telesignResponse.StatusCode}");
            Console.WriteLine($"Response body: {Environment.NewLine + telesignResponse.Body}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public void SendCustomVerifyVoiceCallWithTextToSpeech()
    {
        Console.WriteLine("***Send custom verify voice call with text to speech***");
        string verifyCode = "12345";
        string ttsMessage = string.Format("Hello, your code is: {0}. Once again, your code is: {1}. Goodbye.", verifyCode, verifyCode);

        Dictionary<string, string> parameters = [];
        parameters.Add("tts_message", ttsMessage);

        try
        {
            VerifyClient verifyClient = new(_CustomerId, _ApiKey);
            Telesign.RestClient.TelesignResponse telesignResponse = verifyClient.Voice(_PhoneNumber, parameters);
            Console.WriteLine($"Http Status code: {telesignResponse.StatusCode}");
            Console.WriteLine($"Response body: {Environment.NewLine + telesignResponse.Body}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public void SendVoiceCallWithVerificationCode()
    {
        Console.WriteLine("***Send voice call with verification code***");
        string verifyCode = "12345";

        Dictionary<string, string> parameters = [];
        parameters.Add("verify_code", verifyCode);

        try
        {
            VerifyClient verifyClient = new (_CustomerId, _ApiKey);
            Telesign.RestClient.TelesignResponse telesignResponse = verifyClient.Voice(_PhoneNumber, parameters);
            Console.WriteLine($"Http Status code: {telesignResponse.StatusCode}");
            Console.WriteLine($"Response body: {Environment.NewLine + telesignResponse.Body}");

            Console.WriteLine("Please enter your verification code:");
            string? code = Console.ReadLine();

            if (!string.IsNullOrEmpty(code))
            {
                if (verifyCode == code.Trim())
                {
                    Console.WriteLine("Your code is correct.");
                }
                else
                {
                    Console.WriteLine("Your code is incorrect.");
                }
            }
            else
            {
                Console.WriteLine("Code validation ommited");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}
