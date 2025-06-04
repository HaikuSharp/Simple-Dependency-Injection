namespace SDI.Abstraction;
public interface IServiceConstructorResolver {
 IServiceConstructorInvoker Resolve(IServiceDescriptor descriptor);
}