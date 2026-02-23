using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using _MessagingClient = Telesign.MessagingClient;

namespace TelesignEnterprise
{
    public class MessagingClient : _MessagingClient
    {
        private const string OMNI_MESSAGING_RESOURCE = "/v1/omnichannel";
        private const string CAPABILITY_RESOURCE = "/capability";
        private const string TEMPLATES_RESOURCE = "/v1/omnichannel/templates";
        public MessagingClient(string customerId,
            string apiKey)
            : base(customerId,
                apiKey,
                "https://rest-ww.telesign.com",
                "csharp_telesign_enterprise",
                Assembly.GetAssembly(typeof(MessagingClient)).GetName().Version.ToString(),
                Assembly.GetAssembly(typeof(_MessagingClient)).GetName().Version.ToString())
        { }

        public MessagingClient(string customerId,
            string apiKey,
            string restEndpoint)
            : base(customerId,
                apiKey,
                restEndpoint,
                "csharp_telesign_enterprise",
                Assembly.GetAssembly(typeof(MessagingClient)).GetName().Version.ToString(),
                Assembly.GetAssembly(typeof(_MessagingClient)).GetName().Version.ToString())
        { }

        public MessagingClient(string customerId,
            string apiKey,
            string restEndpoint,
            int timeout,
            WebProxy proxy,
            string proxyUsername,
            string proxyPassword)
            : base(customerId,
                apiKey,
                restEndpoint,
                timeout: timeout,
                proxy: proxy,
                proxyUsername: proxyUsername,
                proxyPassword: proxyPassword,
                "csharp_telesign_enterprise",
                Assembly.GetAssembly(typeof(MessagingClient)).GetName().Version.ToString(),
                Assembly.GetAssembly(typeof(_MessagingClient)).GetName().Version.ToString())
        { }
        /// <summary>
        /// Send a message to the target recipient using any of Telesign's supported channels.
        /// 
        /// See  https://developer.telesign.com/enterprise/reference/sendadvancedmessage for detailed API documentation.
        /// </summary>
        public TelesignResponse OmniMessage(Dictionary<string,object> parameters)
        {
            return Post(OMNI_MESSAGING_RESOURCE, parameters);
        }
        /// <summary>
        /// Asynchronously Send a message to the target recipient using any of Telesign's supported channels.
        /// 
        /// See  https://developer.telesign.com/enterprise/reference/sendadvancedmessage for detailed API documentation.
        /// </summary>
        public Task<TelesignResponse> OmniMessageAsync(Dictionary<string,object> parameters)
        {
            return PostAsync(OMNI_MESSAGING_RESOURCE, parameters);
        }

        /// <summary>
        /// Use this action to check capability of a phone number to use the specified channel.
        /// See https://developer.telesign.com/enterprise/reference/checkphonenumberchannelcapability for detailed API documentation.
        /// </summary>
        /// <param name="channel">The channel to check (e.g., "rcs")</param>
        /// <param name="phoneNumber">The phone number to check</param>
        public TelesignResponse CheckPhoneNumberChannelCapability(string channel, string phoneNumber)
        {
            string resource = $"{CAPABILITY_RESOURCE}/{channel}/{phoneNumber}";
            return Get(resource, new Dictionary<string, string>());
        }

        /// <summary>
        /// Asynchronously Use this action to check capability of a phone number to use the specified channel.
        /// See https://developer.telesign.com/enterprise/reference/checkphonenumberchannelcapability for detailed API documentation.
        /// </summary>
        public Task<TelesignResponse> CheckPhoneNumberChannelCapabilityAsync(string channel, string phoneNumber)
        {
            string resource = $"{CAPABILITY_RESOURCE}/{channel}/{phoneNumber}";
            return GetAsync(resource, new Dictionary<string, string>());
        }

        /// <summary>
        /// Use this action to check capability of a phone number to receive messages from the specified RBM agent.
        /// See https://developer.telesign.com/enterprise/reference/checkphonenumberrbmcapability for detailed API documentation.
        /// </summary>
        /// <param name="phoneNumber">The phone number to check</param>
        /// <param name="agentId">The RBM agent ID</param>
        public TelesignResponse CheckPhoneNumberRBMCapability(string phoneNumber, string agentId)
        {
            string resource = $"{CAPABILITY_RESOURCE}/rcs/{phoneNumber}/{agentId}";
            return Get(resource, new Dictionary<string, string>());
        }

        /// <summary>
        /// Asynchronously Use this action to check capability of a phone number to receive messages from the specified RBM agent.
        /// See https://developer.telesign.com/enterprise/reference/checkphonenumberrbmcapability for detailed API documentation.
        /// </summary>
        public Task<TelesignResponse> CheckPhoneNumberRBMCapabilityAsync(string phoneNumber, string agentId)
        {
            string resource = $"{CAPABILITY_RESOURCE}/rcs/{phoneNumber}/{agentId}";
            return GetAsync(resource, new Dictionary<string, string>());
        }

