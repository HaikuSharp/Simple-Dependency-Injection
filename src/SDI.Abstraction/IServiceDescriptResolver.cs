namespace SDI.Abstraction;
public interface IServiceDescriptResolver<TServiceDescriptSource> {
 IServiceDescriptor Resolve(TServiceDescriptSource source);
}
