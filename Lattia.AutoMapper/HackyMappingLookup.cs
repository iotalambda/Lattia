using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lattia.AutoMapper
{
    /// <summary>
    /// Let's see if we can get rid of this https://github.com/AutoMapper/AutoMapper/discussions/3592
    /// </summary>
    internal static class HackyMappingLookup
    {
        private static Dictionary<string, Type> lookup = new Dictionary<string, Type>();

        public static Type GetTargetType(Type sourceType, IRuntimeMapper runtimeMapper)
        {
            if (!lookup.TryGetValue(sourceType.FullName, out var targetType))
            {
                var mapper = runtimeMapper.GetType()
                    .GetField("_inner", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    .GetValue(runtimeMapper) as Mapper;

                targetType = mapper.ConfigurationProvider.GetAllTypeMaps().Single(m => m.SourceType == sourceType).DestinationType;

                lookup[sourceType.FullName] = targetType;
            }

            return targetType;
        }
    }
}