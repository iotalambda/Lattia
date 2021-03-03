using Lattia.Attributes;
using Lattia.Contexts;
using System.Linq;

namespace Lattia.Setups.ReadOnly
{
    public class InitializePropertyTypeNode : IInitializePropertyTypeNode
    {
        public void Initialize(InitializePropertyTypeNodeContext context)
        {
            var node = context.PropertyTypeNode;

            var parent = node.Parent;

            var attribute = node.PropertyInfo.GetCustomAttributes(typeof(ReadOnlyAttribute), true)?.FirstOrDefault() as ReadOnlyAttribute;

            if (attribute != null || parent != null && parent.Extensions.TryGet<ReadOnlyAttribute, bool>(out var isReadOnly) && isReadOnly)
            {
                node.Extensions.Set<ReadOnlyAttribute, bool>(true);
            }
        }
    }
}
