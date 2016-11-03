using System;
using Adapt.Analyzer.Core.IoC;
using Adapt.Analyzer.Core.Test.IoC.Fakes;
using Adapt.Analyzer.Core.Test.IoC.Fakes.Adapt.Analyzer.Core.Test.IoC.Fakes;
using Fakes.General;
using Fakes.IoC;
using NUnit.Framework;

namespace Adapt.Analyzer.Core.Test.IoC
{
    [TestFixture]
    public class ContainerTest
    {
        private Container _container;

        [SetUp]
        public void Setup()
        {
            _container = new Container(typeof(ContainerTest).Assembly, typeof(INotInTestAssembly).Assembly);
        }

        [Test]
        public void ShouldGetTheSameInstanceIfSingleton()
        {
            var first = _container.Get<IAmASingleton>();
            var second = _container.Get<IAmASingleton>();

            Assert.AreSame(first, second);
        }

        [Test]
        public void ShouldGetNewInstanceIfTransient()
        {
            var first = _container.Get<IAmATransient>();
            var second = _container.Get<IAmATransient>();

            Assert.AreNotSame(first, second);
        }

        [Test]
        public void ShouldRegisterInstancesFromAllAssemblies()
        {
            var instance = _container.Get<INotInTestAssembly>();
            Assert.IsNotNull(instance);
        }

        [Test]
        public void ShouldThrowDisposedExceptionAfterDisposed()
        {
            _container.Dispose();
            Assert.Throws<ObjectDisposedException>(() => _container.Get<INotInTestAssembly>());
        }

        [Test]
        public void ShouldGetTypeOfServiceFromType()
        {
            var type = typeof(IAmATransient);
            var instance = _container.Get(type);
            Assert.IsInstanceOf<AmATransient>(instance);
        }

        [Test]
        public void ShouldRegisterTypesAsTransient()
        {
            var types = new[]
            {
                typeof(ConfigFake),
                typeof(FileFake)
            };

            _container.RegisterTypes(types);
            Assert.IsInstanceOf<ConfigFake>(_container.Get<ConfigFake>());
            Assert.IsInstanceOf<FileFake>(_container.Get<FileFake>());
        }
    }
}
