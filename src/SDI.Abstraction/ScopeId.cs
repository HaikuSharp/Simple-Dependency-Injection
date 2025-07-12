using System;

namespace SDI.Abstraction;

public class ScopeId(object id) : IEquatable<ScopeId>
{
    private readonly object m_Id = id;

    public static ScopeId Default => new(null);

    public bool Equals(ScopeId other) => m_Id == other.m_Id;

    public override bool Equals(object obj) => obj is ScopeId sid && Equals(sid);

    public override int GetHashCode() => m_Id.GetHashCode();

    public override string ToString() => m_Id?.ToString() ?? "null";

    public static bool operator ==(ScopeId left, ScopeId right) => left.Equals(right);

    public static bool operator !=(ScopeId left, ScopeId right) => !(left == right);
}