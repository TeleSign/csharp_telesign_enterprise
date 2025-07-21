using System;
using TelesignEnterprise.Example;

class Program
{
    static void Main()
    {
        Console.WriteLine("Hello, World!");

        string customerId = Environment.GetEnvironmentVariable("CUSTOMER_ID") ?? "FFFFFFFF-EEEE-DDDD-1234-AB1234567890";
        string apiKey = Environment.GetEnvironmentVariable("API_KEY") ?? "ABC12345yusumoN6BYsBVkh+yRJ5czgsnCehZaOYldPJdmFh6NeX8kunZ2zU1YWaUw/0wV6xfw==";
        string phoneNumber = Environment.GetEnvironmentVariable("PHONE_NUMBER") ?? "11234567890";

        int option = -1;
        bool validOption;

        do
        {
            Console.WriteLine("\nChoose one of the following options:");
            Console.WriteLine("1. Execute Messaging examples");
            Console.WriteLine("2. Execute PhoneId examples");
            Console.WriteLine("3. Execute Verify examples");
            Console.WriteLine("4. Execute Voice examples");
            Console.WriteLine("5. Execute OmniVerify examples Retrieve");
            Console.WriteLine("6. Execute OmniVerify examples Update");
            Console.WriteLine("7. Execute AppVerify examples");
            Console.WriteLine("8. Quit");
            Console.Write("Enter your choice (1-8): ");

            var input = Console.ReadLine() ?? string.Empty;
            validOption = int.TryParse(input, out option) && option >= 1 && option <= 8;

            if (!validOption)
            {
                Console.WriteLine("Invalid option. Please enter a number between 1 and 8.");
                continue;
            }

            switch (option)
            {
                case 1:
                    RunMessagingExamples(apiKey, customerId, phoneNumber);
                    break;
                case 2:
                    RunPhoneIdExamples(apiKey, customerId, phoneNumber);
                    break;
                case 3:
                    RunVerifyExamples(apiKey, customerId, phoneNumber);
                    break;
                case 4:
                    RunVoiceExamples(apiKey, customerId, phoneNumber);
                    break;
                case 5:
                    RunOmniVerifyRetrieveExample(apiKey, customerId, phoneNumber);
                    break;
                case 6:
                    RunOmniVerifyUpdateExample(apiKey, customerId, phoneNumber);
                    break;
                case 7:
                    RunAppVerifyMenu(customerId, apiKey, phoneNumber);
                    break;
                case 8:
                    Console.WriteLine("Bye! :)");
                    break;
            }

        } while (option != 8);
    }

    private static void RunMessagingExamples(string apiKey, string customerId, string phoneNumber)
    {
        var messagingExample = new MessagingExample(apiKey, customerId, phoneNumber);
        messagingExample.SendMessage();
        messagingExample.SendMessageWithVerificationCode();
        messagingExample.SendOmniMessageRcsChannel();
        messagingExample.SendOmniMessageSmsChannel();
        messagingExample.SendOmniMessageSmsRcsWithParams();
    }

    private static void RunPhoneIdExamples(string apiKey, string customerId, string phoneNumber)
    {
        var phoneIdExample = new PhoneIdExample(apiKey, customerId, phoneNumber);
        phoneIdExample.CheckPhoneNumberRiskLevel();
        phoneIdExample.CheckPhoneIdPath();
        phoneIdExample.CheckPhoneIdBody();
    }

    private static void RunVerifyExamples(string apiKey, string customerId, string phoneNumber)
    {
        var verifyExample = new VerifyExample(apiKey, customerId, phoneNumber);
        verifyExample.ReportCompletionAfterReceivingSMS();
        verifyExample.ReportCompletionAfterReceivingVoiceCall();
        verifyExample.SendCustomVerifySMS();
        verifyExample.SendCustomVerifySMSInDifferentLanguage();
        verifyExample.SendSMSWithVerificationCode();
        verifyExample.SendCustomVerifyVoiceCallInDifferentLanguage();
        verifyExample.SendCustomVerifyVoiceCallWithTextToSpeech();
        verifyExample.SendVoiceCallWithVerificationCode();
    }

    private static void RunVoiceExamples(string apiKey, string customerId, string phoneNumber)
    {
        var voiceExample = new VoiceExample(apiKey, customerId, phoneNumber);
        voiceExample.SendVoiceCall();
        voiceExample.SendVoiceCallFrench();
        voiceExample.SendVoiceCallWithVerificationCode();
    }

