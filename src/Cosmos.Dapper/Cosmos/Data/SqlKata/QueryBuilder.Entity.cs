using Dapper;
using SqlKata.Compilers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Cosmos.Dapper;
using Cosmos.Dapper.Core;
using SqlKata;

namespace Cosmos.Data.SqlKata
{
    /// <summary>
    /// Entity Query Builder
    /// </summary>
    public class EntityQueryBuilder : BaseQueryBuilder, IDisposable
    {
        private readonly bool _aloneMode;
        private readonly DapperOptions _options;
        private readonly IDapperConnector _connector;

        /// <summary>
        /// Create a new instance of <see cref="EntityQueryBuilder" />
        /// </summary>
        /// <param name="connector"></param>
        /// <param name="compiler"></param>
        /// <param name="options"></param>
        /// <param name="aloneMode"></param>
        public EntityQueryBuilder(IDapperConnector connector, Compiler compiler, DapperOptions options, bool aloneMode = true)
            : base(connector.Connection, compiler)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _connector = connector;
            _aloneMode = aloneMode;
        }

        /// <summary>
        /// Create a new instance of <see cref="EntityQueryBuilder" />
        /// </summary>
        /// <param name="connector"></param>
        /// <param name="compiler"></param>
        /// <param name="table"></param>
        /// <param name="options"></param>
        /// <param name="aloneMode"></param>
        public EntityQueryBuilder(IDapperConnector connector, Compiler compiler, string table, DapperOptions options, bool aloneMode = true)
            : base(connector.Connection, compiler, table)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _connector = connector;
            _aloneMode = aloneMode;
        }

        private IDbTransaction Transaction => _connector.TransactionWrapper.GetOrBegin(false);

        /// <summary>
        /// Find one...
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T FindOne<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstOrDefault<T>(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Find one...
        /// </summary>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T FindOne<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstOrDefault<T>(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Find one async...
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> FindOneAsync<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return await _connection.QueryFirstOrDefaultAsync<T>(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Find one async...
        /// </summary>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> FindOneAsync<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            return await _connection.QueryFirstOrDefaultAsync<T>(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Unique result to int...
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public int UniqueResultToInt(IDbTransaction transaction, CommandType? commandType = null)
            => UniqueResult<int>(transaction, commandType);

        /// <summary>
        /// Unique result to int...
        /// </summary>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public int UniqueResultToInt(CommandType? commandType = null)
            => UniqueResult<int>(commandType);

        /// <summary>
        /// Unique result to int async...
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<int> UniqueResultToIntAsync(IDbTransaction transaction, CommandType? commandType = null)
            => await UniqueResultAsync<int>(transaction, commandType);

        /// <summary>
        /// Unique result to int async...
        /// </summary>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<int> UniqueResultToIntAsync(CommandType? commandType = null)
            => await UniqueResultAsync<int>(commandType);

        /// <summary>
        /// Unique result to long...
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public long UniqueResultToLong(IDbTransaction transaction, CommandType? commandType = null)
            => UniqueResult<long>(transaction, commandType);

        /// <summary>
        /// Unique result to long...
        /// </summary>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public long UniqueResultToLong(CommandType? commandType = null)
            => UniqueResult<long>(commandType);

        /// <summary>
        /// Unique result to long async...
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<long> UniqueResultToLongAsync(IDbTransaction transaction, CommandType? commandType = null)
            => await UniqueResultAsync<long>(transaction, commandType);

        /// <summary>
        /// Unique result to long async...
        /// </summary>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<long> UniqueResultToLongAsync(CommandType? commandType = null)
            => await UniqueResultAsync<long>(commandType);

        /// <summary>
        /// Unique result...
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T UniqueResult<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstOrDefault<T>(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Unique result...
        /// </summary>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T UniqueResult<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstOrDefault<T>(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Unique result async...
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> UniqueResultAsync<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return await _connection.QueryFirstOrDefaultAsync<T>(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Unique result async...
        /// </summary>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> UniqueResultAsync<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            return await _connection.QueryFirstOrDefaultAsync<T>(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// List...
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> List<T>(IDbTransaction transaction, bool buffered = true, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query<T>(result.Sql, result.NamedBindings, transaction, buffered, _options.Timeout, commandType);
        }

        /// <summary>
        /// List...
        /// </summary>
        /// <param name="buffered"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> List<T>(bool buffered = true, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query<T>(result.Sql, result.NamedBindings, Transaction, buffered, _options.Timeout, commandType);
        }

        /// <summary>
        /// List async...
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<T>> ListAsync<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return await _connection.QueryAsync<T>(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// List async...
        /// </summary>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<T>> ListAsync<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            return await _connection.QueryAsync<T>(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Save update
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public bool SaveUpdate(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Execute(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType) == 1;
        }

        /// <summary>
        /// Save update
        /// </summary>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public bool SaveUpdate(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Execute(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType) == 1;
        }

        /// <summary>
        /// Save update async
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<bool> SaveUpdateAsync(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return await _connection.ExecuteAsync(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType) == 1;
        }

        /// <summary>
        /// Save update async
        /// </summary>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<bool> SaveUpdateAsync(CommandType? commandType = null)
        {
            var result = Compiler();
            return await _connection.ExecuteAsync(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType) == 1;
        }

        /// <summary>
        /// Save insert
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public bool SaveInsert(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Execute(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType) == 1;
        }

        /// <summary>
        /// Save insert
        /// </summary>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public bool SaveInsert(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Execute(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType) == 1;
        }

        /// <summary>
        /// Save insert async
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<bool> SaveInsertAsync(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return await _connection.ExecuteAsync(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType) == 1;
        }

        /// <summary>
        /// Save insert async
        /// </summary>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<bool> SaveInsertAsync(CommandType? commandType = null)
        {
            var result = Compiler();
            return await _connection.ExecuteAsync(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType) == 1;
        }

        #region ForMysqlInserted

        /// <summary>
        /// Save insert for mysql
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T SaveInsertForMysql<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstOrDefault<T>(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Save insert for mysql
        /// </summary>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T SaveInsertForMysql<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstOrDefault<T>(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Save insert for mysql async
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> SaveInsertForMysqlAsync<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return await _connection.QueryFirstOrDefaultAsync<T>(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Save insert for mysql async
        /// </summary>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> SaveInsertForMysqlAsync<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            return await _connection.QueryFirstOrDefaultAsync<T>(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        #endregion

        #region ForPostgresInserted

        /// <summary>
        /// Save insert for postgresql
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T SaveInsertForPostgreSql<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstOrDefault<T>(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Save insert for postgresql
        /// </summary>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T SaveInsertForPostgreSql<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstOrDefault<T>(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        /// <summary>
        ///  Save insert for postgresql async
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> SaveInsertForPostgreSqlAsync<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return await _connection.QueryFirstOrDefaultAsync<T>(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Save insert for postgresql async
        /// </summary>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> SaveInsertForPostgreSqlAsync<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            return await _connection.QueryFirstOrDefaultAsync<T>(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        #endregion

        #region ForSqlServerInserted

        /// <summary>
        /// Save insert for Microsoft SQL Server
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T SaveInsertForSqlServer<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            string sql = ReplaceSqlToGuid<T>(result);
            return _connection.QueryFirstOrDefault<T>(sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Save insert for Microsoft SQL Server
        /// </summary>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T SaveInsertForSqlServer<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            string sql = ReplaceSqlToGuid<T>(result);
            return _connection.QueryFirstOrDefault<T>(sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Save insert for Microsoft SQL Server async
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> SaveInsertForSqlServerAsync<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            string sql = ReplaceSqlToGuid<T>(result);
            return await _connection.QueryFirstOrDefaultAsync<T>(sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Save insert for Microsoft SQL Server async
        /// </summary>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> SaveInsertForSqlServerAsync<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            string sql = ReplaceSqlToGuid<T>(result);
            return await _connection.QueryFirstOrDefaultAsync<T>(sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Replace sql to guid
        /// </summary>
        /// <param name="result"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        internal protected string ReplaceSqlToGuid<T>(SqlResult result)
        {
            if (typeof(T) == typeof(Guid))
            {
                int index = result.Sql.IndexOf(" VALUES", StringComparison.Ordinal);
                if (index > -1)
                {
                    return result.Sql.Insert(index, " OUTPUT INSERTED.Id");
                }
            }

            return result.Sql;
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (_aloneMode)
            {
                _connection = null;
            }

            _compiler = null;
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}