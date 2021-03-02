using System.Collections.Generic;

namespace Lattia
{
    public class Extensions
    {
        public IDictionary<string, object> Values { get; } = new Dictionary<string, object>();

        public void Set<TType, TValue>(TValue value)
        {
            Values[typeof(TType).FullName] = value;
        }

        public bool TryGet<TType, TValue>(out TValue value)
        {
            if (Values.TryGetValue(typeof(TType).FullName, out object obj))
            {
                value = (TValue)obj;
                return true;
            };

            value = default;
            return false;
        }
    }
}
