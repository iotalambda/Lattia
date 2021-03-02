namespace Lattia.Services
{
    public interface ICheckPropertyPermissionsService
    {
        bool IsAuthorizedToWriteProperties<TModel>(TModel model);
    }
}
