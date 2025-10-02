namespace SDI.Sculptor.Descripting.Abstraction;

public interface IInstanceValueServiceDescriptor<TSelf> : IValueServiceDescriptor<TSelf> where TSelf : IInstanceValueServiceDescriptor<TSelf>
{
    TSelf WithInstance(object instance);
}