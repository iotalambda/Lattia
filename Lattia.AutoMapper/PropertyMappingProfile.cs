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
                    else if (Utils.IsObjectType(property.ObjValue.GetType()))
                    {
                        var targetType = HackyMappingLookup.GetTargetType(property.ObjValue.GetType(), context.Mapper);

                        return context.Mapper.Map(property.ObjValue, property.ObjValue.GetType(), targetType);
                    }
                    else if (Utils.IsSimpleType(property.ObjValue.GetType()))
                    {
                        return property.ObjValue;
                    }
                    else
                    {
                        throw new NotImplementedException(property.GetType().FullName);
                    }
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

                    if (Utils.IsObjectType(source.GetType()))
                    {
                        var targetType = HackyMappingLookup.GetTargetType(source.GetType(), context.Mapper);

                        var targetValue = context.Mapper.Map(source, source.GetType(), targetType);

                        return Activator.CreateInstance(typeof(Property<>).MakeGenericType(targetType), new object[] { targetValue, true });
                    }
                    else if (Utils.IsSimpleType(source.GetType()))
                    {
                        return Activator.CreateInstance(typeof(Property<>).MakeGenericType(source.GetType()), new object[] { source, source != default });
                    }
                    else
                    {
                        throw new NotImplementedException(source.GetType().FullName);
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