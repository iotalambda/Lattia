using Lattia.Contexts;
using Lattia.Setups;
using System.Collections.Generic;
using System.Linq;

namespace Lattia.Pipelines
{
    public class CheckPropertyPermissionsPipeline : ICheckPropertyPermissionsPipeline
    {
        private readonly IEnumerable<ICheckPropertyPermissions> items;

        public CheckPropertyPermissionsPipeline(IEnumerable<ICheckPropertyPermissions> items)
        {
            this.items = items;
        }

        public bool CheckPropertyReadPermission(CheckPropertyReadPermissionContext context)
        {
            return items.All(i => i.CheckPropertyReadPermission(context));
        }

        public bool CheckPropertyWritePermission(CheckPropertyWritePermissionContext context)
        {
            return items.All(i => i.CheckPropertyWritePermission(context));
        }
    }
}
