using Lattia.Attributes;
using System.Linq;

namespace Lattia.Setups.ReadOnly
{
    public class InitializePropertyTypeNode : IInitializePropertyTypeNode
    {
        public void Initialize(PropertyTypeNode propertyType)
        {
            var parent = propertyType.Parent;

            var attribute = propertyType.PropertyInfo.GetCustomAttributes(typeof(ReadOnlyAttribute), true)?.FirstOrDefault() as ReadOnlyAttribute;

            if (attribute != null || parent != null && parent.Extensions.TryGet<ReadOnlyAttribute, bool>(out var isReadOnly) && isReadOnly)
            {
                propertyType.Extensions.Set<ReadOnlyAttribute, bool>(true);
            }
        }
    }
}
