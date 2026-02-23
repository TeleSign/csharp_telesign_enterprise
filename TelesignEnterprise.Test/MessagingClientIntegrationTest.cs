using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using TelesignEnterprise;
using Newtonsoft.Json.Linq;

namespace TelesignEnterprise.Test
{
    [TestFixture]
    [Category("Integration")]
    public class MessagingClientIntegrationTest
    {
        private readonly string _customerId = Environment.GetEnvironmentVariable("CUSTOMER_ID") ?? "FFFFFFFF-EEEE-DDDD-1234-AB1234567890";
        private readonly string _apiKey = Environment.GetEnvironmentVariable("API_KEY") ?? "ABC12345yusumoN6BYsBVkh+yRJ5czgsnCehZaOYldPJdmFh6NeX8kunZ2zU1YWaUw/0wV6xfw==";
        private readonly string _testPhoneNumber = Environment.GetEnvironmentVariable("PHONE_NUMBER") ?? "+11234567890";

        private MessagingClient CreateClient() => new MessagingClient(_customerId, _apiKey);

        [Test]
        public void OmniMessage_ShouldReturnValidResponse()
        {
            var client = CreateClient();
            
            var parameters = new Dictionary<string, object>
            {
                {
                    "message", new Dictionary<string, object>
                    {
                        {
                            "sms", new Dictionary<string, object>
                            {
                                { "template", "text" },
                                { 
                                    "parameters", new Dictionary<string, string>
                                    {
                                        { "text", "OmniMessage_ShouldReturnValidResponse integration test success" }
                                    }
                                }
                            }
                        }
                    }
                },
                {
                    "recipient", new Dictionary<string, string>
                    {
                        { "phone_number", _testPhoneNumber }
                    }
                },
                {
                    "channels", new List<Dictionary<string, object>>
                    {
                        new Dictionary<string, object>
                        {
                            { "channel", "sms" },
                            { "fallback_time", 300 }
                        }
                    }
                },
                { "message_type", "ARN" }
            };

            var response = client.OmniMessage(parameters);

            Console.WriteLine($"OmniMessage Status: {response.StatusCode}");
            Console.WriteLine($"Response: {response.Body}");

            Assert.AreEqual(200, response.StatusCode, "Expected 200/202");

            var json = JObject.Parse(response.Body);
            Assert.AreEqual(3001, (int)json["status"]["code"], "Expected 'Message in progress'");
            Assert.IsNotNull(json["reference_id"], "Expected reference_id");
            Assert.IsNotEmpty(json["reference_id"].ToString(), "reference_id should not be empty");
        }

        [Test]
        public async Task OmniMessageAsync_ShouldReturnValidResponse()
        {
            var client = CreateClient();
            
            var parameters = new Dictionary<string, object>
            {
                {
                    "message", new Dictionary<string, object>
                    {
                        {
                            "sms", new Dictionary<string, object>
                            {
                                { "template", "text" },
                                { 
                                    "parameters", new Dictionary<string, string>
                                    {
                                        { "text", "Async OmniMessageAsync_ShouldReturnValidResponse integration test success" }
                                    }
                                }
                            }
                        }
                    }
                },
                {
                    "recipient", new Dictionary<string, string>
                    {
                        { "phone_number", _testPhoneNumber }
                    }
                },
                {
                    "channels", new List<Dictionary<string, object>>
                    {
                        new Dictionary<string, object>
                        {
                            { "channel", "sms" },
                            { "fallback_time", 300 }
                        }
                    }
                },
                { "message_type", "ARN" }
            };

            var response = await client.OmniMessageAsync(parameters);

            Console.WriteLine($"Async Status: {response.StatusCode}");
            Assert.AreEqual(200, response.StatusCode);

            var json = JObject.Parse(response.Body);
            Assert.AreEqual(3001, (int)json["status"]["code"]);
            Assert.IsNotNull(json["reference_id"]);
        }

        [Test]
        public void CheckChannelCapability_ShouldReturnValidResponse()
        {
            var client = CreateClient();
            
            var response = client.CheckPhoneNumberChannelCapability("rcs", _testPhoneNumber); 

            Console.WriteLine($"RCS Status: {response.StatusCode}");
            Console.WriteLine($"RCS Body: {response.Body}");

            if (response.StatusCode == 200)
            {
                AssertChannelCapabilityResponse(response.Body, "RCS");
            }
            else
            {
                Console.WriteLine($"RCS HTTP Error{response.StatusCode} (network/API issue/carrer rejected)");
                Console.WriteLine($"Response: {response.Body}");
            }
        }

