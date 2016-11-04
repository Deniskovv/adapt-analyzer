using System;
using Adapt.Analyzer.IoC.Test.Fakes;
using Adapt.Analyzer.IoC.Test.Fakes.Adapt.Analyzer.Core.Test.IoC.Fakes;
using Fakes.IoC;
using NUnit.Framework;
using Adapt.Analyzer.IoC;
using Fakes.General;

namespace Adapt.Analyzer.IoC.Test
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
                typeof(AmATransient),
                typeof(NotInTestAssembly)
            };

            _container.RegisterTypes(types);
            Assert.IsInstanceOf<AmATransient>(_container.Get<AmATransient>());
            Assert.IsInstanceOf<NotInTestAssembly>(_container.Get<NotInTestAssembly>());
        }
    }
}
