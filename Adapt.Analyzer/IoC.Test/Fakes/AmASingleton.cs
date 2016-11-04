using Adapt.Analyzer.IoC.Test.Fakes.Adapt.Analyzer.Core.Test.IoC.Fakes;

namespace Adapt.Analyzer.IoC.Test.Fakes
{
    namespace Adapt.Analyzer.Core.Test.IoC.Fakes
    {
        public interface IAmASingleton
        {

        }
    }

    [Dependency(typeof(IAmASingleton), RegistrationType.Singleton)]
    public class AmASingleton : IAmASingleton
    {
        
    }
}