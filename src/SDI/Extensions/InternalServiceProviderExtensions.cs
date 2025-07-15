using SDI.Abstraction;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Extensions;

internal static class InternalServiceProviderExtensions
{
    internal static object GetIsntanceFromScope(this IServiceProvider provider, ServiceId serviceId, ScopeId scopeId) => provider.GetScope(scopeId).GetInstance(serviceId);

    internal static IServiceInstanceContainer GetScope(this IServiceProvider provider, ScopeId scopeId) => provider.GetService<IServiceInstanceContainer>(scopeId);
}
