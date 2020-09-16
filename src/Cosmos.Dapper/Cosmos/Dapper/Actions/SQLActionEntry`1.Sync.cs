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
    /// SqlAction entry
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    // ReSharper disable once InconsistentNaming
    public class SQLActionSyncEntry<TEntity> : SQLActionSyncEntry, ISQLActionEntry<TEntity> where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Create a new instance of <see cref="SQLActionSyncEntry{TEntity}" />
        /// </summary>
        /// <param name="rootActionSet"></param>
        /// <param name="contextParams"></param>
        /// <param name="filters"></param>
        public SQLActionSyncEntry(SQLActionSet<TEntity> rootActionSet, IDapperContextParams contextParams, ISQLPredicate[] filters = null)
            : base(rootActionSet, contextParams, filters) { }

        #region single actions

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public InsertAction<TEntity> Insert(TEntity entity)
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var action = new InsertAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entity);
            return StoreActionToBank(action) as InsertAction<TEntity>;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <returns></returns>
        public UpdateAction<TEntity> Update(TEntity entity, bool ignoreAllKeyProperties = false)
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var filters = MixedDataFilter<TEntity>(((IHasDataFilter) this).Filters);
            var action = new UpdateAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entity, filters, ignoreAllKeyProperties);
            return StoreActionToBank(action) as UpdateAction<TEntity>;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DeleteAction<TEntity> Delete(TEntity entity)
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var filters = MixedDataFilter<TEntity>(((IHasDataFilter) this).Filters);
            var action = new DeleteAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entity, filters);
            return StoreActionToBank(action) as DeleteAction<TEntity>;
        }

        #endregion

        #region batch actions

        /// <summary>
        /// Batch insert
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public BatchInsertAction<TEntity> BatchInsert(IEnumerable<TEntity> entities)
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var action = new BatchInsertAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entities);
            return StoreActionToBank(action) as BatchInsertAction<TEntity>;
        }

        /// <summary>
        /// Batch update
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <returns></returns>
        public BatchUpdateAction<TEntity> BatchUpdate(IEnumerable<TEntity> entities, bool ignoreAllKeyProperties = false)
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var filters = MixedDataFilter<TEntity>(((IHasDataFilter) this).Filters);
            var action = new BatchUpdateAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entities, filters, ignoreAllKeyProperties);
            return StoreActionToBank(action) as BatchUpdateAction<TEntity>;
        }

        /// <summary>
        /// Batch delete
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public BatchDeleteAction<TEntity> BatchDelete(IEnumerable<TEntity> entities)
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var filters = MixedDataFilter<TEntity>(((IHasDataFilter) this).Filters);
            var action = new BatchDeleteAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entities, filters);
            return StoreActionToBank(action) as BatchDeleteAction<TEntity>;
        }

        #endregion

        #region bulk action

        /// <summary>
        /// Bulk insert
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public BulkInsertAction<TEntity> BulkInsert(IEnumerable<TEntity> entities)
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var action = new BulkInsertAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entities);
            return StoreActionToBank(action) as BulkInsertAction<TEntity>;
        }

        #endregion

        #region expression action

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="predicateExpression"></param>
        /// <returns></returns>
        public ExpressionDeleteAction<TEntity> Delete(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicateExpression)
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var filters = MixedDataFilter<TEntity>(((IHasDataFilter) this).Filters);
            var action = new ExpressionDeleteAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, predicateExpression, filters);
            return StoreActionToBank(action) as ExpressionDeleteAction<TEntity>;
        }

        #endregion
    }
}