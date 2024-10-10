using System.Text.Json;
using TypedHttpClient.API.DataTransferObjects.Cep;

namespace TypedHttpClient.API.Services;

public sealed class ViaCepService(HttpClient httpClient) : IDisposable
{
    public async Task<ViaCepResponse?> GetZipCodeInfoAsync(string zipCode, CancellationToken cancellationToken)
    {
        var viaCepHttpResponseMessage = await httpClient.GetAsync($"/ws/{zipCode}/json/", cancellationToken);

        if (!viaCepHttpResponseMessage.IsSuccessStatusCode)
        {
            return null;
        }

        return JsonSerializer.Deserialize<ViaCepResponse>(
            await viaCepHttpResponseMessage.Content.ReadAsStringAsync(cancellationToken),
            _jsonSerializerOptions);
    }

    public void Dispose()
    {
        httpClient.Dispose();

        GC.SuppressFinalize(this);
    }

    private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };
}
