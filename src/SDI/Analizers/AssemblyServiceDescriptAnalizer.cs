using SDI.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
namespace SDI.Analizers;
public class AssemblyServiceDescriptAnalizer(IServiceDescriptAnalizer<Type> typeAnalizer) : IServiceDescriptAnalizer<Assembly> {
 public IEnumerable<IServiceDescriptor> Analize(Assembly source) {
  return source.GetTypes().SelectMany(typeAnalizer.Analize);
 }
}
