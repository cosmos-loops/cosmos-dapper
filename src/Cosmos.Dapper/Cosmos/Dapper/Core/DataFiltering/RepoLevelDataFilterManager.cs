using System;
using System.Collections.Concurrent;
using Cosmos.Data.Statements;

namespace Cosmos.Dapper.Core.DataFiltering
{
    /// <summary>
    /// Repository level data filter manager
    /// </summary>
    public static class RepoLevelDataFilterManager
    {
        // ReSharper disable once InconsistentNaming
        private static readonly ConcurrentDictionary<(Type, Type), ISQLPredicate> _sqlPredicateCache;
        private static object _lockObj = new object();

        static RepoLevelDataFilterManager()
        {
            _sqlPredicateCache = new ConcurrentDictionary<(Type, Type), ISQLPredicate>();
        }

        /// <summary>
        /// Is contains key...
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsContainerKey((Type, Type) key)
        {
            return _sqlPredicateCache.ContainsKey(key);
        }

        /// <summary>
        /// Register
        /// </summary>
        /// <param name="key"></param>
        /// <param name="predicate"></param>
        public static void Register((Type, Type) key, ISQLPredicate predicate)
        {
            lock (_lockObj)
            {
                if (!IsContainerKey(key))
                {
                    _sqlPredicateCache.AddOrUpdate(key, predicate, (tuple, sqlPredicate) => predicate);
                }
            }
        }

        /// <summary>
        /// Gets filter
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static ISQLPredicate GetFilter((Type, Type) key)
        {
            return _sqlPredicateCache.TryGetValue(key, out var ret) ? ret : null;
        }
    }
}