using Lattia.Services;
using Lattia.Setups.ReadOnly;
using Lattia.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Lattia.Webi
{
    public class RequirePropertyGateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var models = context.GetLattiaModelArguments().Select(a => a.Value).ToList();
            var service = context.HttpContext.RequestServices.GetRequiredService<IPropertyGatesService>();

            var sanityErrors = service.CheckForErrors(models, ReadOnlyPropertyGate.Instance);
            if (sanityErrors.Any())
            {
                context.Result = new StatusCodeResult(400);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
