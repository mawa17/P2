using BlazorApp.Services.Collection;

namespace BlazorApp.Services;

public static class Services
{
    public enum ServiceKind
    {
        Singleton = 1,
        Scope,
        Transient
    }
    public static IEnumerable<(ServiceKind kind, Type interfaceType, Type implementationType)> Collection()
    {
        yield return (ServiceKind.Singleton, typeof(IDataService), typeof(DataService));
        yield return (ServiceKind.Singleton, typeof(IAppDbContextService), typeof(AppDbContextService));
        yield return (ServiceKind.Scope, typeof(IAlertService), typeof(AlertService));
        yield break;
    }
}