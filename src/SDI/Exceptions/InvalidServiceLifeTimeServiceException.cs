using SDI.Abstraction;
using System;
namespace SDI.Exceptions;
public class InvalidServiceLifeTimeServiceException(ServiceId id, Type lifeTimeType) : Exception($"Invalid service lifetime type [id: {id}] [type: {lifeTimeType.FullName}].") {
 internal static void ThrowInvalidType(ServiceId id, Type lifeTimeType) {
  throw new InvalidServiceLifeTimeServiceException(id, lifeTimeType);
 }
}