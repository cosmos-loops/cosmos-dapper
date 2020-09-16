using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Data.SqlKata;
using SqlKata.Execution;

namespace Cosmos.Dapper.Store
{
    // ReSharper disable once UnusedTypeParameter
    public abstract partial class StoreBase<TContext, TEntity>
    {
        #region Add # in base IStore.WriteableStore

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual void Add(TEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));
            RawTypedContext.EntityOperators.Insert(entity);
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entities"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual void Add(IEnumerable<TEntity> entities)
        {
            if (entities is null)
                throw new ArgumentNullException(nameof(entities));
            RawTypedContext.EntityOperators.Insert(entities);
        }

        /// <summary>
        /// Add async
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));
            return RawTypedContext.EntityOperators.InsertAsync(entity, cancellationToken);
        }

        /// <summary>
        /// Add async
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual Task AddAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            if (entities is null)
                throw new ArgumentNullException(nameof(entities));
            return RawTypedContext.EntityOperators.InsertAsync(entities, cancellationToken);
        }

        #endregion

        #region Add # in IStore.WriteableStore

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual int Add(Func<QueryBuilder, QueryBuilder> sqlKataFunc, TEntity data)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()()).Insert(data);
        }

        /// <summary>
        /// Add async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual Task<int> AddAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc, TEntity data)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()()).InsertAsync(data);
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <param name="columnNames"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual int Add(Func<QueryBuilder, QueryBuilder> sqlKataFunc, IEnumerable<string> columnNames, IEnumerable<IEnumerable<object>> data)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()()).Insert(columnNames, data);
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <param name="newValues"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual int Add(Func<QueryBuilder, QueryBuilder> sqlKataFunc, IReadOnlyDictionary<string, object> newValues)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()()).Insert(newValues);
        }

        /// <summary>
        /// Add Async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <param name="newValues"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual Task<int> AddAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc, IReadOnlyDictionary<string, object> newValues)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()()).InsertAsync(newValues);
        }

        #endregion
    }
}