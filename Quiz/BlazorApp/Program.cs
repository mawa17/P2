using BlazorApp.Components;
using BlazorApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

// Register HttpContextAccessor first (for access in other services)
builder.Services.AddHttpContextAccessor();

// Register state management service
builder.Services.AddSingleton<IStateService, StateService>();

// Register HttpClient for general use
builder.Services.AddHttpClient();

// Register QuickGrid's EF Adapter
builder.Services.AddQuickGridEntityFrameworkAdapter();

// Register the database context with connection string configuration
builder.Services.AddDbContext<AppDbContext>(options =>
{
#if DEBUG
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection"), sqlOptions =>
    {
    });
    options.EnableSensitiveDataLogging();
    options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
#else
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProdConnection"), sqlOptions =>
    {
    });
#endif
});

// Register AppDbContextService for scoped DI
builder.Services.AddScoped<AppDbContextService>();

// Add Razor Components for interactive server-side rendering
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(options =>
    {
        options.DetailedErrors = true; // Enable detailed error reporting for debugging
    });

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts(); // HTTP Strict Transport Security
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