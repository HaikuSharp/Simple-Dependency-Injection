namespace SDI.Abstraction;

public interface IServiceDependencyResolver<in TScource>
{
    IServiceDependency Resolve(TScource source);
}