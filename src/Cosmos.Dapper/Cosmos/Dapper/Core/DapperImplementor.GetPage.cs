using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Dapper.Mapper;
using Cosmos.Data.Statements;
using Dapper;

namespace Cosmos.Dapper.Core
{
    public partial class DapperImplementor
    {
        #region Get Page by predicate

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
        public IEnumerable<T> GetPage<T>(
            IDbConnection connection,
            object predicate, SQLSortSet sort, int pageNumber, int pageSize,
            IDbTransaction transaction,
            ISQLPredicate[] filters = null,
            bool buffered = true)
            where T : class
        {
            var classMap = GetClassMap<T>();
            var where = GetPredicate(classMap, predicate).Join(filters);
            return ExecuteQueryPageCommand<T>(connection, classMap, where, sort, pageNumber, pageSize, transaction, Options.Timeout, buffered);
        }

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
        public Task<IEnumerable<T>> GetPageAsync<T>(
            IDbConnection connection,
            object predicate, SQLSortSet sort, int pageNumber, int pageSize,
            IDbTransaction transaction,
            ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class
        {
            var classMap = GetClassMap<T>();
            var where = GetPredicate(classMap, predicate).Join(filters);
            return ExecuteQueryPageCommandAsync<T>(connection, classMap, where, sort, pageNumber, pageSize, transaction, Options.Timeout, cancellationToken);
        }

        #endregion

        #region Get page by expression

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
        public IEnumerable<T> GetPage<T>(
            IDbConnection connection,
            Expression<Func<T, bool>> predicate,
            SQLSortSet sort, int pageNumber, int pageSize,
            IDbTransaction transaction,
            ISQLPredicate[] filters = null,
            bool buffered = true)
            where T : class
        {
            var classMap = GetClassMap<T>();
            var where = ConvertToPredicate(predicate).Join(filters);
            return ExecuteQueryPageCommand<T>(connection, classMap, where, sort, pageNumber, pageSize, transaction, Options.Timeout, buffered);
        }

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
        public Task<IEnumerable<T>> GetPageAsync<T>(
            IDbConnection connection,
            Expression<Func<T, bool>> predicate,
            SQLSortSet sort, int pageNumber, int pageSize,
            IDbTransaction transaction,
            ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class
        {
            var classMap = GetClassMap<T>();
            var where = ConvertToPredicate(predicate).Join(filters);
            return ExecuteQueryPageCommandAsync<T>(connection, classMap, where, sort, pageNumber, pageSize, transaction, Options.Timeout, cancellationToken);
        }

        #endregion

        #region internal helpers

        /// <summary>
        /// Execute command to query page
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="classMap"></param>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="resultsPerPage"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected IEnumerable<T> ExecuteQueryPageCommand<T>(IDbConnection connection, IClassMap classMap, ISQLPredicate predicate, SQLSortSet sort, int page, int resultsPerPage,
            IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            var sql = SQLGenerator.SelectPaged(classMap, predicate, sort, page, resultsPerPage, new Dictionary<string, object>());
            var cmd = sql.ToSQLCommand(transaction, commandTimeout, commandFlags: buffered ? CommandFlags.Buffered : CommandFlags.None);
            return connection.Query<T>(cmd);
        }

        /// <summary>
        /// Execute command to query page async
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="classMap"></param>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="resultsPerPage"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected async Task<IEnumerable<T>> ExecuteQueryPageCommandAsync<T>(IDbConnection connection, IClassMap classMap, ISQLPredicate predicate, SQLSortSet sort, int page,
            int resultsPerPage, IDbTransaction transaction, int? commandTimeout, CancellationToken cancellationToken) where T : class
        {
            var sql = SQLGenerator.SelectPaged(classMap, predicate, sort, page, resultsPerPage, new Dictionary<string, object>());
            var cmd = sql.ToSQLCommand(transaction, commandTimeout, cancellationToken: cancellationToken);
            return await connection.QueryAsync<T>(cmd).ConfigureAwait(false);
        }

        #endregion
    }
}