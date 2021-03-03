using Lattia.Attributes;
using Lattia.Contexts;

namespace Lattia.Setups.ReadOnly
{
    public class CheckPropertyWriteSanityGate : ICheckPropertyWriteSanityGate
    {
        public CheckPropertyGateResult CheckPropertyGate(CheckPropertyWriteGateContext context)
        {
            var node = context.PropertyTypeNode;

            if (node.Extensions.TryGet<ReadOnlyAttribute, bool>(out var isReadOnly) && isReadOnly)
            {
                return new CheckPropertyGateResult.Nok.InvalidRequest();
            }

            return CheckPropertyGateResult.Ok.Instance;
        }
    }
}
