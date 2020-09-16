using System.Threading;
using System.Threading.Tasks;
using Cosmos.Models;
using Cosmos.Validation.Parameters;

namespace Cosmos.Dapper.Store
{
    /// <summary>
    /// Interface for predicate writeable store
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    // ReSharper disable once UnusedTypeParameter
    public interface IPredicateWriteableStore<TEntity> where TEntity : class, IEntity, new()
    {
        #region Remove

        /// <summary>
        /// Remove by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        void Remove([NotNull] object predicate);

        /// <summary>
        /// Remove by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RemoveAsync([NotNull] object predicate, CancellationToken cancellationToken = default);

        #endregion

        #region Remove unsafe

        /// <summary>
        /// Remove by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        void UnsafeRemove([NotNull] object predicate);

        /// <summary>
        /// Remove by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UnsafeRemoveAsync([NotNull] object predicate, CancellationToken cancellationToken = default);

        #endregion

        #region Remove by Id

        /// <summary>
        /// Remove by given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void RemoveById(dynamic id);

        /// <summary>
        /// Remove by given id async
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RemoveByIdAsync(dynamic id, CancellationToken cancellationToken = default);

        #endregion

        #region Remove unsafe by Id

        /// <summary>
        /// Remove by given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void UnsafeRemoveById(dynamic id);

        /// <summary>
        /// Remove by given id async
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UnsafeRemoveByIdAsync(dynamic id, CancellationToken cancellationToken = default);

        #endregion
    }
}