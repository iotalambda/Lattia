using Lattia.Contexts;
using Lattia.Executors;
using Lattia.Services;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Lattia.Attributes
{
    public class RequirePropertyGateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var service = context.HttpContext.RequestServices.GetRequiredService<ICheckPropertyGatesService>();

            var executor = context.HttpContext.RequestServices.GetService<IExecuteBeforeActionExecuting>();

            var lattiaContext = context.HttpContext.RequestServices.GetRequiredService<LattiaSingletonContext>();

            var models = (from arg in context.ActionArguments
                join par in (context.ActionDescriptor as ControllerActionDescriptor).MethodInfo.GetParameters() on arg.Key equals par.Name
                join mod in lattiaContext.ModelTypeFullNameToPropertyPaths on arg.Value.GetType().FullName equals mod.Key
                where par.GetCustomAttributes(true)?.Any(p => p is RequirePropertyWriteGates) == true
                select arg.Value).ToArray();

            var checkPropertyGateResults = service.CheckPropertyWriteGates(models);

            if (!executor.Execute(checkPropertyGateResults, context))
            {
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
