using System;
namespace SDI.Attribute;
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter)]
public class KeyInjectAttribute(object key) : InjectAttribute {
 public object Key {
  get {
   return key;
  }
 }
}
