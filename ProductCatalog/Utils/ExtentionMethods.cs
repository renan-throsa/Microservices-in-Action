﻿using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using ProductCatalog.Data;
using ProductCatalog.Domain.Interfaces;
using ProductCatalog.Queues;
using ProductCatalog.Services;

namespace ProductCatalog.Utils
{
    public static class ExtentionMethods
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment Environment)
        {
            var DataBaseSettingsSection = configuration.GetSection(nameof(DataBaseSettings));
            services.Configure<DataBaseSettings>(DataBaseSettingsSection);

            var QueueSettingsSection = configuration.GetSection(nameof(QueueSettings));
            services.Configure<QueueSettings>(QueueSettingsSection);

            services.AddSingleton<ApplicationContext>();

            if (Environment.IsDevelopment())
            {
                services.AddSingleton<IQueue, MockQueue>();
            }
            else
            {
                services.AddSingleton<IQueue, NatsQueue>();
            }

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();

            return services;
        }


        public static IServiceCollection AddHealthCheckesConfig(this IServiceCollection services)
        {
            services.AddHealthChecks()
              .AddCheck(
                "LivenessHealthCheck",
                () => HealthCheckResult.Healthy(),
                tags: new[] { "liveness" })

              .AddCheck<DbHealthCheck>(nameof(DbHealthCheck),
                tags: new[] { "startup" });

            return services;
        }

        public static IApplicationBuilder UseHealthChecksConfig(this IApplicationBuilder app)
        {

            app.UseHealthChecks("/health/live",
              new HealthCheckOptions
              {
                  Predicate = x => x.Tags.Contains("liveness")
              });

            app.UseHealthChecks("/health/startup",
              new HealthCheckOptions
              {
                  Predicate = x => x.Tags.Contains("startup")
              });

            return app;
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
