var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var assembly = typeof(Program).Assembly;

builder.Services.AddCarter(configurator: c =>
{
    var modules = assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(ICarterModule))).ToArray();
    c.WithModules(modules);
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.Run();
