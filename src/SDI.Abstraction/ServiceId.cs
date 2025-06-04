using Sugar.Object;
using Sugar.Object.Extensions;
using System;
using System.Collections.Generic;
namespace SDI.Abstraction;
public readonly struct ServiceId {
 private readonly Type m_Type;
 private readonly object m_Key;
 private ServiceId(Type type, object key) {
  this.m_Type = type;
  this.m_Key = key;
 }
 public static ServiceId FromType(Type type) {
  return FromType(type, null);
 }
 public static ServiceId FromType(Type type, object key) {
  return new(type, key ?? AnyObject.Any);
 }
 public static ServiceId From<T>() {
  return From<T>(null);
 }
 public static ServiceId From<T>(object key) {
  return FromType(typeof(T), key);
 }
 public static ServiceId FromDescriptor(IServiceDescriptor descriptor) {
  return FromType(descriptor.ServiceType, descriptor.Key);
 }
 public bool Equals(ServiceId other) {
  return this.m_Type.Equals(other.m_Type) && EqualsKeys(this.m_Key, other.m_Key);
 }
 public override bool Equals(object other) {
  return other is ServiceId otherServiceId && this.Equals(otherServiceId);
 }
 public override int GetHashCode() {
  var hashCode = 1193439425;
  hashCode = (hashCode * -1521134295) + EqualityComparer<Type>.Default.GetHashCode(this.m_Type);
  hashCode = (hashCode * -1521134295) + EqualityComparer<object>.Default.GetHashCode(this.m_Key);
  return hashCode;
 }
 public override string ToString() {
  return $"({this.m_Type.FullName}, {this.m_Key?.ToString() ?? string.Empty})";
 }
 private static bool EqualsKeys(object left, object rigth) {
  if(left.IsNotNull) {
   return left.Equals(rigth);
  }
  if(rigth.IsNotNull) {
   return rigth.Equals(left);
  }
  return true;
 }
 public static bool operator ==(ServiceId left, ServiceId rigth) {
  return left.Equals(rigth);
 }
 public static bool operator !=(ServiceId left, ServiceId rigth) {
  return !(left == rigth);
 }
}
