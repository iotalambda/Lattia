﻿using Lattia.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Lattia.Json
{
    public class PropertyJsonConverter : JsonConverter<Property>
    {
        public override bool CanRead => true;

        public override bool CanWrite => true;

        public override Property ReadJson(JsonReader reader, Type objectType, Property existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            object value;

            if (reader.TokenType == JsonToken.StartObject)
            {
                var innerReader = JObject.Load(reader).CreateReader();

                var innerType = objectType.GetGenericArgument();

                value = serializer.Deserialize(innerReader, innerType);
            }
            else if (reader.TokenType == JsonToken.StartArray)
            {
                var innerReader = JArray.Load(reader).CreateReader();

                var innerType = objectType.GetGenericArgument();

                value = serializer.Deserialize(innerReader, innerType);
            }
            else
            {
                value = reader.Value;
            }

            // This if is used only for simple values.
            if (reader.ValueType != default)
            {
                var actualValueType = objectType.GetGenericArguments()[0];

                // Avoid problems with e.g. trying to set int64 to int32 property.
                if (reader.ValueType != actualValueType)
                {
                    // Convert does not work if one is nullable, so in case of nullables, use just the generic argument as type.
                    if (actualValueType.IsGenericType && actualValueType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        actualValueType = actualValueType.GetGenericArguments()[0];
                    }

                    value = Convert.ChangeType(value, actualValueType);
                }
            }

            var result = Activator.CreateInstance(objectType, new object[] { value, true }) as Property;

            return result;
        }

        public override void WriteJson(JsonWriter writer, Property value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
