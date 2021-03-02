using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Lattia.Swashbuckle.SchemaFilters
{
    public class PropertySchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsGenericType && context.Type.GetGenericTypeDefinition() == typeof(Property<>))
            {
                schema.Type = schema.Properties["value"].Type;

                schema.Properties.Clear();
            }
        }
    }
}
