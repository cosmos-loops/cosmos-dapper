using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Cosmos.Dapper.Operations
{
    public partial class DapperQueryOperator
    {
        #region Query

        /// <summary>
        /// Query async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<IEnumerable<object>> QueryAsync(string sql, object param = null, CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QueryAsync(sql, param, Transaction, Options.Timeout, commandType).ConfigureAwait(false);
        }

        /// <summary>
        /// Query async
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<IEnumerable<object>> QueryAsync(CommandDefinition command)
        {
            await PrepareConnectionAndTransactionAsync(command.CancellationToken).ConfigureAwait(false);
            return await Connection.QueryAsync(InjectTransaction(command)).ConfigureAwait(false);
        }

        /// <summary>
        /// Query async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QueryAsync<T>(sql, param, Transaction, Options.Timeout, commandType).ConfigureAwait(false);
        }

        /// <summary>
        /// Query async
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<IEnumerable<object>> QueryAsync(Type type, string sql, object param = null, CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QueryAsync(type, sql, param, Transaction, Options.Timeout, commandType).ConfigureAwait(false);
        }

        /// <summary>
        /// Query async
        /// </summary>
        /// <param name="type"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<IEnumerable<object>> QueryAsync(Type type, CommandDefinition command)
        {
            await PrepareConnectionAndTransactionAsync(command.CancellationToken).ConfigureAwait(false);
            return await Connection.QueryAsync(type, InjectTransaction(command)).ConfigureAwait(false);
        }

        /// <summary>
        /// Query async
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<T>> QueryAsync<T>(CommandDefinition command)
        {
            await PrepareConnectionAndTransactionAsync(command.CancellationToken).ConfigureAwait(false);
            return await Connection.QueryAsync<T>(InjectTransaction(command)).ConfigureAwait(false);
        }

        #endregion

        #region multi-query

        /// <summary>
        /// Query async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="param"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, TReturn>(string sql, Func<T1, T2, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id",
            CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QueryAsync(sql, map, param, Transaction, buffered, splitOn, Options.Timeout, commandType).ConfigureAwait(false);
        }

        /// <summary>
        /// Query async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="param"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, TReturn>(string sql, Func<T1, T2, T3, TReturn> map, object param = null, bool buffered = true,
            string splitOn = "Id", CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QueryAsync(sql, map, param, Transaction, buffered, splitOn, Options.Timeout, commandType).ConfigureAwait(false);
        }

        /// <summary>
        /// Query async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="param"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, TReturn>(string sql, Func<T1, T2, T3, T4, TReturn> map, object param = null, bool buffered = true,
            string splitOn = "Id", CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QueryAsync(sql, map, param, Transaction, buffered, splitOn, Options.Timeout, commandType).ConfigureAwait(false);
        }

        /// <summary>
        /// Query async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="param"></param>
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
        public async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, T5, TReturn>(string sql, Func<T1, T2, T3, T4, T5, TReturn> map, object param = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QueryAsync(sql, map, param, Transaction, buffered, splitOn, Options.Timeout, commandType).ConfigureAwait(false);
        }

        /// <summary>
        /// Query async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="param"></param>
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
        public async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, T5, T6, TReturn>(string sql, Func<T1, T2, T3, T4, T5, T6, TReturn> map, object param = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QueryAsync(sql, map, param, Transaction, buffered, splitOn, Options.Timeout, commandType).ConfigureAwait(false);
        }

        /// <summary>
        /// Query async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="param"></param>
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
        public async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, T5, T6, T7, TReturn>(string sql, Func<T1, T2, T3, T4, T5, T6, T7, TReturn> map, object param = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QueryAsync(sql, map, param, Transaction, buffered, splitOn, Options.Timeout, commandType).ConfigureAwait(false);
        }

        /// <summary>
        /// Query async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="types"></param>
        /// <param name="map"></param>
        /// <param name="param"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<TReturn>> QueryAsync<TReturn>(string sql, Type[] types, Func<object[], TReturn> map, object param = null, bool buffered = true,
            string splitOn = "Id", CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QueryAsync(sql, types, map, param, Transaction, buffered, splitOn, Options.Timeout, commandType).ConfigureAwait(false);
        }

        /// <summary>
        /// Query async
        /// </summary>
        /// <param name="command"></param>
        /// <param name="map"></param>
        /// <param name="splitOn"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, TReturn>(CommandDefinition command, Func<T1, T2, TReturn> map, string splitOn = "Id")
        {
            await PrepareConnectionAndTransactionAsync(command.CancellationToken).ConfigureAwait(false);
            return await Connection.QueryAsync(InjectTransaction(command), map, splitOn).ConfigureAwait(false);
        }

        /// <summary>
        /// Query async
        /// </summary>
        /// <param name="command"></param>
        /// <param name="map"></param>
        /// <param name="splitOn"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, TReturn>(CommandDefinition command, Func<T1, T2, T3, TReturn> map, string splitOn = "Id")
        {
            await PrepareConnectionAndTransactionAsync(command.CancellationToken).ConfigureAwait(false);
            return await Connection.QueryAsync(InjectTransaction(command), map, splitOn).ConfigureAwait(false);
        }

        /// <summary>
        /// Query async
        /// </summary>
        /// <param name="command"></param>
        /// <param name="map"></param>
        /// <param name="splitOn"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, TReturn>(CommandDefinition command, Func<T1, T2, T3, T4, TReturn> map, string splitOn = "Id")
        {
            await PrepareConnectionAndTransactionAsync(command.CancellationToken).ConfigureAwait(false);
            return await Connection.QueryAsync(InjectTransaction(command), map, splitOn).ConfigureAwait(false);
        }

        /// <summary>
        /// Query async
        /// </summary>
        /// <param name="command"></param>
        /// <param name="map"></param>
        /// <param name="splitOn"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, T5, TReturn>(CommandDefinition command, Func<T1, T2, T3, T4, T5, TReturn> map, string splitOn = "Id")
        {
            await PrepareConnectionAndTransactionAsync(command.CancellationToken).ConfigureAwait(false);
            return await Connection.QueryAsync(InjectTransaction(command), map, splitOn).ConfigureAwait(false);
        }

        /// <summary>
        /// Query async
        /// </summary>
        /// <param name="command"></param>
        /// <param name="map"></param>
        /// <param name="splitOn"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, T5, T6, TReturn>(CommandDefinition command, Func<T1, T2, T3, T4, T5, T6, TReturn> map,
            string splitOn = "Id")
        {
            await PrepareConnectionAndTransactionAsync(command.CancellationToken).ConfigureAwait(false);
            return await Connection.QueryAsync(InjectTransaction(command), map, splitOn).ConfigureAwait(false);
        }

        /// <summary>
        /// Query async
        /// </summary>
        /// <param name="command"></param>
        /// <param name="map"></param>
        /// <param name="splitOn"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, T5, T6, T7, TReturn>(CommandDefinition command, Func<T1, T2, T3, T4, T5, T6, T7, TReturn> map,
            string splitOn = "Id")
        {
            await PrepareConnectionAndTransactionAsync(command.CancellationToken).ConfigureAwait(false);
            return await Connection.QueryAsync(InjectTransaction(command), map, splitOn).ConfigureAwait(false);
        }

        #endregion

        #region crosscut multi-query

        /// <summary>
        /// Query async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<T1>> QueryAsync<T1, T2>(string sql, dynamic param = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await MultiQueryExtensions.QueryAsync<T1, T2>(Connection, sql, param, Transaction, buffered, splitOn, Options.Timeout, commandType);
        }

        /// <summary>
        /// Query async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<T1>> QueryAsync<T1, T2, T3>(string sql, dynamic param = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await MultiQueryExtensions.QueryAsync<T1, T2, T3>(Connection, sql, param, Transaction, buffered, splitOn, Options.Timeout, commandType);
        }

        /// <summary>
        /// Query async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<T1>> QueryAsync<T1, T2, T3, T4>(string sql, dynamic param = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await MultiQueryExtensions.QueryAsync<T1, T2, T3, T4>(Connection, sql, param, Transaction, buffered, splitOn, Options.Timeout, commandType);
        }

        /// <summary>
        /// Query async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<T1>> QueryAsync<T1, T2, T3, T4, T5>(string sql, dynamic param = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await MultiQueryExtensions.QueryAsync<T1, T2, T3, T4, T5>(Connection, sql, param, Transaction, buffered, splitOn, Options.Timeout, commandType);
        }

        /// <summary>
        /// Query async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<T1>> QueryAsync<T1, T2, T3, T4, T5, T6>(string sql, dynamic param = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await MultiQueryExtensions.QueryAsync<T1, T2, T3, T4, T5, T6>(Connection, sql, param, Transaction, buffered, splitOn, Options.Timeout, commandType);
        }

        /// <summary>
        /// Query async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
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
        /// <returns></returns>
        public async Task<IEnumerable<T1>> QueryAsync<T1, T2, T3, T4, T5, T6, T7>(string sql, dynamic param = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await MultiQueryExtensions.QueryAsync<T1, T2, T3, T4, T5, T6, T7>(Connection, sql, param, Transaction, buffered, splitOn, Options.Timeout, commandType);
        }

        /// <summary>
        /// Query a collection of entity by given sql async
        /// </summary>
        /// <param name="command"></param>
        /// <param name="splitOn"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T1>> QueryAsync<T1, T2>(CommandDefinition command, string splitOn = "Id")
        {
            command = InjectTransaction(command);
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QueryAsync<T1, T2>(
                command.CommandText,
                command.Parameters,
                command.Transaction,
                command.Buffered,
                splitOn,
                command.CommandTimeout,
                command.CommandType);
        }

        /// <summary>
        /// Query a collection of entity by given sql async
        /// </summary>
        /// <param name="command"></param>
        /// <param name="splitOn"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T1>> QueryAsync<T1, T2, T3>(CommandDefinition command, string splitOn = "Id")
        {
            command = InjectTransaction(command);
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QueryAsync<T1, T2, T3>(
                command.CommandText,
                command.Parameters,
                command.Transaction,
                command.Buffered,
                splitOn,
                command.CommandTimeout,
                command.CommandType);
        }

        /// <summary>
        /// Query a collection of entity by given sql async
        /// </summary>
        /// <param name="command"></param>
        /// <param name="splitOn"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T1>> QueryAsync<T1, T2, T3, T4>(CommandDefinition command, string splitOn = "Id")
        {
            command = InjectTransaction(command);
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QueryAsync<T1, T2, T3, T4>(
                command.CommandText,
                command.Parameters,
                command.Transaction,
                command.Buffered,
                splitOn,
                command.CommandTimeout,
                command.CommandType);
        }

        /// <summary>
        /// Query a collection of entity by given sql async
        /// </summary>
        /// <param name="command"></param>
        /// <param name="splitOn"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T1>> QueryAsync<T1, T2, T3, T4, T5>(CommandDefinition command, string splitOn = "Id")
        {
            command = InjectTransaction(command);
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QueryAsync<T1, T2, T3, T4, T5>(
                command.CommandText,
                command.Parameters,
                command.Transaction,
                command.Buffered,
                splitOn,
                command.CommandTimeout,
                command.CommandType);
        }

        /// <summary>
        /// Query a collection of entity by given sql async
        /// </summary>
        /// <param name="command"></param>
        /// <param name="splitOn"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T1>> QueryAsync<T1, T2, T3, T4, T5, T6>(CommandDefinition command, string splitOn = "Id")
        {
            command = InjectTransaction(command);
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QueryAsync<T1, T2, T3, T4, T5, T6>(
                command.CommandText,
                command.Parameters,
                command.Transaction,
                command.Buffered,
                splitOn,
                command.CommandTimeout,
                command.CommandType);
        }

        /// <summary>
        /// Query a collection of entity by given sql async
        /// </summary>
        /// <param name="command"></param>
        /// <param name="splitOn"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T1>> QueryAsync<T1, T2, T3, T4, T5, T6, T7>(CommandDefinition command, string splitOn = "Id")
        {
            command = InjectTransaction(command);
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QueryAsync<T1, T2, T3, T4, T5, T6, T7>(
                command.CommandText,
                command.Parameters,
                command.Transaction,
                command.Buffered,
                splitOn,
                command.CommandTimeout,
                command.CommandType);
        }

        #endregion
    }
}