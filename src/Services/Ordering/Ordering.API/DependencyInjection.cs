using System.Reflection;
using BuildingBlocks.Exceptions.Handler;

namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(
        this IServiceCollection services
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

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.MapCarter();
        app.UseExceptionHandler(options => { });
        return app;
    }
}
