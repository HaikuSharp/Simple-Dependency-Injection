using SDI.Attribute;
using SDI.LifeTimes.Lazy;
using Sugar.Object.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
namespace SDI.Test.Types;
public interface IService {
 void PrintHello();
}
public class PrintHelloService : IService {
 public void PrintHello() {
  Console.WriteLine($"Hello! Im {this.Type.FullName}");
 }
}
public interface IFirstService : IService;
public interface ISecondService : IService {
 IFirstService First { get; }
}
[TypeInject(typeof(IFirstService)), KeyInject("0"), LifeTimeInject(typeof(LazySingletonServiceLifeTime))]
public class FirstService0 : PrintHelloService, IFirstService;
[TypeInject(typeof(IFirstService)), KeyInject("1"), LifeTimeInject(typeof(LazySingletonServiceLifeTime))]
public class FirstService1 : PrintHelloService, IFirstService;
[TypeInject(typeof(ISecondService)), KeyInject("default"), LifeTimeInject(typeof(LazySingletonServiceLifeTime))]
public class SecondService(IEnumerable<IFirstService> firsts) : PrintHelloService, ISecondService {
 public IFirstService First {
  get {
   return firsts.FirstOrDefault();
  }
 }
}
