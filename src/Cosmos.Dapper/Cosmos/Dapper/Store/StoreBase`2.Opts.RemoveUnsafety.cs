using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
        #region Remove # in base IStore.WriteableStore

        /// <summary>
        /// Unsafe remove
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual void UnsafeRemove(TEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            InternalRemove(entity, true);
        }

        /// <summary>
        /// Unsafe remove async
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual async Task UnsafeRemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            await InternalRemoveAsync(entity, true, cancellationToken);
        }

        /// <summary>
        /// Unsafe remove
        /// </summary>
        /// <param name="entities"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual void UnsafeRemove(IEnumerable<TEntity> entities)
        {
            if (entities is null)
                throw new ArgumentNullException(nameof(entities));

            InternalRemove(entities, true);
        }

        /// <summary>
        /// Unsafe remove async
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual async Task UnsafeRemoveAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            if (entities is null)
                throw new ArgumentNullException(nameof(entities));

            await InternalRemoveAsync(entities, true, cancellationToken);
        }

        #endregion

        #region Remove unsafe # in IStore.WriteableStore.DynamicExpression

        /// <summary>
        /// Remove by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual void UnsafeRemove(Expression<Func<TEntity, bool>> predicate)
        {
            RawTypedContext.EntityOperators.Delete(predicate, RepoLevelDataFilters);
        }

        /// <summary>
        /// Remove by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task UnsafeRemoveAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            await RawTypedContext.EntityOperators.DeleteAsync(predicate, RepoLevelDataFilters, cancellationToken);
        }

        #endregion

        #region Remove unsafe # in IStore.WriteableStore.Predicates

        /// <summary>
        /// Remove by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual void UnsafeRemove(object predicate)
        {
            RawTypedContext.EntityOperators.Delete<TEntity>(predicate, RepoLevelDataFilters);
        }

        /// <summary>
        /// Remove by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task UnsafeRemoveAsync(object predicate, CancellationToken cancellationToken = default)
        {
            await RawTypedContext.EntityOperators.DeleteAsync<TEntity>(predicate, RepoLevelDataFilters, cancellationToken);
        }

        #endregion

        #region Remove unsafe by Id # in IStore.IStore.WriteableStore.Predicates

        /// <summary>
        /// Remove entity by given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual void UnsafeRemoveById(dynamic id)
        {
            if (FindById(id) is TEntity entity)
            {
                InternalRemove(entity, true);
            }
        }

        /// <summary>
        /// Remove entity by given id async
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task UnsafeRemoveByIdAsync(dynamic id, CancellationToken cancellationToken = default)
        {
            if (await FindByIdAsync(id, cancellationToken) is TEntity entity)
            {
                await InternalRemoveAsync(entity, true, cancellationToken);
            }
        }

        #endregion

        #region Remove unsafe # in IStore.WriteableStore

        /// <summary>
        /// Unsafe remove
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        public virtual void UnsafeRemove(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()())
               .WhereRawSafety(SqlKataRepoLevelDataFilters)
               .Delete();
        }

        /// <summary>
        /// Unsafe remove async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        public virtual Task UnsafeRemoveAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()())
               .WhereRawSafety(SqlKataRepoLevelDataFilters)
               .DeleteAsync();
        }

        #endregion
    }
}