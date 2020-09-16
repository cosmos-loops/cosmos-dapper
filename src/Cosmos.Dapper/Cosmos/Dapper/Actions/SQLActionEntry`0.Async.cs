using System;
using System.Collections.Generic;
using Cosmos.Dapper.Actions.Delete;
using Cosmos.Dapper.Actions.Insert;
using Cosmos.Dapper.Actions.Update;
using Cosmos.Dapper.Core;
using Cosmos.Dapper.Core.DataFiltering;
using Cosmos.Data.Statements;
using Cosmos.Models;

namespace Cosmos.Dapper.Actions
{
    /// <summary>
    /// Asynchronous SqlAction entry
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class SQLActionAsyncEntry : ISQLActionAsyncEntry, IHasRootActionBank, IHasDataFilter, IHasBulkOpt
    {
        SQLActionSetBase IHasRootActionBank.RootActionBank { get; set; }

        IDapperContextParams IHasBulkOpt.ContextParams { get; set; }

        ISQLPredicate[] IHasDataFilter.Filters { get; set; }

        /// <summary>
        /// Gets SqlAction bank getter
        /// </summary>
        protected IHasRootActionBank ActionBankGetter => this;

        /// <summary>
        /// Create a new instance of <see cref="SQLActionAsyncEntry" />
        /// </summary>
        /// <param name="rootActionSet"></param>
        /// <param name="contextParams"></param>
        /// <param name="filters"></param>
        public SQLActionAsyncEntry(SQLActionSetBase rootActionSet, IDapperContextParams contextParams, ISQLPredicate[] filters = null)
        {
            var _ = (IHasRootActionBank) this;
            _.RootActionBank = rootActionSet ?? throw new ArgumentNullException(nameof(rootActionSet));

            var __ = (IHasDataFilter) this;
            __.Filters = filters;

            var ___ = (IHasBulkOpt) this;
            ___.ContextParams = contextParams ?? throw new ArgumentNullException(nameof(contextParams));
        }

        #region Add new action

        /// <summary>
        /// Store SqlAction into bank
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        protected ISQLAction StoreActionToBank(ISQLAction action)
        {
            if (action != null)
            {
                ActionBankGetter.RootActionBank.AddSQLAction(action);
            }

            return action;
        }

        #endregion

        #region Merge filters with global

        /// <summary>
        /// Mixed DataFilter
        /// </summary>
        /// <param name="filters"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        protected ISQLPredicate[] MixedDataFilter<TEntity>(ISQLPredicate[] filters) where TEntity : class, IEntity, new()
        {
            var globalFilter = GlobalDataFilterManager.GetFilter((typeof(TEntity), typeof(TEntity)));
            return DataFilterMixer.Mix(globalFilter, filters);
        }

        #endregion

        #region single actions

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public AsynchronousInsertAction<TEntity> Insert<TEntity>(TEntity entity) where TEntity : class, IEntity, new()
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var action = new AsynchronousInsertAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entity);
            return StoreActionToBank(action) as AsynchronousInsertAction<TEntity>;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public AsynchronousUpdateAction<TEntity> Update<TEntity>(TEntity entity, bool ignoreAllKeyProperties = false) where TEntity : class, IEntity, new()
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var filters = MixedDataFilter<TEntity>(((IHasDataFilter) this).Filters);
            var action = new AsynchronousUpdateAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entity, filters, ignoreAllKeyProperties);
            return StoreActionToBank(action) as AsynchronousUpdateAction<TEntity>;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public AsynchronousDeleteAction<TEntity> Delete<TEntity>(TEntity entity) where TEntity : class, IEntity, new()
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var filters = MixedDataFilter<TEntity>(((IHasDataFilter) this).Filters);
            var action = new AsynchronousDeleteAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entity, filters);
            return StoreActionToBank(action) as AsynchronousDeleteAction<TEntity>;
        }

        #endregion

        #region batch actions

        /// <summary>
        /// Batch insert
        /// </summary>
        /// <param name="entities"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public AsynchronousBatchInsertAction<TEntity> BatchInsert<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity, new()
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var action = new AsynchronousBatchInsertAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entities);
            return StoreActionToBank(action) as AsynchronousBatchInsertAction<TEntity>;
        }

        /// <summary>
        /// Batch update
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public AsynchronousBatchUpdateAction<TEntity> BatchUpdate<TEntity>(IEnumerable<TEntity> entities, bool ignoreAllKeyProperties = false)
            where TEntity : class, IEntity, new()
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var filters = MixedDataFilter<TEntity>(((IHasDataFilter) this).Filters);
            var action = new AsynchronousBatchUpdateAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entities, filters, ignoreAllKeyProperties);
            return StoreActionToBank(action) as AsynchronousBatchUpdateAction<TEntity>;
        }

        /// <summary>
        /// Batch delete
        /// </summary>
        /// <param name="entities"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public AsynchronousBatchDeleteAction<TEntity> BatchDelete<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity, new()
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var filters = MixedDataFilter<TEntity>(((IHasDataFilter) this).Filters);
            var action = new AsynchronousBatchDeleteAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entities, filters);
            return StoreActionToBank(action) as AsynchronousBatchDeleteAction<TEntity>;
        }

        #endregion

        #region bulk action

        /// <summary>
        /// Bulk insert
        /// </summary>
        /// <param name="entities"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public AsynchronousBulkInsertAction<TEntity> BulkInsert<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity, new()
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var action = new AsynchronousBulkInsertAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entities);
            return StoreActionToBank(action) as AsynchronousBulkInsertAction<TEntity>;
        }

        #endregion

        #region expression action

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="predicateExpression"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public AsynchronousExpressionDeleteAction<TEntity> Delete<TEntity>(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicateExpression)
            where TEntity : class, IEntity, new()
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var filters = MixedDataFilter<TEntity>(((IHasDataFilter) this).Filters);
            var action = new AsynchronousExpressionDeleteAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, predicateExpression, filters);
            return StoreActionToBank(action) as AsynchronousExpressionDeleteAction<TEntity>;
        }

        #endregion
    }
}