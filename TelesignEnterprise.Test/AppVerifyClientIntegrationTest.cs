using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using TelesignEnterprise;

namespace TelesignEnterprise.Tests
{
    [TestFixture]
    [Category("Integration")]
    public class AppVerifyClientIntegrationTest
    {
        private readonly string _customerId = Environment.GetEnvironmentVariable("CUSTOMER_ID")?? "FFFFFFFF-EEEE-DDDD-1234-AB1234567890";
        private readonly string _apiKey = Environment.GetEnvironmentVariable("API_KEY") ?? "ABC12345yusumoN6BYsBVkh+yRJ5czgsnCehZaOYldPJdmFh6NeX8kunZ2zU1YWaUw/0wV6xfw==";
        private readonly string _testPhoneNumber = Environment.GetEnvironmentVariable("PHONE_NUMBER") ?? "+11234567890";

        private AppVerifyClient CreateClient() =>
            new AppVerifyClient(_customerId, _apiKey);

        [Test]
        public void Initiate_ShouldReturnValidResponse()
        {
            var client = CreateClient();

            var response = client.Initiate(_testPhoneNumber);

            Console.WriteLine($"Status Code: {response.StatusCode}");
            Console.WriteLine($"Response Body: {Environment.NewLine + response.Body}");

            Assert.NotNull(response);
            Assert.AreEqual(200, response.StatusCode, "Expected HTTP status 200");

            var json = JObject.Parse(response.Body);
            string referenceId = json["reference_id"]?.ToString();
            Assert.IsFalse(string.IsNullOrEmpty(referenceId), "reference_id is null or empty");

            var statusCode = json["status"]?["code"]?.ToObject<int>() ?? -1;
            Assert.IsTrue(statusCode == 2400, $"Unexpected status.code: {statusCode}"); // 2400 = Pending
        }

        [Test]
        public async Task InitiateAsync_ShouldReturnValidResponse()
        {
            var client = CreateClient();

            var response = await client.InitiateAsync(_testPhoneNumber);

            Console.WriteLine($"Status Code: {response.StatusCode}");
            Console.WriteLine($"Response Body: {Environment.NewLine + response.Body}");

            Assert.NotNull(response);
            Assert.AreEqual(200, response.StatusCode, "Expected HTTP status 200");

            var json = JObject.Parse(response.Body);
            string referenceId = json["reference_id"]?.ToString();
            Assert.IsFalse(string.IsNullOrEmpty(referenceId), "reference_id is null or empty");

            var statusCode = json["status"]?["code"]?.ToObject<int>() ?? -1;
            Assert.IsTrue(statusCode == 2400, $"Unexpected status.code: {statusCode}");
        }

        [Test]
        public void Finalize_ShouldReturnValidResponse()
        {
            var client = CreateClient();

            string referenceId = GetValidReferenceId(client);

            var response = client.Finalize(referenceId);

            Console.WriteLine($"Status Code: {response.StatusCode}");
            Console.WriteLine($"Response Body: {Environment.NewLine + response.Body}");

            Assert.NotNull(response);
            Assert.AreEqual(200, response.StatusCode);

            var json = JObject.Parse(response.Body);
            var statusCode = json["status"]?["code"]?.ToObject<int>() ?? -1;
            Assert.IsTrue(statusCode == 2401, $"Unexpected status.code: {statusCode}"); // 2401 = Success
        }

        [Test]
        public async Task FinalizeAsync_ShouldReturnValidResponse()
        {
            var client = CreateClient();

            string referenceId = GetValidReferenceId(client);

            var response = await client.FinalizeAsync(referenceId);

            Console.WriteLine($"Status Code: {response.StatusCode}");
            Console.WriteLine($"Response Body: {Environment.NewLine + response.Body}");

            Assert.NotNull(response);
            Assert.AreEqual(200, response.StatusCode);

            var json = JObject.Parse(response.Body);
            var statusCode = json["status"]?["code"]?.ToObject<int>() ?? -1;
            Assert.IsTrue(statusCode == 2401, $"Unexpected status.code: {statusCode}");
        }

        [Test]
        public void ReportUnknownCallerId_ShouldReturnValidResponse()
        {
            var client = CreateClient();

            string referenceId = GetValidReferenceId(client);
            string unknownCallerId = "123456789";
            var response = client.ReportUnknownCallerId(referenceId, unknownCallerId);

            Console.WriteLine($"Status Code: {response.StatusCode}");
            Console.WriteLine($"Response Body: {Environment.NewLine + response.Body}");

            Assert.NotNull(response);
            Assert.AreEqual(200, response.StatusCode);

            var json = JObject.Parse(response.Body);
            var statusCode = json["status"]?["code"]?.ToObject<int>() ?? -1;
            Assert.IsTrue(statusCode == 2407, $"Unexpected status.code: {statusCode}"); // 2407 = Caller ID prefix mismatch
        }

