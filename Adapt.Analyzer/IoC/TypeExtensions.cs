using System;
using System.Reflection;

namespace Adapt.Analyzer.IoC
{
    public static class TypeExtensions
    {
        public static bool HasAttribute<T>(this Type type)
        {
            return type.GetCustomAttribute<DependencyAttribute>() != null;
        }
    }
}