        /// <summary>
        /// Get delivery status and other details for a Telesign Messaging transaction that you have created. 
        /// Get transaction status can be used for all Messaging API transactions. 
        /// Also use this endpoint to complete verification, if Telesign generated your code.
        /// See https://developer.telesign.com/enterprise/reference/getmessagingstatus for detailed API documentation.
        /// </summary>
        /// <param name="referenceId">The reference ID of the transaction</param>
        /// <param name="parameters">Optional parameters (e.g., verify_code)</param>
        public TelesignResponse GetMessagingStatus(string referenceId, Dictionary<string, string> parameters = null)
        {
            parameters ??= new Dictionary<string, string>();
            string resource = $"{OMNI_MESSAGING_RESOURCE}/{referenceId}";
            return Get(resource, parameters);
        }

        /// <summary>
        /// Asynchronously get delivery status and other details for a Telesign Messaging transaction that you have created. 
        /// See https://developer.telesign.com/enterprise/reference/getmessagingstatus for detailed API documentation.
        /// </summary>
        public Task<TelesignResponse> GetMessagingStatusAsync(string referenceId, Dictionary<string, string> parameters = null)
        {
            parameters ??= new Dictionary<string, string>();
            string resource = $"{OMNI_MESSAGING_RESOURCE}/{referenceId}";
            return GetAsync(resource, parameters);
        }

        /// <summary>
        /// Use this action to get details for all Telesign Messaging templates associated with this Customer ID.
        /// See https://developer.telesign.com/enterprise/reference/getallmsgtemplates for detailed API documentation.
        /// </summary>
        public TelesignResponse GetAllMsgTemplates()
        {
            return Get(TEMPLATES_RESOURCE, new Dictionary<string, string>());
        }

        /// <summary>
        /// Asynchronously use this action to get details for all Telesign Messaging templates associated with this Customer ID.
        /// See https://developer.telesign.com/enterprise/reference/getallmsgtemplates for detailed API documentation.
        /// </summary>
        public Task<TelesignResponse> GetAllMsgTemplatesAsync()
        {
            return GetAsync(TEMPLATES_RESOURCE, new Dictionary<string, string>());
        }

        /// <summary>
        /// Use this action to create a Telesign Messaging template.
        /// See https://developer.telesign.com/enterprise/reference/createmsgtemplate for detailed API documentation.
        /// </summary>
        /// <param name="parameters">Dictionary containing template parameters (name, type, channel, content, etc.)</param>
        public TelesignResponse CreateMsgTemplate(Dictionary<string, object> parameters)
        {
            return Post(TEMPLATES_RESOURCE, parameters);
        }

        /// <summary>
        /// Asynchronously use this action to create a Telesign Messaging template.
        /// See https://developer.telesign.com/enterprise/reference/createmsgtemplate for detailed API documentation.
        /// </summary>
        public Task<TelesignResponse> CreateMsgTemplateAsync(Dictionary<string, object> parameters)
        {
            return PostAsync(TEMPLATES_RESOURCE, parameters);
        }

        /// <summary>
        /// Use this action to get details for the specified Telesign Messaging template.
        /// See https://developer.telesign.com/enterprise/reference/getmsgtemplate for detailed API documentation.
        /// </summary>
        /// <param name="channel">"whatsapp" or "sms" (template channel)</param>
        /// <param name="templateName">The name of the template</param>
        public TelesignResponse GetMsgTemplate(string channel, string templateName)
        {
            string resource = $"{TEMPLATES_RESOURCE}/{channel}/{templateName}";
            return Get(resource, new Dictionary<string, string>());
        }

        /// <summary>
        /// Asynchronously use this action to get details for the specified Telesign Messaging template.
        /// See https://developer.telesign.com/enterprise/reference/getmsgtemplate for detailed API documentation.
        /// </summary>
        public Task<TelesignResponse> GetMsgTemplateAsync(string channel, string templateName)
        {
            string resource = $"{TEMPLATES_RESOURCE}/{channel}/{templateName}";
            return GetAsync(resource, new Dictionary<string, string>());
        }


        /// <summary>
        /// Use this action to delete a Telesign Messaging template.
        /// See https://developer.telesign.com/enterprise/reference/deletemsgtemplate for detailed API documentation.
        /// </summary>
        /// <param name="channel">"whatsapp" or "sms" (template channel)</param>
        /// <param name="templateName">The name of the template to delete</param>
        public TelesignResponse DeleteMsgTemplate(string channel, string templateName)
        {
            string resource = $"{TEMPLATES_RESOURCE}/{channel}/{templateName}";
            return Delete(resource, new Dictionary<string, string>());
        }

        /// <summary>
        /// Asynchronously use this action to delete a Telesign Messaging template.
        /// See https://developer.telesign.com/enterprise/reference/deletemsgtemplate for detailed API documentation.
        /// </summary>
        public Task<TelesignResponse> DeleteMsgTemplateAsync(string channel, string templateName)
        {
            string resource = $"{TEMPLATES_RESOURCE}/{channel}/{templateName}";
            return DeleteAsync(resource, new Dictionary<string, string>());
        }
    }
}