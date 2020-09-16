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
        #region Get one entity by predicate

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
        public T GetOne<T>(
            IDbConnection connection,
            object predicate,
            SQLSortSet sort,
            IDbTransaction transaction,
            ISQLPredicate[] filters = null,
            bool buffered = true,
            QueryOneType type = QueryOneType.FirstOrDefault)
            where T : class
        {
            var classMap = GetClassMap<T>();
            var where = GetPredicate(classMap, predicate).Join(filters);
            return ExecuteQueryOneCommand<T>(connection, classMap, where, sort, transaction, Options.Timeout, buffered, type);
        }

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
        public Task<T> GetOneAsync<T>(
            IDbConnection connection,
            object predicate,
            SQLSortSet sort,
            IDbTransaction transaction,
            ISQLPredicate[] filters = null,
            QueryOneType type = QueryOneType.FirstOrDefault,
            CancellationToken cancellationToken = default) where T : class
        {
            var classMap = GetClassMap<T>();
            var where = GetPredicate(classMap, predicate).Join(filters);
            return ExecuteQueryOneCommandAsync<T>(connection, classMap, where, sort, transaction, Options.Timeout, cancellationToken, type);
        }

        #endregion

        #region Get one entity by expression

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
        public T GetOne<T>(
            IDbConnection connection,
            Expression<Func<T, bool>> predicate,
            SQLSortSet sort,
            IDbTransaction transaction,
            ISQLPredicate[] filters = null,
            bool buffered = true,
            QueryOneType type = QueryOneType.FirstOrDefault)
            where T : class
        {
            var classMap = GetClassMap<T>();
            var where = ConvertToPredicate(predicate).Join(filters);
            return ExecuteQueryOneCommand<T>(connection, classMap, where, sort, transaction, Options.Timeout, buffered, type);
        }

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
        public Task<T> GetOneAsync<T>(
            IDbConnection connection,
            Expression<Func<T, bool>> predicate,
            SQLSortSet sort, IDbTransaction transaction,
            ISQLPredicate[] filters = null,
            QueryOneType type = QueryOneType.FirstOrDefault,
            CancellationToken cancellationToken = default) where T : class
        {
            var classMap = GetClassMap<T>();
            var where = ConvertToPredicate(predicate).Join(filters);
            return ExecuteQueryOneCommandAsync<T>(connection, classMap, where, sort, transaction, Options.Timeout, cancellationToken, type);
        }

        #endregion

        #region internal helpers

        /// <summary>
        /// Execute command to query one
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="classMap"></param>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="buffered"></param>
        /// <param name="type"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        protected T ExecuteQueryOneCommand<T>(IDbConnection connection, IClassMap classMap, ISQLPredicate predicate, SQLSortSet sort,
            IDbTransaction transaction, int? commandTimeout, bool buffered, QueryOneType type) where T : class
        {
            var sql = SQLGenerator.Select(classMap, predicate, sort, new Dictionary<string, object>());
            var cmd = sql.ToSQLCommand(transaction, commandTimeout, commandFlags: buffered ? CommandFlags.Buffered : CommandFlags.None);
            switch (type)
            {
                case QueryOneType.First:
                    return connection.QueryFirst<T>(cmd);
                case QueryOneType.FirstOrDefault:
                    return connection.QueryFirstOrDefault<T>(cmd);
                case QueryOneType.Single:
                    return connection.QuerySingle<T>(cmd);
                case QueryOneType.SingleOrDefault:
                    return connection.QuerySingleOrDefault<T>(cmd);
                default:
                    throw new InvalidOperationException("Invalid operation type for dapper implementor.");
            }
        }

        /// <summary>
        /// Execute command to query one async
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="classMap"></param>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="type"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        protected async Task<T> ExecuteQueryOneCommandAsync<T>(IDbConnection connection, IClassMap classMap, ISQLPredicate predicate, SQLSortSet sort,
            IDbTransaction transaction, int? commandTimeout, CancellationToken cancellationToken, QueryOneType type) where T : class
        {
            var sql = SQLGenerator.Select(classMap, predicate, sort, new Dictionary<string, object>());
            var cmd = sql.ToSQLCommand(transaction, commandTimeout, cancellationToken: cancellationToken);
            switch (type)
            {
                case QueryOneType.First:
                    return await connection.QueryFirstAsync<T>(cmd);
                case QueryOneType.FirstOrDefault:
                    return await connection.QueryFirstOrDefaultAsync<T>(cmd);
                case QueryOneType.Single:
                    return await connection.QuerySingleAsync<T>(cmd);
                case QueryOneType.SingleOrDefault:
                    return await connection.QuerySingleOrDefaultAsync<T>(cmd);
                default:
                    throw new InvalidOperationException("Invalid operation type for dapper implementor.");
            }
        }

        /// <summary>
        /// Query one type
        /// </summary>
        public enum QueryOneType
        {
            /// <summary>
            /// First
            /// </summary>
            First,

            /// <summary>
            /// First or default
            /// </summary>
            FirstOrDefault,

            /// <summary>
            /// Single
            /// </summary>
            Single,

            /// <summary>
            /// Single or default
            /// </summary>
            SingleOrDefault
        }

        #endregion
    }
}