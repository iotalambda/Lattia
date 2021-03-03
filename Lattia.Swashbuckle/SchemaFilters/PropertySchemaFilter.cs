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
                var valueSchema = schema.Properties["value"];

                foreach (var propertyInfo in valueSchema.GetType().GetProperties())
                {
                    propertyInfo.SetValue(schema, propertyInfo.GetValue(valueSchema));
                }
            }
        }
    }
}
