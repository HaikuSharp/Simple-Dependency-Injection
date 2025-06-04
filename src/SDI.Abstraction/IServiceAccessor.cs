using System.Collections.Generic;
using System.Reflection;
namespace SDI.Abstraction;
public interface IServiceAccessor {
 bool CanAccess(ServiceId id);
 object Access(IServiceProvider provider);
}
