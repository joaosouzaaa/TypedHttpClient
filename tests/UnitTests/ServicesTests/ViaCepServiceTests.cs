using Moq;
using Moq.Protected;
using System.Net;
using System.Text.Json;
using TypedHttpClient.API.DataTransferObjects.Cep;
using TypedHttpClient.API.Services;

namespace UnitTests.ServicesTests;

public sealed class ViaCepServiceTests
{
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly ViaCepService _viaCepService;

    public ViaCepServiceTests()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        const string baseAddress = "https://localhost";
        var httpClient = new HttpClient(_httpMessageHandlerMock.Object)
        {
            BaseAddress = new Uri(baseAddress)
        };
        _viaCepService = new ViaCepService(httpClient);
    }

    [Fact]
    public async Task GetZipCodeInfoAsync_SuccessfulScenario_ReturnsResponse()
    {
        // A
        var viaCepResponse = new ViaCepResponse(
            "test",
            "asd",
            null,
            "test",
            "asdasd",
            "ufu",
            "asd");
        var viaCepJsonString = JsonSerializer.Serialize(viaCepResponse);
        var httpResponseMessage = new HttpResponseMessage()
        {
            Content = new StringContent(viaCepJsonString),
            StatusCode = HttpStatusCode.OK
        };
        _httpMessageHandlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(httpResponseMessage);

        // A
        var viaCepResponseResult = await _viaCepService.GetZipCodeInfoAsync("test", default);

        // A
        Assert.NotNull(viaCepResponseResult);
    }

    [Fact]
    public async Task GetZipCodeInfoAsync_InvalidHttpStatusCode_ReturnsNull()
    {
        // A
        var httpResponseMessage = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.BadRequest
        };
        _httpMessageHandlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(httpResponseMessage);

        // A
        var viaCepResponseResult = await _viaCepService.GetZipCodeInfoAsync("test", default);

        // A
        Assert.Null(viaCepResponseResult);
    }
}
