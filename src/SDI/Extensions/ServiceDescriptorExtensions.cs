using SDI.Abstraction;

namespace SDI.Extensions;

public static class ServiceDescriptorExtensions
{
    public static ServiceId GetId(this IServiceDescriptor descriptor) => ServiceId.FromDescriptor(descriptor);
}
