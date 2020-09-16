using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using Cosmos.Dapper.Actions;
using Cosmos.Dapper.Core.Contextual;
using Cosmos.Data.Common;
using Cosmos.Disposables;
using Cosmos.Expressions;
using Cosmos.Models;
using Cosmos.Models.Descriptors.EntityDescriptors;

namespace Cosmos.Dapper.Store
{
    /// <summary>
    /// Store base
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public abstract partial class StoreBase<TContext, TEntity> : DisposableObjects, IStore<TEntity>
        where TContext : class, IDapperContext, IDbContext, IWithSQLGenerator
        where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Create a new instance of <see cref="StoreBase{TContext, TEntity}" />
        /// </summary>
        /// <param name="context"></param>
        /// <param name="bindingExpression"></param>
        /// <param name="includeUnsafeOpt"></param>
        protected StoreBase(TContext context, Expression<Func<TContext, IDapperSet<TEntity>>> bindingExpression, bool includeUnsafeOpt)
        {
            EntityType = typeof(TEntity);
            DeletableEntity = EntityType.GetTypeInfo().ImplementedInterfaces.Any(x => x == typeof(IDeletable));

            RawTypedContext = context ?? throw new ArgumentNullException(nameof(context));

            BindingPropertyName = GetBindingPropertyName(bindingExpression);
            _dapperSet = context.DapperSetLazy<TEntity>(BindingPropertyName);

            const LazyThreadSafetyMode eap = LazyThreadSafetyMode.ExecutionAndPublication;
            _lazyEntityEntry = new Lazy<ISQLActionEntry<TEntity>>(() => RawTypedContext.GetActionEntry<TEntity>(), eap);
            _lazyAsynchronousEntityEntry = new Lazy<ISQLActionAsyncEntry<TEntity>>(() => RawTypedContext.GetAsynchronousActionEntry<TEntity>(), eap);

            IncludeUnsafeOpt = includeUnsafeOpt;
        }

        /// <summary>
        /// Gets raw typed context
        /// </summary>
        protected TContext RawTypedContext { get; }

        /// <summary>
        /// Gets include unsafe opt
        /// </summary>
        protected bool IncludeUnsafeOpt { get; }

        #region Binding property name

        static StoreBase()
        {
            _bindingPropertyCache = new ConcurrentDictionary<(Type, Type, int), PropertyInfo>();
        }

        // ReSharper disable once InconsistentNaming
        // ReSharper disable once StaticMemberInGenericType
        // key: typeOfContext, typeOfEntity and hashOfBindingExpression
        private static readonly ConcurrentDictionary<(Type, Type, int), PropertyInfo> _bindingPropertyCache;

        private static string GetBindingPropertyName(Expression<Func<TContext, IDapperSet<TEntity>>> bindingExpression)
        {
            bindingExpression.CheckNull(nameof(bindingExpression));
            var key = (typeof(TContext), typeof(TEntity), bindingExpression.GetHashCode());
            var result = _bindingPropertyCache.GetOrAdd(key, (tuple) =>
            {
                if (!(Lambdas.GetMember(bindingExpression) is PropertyInfo propertyInfo))
                    throw new ArgumentException($"Cannot get property by expression '{bindingExpression}'");
                var typeOfPropertyInfoReflected = propertyInfo.ReflectedType;
                if (typeOfPropertyInfoReflected is null)
                    throw new ArgumentException($"Property '{propertyInfo.Name}' is not a property of '{tuple.Item1}'");
                if (tuple.Item1 != typeOfPropertyInfoReflected && !tuple.Item1.IsSubclassOf(typeOfPropertyInfoReflected))
                    throw new ArgumentException($"Expression '{bindingExpression}' refers to a property that is not from type {tuple.Item1}");
                var propertyType = propertyInfo.PropertyType;
                if (!propertyType.IsGenericType || propertyType.GenericTypeArguments.Length != 1)
                    throw new ArgumentException($"Such property type '{propertyType}' has no or more than one generic arguments.");
                var args1 = propertyType.GenericTypeArguments[0];
                if (args1 != tuple.Item2)
                    throw new ArgumentException($"Type '{args1}' is not same as entity type '{tuple.Item2}'");
                return propertyInfo;
            });

            if (result is null)
                throw new ArgumentException($"Cannot get property by expression '{bindingExpression}'");

            return result.Name;
        }

        #endregion
    }
}