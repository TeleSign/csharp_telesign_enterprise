using TelesignEnterprise.Example;

Console.WriteLine("Hello, World!");

Console.WriteLine("Choose one of the follow options:");
Console.WriteLine("1. Execute Messaging examples");
Console.WriteLine("2. Execute PhoneId examples");
Console.WriteLine("3. Execute Verify examples");
Console.WriteLine("4. Execute Voice examples");
Console.WriteLine("5. Execute OmniVerify examples");
Console.WriteLine("6. Quit");

int options = -1;
bool validOption;

do
{
    if (options != -1)
        Console.WriteLine("Invalid option. Please type a number between 1 and 5");
    _ = int.TryParse(Console.ReadLine(), out options);
    validOption = options > 0 && options < 6;
}
while(!validOption);


string customerId = Environment.GetEnvironmentVariable("CUSTOMER_ID")?? "FFFFFFFF-EEEE-DDDD-1234-AB1234567890";
string apiKey = Environment.GetEnvironmentVariable("API_KEY") ?? "ABC12345yusumoN6BYsBVkh+yRJ5czgsnCehZaOYldPJdmFh6NeX8kunZ2zU1YWaUw/0wV6xfw==";
string phoneNumber = Environment.GetEnvironmentVariable("PHONE_NUMBER") ?? "11234567890";

switch (options)
{
    case 1:
        MessagingExample messagingExample = new(apiKey, customerId, phoneNumber);
        messagingExample.SendMessage();
        messagingExample.SendMessageWithVerificationCode();
        messagingExample.SendOmniMessageRcsChannel();
        messagingExample.SendOmniMessageSmsChannel();
        messagingExample.SendOmniMessageSmsRcsWithParams();
    break;
    case 2:
        PhoneIdExample phoneIdExample = new(apiKey, customerId, phoneNumber);
        phoneIdExample.CheckPhoneNumberRiskLevel();
        phoneIdExample.CheckPhoneNumberDeactivated();
    break;
    case 3:
        VerifyExample verifyExample = new(apiKey, customerId, phoneNumber);
        verifyExample.ReportCompletionAfterReceivingSMS();
        verifyExample.ReportCompletionAfterReceivingVoiceCall();
        verifyExample.SendCustomVerifySMS();
        verifyExample.SendCustomVerifySMSInDifferentLanguage();
        verifyExample.SendSMSWithVerificationCode();
        verifyExample.SendCustomVerifyVoiceCallInDifferentLanguage();
        verifyExample.SendCustomVerifyVoiceCallWithTextToSpeech();
        verifyExample.SendVoiceCallWithVerificationCode();
    break;
    case 4:
        VoiceExample voiceExample = new(apiKey, customerId, phoneNumber);
        voiceExample.SendVoiceCall();
        voiceExample.SendVoiceCallFrench();
        voiceExample.SendVoiceCallWithVerificationCode();
    break;
    case 5:
        OmniVerifyExample omniExample = new(apiKey, customerId, phoneNumber);
        omniExample.SendOmniVerificationSmsChannel();
        Console.WriteLine("Enter reference ID to retrieve verification process (or leave blank to skip):");
        string? refId = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(refId))
        {
            omniExample.RetrieveVerificationProcess(refId.Trim());
        }
    break;
    default:
        Console.WriteLine("Bye! :)");
    break;
}
