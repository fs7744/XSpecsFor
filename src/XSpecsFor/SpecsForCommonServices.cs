using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Reflection;

namespace XSpecsFor
{
    internal sealed class SpecsForCommonServices
    {
        internal static readonly SpecsForCommonServices Instance = new SpecsForCommonServices();

        internal ServiceCollection Services { get; private set; } = new ServiceCollection();
        private HashSet<Assembly> _RegistedAssembly = new HashSet<Assembly>();

        internal void RegisterCommonServices(Assembly assembly)
        {
            if (_RegistedAssembly.Contains(assembly)) return;
            var scanner = new AssemblyScanner(assembly);
            var setupers = scanner.GetInstances<ICommonServicesSetuper>();
            foreach (var item in setupers)
            {
                item.SetUp(Services);
            }
        }

        internal IServiceCollection Copy()
        {
            var services = new ServiceCollection();
            var s = services as ICollection<ServiceDescriptor>;
            foreach (var item in Services)
            {
                s.Add(item);
            }

            return services;
        }
    }
}