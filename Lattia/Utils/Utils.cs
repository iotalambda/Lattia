﻿using System;
using System.Collections;
using System.Reflection;

namespace Lattia.Utils
{
    public class Helpers
    {
        public static bool IsSimpleType(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return IsSimpleType(type.GetGenericArguments()[0].GetTypeInfo());
            }

            return type.IsPrimitive
              || type.IsEnum
              || type.Equals(typeof(string))
              || type.Equals(typeof(decimal))
              || type.Equals(typeof(DateTime));
        }

        public static bool IsObjectType(Type type)
        {
            return type.IsClass && !type.FullName.StartsWith("System.") && !IsSimpleType(type) && !IsNonStringEnumerable(type);
        }

        public static bool IsNonStringEnumerable(Type type)
        {
            return type != typeof(string) && typeof(IEnumerable).IsAssignableFrom(type);
        }

        public static bool IsLattiaProperty(PropertyInfo property)
        {
            return property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Property<>);
        }

        public static Property CreatePropertyValue(Type innerType, object value, bool hasValue)
        {
            return Activator.CreateInstance(typeof(Property<>).MakeGenericType(innerType), new object[] { value, hasValue }) as Property;
        }
    }
}
