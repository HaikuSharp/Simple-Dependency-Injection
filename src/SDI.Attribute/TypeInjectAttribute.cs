using System;
namespace SDI.Attribute;
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter)]
public class TypeInjectAttribute(Type type) : InjectAttribute {
 public Type ServiceType {
  get {
   return type;
  }
 }
}
