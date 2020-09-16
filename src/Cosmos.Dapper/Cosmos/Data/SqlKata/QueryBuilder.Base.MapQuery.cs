using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Cosmos.Data.SqlKata
{
    public partial class QueryBuilder
    {
        /// <summary>
        /// Query...
        /// </summary>
        /// <param name="types"></param>
        /// <param name="map"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public IEnumerable<TReturn> Query<TReturn>(Type[] types, Func<object[], TReturn> map, IDbTransaction transaction,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query(result.Sql, types, map, result.NamedBindings, transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query...
        /// </summary>
        /// <param name="types"></param>
        /// <param name="map"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public IEnumerable<TReturn> Query<TReturn>(Type[] types, Func<object[], TReturn> map,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query(result.Sql, types, map, result.NamedBindings, Transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query...
        /// </summary>
        /// <param name="map"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public IEnumerable<TReturn> Query<T1, T2, TReturn>(Func<T1, T2, TReturn> map, IDbTransaction transaction,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query(result.Sql, map, result.NamedBindings, transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query...
        /// </summary>
        /// <param name="map"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public IEnumerable<TReturn> Query<T1, T2, TReturn>(Func<T1, T2, TReturn> map,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query(result.Sql, map, result.NamedBindings, Transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query...
        /// </summary>
        /// <param name="map"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public IEnumerable<TReturn> Query<T1, T2, T3, TReturn>(Func<T1, T2, T3, TReturn> map, IDbTransaction transaction,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query(result.Sql, map, result.NamedBindings, transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query...
        /// </summary>
        /// <param name="map"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public IEnumerable<TReturn> Query<T1, T2, T3, TReturn>(Func<T1, T2, T3, TReturn> map,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query(result.Sql, map, result.NamedBindings, Transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query...
        /// </summary>
        /// <param name="map"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public IEnumerable<TReturn> Query<T1, T2, T3, T4, TReturn>(Func<T1, T2, T3, T4, TReturn> map, IDbTransaction transaction,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query(result.Sql, map, result.NamedBindings, transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query...
        /// </summary>
        /// <param name="map"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public IEnumerable<TReturn> Query<T1, T2, T3, T4, TReturn>(Func<T1, T2, T3, T4, TReturn> map,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query(result.Sql, map, result.NamedBindings, Transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query...
        /// </summary>
        /// <param name="map"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public IEnumerable<TReturn> Query<T1, T2, T3, T4, T5, TReturn>(Func<T1, T2, T3, T4, T5, TReturn> map, IDbTransaction transaction,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query(result.Sql, map, result.NamedBindings, transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query...
        /// </summary>
        /// <param name="map"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public IEnumerable<TReturn> Query<T1, T2, T3, T4, T5, TReturn>(Func<T1, T2, T3, T4, T5, TReturn> map,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query(result.Sql, map, result.NamedBindings, Transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query...
        /// </summary>
        /// <param name="map"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public IEnumerable<TReturn> Query<T1, T2, T3, T4, T5, T6, TReturn>(Func<T1, T2, T3, T4, T5, T6, TReturn> map, IDbTransaction transaction,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query(result.Sql, map, result.NamedBindings, transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query...
        /// </summary>
        /// <param name="map"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public IEnumerable<TReturn> Query<T1, T2, T3, T4, T5, T6, TReturn>(Func<T1, T2, T3, T4, T5, T6, TReturn> map,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query(result.Sql, map, result.NamedBindings, Transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query...
        /// </summary>
        /// <param name="map"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public IEnumerable<TReturn> Query<T1, T2, T3, T4, T5, T6, T7, TReturn>(Func<T1, T2, T3, T4, T5, T6, T7, TReturn> map, IDbTransaction transaction,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query(result.Sql, map, result.NamedBindings, transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query...
        /// </summary>
        /// <param name="map"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public IEnumerable<TReturn> Query<T1, T2, T3, T4, T5, T6, T7, TReturn>(Func<T1, T2, T3, T4, T5, T6, T7, TReturn> map,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.Query(result.Sql, map, result.NamedBindings, Transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query async...
        /// </summary>
        /// <param name="types"></param>
        /// <param name="map"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public Task<IEnumerable<TReturn>> QueryAsync<TReturn>(Type[] types, Func<object[], TReturn> map, IDbTransaction transaction,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync(result.Sql, types, map, result.NamedBindings, transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query async...
        /// </summary>
        /// <param name="types"></param>
        /// <param name="map"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public Task<IEnumerable<TReturn>> QueryAsync<TReturn>(Type[] types, Func<object[], TReturn> map,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync(result.Sql, types, map, result.NamedBindings, Transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query async...
        /// </summary>
        /// <param name="map"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public Task<IEnumerable<TReturn>> QueryAsync<T1, T2, TReturn>(Func<T1, T2, TReturn> map, IDbTransaction transaction,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync(result.Sql, map, result.NamedBindings, transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query async...
        /// </summary>
        /// <param name="map"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public Task<IEnumerable<TReturn>> QueryAsync<T1, T2, TReturn>(Func<T1, T2, TReturn> map,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync(result.Sql, map, result.NamedBindings, Transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query async...
        /// </summary>
        /// <param name="map"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, TReturn>(Func<T1, T2, T3, TReturn> map, IDbTransaction transaction,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync(result.Sql, map, result.NamedBindings, transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query async...
        /// </summary>
        /// <param name="map"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, TReturn>(Func<T1, T2, T3, TReturn> map,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync(result.Sql, map, result.NamedBindings, Transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query async...
        /// </summary>
        /// <param name="map"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, TReturn>(Func<T1, T2, T3, T4, TReturn> map, IDbTransaction transaction,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync(result.Sql, map, result.NamedBindings, transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query async...
        /// </summary>
        /// <param name="map"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, TReturn>(Func<T1, T2, T3, T4, TReturn> map,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync(result.Sql, map, result.NamedBindings, Transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query async...
        /// </summary>
        /// <param name="map"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, T5, TReturn>(Func<T1, T2, T3, T4, T5, TReturn> map, IDbTransaction transaction,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync(result.Sql, map, result.NamedBindings, transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query async...
        /// </summary>
        /// <param name="map"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, T5, TReturn>(Func<T1, T2, T3, T4, T5, TReturn> map,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync(result.Sql, map, result.NamedBindings, Transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query async...
        /// </summary>
        /// <param name="map"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, T5, T6, TReturn>(Func<T1, T2, T3, T4, T5, T6, TReturn> map, IDbTransaction transaction,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync(result.Sql, map, result.NamedBindings, transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query async...
        /// </summary>
        /// <param name="map"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, T5, T6, TReturn>(Func<T1, T2, T3, T4, T5, T6, TReturn> map,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync(result.Sql, map, result.NamedBindings, Transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query async...
        /// </summary>
        /// <param name="map"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, T5, T6, T7, TReturn>(Func<T1, T2, T3, T4, T5, T6, T7, TReturn> map, IDbTransaction transaction,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync(result.Sql, map, result.NamedBindings, transaction, buffered, splitOn, _options.Timeout, commandType);
        }

        /// <summary>
        /// Query async...
        /// </summary>
        /// <param name="map"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, T5, T6, T7, TReturn>(Func<T1, T2, T3, T4, T5, T6, T7, TReturn> map,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            var result = Compiler();
            return _connection.QueryAsync(result.Sql, map, result.NamedBindings, Transaction, buffered, splitOn, _options.Timeout, commandType);
        }
    }
}