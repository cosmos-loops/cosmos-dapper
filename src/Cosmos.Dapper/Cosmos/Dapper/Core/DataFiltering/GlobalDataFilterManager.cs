using System;
using System.Collections.Concurrent;
using Cosmos.Data.Statements;
using Cosmos.Models;

namespace Cosmos.Dapper.Core.DataFiltering
{
    /// <summary>
    /// Global data filter manager
    /// </summary>
    public static class GlobalDataFilterManager
    {
        // ReSharper disable once InconsistentNaming
        private static readonly ConcurrentDictionary<(Type, Type), ISQLPredicate> _sqlPredicateCache;

        static GlobalDataFilterManager()
        {
            _sqlPredicateCache = new ConcurrentDictionary<(Type, Type), ISQLPredicate>();
        }

        /// <summary>
        /// Register
        /// </summary>
        /// <param name="key"></param>
        /// <param name="predicate"></param>
        public static void Register((Type, Type) key, ISQLPredicate predicate)
        {
            _sqlPredicateCache.AddOrUpdate(key, predicate, (tuple, sqlPredicate) => predicate);
        }

        /// <summary>
        /// Register
        /// </summary>
        /// <param name="strategy"></param>
        /// <typeparam name="TEntity"></typeparam>
        public static void Register<TEntity>(GlobalLevelDataFilteringStrategy<TEntity> strategy) where TEntity : class, IEntity
        {
            if (strategy is null)
                return;

            Register(strategy.GetSignature(), strategy.GetFilteringPredicate());
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