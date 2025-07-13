using SDI.Abstraction;
using SDI.Activating;

namespace SDI.Extensions;

internal static class InternalServiceConstructorExtensions
{
    internal static IServiceInstanceActivator AsActivator(this IServiceConstructor constructor) => constructor.Parameters.Count > 0 ? new ConstructorServiceActivator(constructor) : new EmptyConstructorServiceActivator(constructor);
}
