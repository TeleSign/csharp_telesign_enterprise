
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Telesign;
using System.Reflection;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;

namespace TelesignEnterprise
{
    public class OmniVerifyClient : RestClient
    {
        private const string OMNIVERIFY_PROCESS_CREATE_RESOURCE = "/verification";
        private const string OMNIVERIFY_PROCESS_RETRIEVE_RESOURCE = "/verification/{0}";
        private const string OMNIVERIFY_PROCESS_UPDATE_RESOURCE = "/verification/{0}";
        private const string OMNIVERIFY_PROCESS_UPDATE_STATE_RESOURCE = "/verification/{0}/state";


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

        public TelesignResponse UpdateVerificationProcess(string verificationId, Dictionary<string, object> bodyParams)
        {
            string resource = string.Format(OMNIVERIFY_PROCESS_UPDATE_RESOURCE, verificationId);
            return this.Patch(resource, bodyParams);
        }

        public Task<TelesignResponse> UpdateVerificationProcessAsync(string verificationId, Dictionary<string, object> bodyParams)
        {
            string resource = string.Format(OMNIVERIFY_PROCESS_UPDATE_RESOURCE, verificationId);
            return this.PatchAsync(resource, bodyParams);
        }
        
        public TelesignResponse UpdateVerificationProcessStateBasicAuth(string verificationId, string action, string securityFactor)
        {
            string resource = string.Format(OMNIVERIFY_PROCESS_UPDATE_STATE_RESOURCE, verificationId);
            string endpoint = restEndpoint.TrimEnd('/') + resource;

            var bodyParams = new Dictionary<string, object>
            {
                { "action", action },
                { "security_factor", securityFactor }
            };
            string jsonBody = JsonConvert.SerializeObject(bodyParams);

            var request = new HttpRequestMessage(new HttpMethod("PATCH"), endpoint)
            {
                Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
            };

            var authValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{customerId}:{apiKey}"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", authValue);
            request.Headers.UserAgent.ParseAdd(userAgent);

            using (var response = httpClient.SendAsync(request).Result)
            {
                return new TelesignResponse(response);
            }
        }

        public async Task<TelesignResponse> UpdateVerificationProcessStateBasicAuthAsync(string verificationId, string action, string securityFactor)
        {
            string resource = string.Format(OMNIVERIFY_PROCESS_UPDATE_STATE_RESOURCE, verificationId);
            string endpoint = restEndpoint.TrimEnd('/') + resource;

            var bodyParams = new Dictionary<string, object>
            {
                { "action", action },
                { "security_factor", securityFactor }
            };
            string jsonBody = JsonConvert.SerializeObject(bodyParams);

            var request = new HttpRequestMessage(new HttpMethod("PATCH"), endpoint)
            {
                Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
            };

            var authValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{customerId}:{apiKey}"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", authValue);
            request.Headers.UserAgent.ParseAdd(userAgent);

            using (var response = await httpClient.SendAsync(request))
            {
                return new TelesignResponse(response);
            }
        }
    }
}