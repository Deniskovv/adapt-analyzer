using System;

namespace Adapt.Analyzer.IoC
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DependencyAttribute : Attribute
    {
        public RegistrationType RegistrationType { get; }
        public Type ServiceType { get; }

        public DependencyAttribute(Type serviceType, RegistrationType registrationType = RegistrationType.Transient)
        {
            ServiceType = serviceType;
            RegistrationType = registrationType;
        }
    }
}
