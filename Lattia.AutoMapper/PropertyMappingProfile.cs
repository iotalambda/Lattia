using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lattia.AutoMapper
{
    public class PropertyMappingProfile : Profile
    {
        public PropertyMappingProfile()
        {
            CreateMap(typeof(Property<>), typeof(object))
                .ConvertUsing((source, target, context) =>
                {
                    var property = source as Property;

                    if (!property.HasValue)
                    {
                        return null;
                    }

                    var sourceType = property.ObjValue.GetType();

                    var sourceValue = property.ObjValue;

                    switch (sourceType.ResolveSerializablePropertyType())
                    {
                        case SerializablePropertyType.Simple:
                            return sourceValue;

                        case SerializablePropertyType.Object:
                            var targetType = HackyMappingLookup.GetTargetType(sourceType, context.Mapper);
                            return context.Mapper.Map(sourceValue, sourceType, targetType);

                        case SerializablePropertyType.Enumerable:
                                
                            var sourceItemType = sourceType.GetEnumerableItemType();
                            Type targetItemType = default;

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
                                throw new NotImplementedException(sourceType.FullName);
                            }

                            return context.Mapper.Map(sourceValue, sourceType, targetEnumerableType);

                        default: throw new NotSupportedException(sourceType.FullName);
                    };

                });


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

            CreateMap(typeof(object), typeof(Property<>))
                .ConvertUsing((source, target, context) =>
                {
                    if (source == default)
                    {
                        return target;
                    }

                    var sourceType = source.GetType();

                    switch (sourceType.ResolveSerializablePropertyType())
                    {
                        case SerializablePropertyType.Simple:
                            return Utils.CreatePropertyValue(sourceType, source, source != default);

                        case SerializablePropertyType.Object:
                            var targetType = HackyMappingLookup.GetTargetType(sourceType, context.Mapper);
                            var targetValue = context.Mapper.Map(source, sourceType, targetType);
                            return Utils.CreatePropertyValue(targetType, targetValue, true);

                        case SerializablePropertyType.Enumerable:
                            throw new NotImplementedException();

                        default: throw new NotSupportedException(sourceType.FullName);
                    }
                });
        }

        private void CreateSimpleNullableMap<T>()
        {
            CreateMap<T, Property<T>>()
                .ConvertUsing((source, target, context) =>
                {
                    if (source == null)
                    {
                        return target;
                    }

                    var sourceType = source.GetType();

                    return Utils.CreatePropertyValue(sourceType, source, source != null) as Property<T>;
                });
        }
    }
}