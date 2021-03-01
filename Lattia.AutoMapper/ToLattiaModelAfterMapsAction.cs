using AutoMapper;
using System;
using System.Linq;

namespace Lattia.AutoMapper
{
    /// <summary>
    /// Hopefully we'll be able to deprecate this at some point.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TOther"></typeparam>
    public class ToLattiaModelAfterMapsAction<TOther, TModel> : IMappingAction<TOther, TModel>
    {
        public void Process(TOther source, TModel destination, ResolutionContext context)
        {
            var propertyInfos = destination.GetType().GetProperties().Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(Property<>));

            foreach (var propertyInfo in propertyInfos)
            {
                if (propertyInfo.GetValue(destination) == default)
                {
                    var value = Activator.CreateInstance(propertyInfo.PropertyType, new object[] { default, false });

                    propertyInfo.SetValue(destination, value);
                }
            }
        }
    }
}
