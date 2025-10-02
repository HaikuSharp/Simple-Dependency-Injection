using SDI.Abstraction;
using System;

namespace SDI.Sculptor.Descripting.Abstraction;

public interface IValueServiceDescriptor<TSelf> : IServiceDescriptor where TSelf : IValueServiceDescriptor<TSelf>
{
    TSelf WithType(Type type);

    TSelf WithKey(object key);

    TSelf WithId(Type type, object key);
}
