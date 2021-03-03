using Lattia;
using Lattia.Attributes;
using Lattia.Contexts;
using Lattia.Executors;
using Lattia.Pipelines;
using Lattia.Services;
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
            // Executors
            services.AddScoped<IExecuteBeforeActionExecuting, ExecuteBeforeActionExecuting>();

            // Pipelines
            services.AddSingleton<IInitializePropertyTypeNodePipeline, InitializePropertyTypeNodePipeline>();
            services.AddSingleton<ICheckPropertyGatesPipeline<ICheckPropertyReadPermissionGate, CheckPropertyReadGateContext>, CheckPropertyGatesPipeline<ICheckPropertyReadPermissionGate, CheckPropertyReadGateContext>>();
            services.AddSingleton<ICheckPropertyGatesPipeline<ICheckPropertyWritePermissionGate, CheckPropertyWriteGateContext>, CheckPropertyGatesPipeline<ICheckPropertyWritePermissionGate, CheckPropertyWriteGateContext>>();
            services.AddSingleton<ICheckPropertyGatesPipeline<ICheckPropertyWriteSanityGate, CheckPropertyWriteGateContext>, CheckPropertyGatesPipeline<ICheckPropertyWriteSanityGate, CheckPropertyWriteGateContext>>();
            
            // Services
            services.AddSingleton<ICheckPropertyGatesService, CheckPropertyGatesService>();

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

            return services;
        }
    }
}
