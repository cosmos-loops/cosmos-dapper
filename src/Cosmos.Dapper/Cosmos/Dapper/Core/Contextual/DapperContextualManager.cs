using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using Cosmos.Collections;
using Cosmos.Dapper.EntityMapping;
using Cosmos.Dapper.Operations;
using Cosmos.Data.Common;
using Cosmos.Models;

namespace Cosmos.Dapper.Core.Contextual
{
    /// <summary>
    /// Dapper contextual manager
    /// </summary>
    public static class DapperContextualManager
    {
        /// <summary>
        /// On model creating...
        /// </summary>
        // ReSharper disable once InconsistentNaming
        private const string ON_MODEL_CREATING = "OnModelCreating";

        // TypeOfContext, typeOfEntity, instanceOfDbSet
        private static readonly ConcurrentDictionary<Type, ConcurrentDictionary<(Type, string), IDapperSet>> DapperEntitySetCache;

        // PropertyInfo, TypeOfEntity(Generic Args of Property), name
        private static readonly ConcurrentDictionary<Type, HashSet<(PropertyInfo, Type, string)>> DapperEntityTypeCache;

        // TypeOfContext, typeOfProperty
        private static readonly ConcurrentDictionary<(Type, Type, string), bool> DapperDbSetMatchedCache;

        static DapperContextualManager()
        {
            DapperEntitySetCache = new ConcurrentDictionary<Type, ConcurrentDictionary<(Type, string), IDapperSet>>();
            DapperEntityTypeCache = new ConcurrentDictionary<Type, HashSet<(PropertyInfo, Type, string)>>();
            DapperDbSetMatchedCache = new ConcurrentDictionary<(Type, Type, string), bool>();
        }

        /// <summary>
        /// Model creating call
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mappingConfig"></param>
        /// <typeparam name="TContext"></typeparam>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void ModelCreatingCall<TContext>(TContext context, DapperConfig mappingConfig)
            where TContext : class, IDapperContext, IDapperQueryOperator, IDbContext
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            if (mappingConfig is null)
                throw new ArgumentNullException(nameof(mappingConfig));

            var method = typeof(TContext).GetMethod(ON_MODEL_CREATING, BindingFlags.Instance | BindingFlags.NonPublic);

            if (method is null)
                throw new ArgumentException($"There is no '{ON_MODEL_CREATING}' method in '{typeof(TContext)}'.", nameof(method));

            method.Invoke(context, new object[] {new DapperClassBuilder(mappingConfig)});
        }

        internal static DapperSet<TEntity> GetOrUpdateEntity<TContext, TEntity, TConnection>(
            Func<DapperSet<TEntity>> createFunc, string dapperDbSetName)
            where TContext : IDapperContext, IWithConnection<TConnection>
            where TEntity : class, IEntity, new()
            where TConnection : class, IDbConnection
        {
            var key = (typeof(TEntity), dapperDbSetName);

            if (DapperEntitySetCache.TryGetValue(typeof(TContext), out var entityDict))
            {
                if (entityDict.TryGetValue(key, out var targetEntity))
                    return targetEntity as DapperSet<TEntity>;
                return entityDict.GetOrAdd(key, createFunc()) as DapperSet<TEntity>;
            }

            var entityDictOfContext = new ConcurrentDictionary<(Type, string), IDapperSet>();
            DapperEntitySetCache.TryAdd(typeof(TContext), entityDictOfContext);
            return entityDictOfContext.GetOrAdd(key, createFunc()) as DapperSet<TEntity>;
        }

        /// <summary>
        /// Gets all properties by given type of Context and Connection
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <typeparam name="TConnection"></typeparam>
        /// <returns></returns>
        public static IEnumerable<(PropertyInfo property, Type entityType, string name)> GetAllProperties<TContext, TConnection>()
            where TContext : DapperContext<TContext, TConnection>, IDapperContext
            where TConnection : DbConnection
        {
            var cachedHashSet = DapperEntityTypeCache.GetOrAdd(typeof(TContext), t =>
            {
                var _ = t.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(property => IsMatchedDbSetRule(t, property))
                    .Select(property => (property, property.PropertyType.GetGenericArguments()[0], property.Name));

                return _.ToHashSet();
            });

            foreach (var item in cachedHashSet)
                yield return item;

            bool IsMatchedDbSetRule(Type typeOfContext, PropertyInfo wrappedProperty)
            {
                if (wrappedProperty is null)
                    return false;

                var wrappedPropertyType = wrappedProperty.PropertyType;
                var wrappedPropertyName = wrappedProperty.Name;

                if (CheckCache(typeOfContext, wrappedPropertyType, wrappedPropertyName, out var ret))
                    return ret;

                if (!wrappedPropertyType.IsGenericType)
                    return CacheAndReturn(typeOfContext, wrappedPropertyType, wrappedPropertyName, false);

                if (wrappedPropertyType.GenericTypeArguments.Length != 1)
                    return CacheAndReturn(typeOfContext, wrappedPropertyType, wrappedPropertyName, false);

                if (wrappedPropertyType.BaseType != typeof(DapperSet))
                    return CacheAndReturn(typeOfContext, wrappedPropertyType, wrappedPropertyName, false);

                return CacheAndReturn(typeOfContext, wrappedPropertyType, wrappedPropertyName, true);
            }

            bool CheckCache(Type typeOfContext, Type typeOfDbSet, string n, out bool ret)
            {
                return DapperDbSetMatchedCache.TryGetValue((typeOfContext, typeOfDbSet, n), out ret);
            }

            bool CacheAndReturn(Type typeOfContext, Type typeOfDbSet, string n, bool result)
            {
                DapperDbSetMatchedCache.TryAdd((typeOfContext, typeOfDbSet, n), result);
                return result;
            }
        }
    }
}