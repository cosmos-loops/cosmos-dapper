#nullable enable
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using AspectCore.Extensions.Reflection;
using Cosmos.Reflection;

namespace Cosmos.Dapper.Core.Contextual
{
    /// <summary>
    /// DapperSets manager
    /// </summary>
    internal static class DapperSetsManager
    {
        private static readonly MethodInfo? _lazyEntityMethod;
        private static readonly ConcurrentDictionary<(Type, string), MethodInfo> _cachedRuntimeEntityMethods;

        private const string LAZY_ENTITY = "LazyEntity";

        static DapperSetsManager()
        {
            _lazyEntityMethod = typeof(DapperSet).GetMethod(LAZY_ENTITY, BindingFlags.Static | BindingFlags.NonPublic)
                             ?? throw new InvalidOperationException($"Cannot call {nameof(DapperSet.LazyEntity)} method.");
            _cachedRuntimeEntityMethods = new ConcurrentDictionary<(Type, string), MethodInfo>();
        }

        private static IEnumerable<(PropertyInfo property, Type entityType, string name)> CachedProperties<TContext, TConnection>()
            where TContext : DapperContext<TContext, TConnection>, IDapperContext, IWithConnection<TConnection>, IWithSQLGenerator
            where TConnection : DbConnection
            => DapperContextualManager.GetAllProperties<TContext, TConnection>();

        public static void RuntimeInit<TContext, TConnection>(IDapperContext instance)
            where TContext : DapperContext<TContext, TConnection>, IDapperContext, IWithConnection<TConnection>, IWithSQLGenerator
            where TConnection : DbConnection
        {
            if (_lazyEntityMethod is null)
                throw new InvalidOperationException($"Cannot call {nameof(DapperSet.LazyEntity)} method.");
            
            foreach (var (property, entityType, name) in CachedProperties<TContext, TConnection>())
            {
                //Got methodInfo of LazyEntity<TContextRef, TEntityRef, TConnectionRef>
                var runtimeMethodRefactor = _cachedRuntimeEntityMethods.GetOrAdd((entityType, name), tuple =>
                    _lazyEntityMethod.MakeGenericMethod(typeof(TContext), tuple.Item1, typeof(TConnection))
                ).GetReflector();

                //Got the result typed Lazy<DapperSet<TEntityRef>>
                var runtimeLazyValue = runtimeMethodRefactor.Invoke(null, instance, name);
                
                //Got the value of Lazy<DapperSet<TEntityRef>>, typed DapperSet<TEntityRef>
                var runtimeValue = runtimeLazyValue.GetPropertyValue("Value");
                
                //Set runtimeValue into then instance typed TContext
                property.GetReflector().SetValue(instance, runtimeValue);
            }
        }
    }
}