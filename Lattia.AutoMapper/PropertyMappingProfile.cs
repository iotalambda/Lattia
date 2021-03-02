using AutoMapper;
using System;

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

                    var sourceValue = property.ObjValue;

                    var sourceType = property.ObjValue.GetType();

                    switch (sourceType.ResolveSerializablePropertyType())
                    {
                        case SerializablePropertyType.Simple:
                            return sourceValue;

                        case SerializablePropertyType.Object:
                            var targetType = HackyMappingLookup.GetTargetType(sourceType, context.Mapper);
                            return context.Mapper.Map(sourceValue, sourceType, targetType);

                        case SerializablePropertyType.Enumerable:
                            throw new NotImplementedException();

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

                    var instance = Activator.CreateInstance(typeof(Property<>).MakeGenericType(source.GetType()), new object[] { source, source != null }) as Property<T>;

                    return instance;
                });
        }
    }
}