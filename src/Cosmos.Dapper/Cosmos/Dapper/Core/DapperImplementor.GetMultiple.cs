using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Dapper.Mapper;
using Cosmos.Data.Statements;
using Dapper;

namespace Cosmos.Dapper.Core
{
    public partial class DapperImplementor
    {
        #region Get multiple result by predicate

        /// <summary>
        /// Get multiple
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public IMultipleResultReader GetMultiple(IDbConnection connection, SQLMultiplePredicate predicate, IDbTransaction transaction)
        {
            if (SQLGenerator.SupportsMultipleStatements())
                return GetMultipleByBatch(connection, predicate, transaction, Options.Timeout);
            return GetMultipleBySequence(connection, predicate, transaction, Options.Timeout);
        }

        /// <summary>
        /// Get multiple async
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IMultipleResultReader> GetMultipleAsync(IDbConnection connection, SQLMultiplePredicate predicate, IDbTransaction transaction,
            CancellationToken cancellationToken = default)
        {
            if (SQLGenerator.SupportsMultipleStatements())
                return await GetMultipleByBatchAsync(connection, predicate, transaction, Options.Timeout, cancellationToken);
            return await GetMultipleBySequenceAsync(connection, predicate, transaction, Options.Timeout, cancellationToken);
        }

        #endregion

        #region internal implementations

        /// <summary>
        /// Get multiple by batch
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        protected GridReaderResultReader GetMultipleByBatch(IDbConnection connection, SQLMultiplePredicate predicate,
            IDbTransaction transaction, int? commandTimeout)
        {
            var sql = new SQLConvertResult();

            foreach (var item in predicate.Items)
            {
                var classMap = GetClassMap(item.Type);
                var itemPredicate = GetMultiplePredicate(classMap, item);

                var tmp = SQLGenerator.Select(classMap, itemPredicate, item.SortSet, sql.WritableParameters);
                sql.AppendSqlLine(tmp.Sql);
                sql.AppendSqlLine(SQLGenerator.Configuration.Dialect.BatchSeperator);
            }

            var grid = connection.QueryMultiple(sql.ToSQLCommand(transaction, commandTimeout));
            return new GridReaderResultReader(grid);
        }

        /// <summary>
        /// Get multiple by batch async
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected async Task<GridReaderResultReader> GetMultipleByBatchAsync(IDbConnection connection, SQLMultiplePredicate predicate,
            IDbTransaction transaction, int? commandTimeout, CancellationToken cancellationToken)
        {
            var sql = new SQLConvertResult();

            foreach (var item in predicate.Items)
            {
                var classMap = GetClassMap(item.Type);
                var itemPredicate = GetMultiplePredicate(classMap, item);

                var tmp = SQLGenerator.Select(classMap, itemPredicate, item.SortSet, sql.WritableParameters);
                sql.AppendSqlLine(tmp.Sql);
                sql.AppendSqlLine(SQLGenerator.Configuration.Dialect.BatchSeperator);
            }

            var cmd = sql.ToSQLCommand(transaction, commandTimeout, cancellationToken: cancellationToken);
            var grid = await connection.QueryMultipleAsync(cmd).ConfigureAwait(false);
            return new GridReaderResultReader(grid);
        }

        /// <summary>
        /// Get multiple by sequence
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        protected SequenceReaderResultReader GetMultipleBySequence(IDbConnection connection, SQLMultiplePredicate predicate,
            IDbTransaction transaction, int? commandTimeout)
        {
            var items = new List<SqlMapper.GridReader>();

            foreach (var item in predicate.Items)
            {
                var classMap = GetClassMap(item.Type);
                var itemPredicate = GetMultiplePredicate(classMap, item);

                var sql = SQLGenerator.Select(classMap, itemPredicate, item.SortSet, new Dictionary<string, object>());
                var queryRet = connection.QueryMultiple(sql.ToSQLCommand(transaction, commandTimeout));
                items.Add(queryRet);
            }

            return new SequenceReaderResultReader(items);
        }

        /// <summary>
        /// Get multiple by sequence async
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected async Task<SequenceReaderResultReader> GetMultipleBySequenceAsync(IDbConnection connection, SQLMultiplePredicate predicate,
            IDbTransaction transaction, int? commandTimeout, CancellationToken cancellationToken)
        {
            var items = new List<SqlMapper.GridReader>();

            foreach (var item in predicate.Items)
            {
                var classMap = GetClassMap(item.Type);
                var itemPredicate = GetMultiplePredicate(classMap, item);

                var sql = SQLGenerator.Select(classMap, itemPredicate, item.SortSet, new Dictionary<string, object>());
                var cmd = sql.ToSQLCommand(transaction, commandTimeout, cancellationToken: cancellationToken);
                var queryRet = await connection.QueryMultipleAsync(cmd).ConfigureAwait(false);
                items.Add(queryRet);
            }

            return new SequenceReaderResultReader(items);
        }

        #endregion

        #region internal helpers

        private ISQLPredicate GetMultiplePredicate(IClassMap classMap, SQLMultiplePredicate.SQLMultiplePredicateItem item)
        {
            var itemPredicate = item.Value as ISQLPredicate;

            if (itemPredicate == null && item.Value != null)
                itemPredicate = GetPredicate(classMap, item.Value);

            return itemPredicate;
        }

        #endregion
    }
}