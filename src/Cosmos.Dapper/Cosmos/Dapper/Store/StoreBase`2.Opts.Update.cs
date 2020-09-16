using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Dapper.Core.SqlKata;
using Cosmos.Data.SqlKata;
using SqlKata.Execution;

namespace Cosmos.Dapper.Store
{
    // ReSharper disable once UnusedTypeParameter
    public abstract partial class StoreBase<TContext, TEntity>
    {
        #region Update # in base IStore.WriteableStore

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual void Update(TEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));
            RawTypedContext.EntityOperators.Update(entity, RepoLevelDataFilters);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entities"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual void Update(IEnumerable<TEntity> entities)
        {
            if (entities is null)
                throw new ArgumentNullException(nameof(entities));
            RawTypedContext.EntityOperators.Update(entities, RepoLevelDataFilters);
        }

        /// <summary>
        /// Update async
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));
            return RawTypedContext.EntityOperators.UpdateAsync(entity, RepoLevelDataFilters, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Update async
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual Task UpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            if (entities is null)
                throw new ArgumentNullException(nameof(entities));
            return RawTypedContext.EntityOperators.UpdateAsync(entities, RepoLevelDataFilters, cancellationToken: cancellationToken);
        }

        #endregion

        #region Update in IStore.WriteableStore

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <param name="newValues"></param>
        /// <returns></returns>
        public virtual int Update(Func<QueryBuilder, QueryBuilder> sqlKataFunc, object newValues)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()())
               .WhereRawSafety(SqlKataRepoLevelDataFilters)
               .Update(newValues);
        }

        /// <summary>
        /// Update async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <param name="newValues"></param>
        /// <returns></returns>
        public virtual Task<int> UpdateAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc, object newValues)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()())
               .WhereRawSafety(SqlKataRepoLevelDataFilters)
               .UpdateAsync(newValues);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <param name="newValues"></param>
        /// <returns></returns>
        public virtual int Update(Func<QueryBuilder, QueryBuilder> sqlKataFunc, IReadOnlyDictionary<string, object> newValues)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()())
               .WhereRawSafety(SqlKataRepoLevelDataFilters)
               .Update(newValues);
        }

        /// <summary>
        /// Update async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <param name="newValues"></param>
        /// <returns></returns>
        public virtual Task<int> UpdateAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc, IReadOnlyDictionary<string, object> newValues)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()())
               .WhereRawSafety(SqlKataRepoLevelDataFilters)
               .UpdateAsync(newValues);
        }

        #endregion
    }
}