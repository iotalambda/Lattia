using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Reflection;

namespace Lattia.Json
{
    public static class JsonExtensions
    {
        public static string GetJsonPropertyName(this IContractResolver resolver, Type modelType, string underlyingPropertyName)
        {
            if (resolver == null || modelType == null)
            {
                throw new ArgumentNullException();
            }

            var contract = resolver.ResolveContract(modelType) as JsonObjectContract;

            if (contract == null)
            {
                return string.Empty;
            }

            return contract.Properties
                .Single(p => !p.Ignored && p.UnderlyingName == underlyingPropertyName)
                .PropertyName;
        }

        public static void WriteModel(this JsonWriter writer, IModel value, JsonSerializer serializer)
        {
            writer.WriteStartObject();

            foreach (var propertyInfo in value.GetType().GetProperties())
            {
                var propertyValue = propertyInfo.GetValue(value);

                writer.WriteProperty(propertyInfo, propertyValue, serializer);
            }

            writer.WriteEndObject();
        }

        public static void WriteProperty(this JsonWriter writer, PropertyInfo propertyInfo, object value, JsonSerializer serializer)
        {
            JToken jtoken;

            if (Utils.IsLattiaProperty(propertyInfo))
            {
                var property = value as Property;

                if (!property.HasValue)
                {
                    return;
                }
                else if (property.ObjValue == null)
                {
                    jtoken = JValue.CreateNull();
                }
                else
                {
                    jtoken = JToken.FromObject(property.ObjValue, serializer);
                }
            }
            else
            {
                jtoken = JToken.FromObject(value, serializer);
            }

            var jproperty = new JProperty(serializer.ContractResolver.GetJsonPropertyName(propertyInfo.DeclaringType, propertyInfo.Name), jtoken);

            jproperty.WriteTo(writer);
        }
    }
}
