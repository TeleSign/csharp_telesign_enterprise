using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using TelesignEnterprise;
using Newtonsoft.Json.Linq;

namespace TelesignEnterprise.Tests
{
    [TestFixture]
    [Category("Integration")]
    public class PhoneIdClientIntegrationTest
    {
        private readonly string _customerId = Environment.GetEnvironmentVariable("CUSTOMER_ID")?? "FFFFFFFF-EEEE-DDDD-1234-AB1234567890";
        private readonly string _apiKey = Environment.GetEnvironmentVariable("API_KEY") ?? "ABC12345yusumoN6BYsBVkh+yRJ5czgsnCehZaOYldPJdmFh6NeX8kunZ2zU1YWaUw/0wV6xfw==";
        private readonly string _testPhoneNumber = Environment.GetEnvironmentVariable("PHONE_NUMBER") ?? "+11234567890";

        private PhoneIdClient CreateClient() =>
            new PhoneIdClient(_customerId, _apiKey);

        [Test]
        public void PhoneIdPath_ShouldReturnValidResponse()
        {
            var client = CreateClient();

            var parameters = new Dictionary<string, object>
            {
                { "consent", new Dictionary<string, int> { { "method", 1 } } },
                { "account_lifecycle_event", "sign-in" },
                { "external_id", "test" },
                { "originating_ip", "203.0.113.42" }
            };

            var response = client.PhoneIdPath(_testPhoneNumber, parameters);

            Console.WriteLine($"Status Code: {response.StatusCode}");
            Console.WriteLine($"Response Body: {response.Body}");

            Assert.NotNull(response);
            Assert.AreEqual(200, response.StatusCode, "Expected HTTP status 200");

            var json = JObject.Parse(response.Body);
            int statusCode = json["status"]["code"].ToObject<int>();
            Assert.IsTrue(statusCode == 200 || statusCode == 300, $"Unexpected status.code: {statusCode}");

            string referenceId = json["reference_id"]?.ToString();
            Assert.IsFalse(string.IsNullOrEmpty(referenceId), "reference_id is null or empty");
            Assert.IsNotNull(json["phone_type"], "Response does not contain phone_type");
        }

        [Test]
        public async Task PhoneIdPathAsync_ShouldReturnValidResponse()
        {
            var client = CreateClient();

            var parameters = new Dictionary<string, object>
            {
                { "consent", new Dictionary<string, int> { { "method", 1 } } },
                { "account_lifecycle_event", "sign-in" },
                { "external_id", "test" },
                { "originating_ip", "203.0.113.42" }
            };

            var response = await client.PhoneIdPathAsync(_testPhoneNumber, parameters);

            Console.WriteLine($"Status Code: {response.StatusCode}");
            Console.WriteLine($"Response Body: {response.Body}");

            Assert.NotNull(response);
            Assert.AreEqual(200, response.StatusCode, "Expected HTTP status 200");

            var json = JObject.Parse(response.Body);

            int statusCode = json["status"]["code"].ToObject<int>();
            Assert.IsTrue(statusCode == 200 || statusCode == 300, $"Unexpected status.code: {statusCode}");

            string referenceId = json["reference_id"]?.ToString();
            Assert.IsFalse(string.IsNullOrEmpty(referenceId), "reference_id is null or empty");

            Assert.IsNotNull(json["phone_type"], "Response does not contain phone_type");
        }

        [Test]
        public void PhoneIdBody_ShouldReturnValidResponse()
        {
            var client = CreateClient();

            var parameters = new Dictionary<string, object>
            {
                { "consent", new Dictionary<string, int> { { "method", 1 } } }
            };

            var response = client.PhoneIdBody(_testPhoneNumber, parameters);

            Console.WriteLine($"Status Code: {response.StatusCode}");
            Console.WriteLine($"Response Body: {response.Body}");

            Assert.NotNull(response);
            Assert.AreEqual(200, response.StatusCode, "Expected HTTP status 200");

            var json = JObject.Parse(response.Body);

            int statusCode = json["status"]["code"].ToObject<int>();
            Assert.IsTrue(statusCode == 200 || statusCode == 300, $"Unexpected status.code: {statusCode}");

            string referenceId = json["reference_id"]?.ToString();
            Assert.IsFalse(string.IsNullOrEmpty(referenceId), "reference_id is null or empty");

            Assert.IsNotNull(json["phone_type"], "Response does not contain phone_type");
        }

        [Test]
        public async Task PhoneIdBodyAsync_ShouldReturnValidResponse()
        {
            var client = CreateClient();

            var parameters = new Dictionary<string, object>
            {
                { "consent", new Dictionary<string, int> { { "method", 1 } } }
            };

            var response = await client.PhoneIdBodyAsync(_testPhoneNumber, parameters);

            Console.WriteLine($"Status Code: {response.StatusCode}");
            Console.WriteLine($"Response Body: {response.Body}");

            Assert.NotNull(response);
            Assert.AreEqual(200, response.StatusCode, "Expected HTTP status 200");

            var json = JObject.Parse(response.Body);

            int statusCode = json["status"]["code"].ToObject<int>();
            Assert.IsTrue(statusCode == 200 || statusCode == 300, $"Unexpected status.code: {statusCode}");

            string referenceId = json["reference_id"]?.ToString();
            Assert.IsFalse(string.IsNullOrEmpty(referenceId), "reference_id is null or empty");

            Assert.IsNotNull(json["phone_type"], "Response does not contain phone_type");
        }
    }
}
