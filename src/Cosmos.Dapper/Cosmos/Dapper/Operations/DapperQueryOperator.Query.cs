using System;
using System.Collections.Generic;
using System.Data;
using Dapper;

namespace Cosmos.Dapper.Operations
{
    public partial class DapperQueryOperator
    {
        #region Query

        /// <summary>
        /// Query
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="buffered"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public IEnumerable<object> Query(string sql, object param = null, bool buffered = true, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.Query(sql, param, Transaction, buffered, Options.Timeout, commandType);
        }

        /// <summary>
        /// Query
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="buffered"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public IEnumerable<object> Query(Type type, string sql, object param = null, bool buffered = true, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.Query(type, sql, param, Transaction, buffered, Options.Timeout, commandType);
        }

        /// <summary>
        /// Query
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="buffered"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(string sql, object param = null, bool buffered = true, CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.Query<T>(sql, param, Transaction, buffered, Options.Timeout, commandType);
        }

        /// <summary>
        /// Query
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public IEnumerable<object> Query(CommandDefinition command)
        {
            command = InjectTransaction(command);
            PrepareConnectionAndTransaction();
            return Connection.Query(
                command.CommandText,
                command.Parameters,
                command.Transaction,
                command.Buffered,
                command.CommandTimeout,
                command.CommandType);
        }

        /// <summary>
        /// Query
        /// </summary>
        /// <param name="type"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public IEnumerable<object> Query(Type type, CommandDefinition command)
        {
            command = InjectTransaction(command);
            PrepareConnectionAndTransaction();
            return Connection.Query(
                type,
                command.CommandText,
                command.Parameters,
                command.Transaction,
                command.Buffered,
                command.CommandTimeout,
                command.CommandType);
        }

        /// <summary>
        /// Query
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(CommandDefinition command)
        {
            PrepareConnectionAndTransaction();
            return Connection.Query<T>(InjectTransaction(command));
        }

        #endregion

        #region multi-query

        /// <summary>
        /// Query
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
        public IEnumerable<TReturn> Query<T1, T2, TReturn>(string sql, Func<T1, T2, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id",
            CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.Query(sql, map, param, Transaction, buffered, splitOn, Options.Timeout, commandType);
        }

        /// <summary>
        /// Query
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
        public IEnumerable<TReturn> Query<T1, T2, T3, TReturn>(string sql, Func<T1, T2, T3, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id",
            CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.Query(sql, map, param, Transaction, buffered, splitOn, Options.Timeout, commandType);
        }

        /// <summary>
        /// Query
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
        public IEnumerable<TReturn> Query<T1, T2, T3, T4, TReturn>(string sql, Func<T1, T2, T3, T4, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id",
            CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.Query(sql, map, param, Transaction, buffered, splitOn, Options.Timeout, commandType);
        }

        /// <summary>
        /// Query
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
        public IEnumerable<TReturn> Query<T1, T2, T3, T4, T5, TReturn>(string sql, Func<T1, T2, T3, T4, T5, TReturn> map, object param = null, bool buffered = true,
            string splitOn = "Id", CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.Query(sql, map, param, Transaction, buffered, splitOn, Options.Timeout, commandType);
        }

        /// <summary>
        /// Query
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
        public IEnumerable<TReturn> Query<T1, T2, T3, T4, T5, T6, TReturn>(string sql, Func<T1, T2, T3, T4, T5, T6, TReturn> map, object param = null, bool buffered = true,
            string splitOn = "Id", CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.Query(sql, map, param, Transaction, buffered, splitOn, Options.Timeout, commandType);
        }

        /// <summary>
        /// Query
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
        public IEnumerable<TReturn> Query<T1, T2, T3, T4, T5, T6, T7, TReturn>(string sql, Func<T1, T2, T3, T4, T5, T6, T7, TReturn> map, object param = null, bool buffered = true,
            string splitOn = "Id", CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.Query(sql, map, param, Transaction, buffered, splitOn, Options.Timeout, commandType);
        }

        /// <summary>
        /// Query
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
        public IEnumerable<TReturn> Query<TReturn>(string sql, Type[] types, Func<object[], TReturn> map, object param = null, bool buffered = true, string splitOn = "Id",
            CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return Connection.Query(sql, types, map, param, Transaction, buffered, splitOn, Options.Timeout, commandType);
        }

