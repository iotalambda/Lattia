using Lattia.Contexts;
using Lattia.Pipelines;
using Lattia.Setups;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lattia.Services
{
    public class CheckPropertyGatesService : ICheckPropertyGatesService
    {
        private readonly LattiaSingletonContext context;

        private readonly ICheckPropertyGatesPipeline<ICheckPropertyWritePermissionGate, CheckPropertyWriteGateContext> writePermissionsPipeline;

        private readonly ICheckPropertyGatesPipeline<ICheckPropertyWriteSanityGate, CheckPropertyWriteGateContext> writeSanityPipeline;

        public CheckPropertyGatesService(
            LattiaSingletonContext context,
            ICheckPropertyGatesPipeline<ICheckPropertyWritePermissionGate, CheckPropertyWriteGateContext> writePermissionsPipeline,
            ICheckPropertyGatesPipeline<ICheckPropertyWriteSanityGate, CheckPropertyWriteGateContext> writeSanityPipeline)
        {
            this.context = context;

            this.writePermissionsPipeline = writePermissionsPipeline;

            this.writeSanityPipeline = writeSanityPipeline;
        }

        public IEnumerable<CheckPropertyGateResult.Nok> CheckPropertyWriteGates(IEnumerable<object> models)
        {
            var contexts = new List<CheckPropertyWriteGateContext>();

            foreach (var model in models)
            {
                PropertyValueVisitor.Traverse(model, n => contexts.Add(new CheckPropertyWriteGateContext(context.PropertyTypeNodes[n.Path], n)));
            }

            var sanityNoks = contexts
                .Select(writeSanityPipeline.CheckPropertyGates)
                .Where(r => r is CheckPropertyGateResult.Nok)
                .Cast<CheckPropertyGateResult.Nok>()
                .ToArray();

            if (sanityNoks.Any())
            {
                return sanityNoks;
            }

            var permissionNoks = contexts
                .Select(writePermissionsPipeline.CheckPropertyGates)
                .Where(r => r is CheckPropertyGateResult.Nok)
                .Cast<CheckPropertyGateResult.Nok>()
                .ToArray();

            if (permissionNoks.Any())
            {
                return permissionNoks;
            }

            return Array.Empty<CheckPropertyGateResult.Nok>();
        }
    }
}