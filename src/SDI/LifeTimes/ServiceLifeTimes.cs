using SDI.LifeTimes.Lazy;
using System;
namespace SDI.LifeTimes;
public class ServiceLifeTimes {
 public static Type Singleton {
  get {
   return typeof(SingletonServiceLifeTime);
  }
 }
 public static Type LazySingleton {
  get {
   return typeof(LazySingletonServiceLifeTime);
  }
 }
 public static Type Transient {
  get {
   return typeof(TransientServiceLifeTime);
  }
 }
}
