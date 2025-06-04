using SDI.Abstraction;
namespace SDI.Activators;
public class ScriptableServiceInstanceActivator(ScriptableServiceInstanceActivator.Activator activator) : IServiceInstanceActivator {
 public delegate object Activator(IServiceProvider provider);
 public object Activate(IServiceProvider provider) {
  return activator?.Invoke(provider);
 }
}
