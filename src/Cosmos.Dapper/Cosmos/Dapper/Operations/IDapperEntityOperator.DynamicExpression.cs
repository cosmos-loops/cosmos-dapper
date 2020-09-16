using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Data.Statements;
using QueryOneType = Cosmos.Dapper.Core.DapperImplementor.QueryOneType;

namespace Cosmos.Dapper.Operations
{
    /// <summary>
    /// Interface for Dapper Entity Operator
    /// </summary>
    public partial interface IDapperEntityOperator
    {
        /// <summary>
        /// Delete by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool Delete<T>(Expression<Func<T, bool>> predicate, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class;

        /// <summary>
        /// Delete by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> DeleteAsync<T>(Expression<Func<T, bool>> predicate, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default)
            where T : class;

        /// <summary>
        /// Delete by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="filters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool Delete<T>(Expression<Func<T, bool>> predicate, ISQLPredicate[] filters = null) where T : class;

        /// <summary>
        /// Delete by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> DeleteAsync<T>(Expression<Func<T, bool>> predicate, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Gets one entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <param name="type"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetOne<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true,
            QueryOneType type = QueryOneType.FirstOrDefault) where T : class;

        /// <summary>
        /// Gets one entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="type"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> GetOneAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null,
            QueryOneType type = QueryOneType.FirstOrDefault, CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Gets one entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <param name="type"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetOne<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, bool buffered = true, QueryOneType type = QueryOneType.FirstOrDefault)
            where T : class;

        /// <summary>
        /// Gets one entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="filters"></param>
        /// <param name="type"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> GetOneAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, QueryOneType type = QueryOneType.FirstOrDefault,
            CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Gets First entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T First<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true) where T : class;

        /// <summary>
        /// Gets First entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> FirstAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Gets First entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T First<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, bool buffered = true) where T : class;

        /// <summary>
        /// Gets First entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> FirstAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default)
            where T : class;

        /// <summary>
        /// Gets First entity or default by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T FirstOrDefault<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true)
            where T : class;

        /// <summary>
        /// Gets First entity or default by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Gets First entity or default by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T FirstOrDefault<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, bool buffered = true) where T : class;

        /// <summary>
        /// Gets First entity or default by given condition asunc
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default)
            where T : class;

        /// <summary>
        /// Gets single by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Single<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true) where T : class;

        /// <summary>
        /// Gets single by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> SingleAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Gets single by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Single<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, bool buffered = true) where T : class;

        /// <summary>
        /// Gets single by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> SingleAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default)
            where T : class;

        /// <summary>
        /// Gets single or default by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T SingleOrDefault<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true)
            where T : class;

        /// <summary>
        /// Gets single or default by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> SingleOrDefaultAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Gets single or default by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T SingleOrDefault<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, bool buffered = true) where T : class;

        /// <summary>
        /// Gets single or default by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> SingleOrDefaultAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default)
            where T : class;

        /// <summary>
        /// Gets list of entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetList<T>(Expression<Func<T, bool>> predicate, SQLSortSet sort, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true)
            where T : class;

        /// <summary>
        /// Gets list of entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<IEnumerable<T>> GetListAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sort, IDbTransaction transaction, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Gets list of entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetList<T>(Expression<Func<T, bool>> predicate, SQLSortSet sort, ISQLPredicate[] filters = null, bool buffered = true) where T : class;

        /// <summary>
        /// Gets list of entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<IEnumerable<T>> GetListAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sort, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default)
            where T : class;

        /// <summary>
        /// Gets paged list of entity by given condition
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
        IEnumerable<T> GetPage<T>(Expression<Func<T, bool>> predicate, SQLSortSet sort, int pageNumber, int pageSize, IDbTransaction transaction, ISQLPredicate[] filters = null,
            bool buffered = true) where T : class;

        /// <summary>
        /// Gets paged list of entity by given condition async
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
        Task<IEnumerable<T>> GetPageAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sort, int pageNumber, int pageSize, IDbTransaction transaction,
            ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Gets paged list of entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetPage<T>(Expression<Func<T, bool>> predicate, SQLSortSet sort, int pageNumber, int pageSize, ISQLPredicate[] filters = null, bool buffered = true)
            where T : class;

        /// <summary>
        /// Gets paged list of entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<IEnumerable<T>> GetPageAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sort, int pageNumber, int pageSize, ISQLPredicate[] filters = null,
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
        IEnumerable<T> GetSet<T>(Expression<Func<T, bool>> predicate, SQLSortSet sort, int limitFrom, int limitTo, IDbTransaction transaction, ISQLPredicate[] filters = null,
            bool buffered = true) where T : class;

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
        Task<IEnumerable<T>> GetSetAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sort, int limitFrom, int limitTo, IDbTransaction transaction,
            ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;

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
        IEnumerable<T> GetSet<T>(Expression<Func<T, bool>> predicate, SQLSortSet sort, int limitFrom, int limitTo, ISQLPredicate[] filters = null, bool buffered = true)
            where T : class;

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
        Task<IEnumerable<T>> GetSetAsync<T>(Expression<Func<T, bool>> predicate, SQLSortSet sort, int limitFrom, int limitTo, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Gets count by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        int Count<T>(Expression<Func<T, bool>> predicate, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class;

        /// <summary>
        /// Gets count by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<int> CountAsync<T>(Expression<Func<T, bool>> predicate, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default)
            where T : class;

        /// <summary>
        /// Gets count by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="filters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        int Count<T>(Expression<Func<T, bool>> predicate, ISQLPredicate[] filters = null) where T : class;

        /// <summary>
        /// Gets count by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<int> CountAsync<T>(Expression<Func<T, bool>> predicate, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
    }
}