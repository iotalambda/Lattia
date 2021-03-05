namespace Lattia
{
    public class WriteOnlyPropertyGate : ICheckPropertyGate
    {
        public static readonly WriteOnlyPropertyGate Instance = new WriteOnlyPropertyGate();

        public CheckPropertyGateResult Check(PropertyTypeNode propertyType, PropertyValueNode propertyValue)
        {
            if (propertyType.Extensions.TryGet<WriteOnlyAttribute, bool>(out var isWriteOnly) && isWriteOnly)
            {
                return new CheckPropertyGateResult.Error();
            }

            return CheckPropertyGateResult.Success.Instance;
        }
    }
}
