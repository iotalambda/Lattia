using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Lattia.Attributes
{
    public class RequirePropertyPermissionsAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var service = context.HttpContext.RequestServices.GetRequiredService<PermissionCheckingService>();

            var lattiaContext = context.HttpContext.RequestServices.GetRequiredService<LattiaContext>();

            var models = from arg in context.ActionArguments
                         join par in (context.ActionDescriptor as ControllerActionDescriptor).MethodInfo.GetParameters() on arg.Key equals par.Name
                         join mod in lattiaContext.ModelTypeFullNameToPropertyPaths on arg.Value.GetType().FullName equals mod.Key
                         where par.GetCustomAttributes(true)?.Any(p => p is RequirePropertyWritePermissions) == true
                         select arg.Value;

            var isAuthorized = true;

            foreach (var m in models)
            {
                isAuthorized &= service.IsAuthorizedToWriteProperties(m);
            }

            if (!isAuthorized)
            {
                context.Result = new StatusCodeResult(403);

                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
