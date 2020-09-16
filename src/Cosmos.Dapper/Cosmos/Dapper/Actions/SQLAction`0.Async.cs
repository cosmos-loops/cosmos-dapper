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
    /// Asynchronous SqlAction base
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public abstract class SQLActionAsyncBase : SQLAction
    {
        /// <summary>
        /// Create a new instance of <see cref="SQLActionAsyncBase" />
        /// </summary>
        /// <param name="rootActionSet"></param>
        /// <param name="kind"></param>
        /// <param name="contextParams"></param>
        /// <param name="filters"></param>
        protected SQLActionAsyncBase(SQLActionSetBase rootActionSet, ActionKind kind, IDapperContextParams contextParams, ISQLPredicate[] filters)
            : base(rootActionSet, kind, ActionCallingMode.AsyncMode, contextParams, filters) { }

        #region single actions

        /// <summary>
        /// And insert...
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public AsynchronousInsertAction<TEntity> AndInsert<TEntity>(TEntity entity) where TEntity : class, IEntity, new()
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var action = new AsynchronousInsertAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entity);
            return StoreActionToBank(action) as AsynchronousInsertAction<TEntity>;
        }

        /// <summary>
        /// And update...
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public AsynchronousUpdateAction<TEntity> AndUpdate<TEntity>(TEntity entity, bool ignoreAllKeyProperties = false) where TEntity : class, IEntity, new()
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var filters = MixedDataFilter<TEntity>(((IHasDataFilter) this).Filters);
            var action = new AsynchronousUpdateAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entity, filters, ignoreAllKeyProperties);
            return StoreActionToBank(action) as AsynchronousUpdateAction<TEntity>;
        }

        /// <summary>
        /// And delete...
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public AsynchronousDeleteAction<TEntity> AndDelete<TEntity>(TEntity entity) where TEntity : class, IEntity, new()
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var filters = MixedDataFilter<TEntity>(((IHasDataFilter) this).Filters);
            var action = new AsynchronousDeleteAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entity, filters);
            return StoreActionToBank(action) as AsynchronousDeleteAction<TEntity>;
        }

        #endregion

        #region batch actions

        /// <summary>
        /// And batch insert...
        /// </summary>
        /// <param name="entities"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public AsynchronousBatchInsertAction<TEntity> AndBatchInsert<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity, new()
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var action = new AsynchronousBatchInsertAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entities);
            return StoreActionToBank(action) as AsynchronousBatchInsertAction<TEntity>;
        }

        /// <summary>
        /// And batch update...
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public AsynchronousBatchUpdateAction<TEntity> AndBatchUpdate<TEntity>(IEnumerable<TEntity> entities, bool ignoreAllKeyProperties = false)
            where TEntity : class, IEntity, new()
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var filters = MixedDataFilter<TEntity>(((IHasDataFilter) this).Filters);
            var action = new AsynchronousBatchUpdateAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entities, filters, ignoreAllKeyProperties);
            return StoreActionToBank(action) as AsynchronousBatchUpdateAction<TEntity>;
        }

        /// <summary>
        /// And batch delete...
        /// </summary>
        /// <param name="entities"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public AsynchronousBatchDeleteAction<TEntity> AndBatchDelete<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity, new()
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var filters = MixedDataFilter<TEntity>(((IHasDataFilter) this).Filters);
            var action = new AsynchronousBatchDeleteAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entities, filters);
            return StoreActionToBank(action) as AsynchronousBatchDeleteAction<TEntity>;
        }

        #endregion

        #region bulk actions

        /// <summary>
        /// And bulk insert...
        /// </summary>
        /// <param name="entities"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public AsynchronousBulkInsertAction<TEntity> AndBulkInsert<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity, new()
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var action = new AsynchronousBulkInsertAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, entities);
            return StoreActionToBank(action) as AsynchronousBulkInsertAction<TEntity>;
        }

        #endregion

        #region expression action

        /// <summary>
        /// And delete...
        /// </summary>
        /// <param name="predicateExpression"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public AsynchronousExpressionDeleteAction<TEntity> AndDelete<TEntity>(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicateExpression)
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