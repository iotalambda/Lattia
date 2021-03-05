namespace Lattia
{
    public interface ICheckPropertyGate
    {
        CheckPropertyGateResult Check(PropertyTypeNode propertyType, PropertyValueNode propertyValue);
    }
}
