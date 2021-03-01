namespace Lattia
{
    public class PermissionCheckingService
    {
        private readonly LattiaContext context;

        public PermissionCheckingService(LattiaContext context) => this.context = context;

        public bool IsAuthorizedToWriteProperties<TModel>(TModel model)
        {
            bool isAuthorized = true;

            PropertyValueVisitor.Traverse(model, n =>
            {
                if (context.PropertyTypeNodes[n.Path].Extensions.TryGetValue("IsReadOnly", out object obj) && (bool)obj == true)
                {
                    isAuthorized = false;
                }
            });

            return isAuthorized;
        }
    }
}