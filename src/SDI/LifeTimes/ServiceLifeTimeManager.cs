using SDI.Abstraction;
using SDI.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDI.LifeTimes;

public class ServiceLifeTimeManager(IEnumerable<IServiceLifeTime> lifeTimes) : IServiceLifeTime
{
    private readonly Dictionary<Type, IServiceLifeTime> m_LifeTimesMap = lifeTimes.ToDictionary(l => l.GetType());

    public IServiceAccessor CreateAccessor(IServiceDescriptor descriptor)
    {
        var type = descriptor.LifeTimeType;
        if(!m_LifeTimesMap.TryGetValue(type, out var lifeTime)) InvalidServiceLifeTimeException.ThrowInvalidType(ServiceId.FromDescriptor(descriptor), type);
        return lifeTime.CreateAccessor(descriptor);
    }
}
