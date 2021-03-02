using Lattia.Contexts;

namespace Lattia.Pipelines
{
    public interface ICheckPropertyPermissionsPipeline
    {
        bool CheckPropertyReadPermission(CheckPropertyReadPermissionContext context);

        bool CheckPropertyWritePermission(CheckPropertyWritePermissionContext context);
    }
}
