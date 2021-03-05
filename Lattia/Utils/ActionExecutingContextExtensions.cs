using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace Lattia.Utils
{
    public static class ActionExecutingContextExtensions
    {
        public static IEnumerable<LattiaModelArgument> GetLattiaModelArguments(this ActionExecutingContext context)
        {
            var lattiaContext = context.HttpContext.RequestServices.GetRequiredService<LattiaSingletonContext>();

            var results = (from arg in context.ActionArguments
                           join par in (context.ActionDescriptor as ControllerActionDescriptor).MethodInfo.GetParameters() on arg.Key equals par.Name
                           join mod in lattiaContext.ModelTypeFullNameToPropertyPaths on arg.Value.GetType().FullName equals mod.Key
                           where par.ParameterType.GetInterfaces().Contains(typeof(IModel))
                           select new LattiaModelArgument(arg.Value, par)).ToArray();

            return results;
        }
    }
}
