using System;
using System.Linq;
using System.Reflection;
using Cosmos.Dapper.Mapper;

namespace Cosmos.Dapper.Core.Mapping.Filters
{
    internal static class ClassMapFilter
    {
        public static Type Filter(Type entityType, Assembly assembly, bool fluentMapMode)
        {
            var types = assembly.GetTypes();
            var typeOfInterface = fluentMapMode ? typeof(IMap) : typeof(IClassMap<>);
            return (from type in types
                    let interfaceType = type.GetInterface(typeOfInterface.FullName!)
                    where interfaceType != null && interfaceType.GetGenericArguments()[0] == entityType
                    select type).SingleOrDefault();
        }
    }
}