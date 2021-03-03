using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lattia.AutoMapper
{
    public class PropertyMappingProfile : Profile
    {
        public PropertyMappingProfile()
        {
            object ConvertPropertyToObject(object source, object target, ResolutionContext context)
            {
                var property = source as Property;

                if (!property.HasValue)
                {
                    return null;
                }

                return ConvertSerializableType(source.GetType().GetGenericArgument(), property.ObjValue, context);
            }

            CreateMap(typeof(Property<>), typeof(object))
                .ConvertUsing(ConvertPropertyToObject);

            CreateMap(typeof(Property<>), typeof(IEnumerable<>))
                .ConvertUsing(ConvertPropertyToObject);

            CreateSimpleNullableMap<sbyte?>();
            CreateSimpleNullableMap<byte?>();
            CreateSimpleNullableMap<short?>();
            CreateSimpleNullableMap<ushort?>();
            CreateSimpleNullableMap<int?>();
            CreateSimpleNullableMap<uint?>();
            CreateSimpleNullableMap<long?>();
            CreateSimpleNullableMap<ulong?>();
            CreateSimpleNullableMap<char?>();
            CreateSimpleNullableMap<float?>();
            CreateSimpleNullableMap<double?>();
            CreateSimpleNullableMap<bool?>();
            CreateSimpleNullableMap<decimal?>();
            CreateSimpleNullableMap<DateTime?>();
            CreateSimpleNullableMap<DateTimeOffset?>();

            Func<object, object, ResolutionContext, object> ConvertObjectToProperty(bool explicitNull, Type type) => (source, target, context) =>
            {
                if (source == default)
                {
                    return explicitNull ? Utils.CreatePropertyValue(type, null, true) : target;
                }

                var targetValue = ConvertSerializableType(source.GetType(), source, context);

                return Utils.CreatePropertyValue(targetValue.GetType(), targetValue, true);
            };

            CreateMap(typeof(string), typeof(Property<>))
                .ConvertUsing(ConvertObjectToProperty(true, typeof(string)));

            CreateMap(typeof(DateTime), typeof(Property<>))
                .ConvertUsing(ConvertObjectToProperty(true, typeof(DateTime)));

            CreateMap(typeof(DateTimeOffset), typeof(Property<>))
                .ConvertUsing(ConvertObjectToProperty(true, typeof(DateTimeOffset)));

            CreateMap(typeof(object), typeof(Property<>))
                .ConvertUsing(ConvertObjectToProperty(false, null));

            new[]
            {
                typeof(ICollection<>),
                typeof(IList<>),
                typeof(IEnumerable<>)
            }
            .ToList()
            .ForEach(generic =>
            {
                CreateMap(generic, typeof(Property<>).MakeGenericType(generic))
                    .ConvertUsing((object source, object target, ResolutionContext context) =>
                    {
                        var sourceProperty = ConvertObjectToProperty(false, null)(source, target, context) as Property;
                        var itemType = sourceProperty.ObjValue.GetType().GetEnumerableItemType();
                        var innerTargetType = generic.MakeGenericType(itemType);
                        var targetProperty = typeof(Property<>).MakeGenericType(innerTargetType)
                            .GetConstructor(new []{ typeof(object), typeof(bool) })
                            .Invoke(new[] { sourceProperty.ObjValue, sourceProperty.HasValue });
                        return targetProperty;
                    });
            });
        }

        private void CreateSimpleNullableMap<T>()
        {
            CreateMap<T, Property<T>>()
                .ConvertUsing((source, target, context) =>
                {
                    if (source == null)
                    {
                        return Utils.CreatePropertyValue(typeof(T), null, true) as Property<T>;
                    }

                    return Utils.CreatePropertyValue(typeof(T), source, source != null) as Property<T>;
                });
        }

        private static object ConvertSerializableType(Type sourceType, object sourceValue, ResolutionContext context)
        {
            switch (sourceType.ResolveSerializablePropertyType())
            {
                case SerializablePropertyType.Simple:
                    return sourceValue;

                case SerializablePropertyType.Object:
                    var targetType = HackyMappingLookup.GetTargetType(sourceType, context.Mapper);
                    return context.Mapper.Map(sourceValue, sourceType, targetType);

                case SerializablePropertyType.Enumerable:

                    var sourceItemType = sourceType.GetEnumerableItemType();
                    Type targetItemType;

                    switch (sourceItemType.ResolveSerializablePropertyType())
                    {
                        case SerializablePropertyType.Simple:
                            targetItemType = sourceItemType;
                            break;

                        case SerializablePropertyType.Object:
                            targetItemType = HackyMappingLookup.GetTargetType(sourceItemType, context.Mapper);
                            break;

                        default: throw new NotSupportedException(sourceItemType.FullName);
                    }

                    Type targetEnumerableType = default;

                    if (sourceValue is Array)
                    {
                        targetEnumerableType = targetItemType.MakeArrayType();
                    }

                    if (sourceType.IsGenericType && sourceType.GetGenericTypeDefinition() is var generic)
                    {
                        targetEnumerableType = generic.MakeGenericType(targetItemType);
                    }

                    if (targetEnumerableType == default)
                    {
                        throw new NotSupportedException(sourceType.FullName);
                    }

                    return context.Mapper.Map(sourceValue, sourceType, targetEnumerableType);

                default: throw new NotSupportedException(sourceType.FullName);
            };
        }
    }
}