using TypedHttpClient.API.Constants;
using TypedHttpClient.API.Options;
using TypedHttpClient.API.Services;

namespace TypedHttpClient.API.DependencyInjection;

internal static class HttpClientDependencyInjection
{
    internal static void AddHttpClientDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        var httpClient = configuration.GetSection(OptionsConstants.HttpClientSection).Get<HttpClientOptions>()!;

        services.AddHttpClient<ViaCepService>(h => h.BaseAddress = new Uri(httpClient.ViaCepBaseAddress));
    }
}
