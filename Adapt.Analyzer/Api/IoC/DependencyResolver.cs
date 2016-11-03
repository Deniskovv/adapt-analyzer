using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Adapt.Analyzer.Core.IoC;

namespace Adapt.Analzyer.Api.IoC
{
    public class DependencyResolver : IDependencyResolver
    {
        private readonly IContainer _container;

        public DependencyResolver(IContainer container)
        {
            _container = container;
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        public object GetService(Type serviceType)
        {
            return _container.Get(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public IDependencyScope BeginScope()
        {
            throw new NotImplementedException();
        }
    }
}