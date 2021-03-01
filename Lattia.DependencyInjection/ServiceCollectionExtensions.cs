using Lattia;
using Lattia.Attributes;
using Lattia.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLattia(this IServiceCollection services, params Type[] modelTypes)
        {
            var lattiaContext = new LattiaContext();

            foreach (var t in modelTypes)
            {
                var nodes = new List<PropertyTypeNode>();

                PropertyTypeVisitor.Traverse(t, n =>
                {
                    if (n.PropertyInfo.GetCustomAttributes(true)?.Any(a => a is ReadOnlyAttribute) == true)
                    {
                        n.Extensions["IsReadOnly"] = true;
                    }

                    if (n.Declaring != null && (bool)n.Declaring.Extensions.TryGetValue("IsReadOnly", out var obj) && (bool)obj == true)
                    {
                        n.Extensions["IsReadOnly"] = true;
                    }

                    nodes.Add(n);
                });

                foreach (var n in nodes)
                {
                    lattiaContext.PropertyTypeNodes[n.Path] = n;
                }

                lattiaContext.ModelTypeFullNameToPropertyPaths[t.FullName] = nodes.Select(n => n.Path).ToList();
            }

            services.AddSingleton(lattiaContext);

            services.AddSingleton<PermissionCheckingService>();

            return services;
        }
    }
}
