using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

namespace Adapt.Analyzer.IoC
{
    public class DependencyResolver : IDependencyResolver
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
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