    private static void RunOmniVerifyRetrieveExample(string apiKey, string customerId, string phoneNumber)
    {
        var omniExample = new OmniVerifyExample(apiKey, customerId, phoneNumber);
        omniExample.SendOmniVerificationSmsChannel();

        Console.Write("Enter reference ID to retrieve verification process (or leave blank to skip): ");
        string? refId = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(refId))
        {
            omniExample.RetrieveVerificationProcess(refId.Trim());
        }
    }

    private static void RunOmniVerifyUpdateExample(string apiKey, string customerId, string phoneNumber)
    {
        var omniUpdateExample = new OmniVerifyExample(apiKey, customerId, phoneNumber);

        Console.Write("Enter reference ID to update verification process: ");
        string? updateRefId = Console.ReadLine();

        Console.Write("Enter the OTP/security code received by the user: ");
        string? securityCode = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(updateRefId) && !string.IsNullOrWhiteSpace(securityCode))
        {
            omniUpdateExample.UpdateVerificationState(updateRefId.Trim(), securityCode.Trim());
        }
        else
        {
            Console.WriteLine("Reference ID and security code are required. Skipping update.");
        }
    }

    private static void RunAppVerifyMenu(string customerId, string apiKey, string phoneNumber)
    {
        var appVerifyExamples = new AppVerifyExamples(customerId, apiKey, phoneNumber);

        int option = 0;
        do
        {
            Console.WriteLine("\nAppVerify Options:");
            Console.WriteLine("1. Initiate Verification");
            Console.WriteLine("2. Finalize Verification");
            Console.WriteLine("3. Report Unknown Caller ID");
            Console.WriteLine("4. Initiate+Report Timeout");
            Console.WriteLine("5. Get Transaction Status");
            Console.WriteLine("6. Quit to Main Menu");
            Console.Write("Choose an option (1-6): ");

            var input = Console.ReadLine() ?? string.Empty;
            if (!int.TryParse(input, out option) || option < 1 || option > 6)
            {
                Console.WriteLine("Invalid option. Please enter a number between 1 and 6");
                continue;
            }

            switch (option)
            {
                case 1:
                    InitiateVerification(appVerifyExamples);
                    break;
                case 2:
                    FinalizeVerification(appVerifyExamples);
                    break;
                case 3:
                    InitiateAndReportUnknownCallerId(appVerifyExamples);
                    break;
                case 4:
                    InitiateAndReportTimeout(appVerifyExamples);
                    break;
                case 5:
                    GetTransactionStatus(appVerifyExamples);
                    break;
                case 6:
                    Console.WriteLine("Returning to main menu...");
                    break;
            }

        } while (option != 6);
    }

    private static void InitiateVerification(AppVerifyExamples appVerifyExamples)
    {
        try
        {
            appVerifyExamples.InitiateVerification();
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error Initiating Verification: {ex.Message}");
        }
    }

    private static void ReportUnknownCallerId(AppVerifyExamples appVerifyExamples)
    {
        Console.Write("Enter Reference ID: ");
        var refId = Console.ReadLine();
        Console.Write("Enter Unknown Caller ID: ");
        var unknownCallerId = Console.ReadLine();
        
        if (string.IsNullOrWhiteSpace(refId) || string.IsNullOrWhiteSpace(unknownCallerId))
        {
            Console.WriteLine("Reference ID and Unknown Caller ID are required.");
            return;
        }

        appVerifyExamples.ReportUnknownCallerId(refId.Trim(), unknownCallerId.Trim());
    }

    private static void FinalizeVerification(AppVerifyExamples appVerifyExamples)
    {
        Console.Write("Enter Reference ID: ");
        var refId = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(refId))
        {
            Console.WriteLine("Reference ID is required.");
            return;
        }

        appVerifyExamples.FinalizeVerification(refId.Trim());
    }

    private static void ReportTimeout(AppVerifyExamples appVerifyExamples)
    {
        Console.Write("Enter Reference ID: ");
        var refId = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(refId))
        {
            Console.WriteLine("Reference ID is required.");
            return;
        }

        appVerifyExamples.ReportTimeout(refId.Trim());
    }

    private static void GetTransactionStatus(AppVerifyExamples appVerifyExamples)
    {
        Console.Write("Enter Reference ID: ");
        var refId = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(refId))
        {
            Console.WriteLine("Reference ID is required.");
            return;
        }

        appVerifyExamples.GetTransactionStatus(refId.Trim());
    }

    private static void InitiateAndReportTimeout(AppVerifyExamples appVerifyExamples)
    {
        appVerifyExamples.InitiateAndReportTimeout();
    }
    private static void InitiateAndReportUnknownCallerId(AppVerifyExamples appVerifyExamples)
    {
        appVerifyExamples.InitiateAndReportUnknownCallerId();
    }
}
