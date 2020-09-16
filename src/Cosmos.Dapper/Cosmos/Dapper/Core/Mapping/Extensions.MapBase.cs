using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Cosmos.Dapper.EntityMapping;
using Cosmos.Reflection;

namespace Cosmos.Dapper.Core.Mapping
{
    /// <summary>
    /// Extensions for MapBase
    /// </summary>
    public static class MapBaseExtension
    {
        private static readonly ConcurrentDictionary<(Type, int), bool> MatchedCached = new ConcurrentDictionary<(Type, int), bool>();

        /// <summary>
        /// Is matched entity mapping rule
        /// </summary>
        /// <param name="type"></param>
        /// <param name="bodyTypes"></param>
        /// <returns></returns>
        public static bool IsMatchedEntityMappingRule(this Type type, List<Type> bodyTypes)
        {
            if (type is null)
                return false;

            if (bodyTypes is null || !bodyTypes.Any())
                return false;

            if (CheckCache(type, bodyTypes, out var ret))
                return ret;

            if (!typeof(IMap).IsAssignableFrom(type))
                return CacheAndReturn(type, bodyTypes, false);

            var bodyType = Types.GetRawTypeFromGenericClass(type, typeof(DapperMapBase<>));

            if (bodyType is null)
                return CacheAndReturn(type, bodyTypes, false);

            return CacheAndReturn(type, bodyTypes, bodyTypes.Contains(bodyType));
        }

        private static bool CheckCache(Type type, IEnumerable<Type> bodyTypes, out bool ret)
        {
            var hashCode = bodyTypes.GetHashCode();
            return MatchedCached.TryGetValue((type, hashCode), out ret);
        }

        private static bool CacheAndReturn(Type type, IEnumerable<Type> bodyTypes, bool result)
        {
            var hashCode = bodyTypes.GetHashCode();
            MatchedCached.TryAdd((type, hashCode), result);
            return result;
        }
    }
}