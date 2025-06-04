namespace SDI.Abstraction;
public interface IServiceActivatorResolver {
 IServiceInstanceActivator Resolve(IServiceDescriptor descriptor);
}
