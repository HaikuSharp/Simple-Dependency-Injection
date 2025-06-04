using System.Collections.Generic;
using System.Reflection;
namespace SDI.Abstraction;
public interface IServiceConstructorInvoker {
 IEnumerable<ParameterInfo> Parameters { get; }
 object Invoke(object[] arguments);
}
