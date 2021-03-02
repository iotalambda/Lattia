using Lattia.Attributes;
using Lattia.Contexts;
using Lattia.Setups;

namespace Lattia.Webi.Configurations
{
    public class ReadOnlyAttributeSetup :
        IInitializePropertyTypeNode,
        ICheckPropertyPermissions
    {
        public void InitializePropertyTypeNode(InitializePropertyTypeNodeContext context)
        {
            var node = context.PropertyTypeNode;

            var parent = node.Parent;

            var attribute = node.PropertyInfo.GetCustomAttributes(typeof(ReadOnlyAttribute), true)[0] as ReadOnlyAttribute;

            if (attribute != null || (parent != null && parent.Extensions.TryGet<ReadOnlyAttribute, bool>(out var isReadOnly) && isReadOnly))
            {
                node.Extensions.Set<ReadOnlyAttribute, bool>(true);
            }
        }

        public bool CheckPropertyReadPermission(CheckPropertyReadPermissionContext context)
        {
            return true;
        }

        public bool CheckPropertyWritePermission(CheckPropertyWritePermissionContext context)
        {
            var node = context.PropertyTypeNode;

            return !(node.Extensions.TryGet<ReadOnlyAttribute, bool>(out var isReadOnly) && isReadOnly);
        }
    }
}
