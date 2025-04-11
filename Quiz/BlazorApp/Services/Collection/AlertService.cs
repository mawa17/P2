using Microsoft.JSInterop;

namespace BlazorApp.Services.Collection;

public interface IAlertService
{
    Task ShowAlertAsync(string message);
}
public sealed class AlertService(IServiceProvider serviceProvider) : IAlertService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private IJSRuntime JS => _serviceProvider.GetRequiredService<IJSRuntime>();
    public async Task ShowAlertAsync(string message) => await JS.InvokeVoidAsync("alert", message);
}