using System;
namespace SDI.Attribute;
[AttributeUsage(AttributeTargets.Class)]
public class LifeTimeInjectAttribute(Type lifeTimeType) : InjectAttribute {
 public Type LifeTimeType {
  get {
   return lifeTimeType;
  }
 }
}