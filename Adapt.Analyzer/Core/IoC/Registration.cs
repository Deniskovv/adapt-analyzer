using System;
using System.Reflection;

namespace Adapt.Analyzer.Core.IoC
{
    public class Registration
    {
        public Registration(Type implementationType)
        {
            var attribute = implementationType.GetCustomAttribute<DependencyAttribute>();
            ImplementationType = implementationType;
            ServiceType = attribute.ServiceType;
            RegistrationType = attribute.RegistrationType;
        }

        public Type ServiceType { get; }
        public RegistrationType RegistrationType { get; }
        public Type ImplementationType { get; }
    }
}