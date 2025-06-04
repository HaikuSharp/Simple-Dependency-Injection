using SDI.Abstraction;
using SDI.Attribute;
using System.Linq;
using System.Reflection;
namespace SDI.Resolve.Inject;
public class ServiceAttributeKeyResolver<TKeySource> : IServiceKeyResolver<TKeySource> where TKeySource : ICustomAttributeProvider {
 public object Resolve(TKeySource source) {
  return source.GetCustomAttributes(typeof(KeyInjectAttribute), true).Cast<KeyInjectAttribute>().FirstOrDefault()?.Key;
 }
}