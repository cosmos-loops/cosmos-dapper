using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Cosmos.Dapper;
using Cosmos.Dapper.Core;
using SqlKata.Compilers;
using static Dapper.SqlMapper;

namespace Cosmos.Data.SqlKata
{
    /// <summary>
    /// Query builder
    /// </summary>
    public partial class QueryBuilder : BaseQueryBuilder, IDisposable
    {
        private readonly bool _aloneMode;
        private readonly DapperOptions _options;
        private readonly IDapperConnector _connector;

        /// <summary>
        /// Create a new instance of <see cref="QueryBuilder" />
        /// </summary>
        /// <param name="connector"></param>
        /// <param name="compiler"></param>
        /// <param name="options"></param>
        /// <param name="aloneMode"></param>
        public QueryBuilder(IDapperConnector connector, Compiler compiler, DapperOptions options, bool aloneMode = true)
            : base(connector.Connection, compiler)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _connector = connector;
            _aloneMode = aloneMode;
        }

        /// <summary>
        /// Create a new instance of <see cref="QueryBuilder" />
        /// </summary>
        /// <param name="connector"></param>
        /// <param name="compiler"></param>
        /// <param name="options"></param>
        /// <param name="table"></param>
        /// <param name="aloneMode"></param>
        public QueryBuilder(IDapperConnector connector, Compiler compiler, DapperOptions options, string table, bool aloneMode = true)
            : base(connector.Connection, compiler, table)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _connector = connector;
            _aloneMode = aloneMode;
        }

        private IDbTransaction Transaction => _connector.TransactionWrapper.GetOrBegin(false);

        /// <summary>
        /// Execute...
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public int Execute(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Execute(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Execute...
        /// </summary>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public int Execute(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Execute(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Execute async...
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public Task<int> ExecuteAsync(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.ExecuteAsync(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Execute async...
        /// </summary>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public Task<int> ExecuteAsync(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.ExecuteAsync(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query...
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public IEnumerable<dynamic> Query(IDbTransaction transaction, bool buffered = true, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query(result.Sql, result.NamedBindings, transaction, buffered, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query...
        /// </summary>
        /// <param name="buffered"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public IEnumerable<dynamic> Query(bool buffered = true, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query(result.Sql, result.NamedBindings, Transaction, buffered, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query...
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(IDbTransaction transaction, bool buffered = true, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query<T>(result.Sql, result.NamedBindings, transaction, buffered, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query...
        /// </summary>
        /// <param name="buffered"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(bool buffered = true, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query<T>(result.Sql, result.NamedBindings, Transaction, buffered, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query...
        /// </summary>
        /// <param name="type"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public IEnumerable<object> Query(Type type, IDbTransaction transaction, bool buffered = true, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query(type, result.Sql, result.NamedBindings, transaction, buffered, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query...
        /// </summary>
        /// <param name="type"></param>
        /// <param name="buffered"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public IEnumerable<object> Query(Type type, bool buffered = true, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query(type, result.Sql, result.NamedBindings, Transaction, buffered, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query async...
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public Task<IEnumerable<dynamic>> QueryAsync(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query async...
        /// </summary>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public Task<IEnumerable<dynamic>> QueryAsync(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query async...
        /// </summary>
        /// <param name="type"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public Task<IEnumerable<object>> QueryAsync(Type type, IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync(type, result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query async...
        /// </summary>
        /// <param name="type"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public Task<IEnumerable<object>> QueryAsync(Type type, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync(type, result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query async...
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<IEnumerable<T>> QueryAsync<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync<T>(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query async...
        /// </summary>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<IEnumerable<T>> QueryAsync<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync<T>(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query first...
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public dynamic QueryFirst(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirst(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query first...
        /// </summary>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public dynamic QueryFirst(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirst(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query first...
        /// </summary>
        /// <param name="type"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public object QueryFirst(Type type, IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirst(type, result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query first...
        /// </summary>
        /// <param name="type"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public object QueryFirst(Type type, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirst(type, result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query first...
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T QueryFirst<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirst<T>(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query first...
        /// </summary>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T QueryFirst<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirst<T>(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query first async...
        /// </summary>
        /// <param name="type"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public Task<object> QueryFirstAsync(Type type, IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstAsync(type, result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query first async...
        /// </summary>
        /// <param name="type"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public Task<object> QueryFirstAsync(Type type, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstAsync(type, result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query first async...
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<T> QueryFirstAsync<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstAsync<T>(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query first async...
        /// </summary>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<T> QueryFirstAsync<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstAsync<T>(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query first or default...
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T QueryFirstOrDefault<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstOrDefault<T>(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query first or default...
        /// </summary>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T QueryFirstOrDefault<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstOrDefault<T>(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query first or default...
        /// </summary>
        /// <param name="type"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public object QueryFirstOrDefault(Type type, IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstOrDefault(type, result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query first or default...
        /// </summary>
        /// <param name="type"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public object QueryFirstOrDefault(Type type, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstOrDefault(type, result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query first or default...
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public dynamic QueryFirstOrDefault(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstOrDefault(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query first or default...
        /// </summary>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public dynamic QueryFirstOrDefault(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstOrDefault(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query first or default async...
        /// </summary>
        /// <param name="type"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public Task<object> QueryFirstOrDefaultAsync(Type type, IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstOrDefaultAsync(type, result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query first or default async...
        /// </summary>
        /// <param name="type"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public Task<object> QueryFirstOrDefaultAsync(Type type, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstOrDefaultAsync(type, result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query first or default async...
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<T> QueryFirstOrDefaultAsync<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstOrDefaultAsync<T>(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query first or default async...
        /// </summary>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<T> QueryFirstOrDefaultAsync<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryFirstOrDefaultAsync<T>(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query multiple...
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public GridReader QueryMultiple(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryMultiple(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query multiple...
        /// </summary>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public GridReader QueryMultiple(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryMultiple(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query multiple async...
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public Task<GridReader> QueryMultipleAsync(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryMultipleAsync(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query multiple async...
        /// </summary>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public Task<GridReader> QueryMultipleAsync(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryMultipleAsync(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query single...
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public dynamic QuerySingle(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QuerySingle(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query single...
        /// </summary>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public dynamic QuerySingle(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QuerySingle(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query single...
        /// </summary>
        /// <param name="type"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public object QuerySingle(Type type, IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QuerySingle(type, result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query single...
        /// </summary>
        /// <param name="type"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public object QuerySingle(Type type, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QuerySingle(type, result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query single...
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T QuerySingle<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QuerySingle<T>(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query single...
        /// </summary>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T QuerySingle<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QuerySingle<T>(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query single async...
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<T> QuerySingleAsync<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QuerySingleAsync<T>(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query single async...
        /// </summary>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<T> QuerySingleAsync<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QuerySingleAsync<T>(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query single async...
        /// </summary>
        /// <param name="type"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public Task<object> QuerySingleAsync(Type type, IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QuerySingleAsync(type, result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query single async...
        /// </summary>
        /// <param name="type"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public Task<object> QuerySingleAsync(Type type, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QuerySingleAsync(type, result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query single or default...
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T QuerySingleOrDefault<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QuerySingleOrDefault<T>(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query single or default...
        /// </summary>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T QuerySingleOrDefault<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QuerySingleOrDefault<T>(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query single or default async...
        /// </summary>
        /// <param name="type"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public Task<dynamic> QuerySingleOrDefaultAsync(Type type, IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QuerySingleOrDefaultAsync(type, result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query single or default async...
        /// </summary>
        /// <param name="type"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public Task<dynamic> QuerySingleOrDefaultAsync(Type type, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QuerySingleOrDefaultAsync(type, result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query single or default async...
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<T> QuerySingleOrDefaultAsync<T>(IDbTransaction transaction, CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QuerySingleOrDefaultAsync<T>(result.Sql, result.NamedBindings, transaction, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query single or default async...
        /// </summary>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<T> QuerySingleOrDefaultAsync<T>(CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QuerySingleOrDefaultAsync<T>(result.Sql, result.NamedBindings, Transaction, _options.Timeout, commandType);
        }

        #region Dispose

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (_aloneMode)
            {
                _connection?.Dispose();
                _connection = null;
            }

            _compiler = null;
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}