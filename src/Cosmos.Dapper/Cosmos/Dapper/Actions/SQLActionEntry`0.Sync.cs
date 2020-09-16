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
    /// SqlAction entry
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class SQLActionSyncEntry : ISQLActionEntry, IHasRootActionBank, IHasDataFilter, IHasBulkOpt
    {
        SQLActionSetBase IHasRootActionBank.RootActionBank { get; set; }

        IDapperContextParams IHasBulkOpt.ContextParams { get; set; }

        ISQLPredicate[] IHasDataFilter.Filters { get; set; }

        /// <summary>
        /// Gets SqlAction bank getter
        /// </summary>
        protected IHasRootActionBank ActionBankGetter => this;

        /// <summary>
        /// Create a new instance of <see cref="SQLActionSyncEntry" />
        /// </summary>
        /// <param name="rootActionSet"></param>
        /// <param name="contextParams"></param>
        /// <param name="filters"></param>
        public SQLActionSyncEntry(SQLActionSetBase rootActionSet, IDapperContextParams contextParams, ISQLPredicate[] filters = null)
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
        public InsertAction<TEntity> Insert<TEntity>(TEntity entity) where TEntity : class, IEntity, new()
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
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public UpdateAction<TEntity> Update<TEntity>(TEntity entity, bool ignoreAllKeyProperties = false) where TEntity : class, IEntity, new()
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
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public DeleteAction<TEntity> Delete<TEntity>(TEntity entity) where TEntity : class, IEntity, new()
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
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public BatchInsertAction<TEntity> BatchInsert<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity, new()
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
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public BatchUpdateAction<TEntity> BatchUpdate<TEntity>(IEnumerable<TEntity> entities, bool ignoreAllKeyProperties = false) where TEntity : class, IEntity, new()
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
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public BatchDeleteAction<TEntity> BatchDelete<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity, new()
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
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public BulkInsertAction<TEntity> BulkInsert<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity, new()
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
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public ExpressionDeleteAction<TEntity> Delete<TEntity>(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicateExpression) where TEntity : class, IEntity, new()
        {
            var contextParams = ((IHasBulkOpt) this).ContextParams;
            var filters = MixedDataFilter<TEntity>(((IHasDataFilter) this).Filters);
            var action = new ExpressionDeleteAction<TEntity>(ActionBankGetter.RootActionBank, contextParams, predicateExpression, filters);
            return StoreActionToBank(action) as ExpressionDeleteAction<TEntity>;
        }

        #endregion
    }
}