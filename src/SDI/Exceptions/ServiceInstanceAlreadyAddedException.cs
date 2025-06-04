using SDI.Abstraction;
using System;
namespace SDI.Exceptions;

public class ServiceInstanceAlreadyAddedException(ServiceId id) : Exception($"Service with [id: {id}] is already added.") {
 internal static void ThrowIfContains(IServiceInstanceContanier contanier, ServiceId id) {
  if(contanier.HasInstance(id)) {
   throw new ServiceInstanceAlreadyAddedException(id);
  }
 }
}
