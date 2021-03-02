using System.Reflection;

namespace Lattia
{
    public sealed class PropertyTypeNode : PropertyNode<PropertyTypeNode>
    {
        public PropertyTypeNode(PropertyTypeNode declaring, PropertyInfo propertyInfo)
            : base(declaring, propertyInfo.Name)
        {
            PropertyInfo = propertyInfo;
        }

        public PropertyInfo PropertyInfo { get; }

        public Extensions Extensions { get; set; } = new Extensions();
    }
}
