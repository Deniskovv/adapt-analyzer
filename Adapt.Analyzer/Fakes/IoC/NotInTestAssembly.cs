using Adapt.Analyzer.Core.IoC;

namespace Fakes.IoC
{
    public interface INotInTestAssembly
    {
        
    }

    [Dependency(typeof(INotInTestAssembly))]
    public class NotInTestAssembly : INotInTestAssembly
    {
    }
}
