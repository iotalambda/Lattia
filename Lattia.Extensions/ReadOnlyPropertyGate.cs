namespace Lattia
{
    public class ReadOnlyPropertyGate : ICheckPropertyGate
    {
        public static readonly ReadOnlyPropertyGate Instance = new ReadOnlyPropertyGate();

        public CheckPropertyGateResult Check(PropertyTypeNode propertyType, PropertyValueNode propertyValue)
        {
            if (propertyType.Extensions.TryGet<ReadOnlyAttribute, bool>(out var isReadOnly) && isReadOnly)
            {
                return new CheckPropertyGateResult.Error();
            }

            return CheckPropertyGateResult.Success.Instance;
        }
    }
}
