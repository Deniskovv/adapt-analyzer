using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SimpleInjector;

namespace Adapt.Analyzer.IoC
{
    public interface IContainer : IDisposable
    {
        T Get<T>() where T : class;
        object Get(Type type);
        void RegisterTypes(Type[] types);
    }

    public class Container : IContainer
    {
        private SimpleInjector.Container _simpleContainer;
        private bool _isDisposed;

        public Container(params Assembly[] assembly)
        {
            var registrations = GetRegistrations(assembly);
            _simpleContainer = CreateSimpleContainer(registrations);
        }

        public T Get<T>() where T : class
        {
            return Get(typeof(T)) as T;
        }

        public object Get(Type type)
        {
            if (_isDisposed)
                throw new ObjectDisposedException("Container has been disposed.");

            return _simpleContainer.GetInstance(type);
        }

        public void RegisterTypes(Type[] types)
        {
            foreach (var type in types)
            {
                var registration = Lifestyle.Transient.CreateRegistration(type, _simpleContainer);
                _simpleContainer.AddRegistration(type, registration);
            }
        }

        public void Dispose()
        {
            if (_isDisposed)
                return;

            _simpleContainer.Dispose();
            _simpleContainer = null;
            _isDisposed = true;
        }

        private static IEnumerable<Registration> GetRegistrations(Assembly[] assembly)
        {
            return assembly
                .SelectMany(a => a.GetTypes())
                .Where(t => t.HasAttribute<DependencyAttribute>())
                .Select(t => new Registration(t));
        }

        private static SimpleInjector.Container CreateSimpleContainer(IEnumerable<Registration> registrations)
        {
            var simpleContainer = new SimpleInjector.Container();
            foreach (var registration in registrations)
                Register(simpleContainer, registration);
            return simpleContainer;
        }

        private static void Register(SimpleInjector.Container container, Registration registration)
        {
            var lifestyle = registration.RegistrationType == RegistrationType.Singleton ? Lifestyle.Singleton : Lifestyle.Transient;
            container.Register(registration.ServiceType, registration.ImplementationType, lifestyle);
        }
    }
}