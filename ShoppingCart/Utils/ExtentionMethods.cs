using Microsoft.Net.Http.Headers;
using Polly;
using ShoppingCart.Data;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Service;

namespace ShoppingCart.Utils
{
    public static class ExtentionMethods
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var DataBaseSettingsSection = configuration.GetSection(nameof(DataBaseSettings));
            var dataBaseSettings = DataBaseSettingsSection.Get<DataBaseSettings>();
            services.Configure<DataBaseSettings>(DataBaseSettingsSection);

            services.AddTransient<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddTransient<IEventRepository, EventRepository>();
            services.AddTransient<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddTransient<IShoppingCartService, ShoppingCartService>();
            services.AddTransient<IEventService, EventService>();

            services.AddSingleton<ApplicationContext>();

            return services;
        }

        public static IServiceCollection AddTypedClient(this IServiceCollection services, IConfiguration configuration)
        {
            var clientSettingsSection = configuration.GetSection(nameof(ClientSettings));
            services.AddHttpClient<IProductCatalogClient, ProductCatalogClient>((HttpClient client) =>
            {
                string address = clientSettingsSection.Get<ClientSettings>().BaseAddress; client.BaseAddress = new Uri(address); client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");

            }).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, attempt => TimeSpan.FromMilliseconds(250 * Math.Pow(2, attempt))));

            return services;
        }


        public static void AddDBData(this IHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationContext>();

            }
        }
    }
}
