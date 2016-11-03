using Adapt.Analyzer.Core.IoC;
using Adapt.Analzyer.Api.IoC;
using Fakes.General;
using Fakes.IoC;
using NUnit.Framework;

namespace Adapt.Analyzer.Api.Test.IoC
{
    [TestFixture]
    public class DependencyResolverTest
    {
        private FakeContainer _container;
        private DependencyResolver _resolver;

        [SetUp]
        public void Setup()
        {
            _container = new FakeContainer();
            _resolver = new DependencyResolver(_container);
        }

        [Test]
        public void ShouldCreateDependencyFromContainer()
        {
            _container.Setup<IContainer, FakeContainer>(_container);

            var service = _resolver.GetService(typeof(IContainer));
            Assert.AreSame(_container, service);
        }

        [Test]
        public void ShouldDisposeOfContainer()
        {
            _resolver.Dispose();
            Assert.IsTrue(_container.IsDisposed);
        }
    }
}
