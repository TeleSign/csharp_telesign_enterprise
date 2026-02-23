using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using NUnit.Framework;
using RichardSzalay.MockHttp;
using TelesignEnterprise;

namespace TelesignEnterprise.Test
{
    [TestFixture]
    [Category("Unit")]
    public class MessagingClientTest : IDisposable
    {
        private string customerId = string.Empty;
        private string apiKey = string.Empty;
        private MockHttpMessageHandler mockHttp;
        private List<HttpRequestMessage> requests;
        private bool disposed = false;

        [SetUp]
        public void SetUp()
        {
            customerId = Environment.GetEnvironmentVariable("CUSTOMER_ID") ?? "FFFFFFFF-EEEE-DDDD-1234-AB1234567890";
            apiKey = Environment.GetEnvironmentVariable("API_KEY") ?? "ABC12345yusumoN6BYsBVkh+yRJ5czgsnCehZaOYldPJdmFh6NeX8kunZ2zU1YWaUw/0wV6xfw==";

            requests = new List<HttpRequestMessage>();
            mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Post, "http://localhost/v1/omnichannel")
                .Respond((request) =>
                {
                    requests.Add(request);
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("{\"reference_id\": \"test_ref123\"}")
                    };
                });

            mockHttp.When(HttpMethod.Get, "http://localhost/capability/rcs/11234567890")
                .Respond((request) =>
                {
                    requests.Add(request);
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("{\"rcs\": true}")
                    };
                });

            mockHttp.When(HttpMethod.Get, "http://localhost/capability/rcs/11234567890/agent123")
                .Respond((request) =>
                {
                    requests.Add(request);
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("{\"rbm_capable\": true}")
                    };
                });

            mockHttp.When(HttpMethod.Get, "http://localhost/v1/omnichannel/test_ref123")
                .Respond((request) =>
                {
                    requests.Add(request);
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("{\"status\": {\"code\": 3001}}")
                    };
                });

            mockHttp.When(HttpMethod.Get, "http://localhost/v1/omnichannel/templates")
                .Respond((request) => new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("[]") });
            mockHttp.When(HttpMethod.Post, "http://localhost/v1/omnichannel/templates")
                .Respond((request) => new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("{}") });
            mockHttp.When(HttpMethod.Get, "http://localhost/v1/omnichannel/templates/sms/test_template")
                .Respond((request) => new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("{}") });
            mockHttp.When(HttpMethod.Delete, "http://localhost/v1/omnichannel/templates/sms/test_template")
                .Respond((request) => new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("{\"status\": {\"code\": 3801}}") });
        }

        [TearDown]
        public void TearDown()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (disposed) return;
            mockHttp.Dispose();
            disposed = true;
        }

        private MessagingClient CreateClient()
        {
            var httpClient = mockHttp.ToHttpClient();
            httpClient.BaseAddress = new Uri("http://localhost");
    
            var client = new MessagingClient(customerId, apiKey, "http://localhost", 10000, null, null, null);
    
            try 
            {
                var httpClientField = typeof(Telesign.RestClient)
                    .GetField("_httpClient", BindingFlags.NonPublic | BindingFlags.Instance) 
                    ?? typeof(Telesign.RestClient)
                    .GetField("httpClient", BindingFlags.NonPublic | BindingFlags.Instance);
        
                httpClientField?.SetValue(client, httpClient);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Reflection failed: {ex.Message}");
            }
    
            return client;
        }


        [Test]
        public void TestMessagingClientConstructors()
        {
            Console.WriteLine("Running TestMessagingClientConstructors...");
            var client1 = new MessagingClient(customerId, apiKey);
            Assert.IsNotNull(client1);

            var client2 = new MessagingClient(customerId, apiKey, "https://test.telesign.com");
            Assert.IsNotNull(client2);
            Console.WriteLine("     TestMessagingClientConstructors Test PASSED");
        }

        [Test]
        public void TestOmniMessage()
        {
            Console.WriteLine("Running TestOmniMessage...");
            var client = CreateClient();
            var parameters = new Dictionary<string, object>
            {
                {"recipient.phone_number", "11234567890"},
                {"message.sms.template", "text"},
                {"message.sms.parameters.text", "Hello OmniMessaging!"},
                {"message_type", "ARN"}
            };

            client.OmniMessage(parameters);

            Assert.That(requests.Count, Is.GreaterThan(0));
            var lastRequest = requests[^1];

            Assert.AreEqual(HttpMethod.Post, lastRequest.Method);
            Assert.AreEqual("/v1/omnichannel", lastRequest.RequestUri.AbsolutePath);
            Assert.IsTrue(lastRequest.Headers.Contains("Authorization"));
            Console.WriteLine("     TestOmniMessage Test PASSED");
        }

        [Test]
        public async Task TestOmniMessageAsync()
        {
            Console.WriteLine("Running TestOmniMessageAsync...");
            var client = CreateClient();
            var parameters = new Dictionary<string, object>
            {
                {"recipient.phone_number", "11234567890"},
                {"message.rcs.template", "text"},
                {"message.rcs.parameters.text", "Hello RCS!"},
                {"message_type", "ARN"}
            };

            await client.OmniMessageAsync(parameters);

            Assert.That(requests.Count, Is.GreaterThan(0));
            var lastRequest = requests[^1];
            Assert.AreEqual(HttpMethod.Post, lastRequest.Method);
            Console.WriteLine("     TestOmniMessageAsync Test PASSED");
        }

        [Test]
        public void TestCheckPhoneNumberChannelCapability()
        {
            Console.WriteLine("Running TestCheckPhoneNumberChannelCapability...");
            var client = CreateClient();
            var response = client.CheckPhoneNumberChannelCapability("rcs", "11234567890");

            Assert.That(requests.Count, Is.GreaterThan(0));
            var lastRequest = requests[^1];
            Assert.AreEqual(HttpMethod.Get, lastRequest.Method);
            Assert.AreEqual("/capability/rcs/11234567890", lastRequest.RequestUri.AbsolutePath);
            Console.WriteLine("     TestCheckPhoneNumberChannelCapability Test PASSED");
        }

        [Test]
        public void TestCheckPhoneNumberRBMCapability()
        {
            Console.WriteLine("Running TestCheckPhoneNumberRBMCapability...");
            var client = CreateClient();
            var response = client.CheckPhoneNumberRBMCapability("11234567890", "agent123");

            Assert.That(requests.Count, Is.GreaterThan(0));
            var lastRequest = requests[^1];
            Assert.AreEqual("/capability/rcs/11234567890/agent123", lastRequest.RequestUri.AbsolutePath);
            Console.WriteLine("     TestCheckPhoneNumberRBMCapability Test PASSED");
        }

        [Test]
        public void TestGetMessagingStatus()
        {
            Console.WriteLine("Running TestGetMessagingStatus...");
            var client = CreateClient();
            var response = client.GetMessagingStatus("test_ref123");

            Assert.That(requests.Count, Is.GreaterThan(0));
            var lastRequest = requests[^1];
            Assert.AreEqual("/v1/omnichannel/test_ref123", lastRequest.RequestUri.AbsolutePath);
            Console.WriteLine("     TestGetMessagingStatus Test PASSED");
        }
    
        [Test]
        public void TestTemplateManagement()
        {
            var client = CreateClient();
    
            Console.WriteLine("Starting template tests....");
    
            try
            {
                Console.WriteLine("Running GetAllMsgTemplates()");
                var getAllResponse = client.GetAllMsgTemplates();
                Console.WriteLine($"   Get all messaging template status: {getAllResponse.StatusCode}");
                
                Console.WriteLine("Running CreateMsgTemplate()");
                var createParams = new Dictionary<string, object> 
                { 
                    {"name", "test_template"}, 
                    {"type", "standard"}, 
                    {"channel", "sms"},
                    { 
                        "content", new List<Dictionary<string, object>>
                        {
                            new Dictionary<string, object>
                            {
                                {"body", new Dictionary<string, object>
                                    {
                                        {"type", "text"},
                                        {"text", "Test {{1}}"}
                                    }
                                }
                            }
                        }
                    }
                };

                var createResponse = client.CreateMsgTemplate(createParams);
                Console.WriteLine($"   Create status: {createResponse.StatusCode}");
                
                Console.WriteLine("Running GetMsgTemplate()");
                var getResponse = client.GetMsgTemplate("sms", "test_template");
                Console.WriteLine($"   Get messaging status: {getResponse.StatusCode}");

                Console.WriteLine("Running DeleteMsgTemplate()");
                var deleteResponse = client.DeleteMsgTemplate("sms", "test_template");
                Console.WriteLine($"   Delete messaging status: {deleteResponse.StatusCode}");

                Assert.That(getAllResponse.StatusCode, Is.EqualTo(200), "GetAllMsgTemplates failed");
                Assert.That(createResponse.StatusCode, Is.EqualTo(200).Or.EqualTo(201), "CreateMsgTemplate failed");
                Assert.That(getResponse.StatusCode, Is.EqualTo(200), "GetMsgTemplate failed");
                Assert.That(deleteResponse.StatusCode, Is.EqualTo(200), "DeleteMsgTemplate failed");
        
                Console.WriteLine("All TemplateManagement Tests PASSED");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EXCEPTION: {ex.Message}");
                Console.WriteLine($"Stack: {ex.StackTrace}");
                throw; 
            }
        }
    }
}
