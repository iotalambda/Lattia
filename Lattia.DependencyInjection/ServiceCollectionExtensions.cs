using Lattia;
using Lattia.Pipelines;
using Lattia.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLattia(this IServiceCollection services, params Type[] modelTypes)
        {
            // Pipelines
            services.AddSingleton<IInitializePropertyTypeNodePipeline, InitializePropertyTypeNodePipeline>();

            // Services
            services.AddSingleton<IPropertyGatesService, PropertyGatesService>();

            services.AddSingleton<LattiaSingletonContext>(s =>
            {
                var lattiaContext = new LattiaSingletonContext();

                var pipeline = s.GetRequiredService<IInitializePropertyTypeNodePipeline>();

                foreach (var modelType in modelTypes)
                {
                    var nodes = new List<PropertyTypeNode>();

                    PropertyTypeVisitor.Traverse(modelType, propertyType =>
                    {
                        pipeline.InitializePropertyTypeNode(propertyType);

                        nodes.Add(propertyType);
                    });

                    foreach (var n in nodes)
                    {
                        lattiaContext.PropertyTypeNodes[n.Path] = n;
                    }

                    lattiaContext.ModelTypeFullNameToPropertyPaths[modelType.FullName] = nodes.Select(n => n.Path).ToList();
                }

                return lattiaContext;
            });

            return services;
        }
    }
}
