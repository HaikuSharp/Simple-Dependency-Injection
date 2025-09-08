using SDI.SCG.Attributes;

namespace FAST_TEST;

[ServiceImplementation(typeof(Service))]
[method: ServiceConstructor]
public class Service(SubService sub)
{
    public SubService Sub => sub;
}

[ServiceImplementation(typeof(Service))]
[method: ServiceConstructor]
public class SubService()
{
}
