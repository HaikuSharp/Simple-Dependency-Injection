namespace SDI.Tests.Services;

public interface IServiceB
{
    IServiceA A { get; }
}

public class ServiceB0(IServiceA a) : IServiceB
{
    public IServiceA A => a;
}

public class ServiceB1() : IServiceB
{
    public IServiceA A => null;
}
