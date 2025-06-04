using SDI.Abstraction;
using Sugar.Object.Extensions;
using System;
namespace SDI.Exceptions;
public class ServiceAccessException(ServiceId id) : NullReferenceException($"Service [id: {id}] instance cant be null.") {
 public static void ThrowIfServiceIntanceIsNull(ServiceId id, object instance) {
  if(instance.IsNull) {
   throw new ServiceAccessException(id);
  }
 }
}
