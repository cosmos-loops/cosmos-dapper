using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
        #region Count by predicate

        /// <summary>
        /// Count
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public int Count<T>(
            IDbConnection connection,
            object predicate,
            IDbTransaction transaction,
            ISQLPredicate[] filters = null) where T : class
        {
            var classMap = GetClassMap<T>();
            var where = GetPredicate(classMap, predicate).Join(filters);
            var sql = GetCountSql(classMap, where);
            var cmd = sql.ToSQLCommand(transaction, Options.Timeout);
            return (int) connection.Query<dynamic>(cmd).Single().Total;
        }

        /// <summary>
        /// Count async
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<int> CountAsync<T>(
            IDbConnection connection,
            object predicate = null,
            IDbTransaction transaction = null,
            ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default)
            where T : class
        {
            var classMap = GetClassMap<T>();
            var where = GetPredicate(classMap, predicate).Join(filters);
            var sql = GetCountSql(classMap, where);
            var cmd = sql.ToSQLCommand(transaction, Options.Timeout, cancellationToken: cancellationToken);
            return (int) (await connection.QueryAsync(cmd)).Single().Total;
        }

        #endregion

        #region Count by expression

        /// <summary>
        /// Count
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public int Count<T>(
            IDbConnection connection, Expression<Func<T, bool>> predicate,
            IDbTransaction transaction,
            ISQLPredicate[] filters = null) where T : class
        {
            var classMap = GetClassMap<T>();
            var where = ConvertToPredicate(predicate).Join(filters);
            var sql = GetCountSql(classMap, where);
            var cmd = sql.ToSQLCommand(transaction, Options.Timeout);
            return (int) connection.Query<dynamic>(cmd).Single().Total;
        }

        /// <summary>
        /// Count async
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<int> CountAsync<T>(
            IDbConnection connection,
            Expression<Func<T, bool>> predicate,
            IDbTransaction transaction,
            ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class
        {
            var classMap = GetClassMap<T>();
            var where = ConvertToPredicate(predicate).Join(filters);
            var sql = GetCountSql(classMap, where);
            var cmd = sql.ToSQLCommand(transaction, Options.Timeout, cancellationToken: cancellationToken);
            return (int) (await connection.QueryAsync<dynamic>(cmd)).Single().Total;
        }

        #endregion

        #region internal helper

        private SQLConvertResult GetCountSql(IClassMap classMap, ISQLPredicate predicate)
        {
            return SQLGenerator.Count(classMap, predicate, new Dictionary<string, object>());
        }

        #endregion
    }
}