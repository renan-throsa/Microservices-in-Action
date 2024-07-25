using Microsoft.Net.Http.Headers;
using Polly;
using PriceCalculation.Domain.Interfaces;
using PriceCalculation.Service;
using PriceCalculation.Services;

namespace PriceCalculation.Utils
{
    public static class ExtentionMethods
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {

            services.AddScoped<IPriceCalculationService, PriceCalculationService>();

            return services;
        }

        public static IServiceCollection AddTypedClient(this IServiceCollection services, IConfiguration configuration)
        {
            var clientSettingsSection = configuration.GetSection(nameof(ClientSettings));
            services.AddHttpClient<ISpecialOffersClient, SpecialOffersClient>((HttpClient client) =>
            {
                string address = clientSettingsSection.Get<ClientSettings>().BaseAddress;
                client.BaseAddress = new Uri(address);
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");

            }).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, attempt => TimeSpan.FromMilliseconds(250 * Math.Pow(2, attempt))));

            return services;
        }



    }
}
