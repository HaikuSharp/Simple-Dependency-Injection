using SDI.Abstraction;
using System.Collections.Generic;
using System.Reflection;
namespace SDI.Invokers;
public class ServiceConstructorInvokerBase<TMethod>(TMethod method) : IServiceConstructorInvoker where TMethod : MethodBase {
 public IEnumerable<ParameterInfo> Parameters {
  get {
   return method.GetParameters();
  }
 }
 public object Invoke(object[] arguments) {
  return this.Invoke(method, arguments);
 }
 public virtual object Invoke(TMethod method, object[] arguments) {
  return method?.Invoke(null, arguments);
 }
}
