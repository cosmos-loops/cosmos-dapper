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
    /// SqlAction base
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public abstract class SQLActionSyncBase : SQLAction
    {
        /// <summary>
        /// Create a new instance of <see cref="SQLActionSyncBase" />
        /// </summary>
        /// <param name="rootActionSet"></param>
        /// <param name="kind"></param>
        /// <param name="contextParams"></param>
        /// <param name="filters"></param>
        protected SQLActionSyncBase(SQLActionSetBase rootActionSet, ActionKind kind, IDapperContextParams contextParams, ISQLPredicate[] filters)
            : base(rootActionSet, kind, ActionCallingMode.SyncMode, contextParams, filters) { }

        #region single actions

        /// <summary>
        /// And insert...
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public InsertAction<TEntity> AndInsert<TEntity>(TEntity entity) where TEntity : class, IEntity, new()
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var action = new InsertAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entity);
            return StoreActionToBank(action) as InsertAction<TEntity>;
        }

        /// <summary>
        /// And update...
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public UpdateAction<TEntity> AndUpdate<TEntity>(TEntity entity) where TEntity : class, IEntity, new()
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var filters = MixedDataFilter<TEntity>(((IHasDataFilter) this).Filters);
            var action = new UpdateAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entity, filters);
            return StoreActionToBank(action) as UpdateAction<TEntity>;
        }

        /// <summary>
        /// And delete...
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public DeleteAction<TEntity> AndDelete<TEntity>(TEntity entity) where TEntity : class, IEntity, new()
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var filters = MixedDataFilter<TEntity>(((IHasDataFilter) this).Filters);
            var action = new DeleteAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entity, filters);
            return StoreActionToBank(action) as DeleteAction<TEntity>;
        }

        #endregion

        #region batch actions

        /// <summary>
        /// And batch insert...
        /// </summary>
        /// <param name="entities"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public BatchInsertAction<TEntity> AndBatchInsert<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity, new()
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var action = new BatchInsertAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entities);
            return StoreActionToBank(action) as BatchInsertAction<TEntity>;
        }

        /// <summary>
        /// And batch update...
        /// </summary>
        /// <param name="entities"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public BatchUpdateAction<TEntity> AndBatchUpdate<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity, new()
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var filters = MixedDataFilter<TEntity>(((IHasDataFilter) this).Filters);
            var action = new BatchUpdateAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entities, filters);
            return StoreActionToBank(action) as BatchUpdateAction<TEntity>;
        }

        /// <summary>
        /// And batch delete...
        /// </summary>
        /// <param name="entities"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public BatchDeleteAction<TEntity> AndBatchDelete<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity, new()
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var filters = MixedDataFilter<TEntity>(((IHasDataFilter) this).Filters);
            var action = new BatchDeleteAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entities, filters);
            return StoreActionToBank(action) as BatchDeleteAction<TEntity>;
        }

        #endregion

        #region bulk actions

        /// <summary>
        /// And bulk insert...
        /// </summary>
        /// <param name="entities"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public BulkInsertAction<TEntity> AndBulkInsert<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity, new()
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var action = new BulkInsertAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entities);
            return StoreActionToBank(action) as BulkInsertAction<TEntity>;
        }

        #endregion

        #region expression action

        /// <summary>
        /// And delete...
        /// </summary>
        /// <param name="predicateExpression"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public ExpressionDeleteAction<TEntity> AndDelete<TEntity>(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicateExpression)
            where TEntity : class, IEntity, new()
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var filters = MixedDataFilter<TEntity>(((IHasDataFilter) this).Filters);
            var action = new ExpressionDeleteAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, predicateExpression, filters);
            return StoreActionToBank(action) as ExpressionDeleteAction<TEntity>;
        }

        #endregion
    }
}