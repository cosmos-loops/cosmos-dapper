using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Models;
using Cosmos.Validation.Parameters;

namespace Cosmos.Dapper.Store
{
    /// <summary>
    /// Interface for dynamic expression writeable store
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IDynamicExpressionWriteableStore<TEntity> where TEntity : class, IEntity, new()
    {
        #region Remove

        /// <summary>
        /// Remove by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        void Remove([NotNull] Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Remove by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RemoveAsync([NotNull] Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        #endregion

        #region Remove unsafe

        /// <summary>
        /// Remove by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        void UnsafeRemove([NotNull] Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Remove by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UnsafeRemoveAsync([NotNull] Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        #endregion
    }
}