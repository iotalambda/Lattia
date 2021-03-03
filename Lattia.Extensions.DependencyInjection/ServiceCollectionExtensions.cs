using Lattia.Setups;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLattiaExtensions(this IServiceCollection services)
        {
            services.AddSingleton<ReadOnlyAttributeSetup>();
            services.AddSingleton<IInitializePropertyTypeNode, ReadOnlyAttributeSetup>();
            services.AddSingleton<ICheckPropertyPermissions, ReadOnlyAttributeSetup>();

            return services;
        }
    }
}
