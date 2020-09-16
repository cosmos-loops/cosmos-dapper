using System;
using System.Data;
using Dapper;

namespace Cosmos.Dapper.Operations
{
    public partial class DapperQueryOperator
    {
        #region Dapper Proxy Members

        /// <summary>
        /// Query first
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public object QueryFirst(string sql, object param = null, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.QueryFirst(sql, param, Transaction, Options.Timeout, commandType);
        }

        /// <summary>
        /// Query first or default
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public object QueryFirstOrDefault(string sql, object param = null, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.QueryFirstOrDefault(sql, param, Transaction, Options.Timeout, commandType);
        }

        /// <summary>
        /// Query single
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public object QuerySingle(string sql, object param = null, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.QuerySingle(sql, param, Transaction, Options.Timeout, commandType);
        }

        /// <summary>
        /// Query single or default
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public object QuerySingleOrDefault(string sql, object param = null, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.QuerySingleOrDefault(sql, param, Transaction, Options.Timeout, commandType);
        }

        /// <summary>
        /// Query first
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T QueryFirst<T>(string sql, object param = null, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.QueryFirst<T>(sql, param, Transaction, Options.Timeout, commandType);
        }

        /// <summary>
        /// Query first or default
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T QueryFirstOrDefault<T>(string sql, object param = null, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.QueryFirstOrDefault<T>(sql, param, Transaction, Options.Timeout, commandType);
        }

        /// <summary>
        /// Query single
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T QuerySingle<T>(string sql, object param = null, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.QuerySingle<T>(sql, param, Transaction, Options.Timeout, commandType);
        }

        /// <summary>
        /// Query single or default
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T QuerySingleOrDefault<T>(string sql, object param = null, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.QuerySingleOrDefault<T>(sql, param, Transaction, Options.Timeout, commandType);
        }

        /// <summary>
        /// Query first
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public object QueryFirst(Type type, string sql, object param = null, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.QueryFirst(type, sql, param, Transaction, Options.Timeout, commandType);
        }

        /// <summary>
        /// Query first or default
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public object QueryFirstOrDefault(Type type, string sql, object param = null, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.QueryFirstOrDefault(type, sql, param, Transaction, Options.Timeout, commandType);
        }

        /// <summary>
        /// Query single
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public object QuerySingle(Type type, string sql, object param = null, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.QuerySingle(type, sql, param, Transaction, Options.Timeout, commandType);
        }

        /// <summary>
        /// Query single or default
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public object QuerySingleOrDefault(Type type, string sql, object param = null, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.QuerySingleOrDefault(type, sql, param, Transaction, Options.Timeout, commandType);
        }

        /// <summary>
        /// Query first
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public object QueryFirst(CommandDefinition command)
        {
            command = InjectTransaction(command);
            PrepareConnectionAndTransaction();
            return Connection.QueryFirst(
                command.CommandText,
                command.Parameters,
                command.Transaction,
                command.CommandTimeout,
                command.CommandType);
        }

        /// <summary>
        /// Query first or default
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public object QueryFirstOrDefault(CommandDefinition command)
        {
            command = InjectTransaction(command);
            PrepareConnectionAndTransaction();
            return Connection.QueryFirstOrDefault(
                command.CommandText,
                command.Parameters,
                command.Transaction,
                command.CommandTimeout,
                command.CommandType);
        }

        /// <summary>
        /// Query single
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public object QuerySingle(CommandDefinition command)
        {
            command = InjectTransaction(command);
            PrepareConnectionAndTransaction();
            return Connection.QuerySingle(
                command.CommandText,
                command.Parameters,
                command.Transaction,
                command.CommandTimeout,
                command.CommandType);
        }

        /// <summary>
        /// Query single or default
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public object QuerySingleOrDefault(CommandDefinition command)
        {
            command = InjectTransaction(command);
            PrepareConnectionAndTransaction();
            return Connection.QuerySingleOrDefault(
                command.CommandText,
                command.Parameters,
                command.Transaction,
                command.CommandTimeout,
                command.CommandType);
        }

        /// <summary>
        /// Query first
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T QueryFirst<T>(CommandDefinition command)
        {
            PrepareConnectionAndTransaction();
            return Connection.QueryFirst<T>(InjectTransaction(command));
        }

        /// <summary>
        /// Query first or default
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T QueryFirstOrDefault<T>(CommandDefinition command)
        {
            PrepareConnectionAndTransaction();
            return Connection.QueryFirstOrDefault<T>(InjectTransaction(command));
        }

        /// <summary>
        /// Query single
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T QuerySingle<T>(CommandDefinition command)
        {
            PrepareConnectionAndTransaction();
            return Connection.QuerySingle<T>(InjectTransaction(command));
        }

        /// <summary>
        /// Query single or default
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T QuerySingleOrDefault<T>(CommandDefinition command)
        {
            PrepareConnectionAndTransaction();
            return Connection.QuerySingleOrDefault<T>(InjectTransaction(command));
        }

        /// <summary>
        /// Query first
        /// </summary>
        /// <param name="type"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public object QueryFirst(Type type, CommandDefinition command)
        {
            command = InjectTransaction(command);
            PrepareConnectionAndTransaction();
            return Connection.QueryFirst(
                type,
                command.CommandText,
                command.Parameters,
                command.Transaction,
                command.CommandTimeout,
                command.CommandType);
        }

        /// <summary>
        /// Query first or default
        /// </summary>
        /// <param name="type"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public object QueryFirstOrDefault(Type type, CommandDefinition command)
        {
            command = InjectTransaction(command);
            PrepareConnectionAndTransaction();
            return Connection.QueryFirstOrDefault(
                type,
                command.CommandText,
                command.Parameters,
                command.Transaction,
                command.CommandTimeout,
                command.CommandType);
        }

        /// <summary>
        /// Query single
        /// </summary>
        /// <param name="type"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public object QuerySingle(Type type, CommandDefinition command)
        {
            command = InjectTransaction(command);
            PrepareConnectionAndTransaction();
            return Connection.QuerySingle(
                type,
                command.CommandText,
                command.Parameters,
                command.Transaction,
                command.CommandTimeout,
                command.CommandType);
        }

        /// <summary>
        /// Query single or default
        /// </summary>
        /// <param name="type"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public object QuerySingleOrDefault(Type type, CommandDefinition command)
        {
            command = InjectTransaction(command);
            PrepareConnectionAndTransaction();
            return Connection.QuerySingleOrDefault(
                type,
                command.CommandText,
                command.Parameters,
                command.Transaction,
                command.CommandTimeout,
                command.CommandType);
        }

        /// <summary>
        /// Query multiple
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public SqlMapper.GridReader QueryMultiple(string sql, object param = null, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.QueryMultiple(sql, param, Transaction, Options.Timeout, commandType);
        }

        /// <summary>
        /// Query multiple
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public SqlMapper.GridReader QueryMultiple(CommandDefinition command)
        {
            PrepareConnectionAndTransaction();
            return Connection.QueryMultiple(InjectTransaction(command));
        }

        #endregion
    }
}