using System;

namespace SDI.SCG.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class ServiceImplementationAttribute(Type serviceType) : Attribute
{
    public Type ServiceType => serviceType;
}

