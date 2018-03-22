using System.Collections.Generic;
using Telesign;

namespace TelesignEnterprise
{
    /// <summary>
    /// The Verify API delivers phone-based verification and two-factor authentication using a time-based, one-time passcode
    /// sent via SMS message, Voice call or Push Notification.
    /// </summary>
    public interface IVerifyClient
    {
        /// <summary>
        /// The SMS Verify API delivers phone-based verification and two-factor authentication using a time-based,
        /// one-time passcode sent over SMS.
        /// 
        /// See https://developer.telesign.com/docs/rest_api-verify-sms for detailed API documentation.
        /// </summary>
        RestClient.TelesignResponse Sms(string phoneNumber, Dictionary<string, string> parameters = null);

        /// <summary>
        /// The Voice Verify API delivers patented phone-based verification and two-factor authentication using a one-time
        /// passcode sent over verify_voice message.
        /// 
        /// See https://developer.telesign.com/docs/rest_api-verify-call for detailed API documentation.
        /// </summary>
        RestClient.TelesignResponse Voice(string phoneNumber, Dictionary<string, string> parameters = null);

        /// <summary>
        /// The Smart Verify web service simplifies the process of verifying user identity by integrating several TeleSign
        /// web services into a single API call. This eliminates the need for you to make multiple calls to the TeleSign
        /// Verify resource.
        /// 
        /// See https://developer.telesign.com/docs/rest_api-smart-verify for detailed API documentation.
        /// </summary>
        RestClient.TelesignResponse Smart(string phoneNumber, string ucid, Dictionary<string, string> parameters = null);

        /// <summary>
        /// The Push Verify web service allows you to provide on-device transaction authorization for your end users. It
        /// works by delivering authorization requests to your end users via push notification, and then by receiving their
        /// permission responses via their mobile device's wireless Internet connection.
        /// 
        /// See https://developer.telesign.com/docs/rest_api-verify-push for detailed API documentation.
        /// </summary>
        RestClient.TelesignResponse Push(string phoneNumber, string ucid, Dictionary<string, string> parameters = null);

        /// <summary>
        /// Retrieves the verification result for any verify resource.
        /// 
        /// See https://developer.telesign.com/docs/rest_api-verify-transaction-callback for detailed API documentation.
        /// </summary>
        RestClient.TelesignResponse Status(string referenceId, Dictionary<string, string> parameters = null);

        /// <summary>
        /// Notifies TeleSign that a verification was successfully delivered to the user in order to help improve the
        /// quality of message delivery routes.
        /// 
        /// See https://developer.telesign.com/docs/completion-service-for-verify-products for detailed API documentation.
        /// </summary>
        RestClient.TelesignResponse Completion(string referenceId, Dictionary<string, string> parameters = null);
    }
}