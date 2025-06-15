using Sugar.Object;
using System;
using System.Collections.Generic;

namespace SDI.Abstraction;

public readonly struct ServiceId
{
    private readonly Type m_Type;
    private readonly object m_Key;

    private ServiceId(Type type, object key)
    {
        m_Type = type;
        m_Key = key;
    }

    public static ServiceId FromType(Type type) => FromType(type, null);

    public static ServiceId FromType(Type type, object key) => new(type, key ?? AnyObject.Any);

    public static ServiceId From<T>() => From<T>(null);

    public static ServiceId From<T>(object key) => FromType(typeof(T), key);

    public static ServiceId FromDescriptor(IServiceDescriptor descriptor) => FromType(descriptor.ServiceType, descriptor.Key);

    public bool Equals(ServiceId other) => m_Type.Equals(other.m_Type) && EqualsKeys(m_Key, other.m_Key);

    public override bool Equals(object other) => other is ServiceId otherServiceId && Equals(otherServiceId);

    public override int GetHashCode()
    {
        int hashCode = 1193439425;
        hashCode = (hashCode * -1521134295) + EqualityComparer<Type>.Default.GetHashCode(m_Type);
        hashCode = (hashCode * -1521134295) + EqualityComparer<object>.Default.GetHashCode(m_Key);
        return hashCode;
    }

    public override string ToString() => $"({m_Type.FullName}, {m_Key?.ToString() ?? string.Empty})";

    private static bool EqualsKeys(object left, object rigth) => left is not null ? left.Equals(rigth) : rigth is null || rigth.Equals(left);

    public static bool operator ==(ServiceId left, ServiceId rigth) => left.Equals(rigth);

    public static bool operator !=(ServiceId left, ServiceId rigth) => !(left == rigth);
}
