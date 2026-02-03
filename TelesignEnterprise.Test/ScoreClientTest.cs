using System;
using System.Collections.Generic;
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
    public class ScoreClientTest : IDisposable
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

            mockHttp.When(HttpMethod.Post, "http://localhost/intelligence/phone")
                .Respond(async (request) =>
                {
                    requests.Add(request);
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                    {
                        Content = new StringContent("{}")
                    };
                });
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

        private ScoreClient CreateClient()
        {
            var httpClient = mockHttp.ToHttpClient();
            httpClient.BaseAddress = new Uri("http://localhost");

            var client = new ScoreClient(customerId, apiKey, "http://localhost", 10000, null, null, null);

            typeof(Telesign.RestClient)
                .GetField("httpClient", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.SetValue(client, httpClient);

            return client;
        }

        [Test]
        public void TestScoreClientConstructors()
        {
            var client = new ScoreClient(customerId, apiKey);
            Assert.IsNotNull(client);
        }

        [Test]
        public void TestScoreClientScore()
        {
            var client = CreateClient();

            var optionalParams = new Dictionary<string, string>
            {
                { "account_id", "acct_123" },
                { "device_id", "device_456" },
                { "email_address", "user@example.com" },
                { "external_id", "ext_789" },
                { "originating_ip", "192.168.1.1" }
            };

            client.Score(
                phoneNumber: "15555555555",
                accountLifecycleEvent: "create",
                scoreParams: optionalParams
            );

            Assert.That(requests.Count, Is.GreaterThan(0));
            var lastRequest = requests[^1];

            Assert.AreEqual(HttpMethod.Post, lastRequest.Method);
            Assert.AreEqual("/intelligence/phone", lastRequest.RequestUri?.AbsolutePath);

            Assert.AreEqual("application/x-www-form-urlencoded", lastRequest.Content.Headers.ContentType?.MediaType);
            Assert.IsTrue(lastRequest.Headers.Contains("x-ts-auth-method"));
            Assert.IsTrue(lastRequest.Headers.Contains("x-ts-nonce"));
            Assert.IsTrue(lastRequest.Headers.Contains("Date"));
            Assert.IsTrue(lastRequest.Headers.Contains("Authorization"));

            var bodyTask = lastRequest.Content.ReadAsStringAsync();
            bodyTask.Wait();
            var body = bodyTask.Result;

            Assert.IsTrue(body.Contains("phone_number=15555555555"));
            Assert.IsTrue(body.Contains("account_lifecycle_event=create"));
            Assert.IsTrue(body.Contains("account_id=acct_123"));
            Assert.IsTrue(body.Contains("device_id=device_456"));
            Assert.IsTrue(body.Contains("email_address=user%40example.com"));
            Assert.IsTrue(body.Contains("external_id=ext_789"));
            Assert.IsTrue(body.Contains("originating_ip=192.168.1.1"));
        }

        [Test]
        public async Task TestScoreClientScoreAsync()
        {
            var client = CreateClient();

            var optionalParams = new Dictionary<string, string>
            {
                { "account_id", "acct_111" },
                { "device_id", "dev_222" },
                { "email_address", "async@example.com" },
                { "external_id", "ext_333" },
                { "originating_ip", "10.0.0.1" }
            };

            await client.ScoreAsync(
                phoneNumber: "15555555555",
                accountLifecycleEvent: "update",
                scoreParams: optionalParams
            );

            Assert.That(requests.Count, Is.GreaterThan(0));
            var lastRequest = requests[^1];

            Assert.AreEqual(HttpMethod.Post, lastRequest.Method);
            Assert.AreEqual("/intelligence/phone", lastRequest.RequestUri?.AbsolutePath);

            Assert.AreEqual("application/x-www-form-urlencoded", lastRequest.Content.Headers.ContentType?.MediaType);
            Assert.IsTrue(lastRequest.Headers.Contains("x-ts-auth-method"));
            Assert.IsTrue(lastRequest.Headers.Contains("x-ts-nonce"));
            Assert.IsTrue(lastRequest.Headers.Contains("Date"));
            Assert.IsTrue(lastRequest.Headers.Contains("Authorization"));

            var body = await lastRequest.Content.ReadAsStringAsync();

            Assert.IsTrue(body.Contains("phone_number=15555555555"));
            Assert.IsTrue(body.Contains("account_lifecycle_event=update"));
            Assert.IsTrue(body.Contains("account_id=acct_111"));
            Assert.IsTrue(body.Contains("device_id=dev_222"));
            Assert.IsTrue(body.Contains("email_address=async%40example.com"));
            Assert.IsTrue(body.Contains("external_id=ext_333"));
            Assert.IsTrue(body.Contains("originating_ip=10.0.0.1"));
        }

        [Test]
        public void TestScoreClientScore_MissingPhoneNumber_ThrowsException()
        {
            var client = CreateClient();

            var ex = Assert.Throws<ArgumentException>(() =>
                client.Score(
                    phoneNumber: "",
                    accountLifecycleEvent: "create"
                )
            );

            Assert.That(ex.Message, Does.Contain("phoneNumber"));
        }

        [Test]
        public void TestScoreClientScore_MissingEvent_ThrowsException()
        {
            var client = CreateClient();

            var ex = Assert.Throws<ArgumentException>(() =>
                client.Score(
                    phoneNumber: "15555555555",
                    accountLifecycleEvent: ""
                )
            );

            Assert.That(ex.Message, Does.Contain("accountLifecycleEvent"));
        }

        [Test]
        public async Task TestScoreClientScoreAsync_MissingEvent_ThrowsException()
        {
            var client = CreateClient();

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
                await client.ScoreAsync(
                    phoneNumber: "15555555555",
                    accountLifecycleEvent: ""
                )
            );

            Assert.That(ex.Message, Does.Contain("accountLifecycleEvent"));
        }
    }
}