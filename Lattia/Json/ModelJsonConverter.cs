using Newtonsoft.Json;
using System;

namespace Lattia.Json
{
    public class ModelJsonConverter : JsonConverter<IModel>
    {
        public override bool CanRead => false;

        public override bool CanWrite => true;

        public override IModel ReadJson(JsonReader reader, Type objectType, IModel existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, IModel value, JsonSerializer serializer)
        {
            writer.WriteModel(value, serializer);
        }
    }
}
