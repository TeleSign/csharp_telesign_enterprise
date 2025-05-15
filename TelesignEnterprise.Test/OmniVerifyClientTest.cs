using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using TelesignEnterprise;
using Xunit;
using Telesign;

public class OmniVerifyClientTests
{
    private const string TestCustomerId = "test_customer_id";
    private const string TestApiKey = "test_api_key";
    private const string TestVerificationId = "123456";
    private const string RetrieveEndpoint = "/verification/123456";
    private const string CreateEndpoint = "/verification";

    [Fact]
    public void RetrieveVerificationProcess_CallsGetWithCorrectParameters()
    {
        // Arrange
        var parameters = new Dictionary<string, string> { { "foo", "bar" } };
        var expectedResponse = new TelesignResponse();

        var mockClient = new Mock<OmniVerifyClient>(TestCustomerId, TestApiKey) { CallBase = true };
        mockClient.Setup(c => c.Get(RetrieveEndpoint, parameters)).Returns(expectedResponse);

        // Act
        var response = mockClient.Object.RetrieveVerificationProcess(TestVerificationId, parameters);

        // Assert
        Assert.Equal(expectedResponse, response);
        mockClient.Verify(c => c.Get(RetrieveEndpoint, parameters), Times.Once);
    }

    [Fact]
    public async Task RetrieveVerificationProcessAsync_CallsGetAsyncWithCorrectParameters()
    {
        // Arrange
        var parameters = new Dictionary<string, string> { { "foo", "bar" } };
        var expectedResponse = new TelesignResponse();

        var mockClient = new Mock<OmniVerifyClient>(TestCustomerId, TestApiKey) { CallBase = true };
        mockClient.Setup(c => c.GetAsync(RetrieveEndpoint, parameters)).ReturnsAsync(expectedResponse);

        // Act
        var response = await mockClient.Object.RetrieveVerificationProcessAsync(TestVerificationId, parameters);

        // Assert
        Assert.Equal(expectedResponse, response);
        mockClient.Verify(c => c.GetAsync(RetrieveEndpoint, parameters), Times.Once);
    }

    [Fact]
    public void RetrieveVerificationProcess_ThrowsOnNullId()
    {
        var mockClient = new Mock<OmniVerifyClient>(TestCustomerId, TestApiKey) { CallBase = true };
        Assert.Throws<ArgumentNullException>(() => mockClient.Object.RetrieveVerificationProcess(null));
    }

    [Fact]
    public void CreateVerificationProcess_CallsPostWithCorrectParameters()
    {
        // Arrange
        var bodyParams = new Dictionary<string, object> { { "phone_number", "1234567890" } };
        var expectedResponse = new TelesignResponse();

        var mockClient = new Mock<OmniVerifyClient>(TestCustomerId, TestApiKey) { CallBase = true };
        mockClient.Setup(c => c.Post(CreateEndpoint, bodyParams)).Returns(expectedResponse);

        // Act
        var response = mockClient.Object.CreateVerificationProcess(bodyParams);

        // Assert
        Assert.Equal(expectedResponse, response);
        mockClient.Verify(c => c.Post(CreateEndpoint, bodyParams), Times.Once);
    }

    [Fact]
    public async Task CreateVerificationProcessAsync_CallsPostAsyncWithCorrectParameters()
    {
        // Arrange
        var bodyParams = new Dictionary<string, object> { { "phone_number", "1234567890" } };
        var expectedResponse = new TelesignResponse();

        var mockClient = new Mock<OmniVerifyClient>(TestCustomerId, TestApiKey) { CallBase = true };
        mockClient.Setup(c => c.PostAsync(CreateEndpoint, bodyParams)).ReturnsAsync(expectedResponse);

        // Act
        var response = await mockClient.Object.CreateVerificationProcessAsync(bodyParams);

        // Assert
        Assert.Equal(expectedResponse, response);
        mockClient.Verify(c => c.PostAsync(CreateEndpoint, bodyParams), Times.Once);
    }
}
