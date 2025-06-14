﻿using SDI.Abstraction;
using System;

namespace SDI.Exceptions;

public class ServiceAlreadyImplementedException(ServiceId id) : Exception($"Service with [id: {id}] is already implemented.")
{
    public static void ThrowIfImplemented(Abstraction.IServiceProvider provider, ServiceId id)
    {
        if(!provider.IsImplemented(id)) return;
        throw new ServiceAlreadyImplementedException(id);
    }
}
