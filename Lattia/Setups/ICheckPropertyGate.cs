namespace Lattia.Setups
{
    public interface ICheckPropertyGate<TContext>
    {
        CheckPropertyGateResult CheckPropertyGate(TContext context);
    }
}
