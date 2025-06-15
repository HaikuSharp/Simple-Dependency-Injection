using SDI.Abstraction;
using System;

namespace SDI.Exceptions;

public class InvalidServiceLifeTimeException(ServiceId id, Type lifeTimeType) : Exception($"Invalid service lifetime type [id: {id}] [type: {lifeTimeType.FullName}].")
{
    public static void ThrowInvalidType(ServiceId id, Type lifeTimeType) => throw new InvalidServiceLifeTimeException(id, lifeTimeType);
}