using System;
using System.Reflection;

namespace Lattia
{
    public static class SystemExtensions
    {
        public static Type GetGenericArgument(this Type type) => type.GetGenericArguments()[0];

        public static Type GetGenericArgument(this PropertyInfo propertyInfo) => GetGenericArgument(propertyInfo.PropertyType);

        public static bool IsNullable(this Type type) => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);

        public static SerializablePropertyType ResolveSerializablePropertyType(this Type type)
        {
            if (Utils.IsSimpleType(type))
            {
                return SerializablePropertyType.Simple;
            }
            else if (Utils.IsObjectType(type))
            {
                return SerializablePropertyType.Object;
            }
            else if (Utils.IsNonStringEnumerable(type))
            {
                return SerializablePropertyType.Enumerable;
            }

            throw new NotSupportedException(type.FullName);
        }
    }
}
