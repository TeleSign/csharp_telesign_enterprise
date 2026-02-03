using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RichardSzalay.MockHttp;
using TelesignEnterprise;

namespace TelesignEnterprise.Tests.Unit
{
    [TestFixture]
    [Category("Unit")] 
    public class OmniVerifyClientTest
    {
        private readonly string TestCustomerId = Environment.GetEnvironmentVariable("CUSTOMER_ID")?? "FFFFFFFF-EEEE-DDDD-1234-AB1234567890";
        private readonly string TestApiKey = Environment.GetEnvironmentVariable("API_KEY") ?? "ABC12345yusumoN6BYsBVkh+yRJ5czgsnCehZaOYldPJdmFh6NeX8kunZ2zU1YWaUw/0wV6xfw==";
        private const string TestVerificationId = "123456";
        private const string BaseUrl = "https://verify.telesign.com";
        private MockHttpMessageHandler _mockHttp;
        private OmniVerifyClient _client;

        [SetUp]
        public void SetUp()
        {
            _mockHttp = new MockHttpMessageHandler();

            // Patch RestClient to accept custom HttpClient
            var httpClient = _mockHttp.ToHttpClient();
            httpClient.BaseAddress = new Uri(BaseUrl);

            // Use reflection to inject the mock HttpClient into the RestClient base class
            _client = new OmniVerifyClient(TestCustomerId, TestApiKey, BaseUrl);
            typeof(Telesign.RestClient)
                .GetField("httpClient", BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(_client, httpClient);
        }

        [Test]
        public void CreateVerificationProcess_CallsPostWithCorrectParameters()
        {
            var parameters = new Dictionary<string, object>();
            _mockHttp.When(HttpMethod.Post, $"{BaseUrl}/verification")
            .Respond("application/json", "{\"reference_id\":\"123456\"}");

            var response = _client.CreateVerificationProcess("1234567890", parameters);

            Assert.IsNotNull(response);
            Assert.That(response.StatusCode, Is.EqualTo(200).Or.EqualTo(201));
            Assert.That(response.Json["reference_id"]?.ToString(), Is.EqualTo(TestVerificationId));
        }


        [Test]
        public async Task CreateVerificationProcessAsync_CallsPostAsyncWithCorrectParameters()
        {
            var parameters = new Dictionary<string, object>();
            _mockHttp.When(HttpMethod.Post, $"{BaseUrl}/verification")
            .Respond("application/json", "{\"reference_id\":\"123456\"}");

            var response = await _client.CreateVerificationProcessAsync("1234567890", parameters);

            Assert.IsNotNull(response);
            Assert.That(response.StatusCode, Is.EqualTo(200).Or.EqualTo(201));
            Assert.That(response.Json["reference_id"]?.ToString(), Is.EqualTo(TestVerificationId));
        }


        [Test]
        public void RetrieveVerificationProcess_CallsGetWithCorrectParameters()
        {
            _mockHttp.When(HttpMethod.Get, $"{BaseUrl}/verification/{TestVerificationId}")
                .Respond("application/json", "{\"status\":\"SUCCESS\"}");

            var response = _client.RetrieveVerificationProcess(TestVerificationId);

            Assert.IsNotNull(response);
            Assert.That(response.StatusCode, Is.EqualTo(200));
            Assert.That(response.Json["status"]?.ToString(), Is.EqualTo("SUCCESS"));
        }

        [Test]
        public async Task RetrieveVerificationProcessAsync_CallsGetAsyncWithCorrectParameters()
        {
            _mockHttp.When(HttpMethod.Get, $"{BaseUrl}/verification/{TestVerificationId}")
                .Respond("application/json", "{\"status\":\"SUCCESS\"}");

            var response = await _client.RetrieveVerificationProcessAsync(TestVerificationId);

            Assert.IsNotNull(response);
            Assert.That(response.StatusCode, Is.EqualTo(200));
            Assert.That(response.Json["status"]?.ToString(), Is.EqualTo("SUCCESS"));
        }

        [Test]
        public void RetrieveVerificationProcess_InvalidId_Returns404()
        {
            _mockHttp.When(HttpMethod.Get, $"{BaseUrl}/verification/invalid")
                .Respond(HttpStatusCode.NotFound, "application/json", "{\"error\":\"Not found\"}");

            var response = _client.RetrieveVerificationProcess("invalid");

            Assert.IsNotNull(response);
            Assert.That(response.StatusCode, Is.EqualTo(404));
            Assert.That(response.Json["error"]?.ToString(), Is.EqualTo("Not found"));
        }

        [Test]
        public void RetrieveVerificationProcess_ThrowsOnNullId()
        {
            Assert.Throws<AggregateException>(() => _client.RetrieveVerificationProcess(null));
        }
    }
}
