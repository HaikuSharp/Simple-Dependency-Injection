using SDI.Abstraction;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Extensions;

internal static class InternalServiceProviderExtensions
{
    internal static object GetIsntanceFromScope(this IServiceProvider provider, ServiceId serviceId, ScopeId scopeId) => provider.GetScopeInstanceContainer(scopeId).GetInstance(serviceId);

    internal static IServiceInstanceContainer GetScopeInstanceContainer(this IServiceProvider provider) => provider.GetScopeInstanceContainer(provider.Id);

    internal static IServiceInstanceContainer GetScopeInstanceContainer(this IServiceProvider provider, ScopeId scopeId) => provider.GetService<IServiceInstanceContainer>(scopeId);
}
