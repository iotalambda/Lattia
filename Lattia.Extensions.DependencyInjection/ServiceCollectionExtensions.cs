using Lattia;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLattiaExtensions(this IServiceCollection services)
        {
            services.AddSingleton<IInitializePropertyTypeNode, Lattia.Setups.ReadOnly.InitializePropertyTypeNode>();
            
            return services;
        }
    }
}
