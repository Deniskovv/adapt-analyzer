using Adapt.Analzyer.Api;
using Adapt.Analzyer.Api.IoC;
using NUnit.Framework;

namespace Adapt.Analyzer.Api.Test
{
    [TestFixture]
    public class HttpConfigFactoryTest
    {
        [Test]
        public void ShouldSetupDependencyResolver()
        {
            var config = HttpConfigFactory.Create();
            Assert.IsInstanceOf<DependencyResolver>(config.DependencyResolver);
        }
    }
}
