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
    public class ScoreClientIntegrationTest
    {
        private readonly string _customerId = Environment.GetEnvironmentVariable("CUSTOMER_ID")?? "FFFFFFFF-EEEE-DDDD-1234-AB1234567890";
        private readonly string _apiKey = Environment.GetEnvironmentVariable("API_KEY") ?? "ABC12345yusumoN6BYsBVkh+yRJ5czgsnCehZaOYldPJdmFh6NeX8kunZ2zU1YWaUw/0wV6xfw==";
        private readonly string _testPhoneNumber = Environment.GetEnvironmentVariable("PHONE_NUMBER") ?? "+11234567890";
        
        private ScoreClient CreateClient() =>
            new ScoreClient(_customerId, _apiKey);

        [Test]
        public void Score_ShouldReturnValidResponse()
        {
            var client = CreateClient();

            var parameters = new Dictionary<string, string>(); 

            var response = client.Score(
                _testPhoneNumber,
                accountLifecycleEvent: "sign-in",
                scoreParams: parameters);

            Console.WriteLine($"Status Code: {response.StatusCode}");
            var json = JObject.Parse(response.Body);
            Console.WriteLine("Response Body (formatted JSON):");
            Console.WriteLine(json.ToString(Newtonsoft.Json.Formatting.Indented));

            Assert.NotNull(response);
            Assert.IsTrue(response.StatusCode == 200 || response.StatusCode == 202, "Expected status code 200 or 202");

            Assert.IsNotNull(json["risk"], "Response does not contain risk info");
            Assert.IsNotNull(json["risk"]["level"], "risk level missing");
            Assert.IsNotNull(json["risk"]["recommendation"], "risk recommendation missing");
        }

        [Test]
        public async Task ScoreAsync_ShouldReturnValidResponse()
        {
            var client = CreateClient();

            var parameters = new Dictionary<string, string>(); 
            var response = await client.ScoreAsync(
                _testPhoneNumber,
                accountLifecycleEvent: "sign-in",
                scoreParams: parameters);

            Console.WriteLine($"Status Code: {response.StatusCode}");
            var json = JObject.Parse(response.Body);
            Console.WriteLine("Response Body (formatted JSON):");
            Console.WriteLine(json.ToString(Newtonsoft.Json.Formatting.Indented));
            
            Assert.NotNull(response);
            Assert.IsTrue(response.StatusCode == 200 || response.StatusCode == 202, "Expected status code 200 or 202");

            Assert.IsNotNull(json["risk"], "Response does not contain risk info");
            Assert.IsNotNull(json["risk"]["level"], "risk level missing");
            Assert.IsNotNull(json["risk"]["recommendation"], "risk recommendation missing");
        }
    }
}