        /// <summary>
        /// Query 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="map"></param>
        /// <param name="splitOn"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public IEnumerable<TReturn> Query<T1, T2, TReturn>(CommandDefinition command, Func<T1, T2, TReturn> map, string splitOn = "Id")
        {
            command = InjectTransaction(command);
            PrepareConnectionAndTransaction();
            return Connection.Query(
                command.CommandText,
                map,
                command.Parameters,
                command.Transaction,
                command.Buffered,
                splitOn,
                command.CommandTimeout,
                command.CommandType);
        }

        /// <summary>
        /// Query 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="map"></param>
        /// <param name="splitOn"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public IEnumerable<TReturn> Query<T1, T2, T3, TReturn>(CommandDefinition command, Func<T1, T2, T3, TReturn> map, string splitOn = "Id")
        {
            command = InjectTransaction(command);
            PrepareConnectionAndTransaction();
            return Connection.Query(
                command.CommandText,
                map,
                command.Parameters,
                command.Transaction,
                command.Buffered,
                splitOn,
                command.CommandTimeout,
                command.CommandType);
        }

        /// <summary>
        /// Query 
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
        public IEnumerable<TReturn> Query<T1, T2, T3, T4, TReturn>(CommandDefinition command, Func<T1, T2, T3, T4, TReturn> map, string splitOn = "Id")
        {
            command = InjectTransaction(command);
            PrepareConnectionAndTransaction();
            return Connection.Query(
                command.CommandText,
                map,
                command.Parameters,
                command.Transaction,
                command.Buffered,
                splitOn,
                command.CommandTimeout,
                command.CommandType);
        }

        /// <summary>
        /// Query 
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
        public IEnumerable<TReturn> Query<T1, T2, T3, T4, T5, TReturn>(CommandDefinition command, Func<T1, T2, T3, T4, T5, TReturn> map, string splitOn = "Id")
        {
            command = InjectTransaction(command);
            PrepareConnectionAndTransaction();
            return Connection.Query(
                command.CommandText,
                map,
                command.Parameters,
                command.Transaction,
                command.Buffered,
                splitOn,
                command.CommandTimeout,
                command.CommandType);
        }

        /// <summary>
        /// Query 
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
        public IEnumerable<TReturn> Query<T1, T2, T3, T4, T5, T6, TReturn>(CommandDefinition command, Func<T1, T2, T3, T4, T5, T6, TReturn> map,
            string splitOn = "Id")
        {
            command = InjectTransaction(command);
            PrepareConnectionAndTransaction();
            return Connection.Query(
                command.CommandText,
                map,
                command.Parameters,
                command.Transaction,
                command.Buffered,
                splitOn,
                command.CommandTimeout,
                command.CommandType);
        }

        /// <summary>
        /// Query 
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
        public IEnumerable<TReturn> Query<T1, T2, T3, T4, T5, T6, T7, TReturn>(CommandDefinition command, Func<T1, T2, T3, T4, T5, T6, T7, TReturn> map,
            string splitOn = "Id")
        {
            command = InjectTransaction(command);
            PrepareConnectionAndTransaction();
            return Connection.Query(
                command.CommandText,
                map,
                command.Parameters,
                command.Transaction,
                command.Buffered,
                splitOn,
                command.CommandTimeout,
                command.CommandType);
        }

        #endregion

        #region crosscut multi-query

        /// <summary>
        /// Query
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        public IEnumerable<T1> Query<T1, T2>(string sql, dynamic param = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return MultiQueryExtensions.Query<T1, T2>(Connection, sql, param, Transaction, buffered, splitOn, Options.Timeout, commandType);
        }

        /// <summary>
        /// Query
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
        public IEnumerable<T1> Query<T1, T2, T3>(string sql, dynamic param = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return MultiQueryExtensions.Query<T1, T2, T3>(Connection, sql, param, Transaction, buffered, splitOn, Options.Timeout, commandType);
        }

        /// <summary>
        /// Query
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
        public IEnumerable<T1> Query<T1, T2, T3, T4>(string sql, dynamic param = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return MultiQueryExtensions.Query<T1, T2, T3, T4>(Connection, sql, param, Transaction, buffered, splitOn, Options.Timeout, commandType);
        }

