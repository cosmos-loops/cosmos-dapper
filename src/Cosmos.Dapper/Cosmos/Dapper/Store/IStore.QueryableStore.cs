using System.Threading;
using System.Threading.Tasks;
using Cosmos.Dapper.Actions;
using Cosmos.Models;

namespace Cosmos.Dapper.Store
{
    /// <summary>
    /// Interface for queryable store
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IQueryableStore<TEntity> where TEntity : class, IEntity, new()
    {
        #region Exist

        /// <summary>
        /// Exist by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool ExistById(dynamic id);

        /// <summary>
        /// Exist by id async
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> ExistByIdAsync(dynamic id, CancellationToken cancellationToken = default);

        #endregion

        #region Find by id

        /// <summary>
        /// FInd entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity FindById(dynamic id);

        /// <summary>
        /// Find entity by id async
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> FindByIdAsync(dynamic id, CancellationToken cancellationToken = default);

        #endregion

        #region Get One

        /// <summary>
        /// Get one or null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetOne(dynamic id);

        /// <summary>
        /// Get one or null
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> GetOneAsync(dynamic id, CancellationToken cancellationToken = default);

        #endregion

        /// <summary>
        /// Gets Dapper Action entry
        /// </summary>
        ISQLActionEntry ActionEntry { get; }

        /// <summary>
        /// Gets Dapper Action entry
        /// </summary>
        ISQLActionEntry<TEntity> EntityEntry { get; }

        /// <summary>
        /// Gets Dapper Action entry asynchronous
        /// </summary>
        ISQLActionAsyncEntry AsynchronousActionEntry { get; }

        /// <summary>
        /// Gets Dapper Action entry asynchronous
        /// </summary>
        ISQLActionAsyncEntry<TEntity> AsynchronousEntityEntry { get; }
    }

    /// <summary>
    /// Interface for queryable store
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IQueryableStore<TEntity, in TKey> : IQueryableStore<TEntity>
        where TEntity : class, IEntity<TKey>, new()
    {
        #region Exist

        /// <summary>
        /// Exist by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool ExistById(TKey id);

        /// <summary>
        /// Exist by id async
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> ExistByIdAsync(TKey id, CancellationToken cancellationToken = default);

        #endregion

        #region Find by id

        /// <summary>
        /// Find entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity FindById(TKey id);

        /// <summary>
        /// Find entity by id async
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> FindByIdAsync(TKey id, CancellationToken cancellationToken = default);

        #endregion
    }
}