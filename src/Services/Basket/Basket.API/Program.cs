var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Application Services
var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddCarter(configurator: c =>
{
    var modules = assembly
        .GetTypes()
        .Where(t => t.IsAssignableTo(typeof(ICarterModule)))
        .ToArray();
    c.WithModules(modules);
});

// Data Services
builder
    .Services.AddMarten(opts =>
    {
        opts.Connection(builder.Configuration.GetConnectionString("Database")!);
        opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);
        // opts.AutoCreateSchemaObjects
    })
    .UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    // options.InstanceName = "Basket";
});

// Grpc Services
builder
    .Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(
        options =>
        {
            options.Address = new Uri(
                builder.Configuration["GrpcSettings:DiscountUrl"]!
            );
        }
    )
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
        return handler;
    });

// Cross-Cutting Services
builder.Services.AddExceptionHandler<CustomExceptionHandler>();


// Async Communication Services
builder.Services.AddMessageBroker(builder.Configuration);

builder
    .Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!);

var app = builder.Build();

app.MapCarter();
app.UseExceptionHandler(options => { });
app.UseHealthChecks(
    "/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    }
);

// Configure the HTTP request pipeline.

app.Run();
