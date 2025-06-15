using SDI.LifeTimes.Lazy;
using System;
namespace SDI.LifeTimes;

public class ServiceLifeTimes
{
    public static Type Singleton => typeof(SingletonServiceLifeTime);

    public static Type LazySingleton => typeof(LazySingletonServiceLifeTime);

    public static Type Transient => typeof(TransientServiceLifeTime);
}
