using SDI.Abstraction;
using SDI.Sculptor.Scoping;

namespace SDI.Sculptor.Extensions;

public static class ServiceRegistrarExtensions
{
    public static Registration<TRegistrar> RegistrationBegin<TRegistrar>(this TRegistrar registrar) where TRegistrar : class, IServiceRegistrar => new(registrar);
} 
