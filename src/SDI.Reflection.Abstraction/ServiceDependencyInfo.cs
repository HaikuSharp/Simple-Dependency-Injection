using System;
using System.Reflection;

namespace SDI.Reflection.Abstraction;

/// <summary>
/// Represents dependency information consisting of a source type and a member (field, property, parameter, etc.) with attributes.
/// </summary>
public readonly struct ServiceDependencyInfo(Type sourceType, ICustomAttributeProvider member) : IEquatable<ServiceDependencyInfo>
{
    /// <summary>
    /// Gets the original dependency type.
    /// </summary>
    public Type SourceType => sourceType;

    /// <summary>
    /// Gets the member (parameter, field, property etc.) that represents the dependency.
    /// </summary>
    public ICustomAttributeProvider Member => member;

    /// <inheritdoc cref="IEquatable{ServiceDependencyInfo}.Equals(ServiceDependencyInfo)"/>
    public bool Equals(ServiceDependencyInfo other) => member.Equals(other.Member);

    /// <inheritdoc/>
    public override bool Equals(object obj) => obj is ServiceDependencyInfo info && Equals(info);

    /// <inheritdoc/>
    public override int GetHashCode() => member.GetHashCode();

    /// <inheritdoc/>
    public override string ToString() => member.ToString();

    /// <summary>
    /// Creates a <see cref="ServiceDependencyInfo"/> from a method parameter.
    /// </summary>
    /// <param name="parameter">The parameter info representing the dependency.</param>
    /// <returns>A new <see cref="ServiceDependencyInfo"/> instance.</returns>
    public static ServiceDependencyInfo FromParameter(ParameterInfo parameter) => new(parameter.ParameterType, parameter);

    /// <inheritdoc/>
    public static bool operator ==(ServiceDependencyInfo left, ServiceDependencyInfo right) => left.Equals(right);

    /// <inheritdoc/>
    public static bool operator !=(ServiceDependencyInfo left, ServiceDependencyInfo right) => !(left == right);
}