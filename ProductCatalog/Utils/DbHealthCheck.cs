using Microsoft.Extensions.Diagnostics.HealthChecks;
using ProductCatalog.Data;

namespace ProductCatalog.Utils
{
    public class DbHealthCheck : IHealthCheck
    {
        private readonly ApplicationContext _context;

        public DbHealthCheck(ApplicationContext context)
        {
            _context= context;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return Task.FromResult( _context.CheckHealthResult());
        }
    }
}
