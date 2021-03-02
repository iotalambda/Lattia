namespace Lattia.Contexts
{
    public class InitializePropertyTypeNodeContext
    {
        public InitializePropertyTypeNodeContext(PropertyTypeNode propertyTypeNode)
        {
            PropertyTypeNode = propertyTypeNode;
        }

        public PropertyTypeNode PropertyTypeNode { get; }
    }
}
