using SpecialOffers.Data;
using SpecialOffers.Domain.Interfaces;
using SpecialOffers.Service;

namespace SpecialOffers.Utils
{
    public static class ExtentionMethods
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var DataBaseSettingsSection = configuration.GetSection(nameof(DataBaseSettings));
            var dataBaseSettings = DataBaseSettingsSection.Get<DataBaseSettings>();
            services.Configure<DataBaseSettings>(DataBaseSettingsSection);


            services.AddScoped<ISpecialOfferRepository, SpecialOfferRepository>();
            services.AddScoped<ISpecialOfferService, SpecialOfferService>();

            services.AddSingleton<ApplicationContext>();

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
