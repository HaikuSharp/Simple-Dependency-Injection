namespace SDI.Abstraction;
public interface IServiceLifeTime {
 IServiceAccessor CreateAccessor(IServiceInstanceContanier contanier, IServiceDescriptor descriptor);
}
