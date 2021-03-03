using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lattia.Executors
{
    public class ExecuteBeforeActionExecuting : IExecuteBeforeActionExecuting
    {
        public bool Execute(IEnumerable<CheckPropertyGateResult.Nok> results, ActionExecutingContext context)
        {
            if (!results.Any())
            {
                return true;
            }

            if (results.Any(r => r is CheckPropertyGateResult.Nok.InvalidRequest))
            {
                context.Result = new StatusCodeResult(400);

                return false;
            }

            if (results.Any(r => r is CheckPropertyGateResult.Nok.NoPermission))
            {
                context.Result = new StatusCodeResult(403);

                return false;
            }

            throw new NotSupportedException();
        }
    }
}
