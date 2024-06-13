using System.Reflection;
using BuildingBlocks.Exceptions.Handler;
using HealthChecks.UI.Client;
using HealthCheckOptions = Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions;

namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddCarter(configurator: c =>
        {
            var modules = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsAssignableTo(typeof(ICarterModule)))
                .ToArray();
            c.WithModules(modules);
        });

        services.AddExceptionHandler<CustomExceptionHandler>();
        services
            .AddHealthChecks()
            .AddSqlServer(configuration.GetConnectionString("Database")!);

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.MapCarter();
        app.UseExceptionHandler(options => { });
        app.UseHealthChecks(
            "/health",
            new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            }
        );

        return app;
    }
}
