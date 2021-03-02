namespace Lattia.Contexts
{
    public class CheckPropertyWritePermissionContext
    {
        public CheckPropertyWritePermissionContext(PropertyTypeNode propertyTypeNode, PropertyValueNode propertyValueNode)
        {
            PropertyTypeNode = propertyTypeNode;
            PropertyValueNode = propertyValueNode;
        }

        public PropertyTypeNode PropertyTypeNode { get; }

        public PropertyValueNode PropertyValueNode { get; }
    }
}
