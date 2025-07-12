using System.Collections.Generic;

namespace SDI.Tests.Services;

public interface IServiceC
{
    int BCount { get; }
}

public class ServiceC(IServiceB[] bs) : IServiceC
{
    public int BCount => bs.Length;
}