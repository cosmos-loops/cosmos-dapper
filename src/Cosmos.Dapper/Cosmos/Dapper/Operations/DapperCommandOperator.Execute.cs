using System.Data;
using Dapper;

namespace Cosmos.Dapper.Operations
{
    public partial class DapperCommandOperator
    {
        #region Dapper Proxy Members

        /// <summary>
        /// Execute
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public int Execute(string sql, object param = null, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.Execute(sql, param, Transaction, Options.Timeout, commandType);
        }

        /// <summary>
        /// Execute
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public int Execute(CommandDefinition command)
        {
            PrepareConnectionAndTransaction();
            return Connection.Execute(InjectTransaction(command));
        }

        /// <summary>
        /// Execute scalar
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public object ExecuteScalar(string sql, object param = null, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.ExecuteScalar(sql, param, Transaction, Options.Timeout, commandType);
        }

        /// <summary>
        /// Execute scalar
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T ExecuteScalar<T>(string sql, object param = null, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.ExecuteScalar<T>(sql, param, Transaction, Options.Timeout, commandType);
        }

        /// <summary>
        /// Execute scalar
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public object ExecuteScalar(CommandDefinition command)
        {
            PrepareConnectionAndTransaction();
            return Connection.ExecuteScalar(InjectTransaction(command));
        }

        /// <summary>
        /// Execute scalar
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T ExecuteScalar<T>(CommandDefinition command)
        {
            PrepareConnectionAndTransaction();
            return Connection.ExecuteScalar<T>(InjectTransaction(command));
        }

        /// <summary>
        /// Execute reader
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public IDataReader ExecuteReader(string sql, object param = null, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.ExecuteReader(sql, param, Transaction, Options.Timeout, commandType);
        }

        /// <summary>
        /// Execute reader
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public IDataReader ExecuteReader(CommandDefinition command)
        {
            PrepareConnectionAndTransaction();
            return Connection.ExecuteReader(InjectTransaction(command));
        }

        /// <summary>
        /// Execute reader
        /// </summary>
        /// <param name="command"></param>
        /// <param name="commandBehavior"></param>
        /// <returns></returns>
        public IDataReader ExecuteReader(CommandDefinition command, CommandBehavior commandBehavior)
        {
            PrepareConnectionAndTransaction();
            return Connection.ExecuteReader(InjectTransaction(command), commandBehavior);
        }

        #endregion
    }
}