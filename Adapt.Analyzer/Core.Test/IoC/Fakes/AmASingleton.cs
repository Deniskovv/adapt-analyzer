using Adapt.Analyzer.Core.IoC;
using Adapt.Analyzer.Core.Test.IoC.Fakes.Adapt.Analyzer.Core.Test.IoC.Fakes;

namespace Adapt.Analyzer.Core.Test.IoC.Fakes
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