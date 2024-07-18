using ClientGateway.Clients;
using ClientGateway.Domain.Interfaces;
using Microsoft.Net.Http.Headers;
using Polly;

namespace ClientGateway.Utils
{
    public static class ExtentionMethods
    {
        public static IServiceCollection AddTypedClient(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddHttpClient<IShopingCartClient, ShopingCartClient>((HttpClient client) =>
            {
                IConfigurationSection clientSettingsSection = configuration.GetSection("CartClientSettings");
                string address = clientSettingsSection.Get<ClientSettings>().BaseAddress;
                client.BaseAddress = new Uri(address);
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");

            }).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, attempt => TimeSpan.FromMilliseconds(250 * Math.Pow(2, attempt))));

            
            services.AddHttpClient<IPriceCalculatorClient, PriceCalculatorClient>((HttpClient client) =>
            {
                IConfigurationSection clientSettingsSection = configuration.GetSection("PriceClientSettings");
                string address = clientSettingsSection.Get<ClientSettings>().BaseAddress;
                client.BaseAddress = new Uri(address);
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");

            }).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, attempt => TimeSpan.FromMilliseconds(250 * Math.Pow(2, attempt))));                       

            
            services.AddHttpClient<IProductCatalogClient, ProductCatalogClient>((HttpClient client) =>
            {
                IConfigurationSection clientSettingsSection = configuration.GetSection("ProductClientSettings");
                string address = clientSettingsSection.Get<ClientSettings>().BaseAddress;
                client.BaseAddress = new Uri(address);
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");

            }).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, attempt => TimeSpan.FromMilliseconds(250 * Math.Pow(2, attempt))));


            return services;
        }
    }
}
