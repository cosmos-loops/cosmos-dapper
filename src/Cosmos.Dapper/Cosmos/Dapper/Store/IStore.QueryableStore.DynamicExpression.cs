using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Data.Statements;
using Cosmos.Models;
using Cosmos.Validation.Parameters;
using DotNetCore.Collections.Paginable;

namespace Cosmos.Dapper.Store
{
    /// <summary>
    /// Interface for dynamic expression queryable store
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IDynamicExpressionQueryableStore<TEntity> where TEntity : class, IEntity, new()
    {
        #region Count

        /// <summary>
        /// Count
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        long Count([NotNull] Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Count async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<long> CountAsync([NotNull] Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        #endregion

        #region Exist

        /// <summary>
        /// Exist
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool Exist([NotNull] Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Exist async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> ExistAsync([NotNull] Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        #endregion

        #region Find First

        /// <summary>
        /// Find first entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        TEntity FindFirst([NotNull] Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, bool buffered = true);

        /// <summary>
        /// Find first entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> FindFirstAsync([NotNull] Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, CancellationToken cancellationToken = default);

        #endregion

        #region Find First or Default

        /// <summary>
        /// Find first entity or default by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        TEntity FindFirstOrDefault([NotNull] Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, bool buffered = true);

        /// <summary>
        /// Find first entity or default by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> FindFirstOrDefaultAsync([NotNull] Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, CancellationToken cancellationToken = default);

        #endregion

        #region Find Single

        /// <summary>
        /// Find single entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        TEntity FindSingle([NotNull] Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, bool buffered = true);

        /// <summary>
        /// Find single entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> FindSingleAsync([NotNull] Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, CancellationToken cancellationToken = default);

        #endregion

        #region Find Single or Default

        /// <summary>
        /// Find single entity or default by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        TEntity FindSingleOrDefault([NotNull] Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, bool buffered = true);

        /// <summary>
        /// Find single entity or default by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> FindSingleOrDefaultAsync([NotNull] Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, CancellationToken cancellationToken = default);

        #endregion

        #region Find

        /// <summary>
        /// Find a collection of entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Find([NotNull] Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, bool buffered = true);

        /// <summary>
        /// Find a collection of entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> FindAsync([NotNull] Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, CancellationToken cancellationToken = default);

        /// <summary>
        /// Find a collection of entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="limitFrom"></param>
        /// <param name="limitTo"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Find([NotNull] Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, int limitFrom, int limitTo, bool buffered = true);

        /// <summary>
        /// Find a collection of entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="limitFrom"></param>
        /// <param name="limitTo"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> Find([NotNull] Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, int limitFrom, int limitTo,
            CancellationToken cancellationToken = default);

        #endregion

        #region Get Page

        /// <summary>
        /// Query paged collection of entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        IPage<TEntity> GetPage([NotNull] Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, int pageNumber, int pageSize, bool buffered = true);

        /// <summary>
        /// Query paged collection of entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IPage<TEntity>> GetPageAsync([NotNull] Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, int pageNumber, int pageSize,
            CancellationToken cancellationToken = default);

        #endregion
    }
}