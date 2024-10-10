using TypedHttpClient.API.Constants;
using TypedHttpClient.API.Options;

namespace TypedHttpClient.API.DependencyInjection;

internal static class OptionsDependencyInjection
{
    internal static void AddOptionsDependencyInjection(this IServiceCollection services, IConfiguration configuration) =>
        services.Configure<HttpClientOptions>(configuration.GetSection(OptionsConstants.HttpClientSection));
}
