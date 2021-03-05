using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Lattia.Utils
{
    public static class GeneralExtensions
    {
        public static Type GetGenericArgument(this Type type) => type.GetGenericArguments()[0];

        public static Type GetGenericArgument(this PropertyInfo propertyInfo) => propertyInfo.PropertyType.GetGenericArgument();

        public static bool IsNullable(this Type type) => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);

        public static SerializablePropertyType ResolveSerializablePropertyType(this Type type)
        {
            if (Helpers.IsSimpleType(type))
            {
                return SerializablePropertyType.Simple;
            }
            else if (Helpers.IsObjectType(type))
            {
                return SerializablePropertyType.Object;
            }
            else if (Helpers.IsNonStringEnumerable(type))
            {
                return SerializablePropertyType.Enumerable;
            }

            throw new NotSupportedException(type.FullName);
        }

        public static Type GetEnumerableItemType(this Type enumerableType)
        {
            if (enumerableType.IsGenericType
                && enumerableType.GetGenericTypeDefinition() is var generic
                && (generic == typeof(IEnumerable<>) || generic == typeof(ICollection<>) || generic == typeof(IList<>)))
            {
                return enumerableType.GetGenericArgument();
            }

            return enumerableType
                .GetInterfaces()
                .Single(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                .GetGenericArgument();
        }
    }
}
