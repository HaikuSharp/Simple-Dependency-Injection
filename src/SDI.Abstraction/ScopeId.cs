using System;

namespace SDI.Abstraction;

/// <summary>
/// Represents a scope identifier that can be used to distinguish different dependency injection scopes.
/// </summary>
public class ScopeId(object id) : IEquatable<ScopeId>
{
    private readonly object m_Id = id;

    /// <summary>
    /// Gets the default scope identifier (with a null value).
    /// </summary>
    public static ScopeId Default => field ??= new(null);

    /// <inheritdoc cref="IEquatable{ScopeId}.Equals(ScopeId)"/>
    public bool Equals(ScopeId other) => m_Id == other.m_Id;

    /// <inheritdoc/>
    public override bool Equals(object obj) => obj is ScopeId sid && Equals(sid);

    /// <inheritdoc/>
    public override int GetHashCode() => m_Id?.GetHashCode() ?? 0;

    /// <inheritdoc/>
    public override string ToString() => m_Id?.ToString() ?? "null";

    /// <inheritdoc/>
    public static bool operator ==(ScopeId left, ScopeId right) => left?.Equals(right) ?? right is null;

    /// <inheritdoc/>
    public static bool operator !=(ScopeId left, ScopeId right) => !(left == right);
}