namespace Adapt.Analyzer.IoC.Test.Fakes
{
    public interface IAmATransient
    {
        
    }

    [Dependency(typeof(IAmATransient))]
    public class AmATransient : IAmATransient
    {
    }
}
