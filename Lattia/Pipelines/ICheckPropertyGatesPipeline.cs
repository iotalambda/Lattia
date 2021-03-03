using Lattia.Setups;

namespace Lattia.Pipelines
{
    public interface ICheckPropertyGatesPipeline<TGate, TContext> where TGate : ICheckPropertyGate<TContext>
    {
        CheckPropertyGateResult CheckPropertyGates(TContext context);
    }
}
