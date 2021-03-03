namespace Lattia.Contexts
{
    public class CheckPropertyWriteGateContext
    {
        public CheckPropertyWriteGateContext(PropertyTypeNode propertyTypeNode, PropertyValueNode propertyValueNode)
        {
            PropertyTypeNode = propertyTypeNode;

            PropertyValueNode = propertyValueNode;
        }

        public PropertyTypeNode PropertyTypeNode { get; }

        public PropertyValueNode PropertyValueNode { get; }
    }
}
