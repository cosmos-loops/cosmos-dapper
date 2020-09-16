using System;
using Cosmos.Dapper.Actions;

namespace Cosmos.Dapper.Store
{
    // ReSharper disable once UnusedTypeParameter
    public abstract partial class StoreBase<TContext, TEntity>
    {
        private readonly Lazy<IDapperSet<TEntity>> _dapperSet;

        /// <summary>
        /// Entity type
        /// </summary>
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public Type EntityType { get; }

        /// <summary>
        /// To flag this entity type has impl IDelete interface or not.
        /// </summary>
        protected bool DeletableEntity { get; }

        /// <summary>
        /// Binding property name
        /// </summary>
        protected string BindingPropertyName { get; }

        #region Dapper action

        private readonly Lazy<ISQLActionEntry<TEntity>> _lazyEntityEntry;
        private readonly Lazy<ISQLActionAsyncEntry<TEntity>> _lazyAsynchronousEntityEntry;

        /// <summary>
        /// Gets entity entry for DapperAction
        /// </summary>
        public ISQLActionEntry<TEntity> EntityEntry => _lazyEntityEntry.Value;

        /// <summary>
        /// Gets entity entry fot asynchronous DapperAction
        /// </summary>
        public ISQLActionAsyncEntry<TEntity> AsynchronousEntityEntry => _lazyAsynchronousEntityEntry.Value;

        #endregion

        #region Dapper Set

        /// <summary>
        /// Gets DapperSet
        /// </summary>
        public IDapperSet<TEntity> Set => _dapperSet.Value;

        #endregion
    }
}