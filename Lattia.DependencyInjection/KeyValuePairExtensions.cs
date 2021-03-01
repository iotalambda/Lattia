using System.Collections.Generic;

namespace Lattia.DependencyInjection
{
    public static class KeyValuePairExtensions
    {
        public static void Deconstruct<T1, T2>(this KeyValuePair<T1, T2> source, out T1 key, out T2 value)
        {
            key = source.Key;
            value = source.Value;
        }
    }
}
