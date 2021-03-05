using System.Linq;

namespace Lattia
{
    public class WriteOnlyInitializePropertyTypeNode : IInitializePropertyTypeNode
    {
        public void Initialize(PropertyTypeNode propertyType)
        {
            var parent = propertyType.Parent;

            var attribute = propertyType.PropertyInfo.GetCustomAttributes(typeof(WriteOnlyAttribute), true)?.FirstOrDefault() as WriteOnlyAttribute;

            if (attribute != null || parent != null && parent.Extensions.TryGet<WriteOnlyAttribute, bool>(out var isWriteOnly) && isWriteOnly)
            {
                propertyType.Extensions.Set<WriteOnlyAttribute, bool>(true);
            }
        }
    }
}