        [Test]
        public void CheckPhoneNumberRBMCapability_ShouldReturnValidResponse()
        {
            var client = CreateClient();
            string rbmAgentId = Environment.GetEnvironmentVariable("RBM_AGENT_ID") ?? "test_rbm_agent_12345";//To be replaced with real RBM_AGENT_ID for 200 response


            Console.WriteLine($"RBM Agent: {_testPhoneNumber} / {rbmAgentId}");

            var response = client.CheckPhoneNumberRBMCapability(_testPhoneNumber, rbmAgentId);

            Console.WriteLine($"Status: {response.StatusCode}");
            Console.WriteLine($"Response: {response.Body}");
    
            if (response.StatusCode == 200)
            {
                AssertChannelCapabilityResponse(response.Body, "RCS");
            }
            else
            {
                Console.WriteLine($"RBM HTTP Error{response.StatusCode} (network/API issue/Invalid sender id)");
                Console.WriteLine($"Response: {response.Body}");
            }
        }

        [Test]
        public void ManageTemplates_ShouldExecuteEndpoints()
        {
            var client = CreateClient();
            string channel = "sms";
            string templateName = $"template_test_{DateTime.Now:yyMMddHHmmss}"; 

            Console.WriteLine($"Testing Templates: {channel}/{templateName}");

            var getAllResponse = client.GetAllMsgTemplates();
            Console.WriteLine($"Get all messaging templates: {getAllResponse.StatusCode}");
            Console.WriteLine($"   Create messaging template response: {getAllResponse.Body}");

            System.Threading.Thread.Sleep(3000);

            var createParams = new Dictionary<string, object>
            {
                {"name", templateName},
                {"type", "standard"},                  
                {"channel", channel},                                 
                { 
                    "content", new List<Dictionary<string, object>>
                    {
                        new Dictionary<string, object>
                        {
                            {
                                "body", new Dictionary<string, object>
                                {
                                    {"type", "text"},                           
                                    {"text", $"Your test order {{1}} at {DateTime.Now:HH:mm:ss}."} 
                                }
                            }
                        }
                    }
                }
            };
            var createResponse = client.CreateMsgTemplate(createParams);
            Console.WriteLine($"Create template: {createResponse.StatusCode}"); 
            Console.WriteLine($"   Create messaging template response: {createResponse.Body}");

            System.Threading.Thread.Sleep(1000);

            var getResponse = client.GetMsgTemplate(channel, templateName);
            Console.WriteLine($"Get Messaging template: {getResponse.StatusCode}");
            Console.WriteLine($"   Get messaging template response: {getResponse.Body}");

            System.Threading.Thread.Sleep(3000);

            var deleteResponse = client.DeleteMsgTemplate(channel, templateName);
            Console.WriteLine($"Delete template: {deleteResponse.StatusCode}");
            Console.WriteLine($"   Delete messaging template response: {deleteResponse.Body}");

            Assert.IsTrue(getAllResponse.StatusCode >= 200 && getAllResponse.StatusCode < 500);
            Assert.IsTrue(createResponse.StatusCode >= 200 && createResponse.StatusCode < 500);
            Assert.IsTrue(getResponse.StatusCode >= 200 && getResponse.StatusCode < 500);
            Assert.IsTrue(deleteResponse.StatusCode >= 200 && deleteResponse.StatusCode < 500);
        }

        private void AssertChannelCapabilityResponse(string responseBody, string testName)
        {
            var json = JObject.Parse(responseBody);
            var statusCode = (int)json["status"]["code"];
            var description = json["status"]["description"].ToString();

            switch (statusCode)
            {       
                case 3055:
                    Assert.IsTrue(description.Contains("Delivery channel not supported by the end user's device"));
                    Console.WriteLine($"{testName}: 3055 - Device doesn't support channel");
                break;
        
                case 3060:
                    Assert.IsTrue(description.Contains("Delivery channel supported by the end user's device"));
                    Console.WriteLine($"{testName}: 3060 - Device supports channel");
                break;
        
                case 3115:
                    Assert.IsTrue(description.Contains("Destination phone number not in coverage"));
                    Console.WriteLine($"{testName}: 3115 - Device supported but not in coverage");
                break;
        
                default:
                    Assert.Fail($"Unexpected status code {statusCode}: {description}");
                break;
            }
        } 
    }
}
