using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Data.SqlKata;

namespace Cosmos.Dapper.Store
{
    // ReSharper disable once UnusedTypeParameter
    public abstract partial class StoreBase<TContext, TEntity>
    {
        #region Exist # in IStore.QueryableStore

        /// <summary>
        /// Exist by given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool ExistById(dynamic id)
        {
            return FindById(id) != null;
        }

        /// <summary>
        /// Exist by given id async
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<bool> ExistByIdAsync(dynamic id, CancellationToken cancellationToken = default)
        {
            return await FindByIdAsync(id, cancellationToken) != null;
        }

        #endregion

        #region Exist # in IStore.QueryableStore.Predicates

        /// <summary>
        /// Exist by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual bool Exist(object predicate)
        {
            return Count(predicate) > 0;
        }

        /// <summary>
        /// Exist by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<bool> ExistAsync(object predicate, CancellationToken cancellationToken = default)
        {
            return await CountAsync(predicate, cancellationToken) > 0;
        }

        #endregion

        #region Exist # in IStore.QueryableStore.DynamicExpression

        /// <summary>
        /// Exist by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual bool Exist(Expression<Func<TEntity, bool>> predicate)
        {
            return Count(predicate) > 0;
        }

        /// <summary>
        /// Exist by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await CountAsync(predicate, cancellationToken) > 0;
        }

        #endregion

        #region Exist # in IStore.QueryableStore.SqlKata

        /// <summary>
        /// Exist by given SqlKata function
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <param name="columnNames"></param>
        /// <returns></returns>
        public virtual bool Exist(Func<QueryBuilder, QueryBuilder> sqlKataFunc, params string[] columnNames)
        {
            return Count(sqlKataFunc, columnNames) > 0;
        }

        /// <summary>
        /// Exist by given SqlKata function async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <param name="columnNames"></param>
        /// <returns></returns>
        public virtual async Task<bool> ExistAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc, params string[] columnNames)
        {
            return await CountAsync(sqlKataFunc, columnNames) > 0;
        }

        #endregion
    }
}