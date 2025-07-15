using Sugar.Object;
using System;

namespace SDI.Abstraction;

/// <summary>
/// Represents a unique service identifier composed of a type and an optional key.
/// </summary>
public readonly struct ServiceId : IEquatable<ServiceId>
{
    private readonly Type m_Type;
    private readonly object m_Key;

    private ServiceId(Type type, object key)
    {
        m_Type = type;
        m_Key = key;
    }

    /// <summary>
    /// Gets a value indicating whether the service type is generic.
    /// </summary>
    public bool IsGeneric => m_Type.IsGenericType;

    /// <summary>
    /// Gets a value indicating whether the service type is a closed generic type (no open type parameters).
    /// </summary>
    public bool IsClosedGeneric => !m_Type.ContainsGenericParameters;

    /// <summary>
    /// Returns a new <see cref="ServiceId"/> with the same type but discards the key.
    /// </summary>
    public ServiceId IgnoreKey => FromType(m_Type);

    /// <summary>
    /// Returns a new <see cref="ServiceId"/> representing the generic type definition (if the current type is generic).
    /// </summary>
    public ServiceId GenericDefinition => FromType(m_Type.GetGenericTypeDefinition(), m_Key);

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
    public static ServiceId FromType(Type type, object key) => new(type, key ?? AnyObject.Any);

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
    public bool Equals(ServiceId other) => m_Type.Equals(other.m_Type) && EqualsKeys(m_Key, other.m_Key);

    /// <inheritdoc/>
    public override bool Equals(object obj) => obj is ServiceId other && Equals(other);

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        int hashCode = 1193439425;
        hashCode = (hashCode * -1521134295) + m_Type.GetHashCode();
        hashCode = (hashCode * -1521134295) + m_Key.GetHashCode();
        return hashCode;
    }

    /// <inheritdoc/>
    public override string ToString() => $"({m_Type.FullName}, {m_Key})";

    private static bool EqualsKeys(object left, object right) => left is not null ? left.Equals(right) : right is null || right.Equals(left);

    /// <inheritdoc/>
    public static bool operator ==(ServiceId left, ServiceId right) => left.Equals(right);

    /// <inheritdoc/>
    public static bool operator !=(ServiceId left, ServiceId right) => !(left == right);

    /// <inheritdoc/>
    public static implicit operator Type(ServiceId id) => id.m_Type;

    /// <inheritdoc/>
    public static implicit operator ServiceId(Type type) => FromType(type);
}