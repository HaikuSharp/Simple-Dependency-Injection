namespace SDI.Abstraction;
public interface IServiceKeyResolver<in TKeySource> {
 object Resolve(TKeySource source);
}
