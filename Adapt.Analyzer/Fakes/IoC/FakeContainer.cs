using System;
using System.Collections.Generic;
using Adapt.Analyzer.Core.IoC;

namespace Fakes.IoC
{
    public class FakeContainer : IContainer
    {
        private readonly Dictionary<Type, object> _registrations;

        public bool IsDisposed { get; private set; }

        public FakeContainer()
        {
            _registrations = new Dictionary<Type, object>();
        }

        public T Get<T>() where T : class
        {
            return Get(typeof(T)) as T;
        }

        public object Get(Type type)
        {
            return _registrations[type];
        }

        public void Setup<TServiceType, TImplementation>(TImplementation instance)
        {
            _registrations[typeof(TServiceType)] = instance;
        }

        public void Dispose()
        {
            IsDisposed = true;
        }

        public void RegisterTypes(Type[] types)
        {
            throw new NotImplementedException();
        }
    }
}
