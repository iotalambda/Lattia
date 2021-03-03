using Lattia;
using Lattia.Attributes;
using Lattia.Contexts;
using Lattia.Pipelines;
using Lattia.Setups;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLattia(this IServiceCollection services, params Type[] modelTypes)
        {
            services.AddTransient<ICheckPropertyPermissionsPipeline, CheckPropertyPermissionsPipeline>();
            services.AddTransient<IInitializePropertyTypeNodePipeline, InitializePropertyTypeNodePipeline>();

            services.AddSingleton<LattiaSingletonContext>(s =>
            {
                var lattiaContext = new LattiaSingletonContext();

                var pipeline = s.GetRequiredService<IInitializePropertyTypeNodePipeline>();

                foreach (var modelType in modelTypes)
                {
                    var nodes = new List<PropertyTypeNode>();

                    PropertyTypeVisitor.Traverse(modelType, n =>
                    {
                        pipeline.InitializePropertyTypeNode(new InitializePropertyTypeNodeContext(n));

                        nodes.Add(n);
                    });

                    foreach (var n in nodes)
                    {
                        lattiaContext.PropertyTypeNodes[n.Path] = n;
                    }

                    lattiaContext.ModelTypeFullNameToPropertyPaths[modelType.FullName] = nodes.Select(n => n.Path).ToList();
                }

                return lattiaContext;
            });

            services.AddSingleton<CheckPropertyPermissionsService>();

            return services;
        }
    }
}