        [Test]
        public async Task ReportUnknownCallerIdAsync_ShouldReturnValidResponse()
        {
            var client = CreateClient();

            string referenceId = GetValidReferenceId(client);
            string unknownCallerId = "123456789";

            var response = await client.ReportUnknownCallerIdAsync(referenceId, unknownCallerId);

            Console.WriteLine($"Status Code: {response.StatusCode}");
            Console.WriteLine($"Response Body: {Environment.NewLine + response.Body}");

            Assert.NotNull(response);
            Assert.AreEqual(200, response.StatusCode);

            var json = JObject.Parse(response.Body);
            var statusCode = json["status"]?["code"]?.ToObject<int>() ?? -1;
            Assert.IsTrue(statusCode == 2407, $"Unexpected status.code: {statusCode}");
        }

        [Test]
        public void ReportTimeout_ShouldReturnValidResponse()
        {
            var client = CreateClient();

            string referenceId = GetValidReferenceId(client);

            var response = client.ReportTimeout(referenceId);

            Console.WriteLine($"Status Code: {response.StatusCode}");
            Console.WriteLine($"Response Body: {Environment.NewLine + response.Body}");

            Assert.NotNull(response);
            Assert.AreEqual(200, response.StatusCode);

            var json = JObject.Parse(response.Body);
            var statusCode = json["status"]?["code"]?.ToObject<int>() ?? -1;
            Assert.IsTrue(statusCode == 2409, $"Unexpected status.code: {statusCode}"); // 2409 = Verification call not received
        }

        [Test]
        public async Task ReportTimeoutAsync_ShouldReturnValidResponse()
        {
            var client = CreateClient();

            string referenceId = GetValidReferenceId(client);

            var response = await client.ReportTimeoutAsync(referenceId);

            Console.WriteLine($"Status Code: {response.StatusCode}");
            Console.WriteLine($"Response Body: {Environment.NewLine + response.Body}");

            Assert.NotNull(response);
            Assert.AreEqual(200, response.StatusCode);

            var json = JObject.Parse(response.Body);
            var statusCode = json["status"]?["code"]?.ToObject<int>() ?? -1;
            Assert.IsTrue(statusCode == 2409, $"Unexpected status.code: {statusCode}");
        }

        [Test]
        public void GetTransactionStatus_ShouldReturnValidResponse()
        {
            var client = CreateClient();

            string referenceId = GetValidReferenceId(client);

            var response = client.GetTransactionStatus(referenceId);

            Console.WriteLine($"Status Code: {response.StatusCode}");
            Console.WriteLine($"Response Body: {Environment.NewLine + response.Body}");

            Assert.NotNull(response);
            Assert.AreEqual(200, response.StatusCode);

            var json = JObject.Parse(response.Body);
            var statusCode = json["status"]?["code"]?.ToObject<int>() ?? -1;
            Assert.IsTrue(statusCode == 2401 || statusCode == 2400, $"Unexpected status.code: {statusCode}"); // Success or Pending
        }

        [Test]
        public async Task GetTransactionStatusAsync_ShouldReturnValidResponse()
        {
            var client = CreateClient();

            string referenceId = GetValidReferenceId(client);

            var response = await client.GetTransactionStatusAsync(referenceId);

            Console.WriteLine($"Status Code: {response.StatusCode}");
            Console.WriteLine($"Response Body: {Environment.NewLine + response.Body}");

            Assert.NotNull(response);
            Assert.AreEqual(200, response.StatusCode);

            var json = JObject.Parse(response.Body);
            var statusCode = json["status"]?["code"]?.ToObject<int>() ?? -1;
            Assert.IsTrue(statusCode == 2401 || statusCode == 2400, $"Unexpected status.code: {statusCode}");
        }

        /// <summary>
        /// Helper method to get a valid reference ID by calling Initiate.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        private string GetValidReferenceId(AppVerifyClient client)
        {
            var response = client.Initiate(_testPhoneNumber);
            if (response.StatusCode != 200)
            {
                throw new Exception($"Failed to initiate verification: HTTP {response.StatusCode}");
            }

            var json = JObject.Parse(response.Body);
            string referenceId = json["reference_id"]?.ToString();

            if (string.IsNullOrEmpty(referenceId))
            {
                throw new Exception("reference_id is null or empty");
            }

            return referenceId;
        }
    }
}