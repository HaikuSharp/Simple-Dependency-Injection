using SDI.Abstraction;
using SDI.Exceptions;
using Sugar.Object.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDI.LifeTimes;

public class ServiceLifeTimeManager(IEnumerable<IServiceLifeTime> lifeTimes) : IServiceLifeTime
{
    private readonly Dictionary<Type, IServiceLifeTime> m_LifeTimesMap = lifeTimes.ToDictionary(l => l.GetType());

    public IServiceAccessor CreateAccessor(IServiceInstanceContanier contanier, IServiceDescriptor descriptor)
    {
        Type type = descriptor.LifeTimeType;
        if(!m_LifeTimesMap.TryGetValue(type, out IServiceLifeTime lifeTime)) InvalidServiceLifeTimeException.ThrowInvalidType(ServiceId.FromDescriptor(descriptor), type);
        return lifeTime.CreateAccessor(contanier, descriptor);
    }
}
