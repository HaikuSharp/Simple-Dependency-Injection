using Sugar.Object;
using System;

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

    public bool IsGeneric => m_Type.IsGenericType;

    public bool IsClosedGeneric => !m_Type.ContainsGenericParameters;

    public ServiceId IgnoreKey => FromType(m_Type);

    public ServiceId GenericDefinition => FromType(m_Type.GetGenericTypeDefinition(), m_Key);

    public static ServiceId FromType(Type type) => FromType(type, null);

    public static ServiceId FromType(Type type, object key) => new(type, key ?? AnyObject.Any);

    public static ServiceId FromDescriptor<TDescriptor>(TDescriptor descriptor) where TDescriptor : IServiceDescriptor => FromType(descriptor.ServiceType, descriptor.Key);

    public static ServiceId From<T>() => From<T>(null);

    public static ServiceId From<T>(object key) => FromType(typeof(T), key);

    public bool Equals(ServiceId other) => m_Type.Equals(other.m_Type) && EqualsKeys(m_Key, other.m_Key);

    public override bool Equals(object other) => other is ServiceId otherServiceId && Equals(otherServiceId);

    public override int GetHashCode()
    {
        int hashCode = 1193439425;
        hashCode = (hashCode * -1521134295) + m_Type.GetHashCode();
        hashCode = (hashCode * -1521134295) + m_Key.GetHashCode();
        return hashCode;
    }

    public override string ToString() => $"({m_Type.FullName}, {m_Key?.ToString() ?? string.Empty})";

    private static bool EqualsKeys(object left, object rigth) => left is not null ? left.Equals(rigth) : rigth is null || rigth.Equals(left);

    public static bool operator ==(ServiceId left, ServiceId rigth) => left.Equals(rigth);

    public static bool operator !=(ServiceId left, ServiceId rigth) => !(left == rigth);

    public static implicit operator Type(ServiceId id) => id.m_Type;

    public static implicit operator ServiceId(Type type) => FromType(type);
}
