var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCarter(configurator: c =>
{
    var modules = typeof(Program)
        .Assembly.GetTypes()
        .Where(t => t.IsAssignableTo(typeof(ICarterModule)))
        .ToArray();
    c.WithModules(modules);
});
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

app.Run();
