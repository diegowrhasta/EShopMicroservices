using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var assembly = Assembly.GetExecutingAssembly();

// Add services to the container.
builder.Services.AddRazorPages();

var modules = assembly
    .GetTypes()
    .Where(t => t.IsInterface && t.IsAssignableTo(typeof(IRefitService)))
    .ToArray();
foreach (var module in modules)
{
    var refitClientType = typeof(IRefitService).Assembly.GetType(
        module.FullName!
    );
    if (refitClientType is null)
    {
        continue;
    }

    builder
        .Services.AddRefitClient(refitClientType)
        .ConfigureHttpClient(c =>
        {
            c.BaseAddress = new Uri(
                builder.Configuration["ApiSettings:GatewayAddress"]!
            );
        });
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
