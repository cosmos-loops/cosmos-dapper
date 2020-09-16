using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Cosmos.Dapper.Operations
{
    public partial class DapperQueryOperator
    {
        #region Dapper Proxy Members

        /// <summary>
        /// Query first async
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<object> QueryFirstAsync(CommandDefinition command)
        {
            await PrepareConnectionAndTransactionAsync(command.CancellationToken).ConfigureAwait(false);
            return await Connection.QueryFirstAsync(InjectTransaction(command)).ConfigureAwait(false);
        }

        /// <summary>
        /// Query first or default async
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<object> QueryFirstOrDefaultAsync(CommandDefinition command)
        {
            await PrepareConnectionAndTransactionAsync(command.CancellationToken).ConfigureAwait(false);
            return await Connection.QueryFirstOrDefaultAsync(InjectTransaction(command)).ConfigureAwait(false);
        }

        /// <summary>
        /// Query single async
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<object> QuerySingleAsync(CommandDefinition command)
        {
            await PrepareConnectionAndTransactionAsync(command.CancellationToken).ConfigureAwait(false);
            return await Connection.QuerySingleAsync(InjectTransaction(command)).ConfigureAwait(false);
        }

        /// <summary>
        /// Query single or default async
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<object> QuerySingleOrDefaultAsync(CommandDefinition command)
        {
            await PrepareConnectionAndTransactionAsync(command.CancellationToken).ConfigureAwait(false);
            return await Connection.QuerySingleOrDefaultAsync(InjectTransaction(command)).ConfigureAwait(false);
        }

        /// <summary>
        /// Query first async
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> QueryFirstAsync<T>(CommandDefinition command)
        {
            await PrepareConnectionAndTransactionAsync(command.CancellationToken).ConfigureAwait(false);
            return await Connection.QueryFirstAsync<T>(InjectTransaction(command)).ConfigureAwait(false);
        }

        /// <summary>
        /// Query first or default async
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> QueryFirstOrDefaultAsync<T>(CommandDefinition command)
        {
            await PrepareConnectionAndTransactionAsync(command.CancellationToken).ConfigureAwait(false);
            return await Connection.QueryFirstOrDefaultAsync<T>(InjectTransaction(command)).ConfigureAwait(false);
        }

        /// <summary>
        /// Query single async
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> QuerySingleAsync<T>(CommandDefinition command)
        {
            await PrepareConnectionAndTransactionAsync(command.CancellationToken).ConfigureAwait(false);
            return await Connection.QuerySingleAsync<T>(InjectTransaction(command)).ConfigureAwait(false);
        }

        /// <summary>
        /// Query single or default async
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> QuerySingleOrDefaultAsync<T>(CommandDefinition command)
        {
            await PrepareConnectionAndTransactionAsync(command.CancellationToken).ConfigureAwait(false);
            return await Connection.QuerySingleOrDefaultAsync<T>(InjectTransaction(command)).ConfigureAwait(false);
        }

        /// <summary>
        /// Query first async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<object> QueryFirstAsync(string sql, object param = null, CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QueryFirstAsync(sql, param, Transaction, Options.Timeout, commandType).ConfigureAwait(false);
        }

        /// <summary>
        /// Query first or default async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<object> QueryFirstOrDefaultAsync(string sql, object param = null, CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QueryFirstOrDefaultAsync(sql, param, Transaction, Options.Timeout, commandType).ConfigureAwait(false);
        }

        /// <summary>
        /// Query single async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<object> QuerySingleAsync(string sql, object param = null, CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QuerySingleAsync(sql, param, Transaction, Options.Timeout, commandType).ConfigureAwait(false);
        }

        /// <summary>
        /// Query single or default async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<object> QuerySingleOrDefaultAsync(string sql, object param = null, CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QuerySingleOrDefaultAsync(sql, param, Transaction, Options.Timeout, commandType).ConfigureAwait(false);
        }

        /// <summary>
        /// Query first async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> QueryFirstAsync<T>(string sql, object param = null, CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QueryFirstAsync<T>(sql, param, Transaction, Options.Timeout, commandType).ConfigureAwait(false);
        }

        /// <summary>
        /// Query first or default async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QueryFirstOrDefaultAsync<T>(sql, param, Transaction, Options.Timeout, commandType).ConfigureAwait(false);
        }

        /// <summary>
        /// Query single async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> QuerySingleAsync<T>(string sql, object param = null, CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QuerySingleAsync<T>(sql, param, Transaction, Options.Timeout, commandType).ConfigureAwait(false);
        }

        /// <summary>
        /// Query single or default async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> QuerySingleOrDefaultAsync<T>(string sql, object param = null, CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QuerySingleOrDefaultAsync<T>(sql, param, Transaction, Options.Timeout, commandType).ConfigureAwait(false);
        }

        /// <summary>
        /// Query first async
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<object> QueryFirstAsync(Type type, string sql, object param = null, CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QueryFirstAsync(type, sql, param, Transaction, Options.Timeout, commandType).ConfigureAwait(false);
        }

        /// <summary>
        /// Query first or default async
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<object> QueryFirstOrDefaultAsync(Type type, string sql, object param = null, CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QueryFirstOrDefaultAsync(type, sql, param, Transaction, Options.Timeout, commandType).ConfigureAwait(false);
        }

        /// <summary>
        /// Query single async
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<object> QuerySingleAsync(Type type, string sql, object param = null, CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QuerySingleAsync(type, sql, param, Transaction, Options.Timeout, commandType).ConfigureAwait(false);
        }

        /// <summary>
        /// Query single or default async
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<object> QuerySingleOrDefaultAsync(Type type, string sql, object param = null, CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QuerySingleOrDefaultAsync(type, sql, param, Transaction, Options.Timeout, commandType).ConfigureAwait(false);
        }

        /// <summary>
        /// Query first async
        /// </summary>
        /// <param name="type"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<object> QueryFirstAsync(Type type, CommandDefinition command)
        {
            await PrepareConnectionAndTransactionAsync(command.CancellationToken).ConfigureAwait(false);
            return await Connection.QueryFirstAsync(type, InjectTransaction(command)).ConfigureAwait(false);
        }

        /// <summary>
        /// Query first or default async
        /// </summary>
        /// <param name="type"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<object> QueryFirstOrDefaultAsync(Type type, CommandDefinition command)
        {
            await PrepareConnectionAndTransactionAsync(command.CancellationToken).ConfigureAwait(false);
            return await Connection.QueryFirstOrDefaultAsync(type, InjectTransaction(command)).ConfigureAwait(false);
        }

        /// <summary>
        /// Query single async
        /// </summary>
        /// <param name="type"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<object> QuerySingleAsync(Type type, CommandDefinition command)
        {
            await PrepareConnectionAndTransactionAsync(command.CancellationToken).ConfigureAwait(false);
            return await Connection.QuerySingleAsync(type, InjectTransaction(command)).ConfigureAwait(false);
        }

        /// <summary>
        /// Query single or default async
        /// </summary>
        /// <param name="type"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<object> QuerySingleOrDefaultAsync(Type type, CommandDefinition command)
        {
            await PrepareConnectionAndTransactionAsync(command.CancellationToken).ConfigureAwait(false);
            return await Connection.QuerySingleOrDefaultAsync(type, InjectTransaction(command)).ConfigureAwait(false);
        }

        /// <summary>
        /// Query multiple async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object param = null, CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.QueryMultipleAsync(sql, param, Transaction, Options.Timeout, commandType).ConfigureAwait(false);
        }

        /// <summary>
        /// Query multiple
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<SqlMapper.GridReader> QueryMultipleAsync(CommandDefinition command)
        {
            await PrepareConnectionAndTransactionAsync(command.CancellationToken).ConfigureAwait(false);
            return await Connection.QueryMultipleAsync(InjectTransaction(command)).ConfigureAwait(false);
        }

        #endregion
    }
}