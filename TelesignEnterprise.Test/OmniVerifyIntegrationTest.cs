
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using TelesignEnterprise;

namespace TelesignEnterprise.Tests.Integration
{
    [TestFixture]
    [Category("Integration")] 
    public class OmniVerifyClientIntegrationTests
    {
        private const string RealCustomerId = "FFFFFFFF-EEEE-DDDD-1234-AB1234567890";
        private const string RealApiKey = "ABC12345yusumoN6BYsBVkh+yRJ5czgsnCehZaOYldPJdmFh6NeX8kunZ2zU1YWaUw/0wV6xfw==";

        private OmniVerifyClient _client;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _client = new OmniVerifyClient(RealCustomerId, RealApiKey);
        }

        //Synchronous test case for create-and-retrieve flow
        [Test, Description("Create a verification process and retrieve it synchronously")]
        public void CreateAndRetrieveVerificationProcessSuccess_Sync()
        {
            var bodyParams = new Dictionary<string, object>
            {
                { "verification_policy", new List<object>
                    {
                        new Dictionary<string, object>
                        {
                            { "method", "sms" },
                            { "fallback_time", 30 }
                        }
                    }
                }
            };
            var createResponse = _client.CreateVerificationProcess("+11234567890", bodyParams);

            Assert.NotNull(createResponse, "CreateVerificationProcess response should not be null");
            Assert.That(createResponse.StatusCode, Is.InRange(200, 299), "CreateVerificationProcess should succeed");

            Assert.IsTrue(createResponse.Json.ContainsKey("reference_id"), "Response should contain reference_id");
            string verificationId = createResponse.Json["reference_id"].ToString();

            var retrieveResponse = _client.RetrieveVerificationProcess(verificationId);

            Assert.NotNull(retrieveResponse, "RetrieveVerificationProcess response should not be null");
            Assert.That(retrieveResponse.StatusCode, Is.InRange(200, 299), "RetrieveVerificationProcess should succeed");
        }

        //Asynchronous test case for create-and-retrieve flow
        [Test, Description("Create a verification process and retrieve it asynchronously")]
        public async Task CreateAndRetrieveVerificationProcessSuccess_Async()
        {
            var bodyParams = new Dictionary<string, object>
            {
                { "verification_policy", new List<object>
                    {
                        new Dictionary<string, object>
                        {
                            { "method", "sms" },
                            { "fallback_time", 30 }
                        }
                    }
                }
            };

            // Optional delay to avoid potential rate limiting or timing issues
            await Task.Delay(5000);

            var createResponse = await _client.CreateVerificationProcessAsync("+11234567891", bodyParams);

            Assert.NotNull(createResponse, "CreateVerificationProcessAsync response should not be null");
            Assert.That(createResponse.StatusCode, Is.InRange(200, 299), "CreateVerificationProcessAsync should succeed");

            Assert.IsTrue(createResponse.Json.ContainsKey("reference_id"), "Response should contain reference_id");
            string verificationId = createResponse.Json["reference_id"].ToString();

            var retrieveResponse = await _client.RetrieveVerificationProcessAsync(verificationId);

            Assert.NotNull(retrieveResponse, "RetrieveVerificationProcessAsync response should not be null");
            Assert.That(retrieveResponse.StatusCode, Is.InRange(200, 299), "RetrieveVerificationProcessAsync should succeed");
        }

        //Negative test case for invalid ID
        [Test, Description("Retrieve verification process with invalid ID returns 400 Not Found")]
        public void RetrieveVerificationProcess_InvalidId()
        {
            string invalidVerificationId = "invalid-id-12345";

            var response = _client.RetrieveVerificationProcess(invalidVerificationId);

            Assert.NotNull(response, "Response should not be null");
            Assert.AreEqual(400, response.StatusCode, "Status code should be 400 for invalid ID");
        }
    }
}