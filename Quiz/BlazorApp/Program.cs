using BlazorApp.Components;
using BlazorApp.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register QuickGrid's EF Adapter
builder.Services.AddQuickGridEntityFrameworkAdapter();

//// Add basic authentication service
//builder.Services.AddAuthentication(BasicAuthenticationDefaults.AuthenticationScheme)
//        .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(
//            BasicAuthenticationDefaults.AuthenticationScheme,
//            options => { })

// Register HttpClient
builder.Services.AddHttpClient();

// Register IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

builder.Services.AddDistributedMemoryCache(); // Required for session storage
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(300); // Session timeout
});


// Register the database context
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

builder.Services.AddScoped<AppDbContextService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();

app.UseStaticFiles(new StaticFileOptions
{
    ServeUnknownFileTypes = true, // Ensure the server can serve all file types
    DefaultContentType = "application/json; charset=utf-8" // Set UTF-8 for JSON files
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
