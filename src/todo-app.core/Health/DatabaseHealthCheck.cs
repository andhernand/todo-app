using Microsoft.Extensions.Diagnostics.HealthChecks;

using TodoApp.Core.Database;

namespace TodoApp.Core.Health;

public class DatabaseHealthCheck(IDbConnectionFactory dbConnectionFactory) : IHealthCheck
{
    public const string Name = "DatabaseHealthCheck";

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _ = await dbConnectionFactory.CreateConnectionAsync(cancellationToken);
            return HealthCheckResult.Healthy();
        }
        catch (Exception)
        {
            return HealthCheckResult.Unhealthy("Database is unhealthy");
        }
    }
}