using BlazorApp.Services.Collection;
using Microsoft.AspNetCore.Authorization;

namespace BlazorApp.Services;

public static class Services
{
    public enum ServiceKind
    {
        Singleton = 1,
        Scope,
        Transient
    }
    public static IEnumerable<(ServiceKind kind, Type implementationType, Type? interfaceType)> Collection()
    {
        yield return (ServiceKind.Scope, typeof(AppDbContextService), null);
        yield return (ServiceKind.Singleton, typeof(DataService), typeof(IDataService));
        yield return (ServiceKind.Scope, typeof(AlertService), typeof(IAlertService));
        yield break;
    }
}