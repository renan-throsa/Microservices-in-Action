using ShoppingCart.Data;
using ShoppingCart.Domain;

namespace ShoppingCart.Utils
{
    public static class ExtentionMethods
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {            
            var DataBaseSettingsSection = configuration.GetSection(nameof(DataBaseSettings));
            var dataBaseSettings = DataBaseSettingsSection.Get<DataBaseSettings>();
            services.Configure<DataBaseSettings>(DataBaseSettingsSection);

            services.AddSingleton<ApplicationContext>();
            

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
