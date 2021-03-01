using Lattia.Abstractions;
using Lattia.Json;
using Newtonsoft.Json;

namespace Lattia
{
    public sealed class Property<T> : Property, IProperty<T>
    {
        public Property(object value, bool hasValue)
            : base(value, hasValue)
        {
        }

        public T Value
        {
            get => (T)ObjValue;

            set => ObjValue = value;
        }

        public static Property<T> Default() => new Property<T>(default(T), false);
    }

    [JsonConverter(typeof(PropertyJsonConverter))]
    public class Property
    {
        public Property(object value, bool hasValue)
        {
            ObjValue = value;

            HasValue = hasValue;
        }

        public object ObjValue { get; set; }

        public bool HasValue { get; set; }
    }
}
