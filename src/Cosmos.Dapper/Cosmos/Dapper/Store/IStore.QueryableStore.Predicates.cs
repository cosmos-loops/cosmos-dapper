using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Data.Statements;
using Cosmos.Models;
using Cosmos.Validation.Parameters;
using DotNetCore.Collections.Paginable;

namespace Cosmos.Dapper.Store
{
    /// <summary>
    /// Interface for predicate queryable store
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IPredicateQueryableStore<TEntity> where TEntity : class, IEntity, new()
    {
        #region Count

        /// <summary>
        /// Count
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        long Count([NotNull] object predicate);

        /// <summary>
        /// Count async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<long> CountAsync([NotNull] object predicate, CancellationToken cancellationToken = default);

        #endregion

        #region Exist

        /// <summary>
        /// Exist
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool Exist([NotNull] object predicate);

        /// <summary>
        /// Exist async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> ExistAsync([NotNull] object predicate, CancellationToken cancellationToken = default);

        #endregion

        #region Find First

        /// <summary>
        /// Find first entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        TEntity FindFirst([NotNull] object predicate, SQLSortSet sort, bool buffered = true);

        /// <summary>
        /// Find first entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> FindFirstAsync([NotNull] object predicate, SQLSortSet sort, CancellationToken cancellationToken = default);

        #endregion

        #region Find First or default

        /// <summary>
        /// Find first entity or default by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        TEntity FindFirstOrDefault([NotNull] object predicate, SQLSortSet sort, bool buffered = true);

        /// <summary>
        /// Find first entity or default by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> FindFirstOrDefaultAsync([NotNull] object predicate, SQLSortSet sort, CancellationToken cancellationToken = default);

        #endregion

        #region Find Single

        /// <summary>
        /// Find single entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        TEntity FindSingle([NotNull] object predicate, SQLSortSet sort, bool buffered = true);

        /// <summary>
        /// Find single entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> FindSingleAsync([NotNull] object predicate, SQLSortSet sort, CancellationToken cancellationToken = default);

        #endregion

        #region Find Single or Default

        /// <summary>
        /// Find single entity or default by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        TEntity FindSingleOrDefault([NotNull] object predicate, SQLSortSet sort, bool buffered = true);

        /// <summary>
        /// Find single entity or default by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> FindSingleOrDefaultAsync([NotNull] object predicate, SQLSortSet sort, CancellationToken cancellationToken = default);

        #endregion

        #region Find

        /// <summary>
        /// Find a collection of entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Find([NotNull] object predicate, SQLSortSet sort, bool buffered = true);

        /// <summary>
        /// Find a collection of entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> FindAsync([NotNull] object predicate, SQLSortSet sort, CancellationToken cancellationToken = default);

        /// <summary>
        /// Find a collection of entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="limitFrom"></param>
        /// <param name="limitTo"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Find([NotNull] object predicate, SQLSortSet sort, int limitFrom, int limitTo, bool buffered = true);

        /// <summary>
        /// Find a collection of entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="limitFrom"></param>
        /// <param name="limitTo"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> FindAsync([NotNull] object predicate, SQLSortSet sort, int limitFrom, int limitTo, CancellationToken cancellationToken = default);

        #endregion

        #region Get Page

        /// <summary>
        /// Query a paged list of entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        IPage<TEntity> GetPage([NotNull] object predicate, SQLSortSet sort, int pageNumber, int pageSize, bool buffered = true);

        /// <summary>
        /// Query a paged list of entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IPage<TEntity>> GetPageAsync([NotNull] object predicate, SQLSortSet sort, int pageNumber, int pageSize, CancellationToken cancellationToken = default);

        #endregion
    }
}