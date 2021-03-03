using Lattia.Setups;
using System.Collections.Generic;

namespace Lattia.Pipelines
{
    public class CheckPropertyGatesPipeline<TGate, TContext> : ICheckPropertyGatesPipeline<TGate, TContext>
        where TGate : ICheckPropertyGate<TContext>
    {
        private readonly IEnumerable<TGate> gates;

        public CheckPropertyGatesPipeline(IEnumerable<TGate> gates)
        {
            this.gates = gates;
        }

        public CheckPropertyGateResult CheckPropertyGates(TContext context)
        {
            foreach (var gate in gates)
            {
                switch (gate.CheckPropertyGate(context))
                {
                    case CheckPropertyGateResult.Ok _:
                        continue;

                    case CheckPropertyGateResult.Nok nok:
                        // Just return naively the first nok result.
                        return nok;
                }
            }

            return CheckPropertyGateResult.Ok.Instance;
        }
    }
}
