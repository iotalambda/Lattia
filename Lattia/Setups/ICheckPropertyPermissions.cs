using Lattia.Contexts;

namespace Lattia.Setups
{
    public interface ICheckPropertyPermissions
    {

        bool CheckPropertyReadPermission(CheckPropertyReadPermissionContext context);

        bool CheckPropertyWritePermission(CheckPropertyWritePermissionContext context);
    }
}
