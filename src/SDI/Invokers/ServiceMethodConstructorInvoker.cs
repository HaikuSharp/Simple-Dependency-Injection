using System.Reflection;
namespace SDI.Invokers;
public class ServiceMethodConstructorInvoker(MethodInfo method) : ServiceConstructorInvokerBase<MethodInfo>(method);
