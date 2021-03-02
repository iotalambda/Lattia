using Lattia.Contexts;
using Lattia.Pipelines;
using Lattia.Services;

namespace Lattia
{
    public class CheckPropertyPermissionsService : ICheckPropertyPermissionsService
    {
        private readonly LattiaSingletonContext context;

        private readonly ICheckPropertyPermissionsPipeline pipeline;

        public CheckPropertyPermissionsService(LattiaSingletonContext context, ICheckPropertyPermissionsPipeline pipeline)
        {
            this.context = context;

            this.pipeline = pipeline;
        }

        public bool IsAuthorizedToWriteProperties<TModel>(TModel model)
        {
            bool isAuthorized = true;

            PropertyValueVisitor.Traverse(model, n =>
            {
                isAuthorized &= pipeline.CheckPropertyWritePermission(new CheckPropertyWritePermissionContext(context.PropertyTypeNodes[n.Path], n));
            });

            return isAuthorized;
        }
    }
}