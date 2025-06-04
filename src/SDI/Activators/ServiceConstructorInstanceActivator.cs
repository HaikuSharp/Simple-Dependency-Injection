using SDI.Abstraction;
using System.Collections.Generic;
namespace SDI.Activators;
public class ServiceConstructorInstanceActivator(IServiceConstructorInvoker invoker, IEnumerable<IServiceDependency> dependencies) : IServiceInstanceActivator {
 private object[] m_ArgumentsBuffer;
 private readonly IServiceDependency[] m_Dependencies = [.. dependencies];
 public object Activate(IServiceProvider provider) {
  return invoker.Invoke(this.GetOrCreateArgumentsBuffer(provider));
 }
 private object[] GetOrCreateArgumentsBuffer(IServiceProvider provider) {
  var dependencies = this.m_Dependencies;
  var buffer = this.m_ArgumentsBuffer ??= new object[dependencies.Length];
  for(var i = 0; i < buffer.Length; i++) {
   buffer[i] = dependencies[i].GetDependency(provider);
  }
  return buffer;
 }
}
