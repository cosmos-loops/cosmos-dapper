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
        #region Get set by predicate

        /// <summary>
        /// Get set
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
        public IEnumerable<T> GetSet<T>(
            IDbConnection connection,
            object predicate,
            SQLSortSet sort, int limitFrom, int limitTo,
            IDbTransaction transaction,
            ISQLPredicate[] filters = null,
            bool buffered = true) where T : class
        {
            var classMap = GetClassMap<T>();
            var where = GetPredicate(classMap, predicate).Join(filters);
            return ExecuteQuerySetCommand<T>(connection, classMap, where, sort, limitFrom, limitTo, transaction, Options.Timeout, buffered);
        }

        /// <summary>
        /// Get set async
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
        public Task<IEnumerable<T>> GetSetAsync<T>(
            IDbConnection connection,
            object predicate,
            SQLSortSet sort, int limitFrom, int limitTo,
            IDbTransaction transaction,
            ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class
        {
            var classMap = GetClassMap<T>();
            var where = GetPredicate(classMap, predicate).Join(filters);
            return ExecuteQuerySetCommandAsync<T>(connection, classMap, where, sort, limitFrom, limitTo, transaction, Options.Timeout, cancellationToken);
        }

        #endregion

        #region Get set by expression

        /// <summary>
        /// Get set
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
        public IEnumerable<T> GetSet<T>(
            IDbConnection connection,
            Expression<Func<T, bool>> predicate,
            SQLSortSet sort, int limitFrom, int limitTo,
            IDbTransaction transaction,
            ISQLPredicate[] filters = null, bool buffered = true) where T : class
        {
            var classMap = GetClassMap<T>();
            var where = ConvertToPredicate(predicate).Join(filters);
            return ExecuteQuerySetCommand<T>(connection, classMap, where, sort, limitFrom, limitTo, transaction, Options.Timeout, buffered);
        }

        /// <summary>
        /// Get set async
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
        public Task<IEnumerable<T>> GetSetAsync<T>(
            IDbConnection connection,
            Expression<Func<T, bool>> predicate,
            SQLSortSet sort, int limitFrom, int limitTo,
            IDbTransaction transaction,
            ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class
        {
            var classMap = GetClassMap<T>();
            var where = ConvertToPredicate(predicate).Join(filters);
            return ExecuteQuerySetCommandAsync<T>(connection, classMap, where, sort, limitFrom, limitTo, transaction, Options.Timeout, cancellationToken);
        }

        #endregion

        #region internal helpers

        /// <summary>
        /// Execute command to query a set
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="classMap"></param>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="firstResult"></param>
        /// <param name="maxResults"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected IEnumerable<T> ExecuteQuerySetCommand<T>(IDbConnection connection, IClassMap classMap, ISQLPredicate predicate, SQLSortSet sort, int firstResult, int maxResults,
            IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            var sql = SQLGenerator.SelectSet(classMap, predicate, sort, firstResult, maxResults, new Dictionary<string, object>());
            var cmd = sql.ToSQLCommand(transaction, commandTimeout, commandFlags: buffered ? CommandFlags.Buffered : CommandFlags.None);
            return connection.Query<T>(cmd);
        }

        /// <summary>
        /// Execute command to query a set async
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="classMap"></param>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="firstResult"></param>
        /// <param name="maxResults"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected async Task<IEnumerable<T>> ExecuteQuerySetCommandAsync<T>(IDbConnection connection, IClassMap classMap, ISQLPredicate predicate, SQLSortSet sort, int firstResult,
            int maxResults, IDbTransaction transaction, int? commandTimeout, CancellationToken cancellationToken) where T : class
        {
            var sql = SQLGenerator.SelectSet(classMap, predicate, sort, firstResult, maxResults, new Dictionary<string, object>());
            var cmd = sql.ToSQLCommand(transaction, commandTimeout, cancellationToken: cancellationToken);
            return await connection.QueryAsync<T>(cmd).ConfigureAwait(false);
        }

        #endregion
    }
}