        /// <summary>
        /// Query
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
        public IEnumerable<T1> Query<T1, T2, T3, T4, T5>(string sql, dynamic param = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return MultiQueryExtensions.Query<T1, T2, T3, T4, T5>(Connection, sql, param, Transaction, buffered, splitOn, Options.Timeout, commandType);
        }

        /// <summary>
        /// Query
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
        public IEnumerable<T1> Query<T1, T2, T3, T4, T5, T6>(string sql, dynamic param = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return MultiQueryExtensions.Query<T1, T2, T3, T4, T5, T6>(Connection, sql, param, Transaction, buffered, splitOn, Options.Timeout, commandType);
        }

        /// <summary>
        /// Query
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
        public IEnumerable<T1> Query<T1, T2, T3, T4, T5, T6, T7>(string sql, dynamic param = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null)
        {
            PrepareConnectionAndTransaction();
            return MultiQueryExtensions.Query<T1, T2, T3, T4, T5, T6, T7>(Connection, sql, param, Transaction, buffered, splitOn, Options.Timeout, commandType);
        }

        /// <summary>
        /// Query a collection of entity by given sql 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="splitOn"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        public virtual IEnumerable<T1> Query<T1, T2>(CommandDefinition command, string splitOn = "Id")
        {
            command = InjectTransaction(command);
            PrepareConnectionAndTransaction();
            return Connection.Query<T1, T2>(command.CommandText,
                command.Parameters,
                command.Transaction,
                command.Buffered,
                splitOn,
                command.CommandTimeout,
                command.CommandType);
        }

        /// <summary>
        /// Query a collection of entity by given sql 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="splitOn"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <returns></returns>
        public virtual IEnumerable<T1> Query<T1, T2, T3>(CommandDefinition command, string splitOn = "Id")
        {
            command = InjectTransaction(command);
            PrepareConnectionAndTransaction();
            return Connection.Query<T1, T2, T3>(command.CommandText,
                command.Parameters,
                command.Transaction,
                command.Buffered,
                splitOn,
                command.CommandTimeout,
                command.CommandType);
        }

        /// <summary>
        /// Query a collection of entity by given sql 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="splitOn"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <returns></returns>
        public virtual IEnumerable<T1> Query<T1, T2, T3, T4>(CommandDefinition command, string splitOn = "Id")
        {
            command = InjectTransaction(command);
            PrepareConnectionAndTransaction();
            return Connection.Query<T1, T2, T3, T4>(command.CommandText,
                command.Parameters,
                command.Transaction,
                command.Buffered,
                splitOn,
                command.CommandTimeout,
                command.CommandType);
        }

        /// <summary>
        /// Query a collection of entity by given sql
        /// </summary>
        /// <param name="command"></param>
        /// <param name="splitOn"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <returns></returns>
        public virtual IEnumerable<T1> Query<T1, T2, T3, T4, T5>(CommandDefinition command, string splitOn = "Id")
        {
            command = InjectTransaction(command);
            PrepareConnectionAndTransaction();
            return Connection.Query<T1, T2, T3, T4, T5>(command.CommandText,
                command.Parameters,
                command.Transaction,
                command.Buffered,
                splitOn,
                command.CommandTimeout,
                command.CommandType);
        }

        /// <summary>
        /// Query a collection of entity by given sql
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
        public virtual IEnumerable<T1> Query<T1, T2, T3, T4, T5, T6>(CommandDefinition command, string splitOn = "Id")
        {
            command = InjectTransaction(command);
            PrepareConnectionAndTransaction();
            return Connection.Query<T1, T2, T3, T4, T5, T6>(command.CommandText,
                command.Parameters,
                command.Transaction,
                command.Buffered,
                splitOn,
                command.CommandTimeout,
                command.CommandType);
        }

        /// <summary>
        /// Query a collection of entity by given sql
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
        public virtual IEnumerable<T1> Query<T1, T2, T3, T4, T5, T6, T7>(CommandDefinition command, string splitOn = "Id")
        {
            command = InjectTransaction(command);
            PrepareConnectionAndTransaction();
            return Connection.Query<T1, T2, T3, T4, T5, T6, T7>(command.CommandText,
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