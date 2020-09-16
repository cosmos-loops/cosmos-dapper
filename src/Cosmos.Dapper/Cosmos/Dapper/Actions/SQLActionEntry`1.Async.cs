using System;
using System.Collections.Generic;
using Cosmos.Dapper.Actions.Delete;
using Cosmos.Dapper.Actions.Insert;
using Cosmos.Dapper.Actions.Update;
using Cosmos.Dapper.Core;
using Cosmos.Data.Statements;
using Cosmos.Models;

namespace Cosmos.Dapper.Actions
{
    /// <summary>
    /// Asynchronous SqlAction Entry
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    // ReSharper disable once InconsistentNaming
    public class SQLActionAsyncEntry<TEntity> : SQLActionAsyncEntry, ISQLActionAsyncEntry<TEntity> where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Create a new instance of <see cref="SQLActionAsyncEntry{TEntity}" />
        /// </summary>
        /// <param name="rootActionSet"></param>
        /// <param name="contextParams"></param>
        /// <param name="filters"></param>
        public SQLActionAsyncEntry(SQLActionSet<TEntity> rootActionSet, IDapperContextParams contextParams, ISQLPredicate[] filters = null)
            : base(rootActionSet, contextParams, filters) { }

        #region single actions

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public AsynchronousInsertAction<TEntity> Insert(TEntity entity)
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
        /// <returns></returns>
        public AsynchronousUpdateAction<TEntity> Update(TEntity entity, bool ignoreAllKeyProperties = false)
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
        /// <returns></returns>
        public AsynchronousDeleteAction<TEntity> Delete(TEntity entity)
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
        /// <returns></returns>
        public AsynchronousBatchInsertAction<TEntity> BatchInsert(IEnumerable<TEntity> entities)
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
        /// <returns></returns>
        public AsynchronousBatchUpdateAction<TEntity> BatchUpdate(IEnumerable<TEntity> entities, bool ignoreAllKeyProperties = false)
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
        /// <returns></returns>
        public AsynchronousBatchDeleteAction<TEntity> BatchDelete(IEnumerable<TEntity> entities)
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
        /// <returns></returns>
        public AsynchronousBulkInsertAction<TEntity> BulkInsert(IEnumerable<TEntity> entities)
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
        /// <returns></returns>
        public AsynchronousExpressionDeleteAction<TEntity> Delete(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicateExpression)
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var filters = MixedDataFilter<TEntity>(((IHasDataFilter) this).Filters);
            var action = new AsynchronousExpressionDeleteAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, predicateExpression, filters);
            return StoreActionToBank(action) as AsynchronousExpressionDeleteAction<TEntity>;
        }

        #endregion
    }
}