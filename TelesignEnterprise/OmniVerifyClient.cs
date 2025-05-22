using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Telesign;
using System.Reflection;

namespace TelesignEnterprise
{
    public class OmniVerifyClient : RestClient
    {
        private const string OMNIVERIFY_PROCESS_CREATE_RESOURCE = "/verification";
        private const string OMNIVERIFY_PROCESS_RETRIEVE_RESOURCE = "/verification/{0}";

        public OmniVerifyClient(string customerId, string apiKey)
            : base(customerId, 
                apiKey, restEndpoint: "https://verify.telesign.com",
                source: "csharp_telesign_enterprise",
                sdkVersionOrigin: Assembly.GetAssembly(typeof(OmniVerifyClient)).GetName().Version.ToString(),
                sdkVersionDependency: Assembly.GetAssembly(typeof(RestClient)).GetName().Version.ToString())
        { }

        public OmniVerifyClient(string customerId, string apiKey, string restEndpoint)
            : base(customerId, 
                apiKey, 
                restEndpoint,
                source: "csharp_telesign_enterprise",
                sdkVersionOrigin: Assembly.GetAssembly(typeof(OmniVerifyClient)).GetName().Version.ToString(),
                sdkVersionDependency: Assembly.GetAssembly(typeof(RestClient)).GetName().Version.ToString())
        { }

        public OmniVerifyClient(string customerId, string apiKey, string restEndpoint,
                                int timeout, WebProxy proxy, string proxyUsername, string proxyPassword)
            : base(customerId, 
                apiKey, 
                restEndpoint, 
                timeout: timeout,
                proxy: proxy, 
                proxyUsername: proxyUsername, 
                proxyPassword: proxyPassword,
                source: "csharp_telesign_enterprise",
                sdkVersionOrigin: Assembly.GetAssembly(typeof(OmniVerifyClient)).GetName().Version.ToString(),
                sdkVersionDependency: Assembly.GetAssembly(typeof(RestClient)).GetName().Version.ToString())
        { }

        public TelesignResponse CreateVerificationProcess(Dictionary<string, object> bodyParams)
        {
            return this.Post(OMNIVERIFY_PROCESS_CREATE_RESOURCE, bodyParams);
        }   

        public Task<TelesignResponse> CreateVerificationProcessAsync(Dictionary<string, object> bodyParams)
        {
            return this.PostAsync(OMNIVERIFY_PROCESS_CREATE_RESOURCE, bodyParams);
        }

        public TelesignResponse RetrieveVerificationProcess(string verificationId, Dictionary<string, string> parameters = null)
        {
            return this.Get(string.Format(OMNIVERIFY_PROCESS_RETRIEVE_RESOURCE, verificationId), parameters);
        }

        public Task<TelesignResponse> RetrieveVerificationProcessAsync(string verificationId, Dictionary<string, string> parameters = null)
        {
            return this.GetAsync(string.Format(OMNIVERIFY_PROCESS_RETRIEVE_RESOURCE, verificationId), parameters);
        }
    }
}