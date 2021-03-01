using System.Reflection;

namespace Lattia
{
    public sealed class PropertyValueNode : PropertyNode<PropertyValueNode>
    {
        public PropertyValueNode(PropertyValueNode declaring, Property value, PropertyInfo propertyInfo)
            : base(declaring, propertyInfo.Name)
        {
            Value = value;
        }

        public Property Value { get; }
    }
}
