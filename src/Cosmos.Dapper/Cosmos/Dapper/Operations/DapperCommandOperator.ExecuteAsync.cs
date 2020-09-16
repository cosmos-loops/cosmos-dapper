using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Cosmos.Dapper.Operations
{
    public partial class DapperCommandOperator
    {
        #region Dapper Proxy Members

        /// <summary>
        /// Execute scalar async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<object> ExecuteScalarAsync(string sql, object param = null, CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.ExecuteScalarAsync(sql, param, Transaction, Options.Timeout, commandType).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute scalar async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> ExecuteScalarAsync<T>(string sql, object param = null, CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.ExecuteScalarAsync<T>(sql, param, Transaction, Options.Timeout, commandType).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute scalar async
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<object> ExecuteScalarAsync(CommandDefinition command)
        {
            await PrepareConnectionAndTransactionAsync(command.CancellationToken).ConfigureAwait(false);
            return await Connection.ExecuteScalarAsync(InjectTransaction(command)).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute scalar async
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> ExecuteScalarAsync<T>(CommandDefinition command)
        {
            await PrepareConnectionAndTransactionAsync(command.CancellationToken).ConfigureAwait(false);
            return await Connection.ExecuteScalarAsync<T>(InjectTransaction(command)).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<int> ExecuteAsync(string sql, object param = null, CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.ExecuteAsync(sql, param, Transaction, Options.Timeout, commandType).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute async
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<int> ExecuteAsync(CommandDefinition command)
        {
            await PrepareConnectionAndTransactionAsync(command.CancellationToken).ConfigureAwait(false);
            return await Connection.ExecuteAsync(InjectTransaction(command)).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute reader async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<IDataReader> ExecuteReaderAsync(string sql, object param = null, CommandType? commandType = null)
        {
            await PrepareConnectionAndTransactionAsync(default).ConfigureAwait(false);
            return await Connection.ExecuteReaderAsync(sql, param, Transaction, Options.Timeout, commandType).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute reader async
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<IDataReader> ExecuteReaderAsync(CommandDefinition command)
        {
            await PrepareConnectionAndTransactionAsync(command.CancellationToken).ConfigureAwait(false);
            return await Connection.ExecuteReaderAsync(InjectTransaction(command)).ConfigureAwait(false);
        }

        #endregion
    }
}