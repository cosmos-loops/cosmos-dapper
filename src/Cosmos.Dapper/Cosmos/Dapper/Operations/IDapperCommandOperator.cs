using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Cosmos.Dapper.Operations
{
    /// <summary>
    /// Interface for Dapper Command Operator
    /// </summary>
    public interface IDapperCommandOperator
    {
        /// <summary>
        /// Execute scalar async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        Task<object> ExecuteScalarAsync(string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// Execute scalar async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> ExecuteScalarAsync<T>(string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// Execute scalar async
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<object> ExecuteScalarAsync(CommandDefinition command);

        /// <summary>
        /// Execute scalar async
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> ExecuteScalarAsync<T>(CommandDefinition command);

        /// <summary>
        /// Execute async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        Task<int> ExecuteAsync(string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// Execute async
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<int> ExecuteAsync(CommandDefinition command);

        /// <summary>
        /// Execute reader async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        Task<IDataReader> ExecuteReaderAsync(string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// Execute reader async
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<IDataReader> ExecuteReaderAsync(CommandDefinition command);

        /// <summary>
        /// Execute
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        int Execute(string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// Execute
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        int Execute(CommandDefinition command);

        /// <summary>
        /// Execute scalar
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        object ExecuteScalar(string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// Execute scalar
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T ExecuteScalar<T>(string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// Execute scalar
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        object ExecuteScalar(CommandDefinition command);

        /// <summary>
        /// Execute scalar
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T ExecuteScalar<T>(CommandDefinition command);

        /// <summary>
        /// Execute reader
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        IDataReader ExecuteReader(string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// Execute reader
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        IDataReader ExecuteReader(CommandDefinition command);

        /// <summary>
        /// Execute reader
        /// </summary>
        /// <param name="command"></param>
        /// <param name="commandBehavior"></param>
        /// <returns></returns>
        IDataReader ExecuteReader(CommandDefinition command, CommandBehavior commandBehavior);
    }
}