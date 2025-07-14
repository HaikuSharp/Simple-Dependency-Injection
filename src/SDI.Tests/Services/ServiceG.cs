namespace SDI.Tests.Services;

public interface IServiceG<TInner0, TInner1>
{
    TInner0 Inner0 { get; }

    TInner1 Inner1 { get; }
}

public class ServiceG0<TInner>(TInner inner0, IServiceA inner1) : IServiceG<TInner, IServiceA>
{
    public TInner Inner0 => inner0;

    public IServiceA Inner1 => inner1;
}

public class ServiceG1<TInner>(TInner inner0, IServiceB inner1) : IServiceG<TInner, IServiceB>
{
    public TInner Inner0 => inner0;

    public IServiceB Inner1 => inner1;
}
