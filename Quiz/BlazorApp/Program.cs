using BlazorApp.Components;
using BlazorApp.Data;
using BlazorApp.Services;
using Microsoft.EntityFrameworkCore;

#region Builder
var builder = WebApplication.CreateBuilder(args);

// Register HttpContextAccessor first (for access in other services)
builder.Services.AddHttpContextAccessor();

// Register HttpClient for general use (after adding HttpContextAccessor)
builder.Services.AddHttpClient("Default", (serviceProvider, client) =>
{
    var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
    var request = httpContextAccessor.HttpContext?.Request;

    // Use the request's scheme and host to create the base URL dynamically
    if (request != null) client.BaseAddress = new($"{request.Scheme}://{request.Host}/");
    else throw new NullReferenceException($"{nameof(request)} IS NULL!");
});

// Register the default HttpClient and inject it into services
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Default"));


// Register QuickGrid's EF Adapter
builder.Services.AddQuickGridEntityFrameworkAdapter();

// Register the database context with connection string configuration
builder.Services.AddDbContext<AppDbContext>(options =>
{
#if DEBUG
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection"));
    options.EnableSensitiveDataLogging();
    options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
#else
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProdConnection"));
#endif
});

// Add Razor Components for interactive server-side rendering
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(options =>
    {
        options.DetailedErrors = true; // Enable detailed error reporting for debugging
    });

// Add custom logging for better diagnostics (optional but helpful for production)
builder.Logging.AddConsole(); // Logs to the console (also ensure this is not disabled in production)

// Register custom services for DI
foreach (var Service in Services.Collection())
{
    switch (Service.kind)
    {
        case Services.ServiceKind.Singleton: builder.Services.AddSingleton(Service.interfaceType, Service.implementationType); break;
        case Services.ServiceKind.Scope: builder.Services.AddScoped(Service.interfaceType, Service.implementationType); break;
        case Services.ServiceKind.Transient: builder.Services.AddTransient(Service.interfaceType, Service.implementationType); break;
    }
    Console.WriteLine($"[{Service.kind}]\tiface: {Service.interfaceType.FullName} -> impl: {Service.implementationType.FullName} ADDED");
}
#endregion

#region Middleware 
var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts(); // HTTP Strict Transport Security
    app.UseHttpsRedirection(); // Force HTTP requests to be redirected to HTTPS
}

// Enable anti-forgery protection (important for securing requests)
app.UseAntiforgery();

// Static asset mapping (ensure proper static file handling)
app.MapStaticAssets();

// Static files middleware configuration
app.UseStaticFiles(new StaticFileOptions
{
    ServeUnknownFileTypes = true,  // Allow serving unknown file types
    DefaultContentType = "application/json; charset=utf-8" // Default MIME type for JSON files
});

// Map Razor components for interactive server-side rendering
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Run the application
app.Run();
#endregion