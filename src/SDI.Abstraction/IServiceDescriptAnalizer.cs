using System.Collections.Generic;
namespace SDI.Abstraction;
public interface IServiceDescriptAnalizer<TDescriptSource> {
 IEnumerable<IServiceDescriptor> Analize(TDescriptSource source);
}
