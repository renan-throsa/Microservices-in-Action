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
            services.Configure<DataBaseSettings>(DataBaseSettingsSection);

            var QueueSettingsSection = configuration.GetSection(nameof(QueueSettings));
            services.Configure<QueueSettings>(QueueSettingsSection);

            services.AddSingleton<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddSingleton<IEventRepository, EventRepository>();
            services.AddSingleton<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddSingleton<IShoppingCartService, ShoppingCartService>();
            services.AddSingleton<IEventService, EventService>();

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

        public static void AddDataToDB(this IHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationContext>();
                context.SeedDatabaseIfEmpty();
            }
        }
    }
}
