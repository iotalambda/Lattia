using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;

namespace Lattia.Executors
{
    public interface IExecuteBeforeActionExecuting
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="results">Request models' CheckPropertyGateResults</param>
        /// <param name="context"></param>
        /// <returns>False terminates request execution</returns>
        bool Execute(IEnumerable<CheckPropertyGateResult.Nok> results, ActionExecutingContext context);
    }
}
