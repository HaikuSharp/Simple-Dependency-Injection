namespace SDI.Tests.Services;

public interface IServiceP
{
    Abstraction.IServiceProvider Provider { get; }
}

public class ServiceP(Abstraction.IServiceProvider provider) : IServiceP
{
    public Abstraction.IServiceProvider Provider => provider;
}
