using System;
using System.Linq;
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
        #region Count # in IStore.QueryableStore.DynamicExpression

        /// <summary>
        /// Count
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual long Count(object predicate)
        {
            return RawTypedContext.EntityOperators.GetList<TEntity>(predicate, null, RepoLevelDataFilters).LongCount();
        }

        /// <summary>
        /// Count async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<long> CountAsync(object predicate, CancellationToken cancellationToken = default)
        {
            var entities = await RawTypedContext.EntityOperators.GetListAsync<TEntity>(predicate, null, RepoLevelDataFilters, cancellationToken);
            return await entities.LongCountAsync(cancellationToken);
        }

        #endregion

        #region Count # in IStore.QueryableStore.Predicates

        /// <summary>
        /// Count
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual long Count(Expression<Func<TEntity, bool>> predicate)
        {
            return RawTypedContext.EntityOperators.GetList(predicate, null, RepoLevelDataFilters).LongCount();
        }

        /// <summary>
        /// Count async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var entities = await RawTypedContext.EntityOperators.GetListAsync(predicate, null, RepoLevelDataFilters, cancellationToken);
            return await entities.CountAsync(cancellationToken);
        }

        #endregion

        #region Count # in IStore.QueryableStore.SqlKata

        /// <summary>
        /// Count
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <param name="columnNames"></param>
        /// <returns></returns>
        public virtual long Count(Func<QueryBuilder, QueryBuilder> sqlKataFunc, params string[] columnNames)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()()).WhereRawSafety(SqlKataRepoLevelDataFilters).Count<long>(columnNames);
        }

        /// <summary>
        /// Count async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <param name="columnNames"></param>
        /// <returns></returns>
        public virtual Task<long> CountAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc, params string[] columnNames)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()()).WhereRawSafety(SqlKataRepoLevelDataFilters).CountAsync<long>(columnNames);
        }

        #endregion
    }
}