using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Data.Statements;
using QueryOneType = Cosmos.Dapper.Core.DapperImplementor.QueryOneType;

namespace Cosmos.Dapper.Operations
{
    public partial interface IDapperEntityOperator
    {
        /// <summary>
        /// Delete entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool Delete<T>(object predicate, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class;

        /// <summary>
        /// Delete entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> DeleteAsync<T>(object predicate, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Delete entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="filters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool Delete<T>(object predicate, ISQLPredicate[] filters = null) where T : class;

        /// <summary>
        /// Delete entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> DeleteAsync<T>(object predicate, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Get one entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <param name="type"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetOne<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true,
            QueryOneType type = QueryOneType.FirstOrDefault) where T : class;

        /// <summary>
        /// Get one entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="type"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> GetOneAsync<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, QueryOneType type = QueryOneType.FirstOrDefault,
            CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Get one entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <param name="type"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetOne<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, bool buffered = true, QueryOneType type = QueryOneType.FirstOrDefault) where T : class;

        /// <summary>
        /// Get one entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="filters"></param>
        /// <param name="type"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> GetOneAsync<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, QueryOneType type = QueryOneType.FirstOrDefault,
            CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Gets first entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T First<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true) where T : class;

        /// <summary>
        /// Gets first entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> FirstAsync<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default)
            where T : class;

        /// <summary>
        /// Gets first entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T First<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, bool buffered = true) where T : class;

        /// <summary>
        /// Gets first entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> FirstAsync<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Gets first entity or default by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T FirstOrDefault<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true) where T : class;

        /// <summary>
        /// Gets first entity or default by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> FirstOrDefaultAsync<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Gets first entity or default by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T FirstOrDefault<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, bool buffered = true) where T : class;

        /// <summary>
        /// Gets first entity or default by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> FirstOrDefaultAsync<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Gets single entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Single<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true) where T : class;

        /// <summary>
        /// Gets single entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> SingleAsync<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default)
            where T : class;

        /// <summary>
        /// Gets single entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Single<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, bool buffered = true) where T : class;

        /// <summary>
        /// Gets single entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> SingleAsync<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Gets single entity or default by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T SingleOrDefault<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true) where T : class;

        /// <summary>
        /// Gets single entity or default by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> SingleOrDefaultAsync<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Gets single entity or default by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T SingleOrDefault<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, bool buffered = true) where T : class;

        /// <summary>
        /// Gets single entity or default by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> SingleOrDefaultAsync<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Gets a list of entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetList<T>(object predicate, SQLSortSet sort, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true) where T : class;

        /// <summary>
        /// Gets a list of entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<IEnumerable<T>> GetListAsync<T>(object predicate, SQLSortSet sort, IDbTransaction transaction, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Gets a list of entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetList<T>(object predicate, SQLSortSet sort, ISQLPredicate[] filters = null, bool buffered = true) where T : class;

        /// <summary>
        /// Gets a list of entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<IEnumerable<T>> GetListAsync<T>(object predicate, SQLSortSet sort, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Gets a paged list of entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetPage<T>(object predicate, SQLSortSet sort, int pageNumber, int pageSize, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true)
            where T : class;

        /// <summary>
        /// Gets a paged list of entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<IEnumerable<T>> GetPageAsync<T>(object predicate, SQLSortSet sort, int pageNumber, int pageSize, IDbTransaction transaction, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Gets a paged list of entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetPage<T>(object predicate, SQLSortSet sort, int pageNumber, int pageSize, ISQLPredicate[] filters = null, bool buffered = true) where T : class;

        /// <summary>
        /// Gets a paged list of entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<IEnumerable<T>> GetPageAsync<T>(object predicate, SQLSortSet sort, int pageNumber, int pageSize, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Gets a set of entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="limitFrom"></param>
        /// <param name="limitTo"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetSet<T>(object predicate, SQLSortSet sort, int limitFrom, int limitTo, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true)
            where T : class;

        /// <summary>
        /// Gets a set of entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="limitFrom"></param>
        /// <param name="limitTo"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<IEnumerable<T>> GetSetAsync<T>(object predicate, SQLSortSet sort, int limitFrom, int limitTo, IDbTransaction transaction, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Gets a set of entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="limitFrom"></param>
        /// <param name="limitTo"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetSet<T>(object predicate, SQLSortSet sort, int limitFrom, int limitTo, ISQLPredicate[] filters = null, bool buffered = true) where T : class;

        /// <summary>
        /// Gets a set of entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="limitFrom"></param>
        /// <param name="limitTo"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<IEnumerable<T>> GetSetAsync<T>(object predicate, SQLSortSet sort, int limitFrom, int limitTo, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Gets count of entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        int Count<T>(object predicate, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class;

        /// <summary>
        /// Gets count of entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<int> CountAsync<T>(object predicate, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Gets count of entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="filters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        int Count<T>(object predicate, ISQLPredicate[] filters = null) where T : class;

        /// <summary>
        /// Gets count of entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<int> CountAsync<T>(object predicate, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
    }
}