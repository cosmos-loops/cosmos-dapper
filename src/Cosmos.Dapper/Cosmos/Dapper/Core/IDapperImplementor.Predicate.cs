using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Data.Statements;
using Dapper;

// ReSharper disable InconsistentNaming

namespace Cosmos.Dapper.Core
{
    public partial interface IDapperImplementor
    {
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool Delete<T>(IDbConnection connection, object predicate, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class;

        /// <summary>
        /// Delete async
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> DeleteAsync<T>(IDbConnection connection, object predicate, IDbTransaction transaction, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Get one
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <param name="type"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetOne<T>(IDbConnection connection, object predicate, SQLSortSet sort, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true,
            DapperImplementor.QueryOneType type = DapperImplementor.QueryOneType.FirstOrDefault) where T : class;

        /// <summary>
        /// Get one async
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="type"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> GetOneAsync<T>(IDbConnection connection, object predicate, SQLSortSet sort, IDbTransaction transaction, ISQLPredicate[] filters = null,
            DapperImplementor.QueryOneType type = DapperImplementor.QueryOneType.FirstOrDefault, CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Get list
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetList<T>(IDbConnection connection, object predicate, SQLSortSet sort, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true)
            where T : class;

        /// <summary>
        /// Get list async
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<IEnumerable<T>> GetListAsync<T>(IDbConnection connection, object predicate, SQLSortSet sort, IDbTransaction transaction, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Get page
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetPage<T>(IDbConnection connection, object predicate, SQLSortSet sort, int pageNumber, int pageSize, IDbTransaction transaction,
            ISQLPredicate[] filters = null, bool buffered = true) where T : class;

        /// <summary>
        /// Get page async
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<IEnumerable<T>> GetPageAsync<T>(IDbConnection connection, object predicate, SQLSortSet sort, int pageNumber, int pageSize, IDbTransaction transaction,
            ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Get a set
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="limitFrom"></param>
        /// <param name="limitTo"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetSet<T>(IDbConnection connection, object predicate, SQLSortSet sort, int limitFrom, int limitTo, IDbTransaction transaction,
            ISQLPredicate[] filters = null, bool buffered = true) where T : class;

        /// <summary>
        /// Get a set async
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="limitFrom"></param>
        /// <param name="limitTo"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<IEnumerable<T>> GetSetAsync<T>(IDbConnection connection, object predicate, SQLSortSet sort, int limitFrom, int limitTo, IDbTransaction transaction,
            ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Get count
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        int Count<T>(IDbConnection connection, object predicate, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class;

        /// <summary>
        /// Get count async
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<int> CountAsync<T>(IDbConnection connection, object predicate, IDbTransaction transaction, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Get multiple
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        IMultipleResultReader GetMultiple(IDbConnection connection, SQLMultiplePredicate predicate, IDbTransaction transaction);

        /// <summary>
        /// Get multiple async
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IMultipleResultReader> GetMultipleAsync(IDbConnection connection, SQLMultiplePredicate predicate, IDbTransaction transaction,
            CancellationToken cancellationToken = default);
    }
}