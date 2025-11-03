using Microsoft.Extensions.DependencyInjection;
using PocketbaseNetSdk.Models.Base;
using PocketbaseNetSdk.Services;

namespace PocketbaseNetSdk
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddPocketbaseClient<AuthModel>(this IServiceCollection services, string baseUrl) where AuthModel : BaseAuthModel, new()
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped(provider =>
            {
                var tokenservice = provider.GetRequiredService<ITokenService>();
                var logger = provider.GetService<Microsoft.Extensions.Logging.ILogger<PocketbaseClient>>();
                return new PocketbaseClient(baseUrl, tokenservice, logger: logger);
            });
            services.AddScoped<IAuthService, AuthService<AuthModel>>();
            services.AddScoped<ICollectionService, CollectionService>();

            return services;
        }
    }
}
