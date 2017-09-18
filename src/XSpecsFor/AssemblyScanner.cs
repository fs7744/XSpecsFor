using System;
using System.Linq;
using System.Reflection;

namespace XSpecsFor
{
    internal class AssemblyScanner
    {
        private Assembly _Assembly;

        internal AssemblyScanner(Assembly assembly)
        {
            _Assembly = assembly;
        }

        internal Type[] FindTypes<T>()
        {
            var types = _Assembly.GetExportedTypes().Distinct();
            var resultType = typeof(T);
            var query = from type in types
                        where !type.IsAbstract && !type.IsGenericTypeDefinition
                        let interfaces = type.GetInterfaces()
                        let genericInterfaces = interfaces.Where(i => i == resultType)
                        let matchingInterface = genericInterfaces.FirstOrDefault()
                        where matchingInterface != null
                        select type;
            return query.ToArray();
        }

        internal T[] GetInstances<T>()
        {
            var types = FindTypes<T>();
            return types.Select(i => (T)Activator.CreateInstance(i)).ToArray();
        }
    }
}