namespace SDI.Abstraction;

public interface IServiceLifeTime
{
    IServiceAccessor CreateAccessor(IServiceDescriptor descriptor);
}
