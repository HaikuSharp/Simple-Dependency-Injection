using System;
using System.Reflection;

namespace SDI.Abstraction;

public readonly struct ServiceDependencyInfo(Type sourceType, ICustomAttributeProvider member) : IEquatable<ServiceDependencyInfo>
{
    public Type SourceType => sourceType;

    public ICustomAttributeProvider Member => member;

    public bool Equals(ServiceDependencyInfo other) => member.Equals(other.Member);

    public override bool Equals(object obj) => obj is ServiceDependencyInfo info && Equals(info);

    public override int GetHashCode() => member.GetHashCode();

    public override string ToString() => member.ToString();

    public static ServiceDependencyInfo FromParameter(ParameterInfo parameter) => new(parameter.ParameterType, parameter);

    public static bool operator ==(ServiceDependencyInfo left, ServiceDependencyInfo rigth) => left.Equals(rigth);

    public static bool operator !=(ServiceDependencyInfo left, ServiceDependencyInfo rigth) => !(left == rigth);
}