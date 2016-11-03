using Adapt.Analyzer.Core.IoC;

namespace Adapt.Analyzer.Core.Test.IoC.Fakes
{
    public interface IAmATransient
    {
        
    }

    [Dependency(typeof(IAmATransient))]
    public class AmATransient : IAmATransient
    {
    }
}
