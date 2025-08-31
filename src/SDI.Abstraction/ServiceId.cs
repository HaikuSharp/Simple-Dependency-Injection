using System;

namespace SDI.Abstraction;

/// <summary>
/// Represents a unique service identifier composed of a type and an optional key.
/// </summary>
public readonly struct ServiceId : IEquatable<ServiceId>
{
    private readonly ServiceType m_ServiceType;
    private readonly ServiceKey m_ServiceKey;

    private ServiceId(Type type, object key)
    {
        m_ServiceType = new(type);
        m_ServiceKey = new(key);
    }

    /// <summary>
    /// Gets a service type.
    /// </summary>
    public Type Type => m_ServiceType.m_Type;

    /// <summary>
    /// Gets a service key.
    /// </summary>
    public object Key => m_ServiceKey.m_Key;

    /// <summary>
    /// Gets a value indicating whether the service type is generic.
    /// </summary>
    public bool IsGeneric => m_ServiceType.IsGeneric;

    /// <summary>
    /// Gets a value indicating whether the service type is a closed generic type (no open type parameters).
    /// </summary>
    public bool IsClosedGeneric => !m_ServiceType.IsClosedGeneric;

    /// <summary>
    /// Returns a new <see cref="ServiceId"/> with the same type but discards the key.
    /// </summary>
    public ServiceId IgnoreKey => FromType(m_ServiceType);

    /// <summary>
    /// Returns a new <see cref="ServiceId"/> representing the generic type definition (if the current type is generic).
    /// </summary>
    public ServiceId GenericDefinition => FromType(m_ServiceType.GenericDefinition, m_ServiceKey);

    /// <summary>
    /// Creates a <see cref="ServiceId"/> from a key with no type.
    /// </summary>
    /// <param name="key">An optional key to distinguish multiple registrations of the same type.</param>
    /// <returns>A new <see cref="ServiceId"/> instance.</returns>
    public static ServiceId FromKey(object key) => FromType(null, key);

    /// <summary>
    /// Creates a <see cref="ServiceId"/> from a type with no key.
    /// </summary>
    /// <param name="type">The service type.</param>
    /// <returns>A new <see cref="ServiceId"/> instance.</returns>
    public static ServiceId FromType(Type type) => FromType(type, null);

    /// <summary>
    /// Creates a <see cref="ServiceId"/> from a type and an optional key.
    /// </summary>
    /// <param name="type">The service type.</param>
    /// <param name="key">An optional key to distinguish multiple registrations of the same type.</param>
    /// <returns>A new <see cref="ServiceId"/> instance.</returns>
    public static ServiceId FromType(Type type, object key) => new(type, key);

    /// <summary>
    /// Creates a <see cref="ServiceId"/> from a service descriptor.
    /// </summary>
    /// <typeparam name="TDescriptor">The descriptor type implementing <see cref="IServiceDescriptor"/>.</typeparam>
    /// <param name="descriptor">The service descriptor containing the type and key.</param>
    /// <returns>A new <see cref="ServiceId"/> instance.</returns>
    public static ServiceId FromDescriptor<TDescriptor>(TDescriptor descriptor) where TDescriptor : IServiceDescriptor => FromType(descriptor.ServiceType, descriptor.Key);

    /// <summary>
    /// Creates a <see cref="ServiceId"/> from a generic type with no key.
    /// </summary>
    /// <typeparam name="T">The service type.</typeparam>
    /// <returns>A new <see cref="ServiceId"/> instance.</returns>
    public static ServiceId From<T>() => From<T>(null);

    /// <summary>
    /// Creates a <see cref="ServiceId"/> from a generic type and an optional key.
    /// </summary>
    /// <typeparam name="T">The service type.</typeparam>
    /// <param name="key">An optional key to distinguish multiple registrations of the same type.</param>
    /// <returns>A new <see cref="ServiceId"/> instance.</returns>
    public static ServiceId From<T>(object key) => FromType(typeof(T), key);

    /// <inheritdoc cref="IEquatable{ServiceId}.Equals(ServiceId)"/>
    public bool Equals(ServiceId other) => m_ServiceType == other.m_ServiceType && m_ServiceKey == other.m_ServiceKey;

    /// <inheritdoc/>
    public override bool Equals(object obj) => obj is ServiceId other && Equals(other);

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        int hashCode = 1193439425;
        hashCode = (hashCode * -1521134295) + m_ServiceType.GetHashCode();
        hashCode = (hashCode * -1521134295) + m_ServiceKey.GetHashCode();
        return hashCode;
    }

    /// <inheritdoc/>
    public override string ToString() => $"({m_ServiceType}, {m_ServiceKey})";

    /// <inheritdoc/>
    public static bool operator ==(ServiceId left, ServiceId right) => left.Equals(right);

    /// <inheritdoc/>
    public static bool operator !=(ServiceId left, ServiceId right) => !(left == right);

    private readonly struct ServiceKey(object key) : IEquatable<ServiceKey>
    {
        internal readonly object m_Key = key;

        public bool Equals(ServiceKey other) => m_Key is null || m_Key == other.m_Key;

        public override bool Equals(object obj) => obj is ServiceKey key && Equals(key);

        public override int GetHashCode() => m_Key?.GetHashCode() ?? 0;

        public override string ToString() => m_Key?.ToString() ?? "AnyKey";

        public static bool operator ==(ServiceKey left, ServiceKey right) => left.Equals(right);

        public static bool operator !=(ServiceKey left, ServiceKey right) => !(left == right);
    }

    private readonly struct ServiceType(Type type) : IEquatable<ServiceType>
    {
        internal readonly Type m_Type = type;

        public bool IsGeneric => m_Type.IsGenericType;

        public bool IsClosedGeneric => !m_Type.ContainsGenericParameters;

        public ServiceType GenericDefinition => new(m_Type.GetGenericTypeDefinition());

        public bool Equals(ServiceType other) => m_Type is null || m_Type == other.m_Type;

        public override bool Equals(object obj) => obj is ServiceType type && Equals(type);

        public override int GetHashCode() => m_Type?.GetHashCode() ?? 0;

        public override string ToString() => m_Type?.FullName ?? "AnyType";

        public static bool operator ==(ServiceType left, ServiceType right) => left.Equals(right);

        public static bool operator !=(ServiceType left, ServiceType right) => !(left == right);

        public static implicit operator Type(ServiceType serviceType) => serviceType.m_Type;

        public static implicit operator ServiceType(Type type) => new(type);
    }
}