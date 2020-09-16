using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Cosmos.Dapper.Operations
{
    /// <summary>
    /// Interface for Dapper Query Operator
    /// </summary>
    public interface IDapperQueryOperator
    {
        #region Query with many parameters

        /// <summary>
        /// Query a collection of object by given sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="buffered"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        IEnumerable<object> Query(string sql, object param = null, bool buffered = true, CommandType? commandType = null);

        /// <summary>
        /// Query a collection of object by given sql and type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="buffered"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        IEnumerable<object> Query(Type type, string sql, object param = null, bool buffered = true, CommandType? commandType = null);

        /// <summary>
        /// Query a collection of entity by given sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="buffered"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> Query<T>(string sql, object param = null, bool buffered = true, CommandType? commandType = null);

        /// <summary>
        /// Query a collection of entity by given sql async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        Task<IEnumerable<object>> QueryAsync(string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// Query a collection of object by given sql and type async
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        Task<IEnumerable<object>> QueryAsync(Type type, string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// Query a collection of entity by given sql async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, CommandType? commandType = null);

        #endregion

        #region Query with many parameters and TReturn

        /// <summary>
        /// Query a collection of entity by given sql
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
        IEnumerable<TReturn> Query<TReturn>(string sql, Type[] types, Func<object[], TReturn> map, object param = null, bool buffered = true, string splitOn = "Id",
            CommandType? commandType = null);

        /// <summary>
        /// Query a collection of entity by given sql async
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
        Task<IEnumerable<TReturn>> QueryAsync<TReturn>(string sql, Type[] types, Func<object[], TReturn> map, object param = null, bool buffered = true, string splitOn = "Id",
            CommandType? commandType = null);

        #endregion

        #region Query with many paramters and T2 ~ T7

        /// <summary>
        /// Query a collection of entity by given sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        IEnumerable<T1> Query<T1, T2>(string sql, dynamic param = null, bool buffered = true, string splitOn = "Id", CommandType? commandType = null);

        /// <summary>
        /// Query a collection of entity by given sql
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
        IEnumerable<T1> Query<T1, T2, T3>(string sql, dynamic param = null, bool buffered = true, string splitOn = "Id", CommandType? commandType = null);

        /// <summary>
        /// Query a collection of entity by given sql
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
        IEnumerable<T1> Query<T1, T2, T3, T4>(string sql, dynamic param = null, bool buffered = true, string splitOn = "Id", CommandType? commandType = null);

        /// <summary>
        /// Query a collection of entity by given sql
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
        IEnumerable<T1> Query<T1, T2, T3, T4, T5>(string sql, dynamic param = null, bool buffered = true, string splitOn = "Id", CommandType? commandType = null);

        /// <summary>
        /// Query a collection of entity by given sql
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
        IEnumerable<T1> Query<T1, T2, T3, T4, T5, T6>(string sql, dynamic param = null, bool buffered = true, string splitOn = "Id", CommandType? commandType = null);

        /// <summary>
        /// Query a collection of entity by given sql
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
        IEnumerable<T1> Query<T1, T2, T3, T4, T5, T6, T7>(string sql, dynamic param = null, bool buffered = true, string splitOn = "Id", CommandType? commandType = null);

        /// <summary>
        /// Query a collection of entity by given sql async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        Task<IEnumerable<T1>> QueryAsync<T1, T2>(string sql, dynamic param = null, bool buffered = true, string splitOn = "Id", CommandType? commandType = null);

        /// <summary>
        /// Query a collection of entity by given sql async
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
        Task<IEnumerable<T1>> QueryAsync<T1, T2, T3>(string sql, dynamic param = null, bool buffered = true, string splitOn = "Id", CommandType? commandType = null);

        /// <summary>
        /// Query a collection of entity by given sql async
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
        Task<IEnumerable<T1>> QueryAsync<T1, T2, T3, T4>(string sql, dynamic param = null, bool buffered = true, string splitOn = "Id", CommandType? commandType = null);

        /// <summary>
        /// Query a collection of entity by given sql async
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
        Task<IEnumerable<T1>> QueryAsync<T1, T2, T3, T4, T5>(string sql, dynamic param = null, bool buffered = true, string splitOn = "Id", CommandType? commandType = null);

        /// <summary>
        /// Query a collection of entity by given sql async
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
        Task<IEnumerable<T1>> QueryAsync<T1, T2, T3, T4, T5, T6>(string sql, dynamic param = null, bool buffered = true, string splitOn = "Id", CommandType? commandType = null);

        /// <summary>
        /// Query a collection of entity by given sql async
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
        Task<IEnumerable<T1>> QueryAsync<T1, T2, T3, T4, T5, T6, T7>(string sql, dynamic param = null, bool buffered = true, string splitOn = "Id",
            CommandType? commandType = null);

        #endregion

        #region Query with many paramters and T2 ~ T7 and TReturn

        /// <summary>
        /// Query a collection of entity by given sql
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
        IEnumerable<TReturn> Query<T1, T2, TReturn>(string sql, Func<T1, T2, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id",
            CommandType? commandType = null);

        /// <summary>
        /// Query a collection of entity by given sql
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
        IEnumerable<TReturn> Query<T1, T2, T3, TReturn>(string sql, Func<T1, T2, T3, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id",
            CommandType? commandType = null);

        /// <summary>
        /// Query a collection of entity by given sql
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
        IEnumerable<TReturn> Query<T1, T2, T3, T4, TReturn>(string sql, Func<T1, T2, T3, T4, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id",
            CommandType? commandType = null);

        /// <summary>
        /// Query a collection of entity by given sql
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
        IEnumerable<TReturn> Query<T1, T2, T3, T4, T5, TReturn>(string sql, Func<T1, T2, T3, T4, T5, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id",
            CommandType? commandType = null);

        /// <summary>
        /// Query a collection of entity by given sql
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
        IEnumerable<TReturn> Query<T1, T2, T3, T4, T5, T6, TReturn>(string sql, Func<T1, T2, T3, T4, T5, T6, TReturn> map, object param = null, bool buffered = true,
            string splitOn = "Id", CommandType? commandType = null);

        /// <summary>
        /// Query a collection of entity by given sql
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
        IEnumerable<TReturn> Query<T1, T2, T3, T4, T5, T6, T7, TReturn>(string sql, Func<T1, T2, T3, T4, T5, T6, T7, TReturn> map, object param = null, bool buffered = true,
            string splitOn = "Id", CommandType? commandType = null);

        /// <summary>
        /// Query a collection of entity by given sql async
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
        Task<IEnumerable<TReturn>> QueryAsync<T1, T2, TReturn>(string sql, Func<T1, T2, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id",
            CommandType? commandType = null);

        /// <summary>
        /// Query a collection of entity by given sql async
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
        Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, TReturn>(string sql, Func<T1, T2, T3, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id",
            CommandType? commandType = null);

        /// <summary>
        /// Query a collection of entity by given sql async
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
        Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, TReturn>(string sql, Func<T1, T2, T3, T4, TReturn> map, object param = null, bool buffered = true,
            string splitOn = "Id", CommandType? commandType = null);

        /// <summary>
        /// Query a collection of entity by given sql async
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
        Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, T5, TReturn>(string sql, Func<T1, T2, T3, T4, T5, TReturn> map, object param = null, bool buffered = true,
            string splitOn = "Id", CommandType? commandType = null);

        /// <summary>
        /// Query a collection of entity by given sql async
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
        Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, T5, T6, TReturn>(string sql, Func<T1, T2, T3, T4, T5, T6, TReturn> map, object param = null, bool buffered = true,
            string splitOn = "Id", CommandType? commandType = null);

        /// <summary>
        /// Query a collection of entity by given sql async
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
        Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, T5, T6, T7, TReturn>(string sql, Func<T1, T2, T3, T4, T5, T6, T7, TReturn> map, object param = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null);

        #endregion

        #region Query with command definitions

        /// <summary>
        /// Query a collection of entity by given command definition
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        IEnumerable<object> Query(CommandDefinition command);

        /// <summary>
        /// Query a collection of entity by given command definition
        /// </summary>
        /// <param name="type"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        IEnumerable<object> Query(Type type, CommandDefinition command);

        /// <summary>
        /// Query a collection of entity by given command definition
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> Query<T>(CommandDefinition command);

        /// <summary>
        /// Query a collection of entity by given command definition async
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<IEnumerable<object>> QueryAsync(CommandDefinition command);

        /// <summary>
        /// Query a collection object by given command definition and type async
        /// </summary>
        /// <param name="type"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<IEnumerable<object>> QueryAsync(Type type, CommandDefinition command);

        /// <summary>
        /// Query a collection entity by given command definition async
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<IEnumerable<T>> QueryAsync<T>(CommandDefinition command);

        #endregion

        #region Query with command definitions and T2 ~ T7

        /// <summary>
        /// Query a collection of entity by given sql 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="splitOn"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        IEnumerable<T1> Query<T1, T2>(CommandDefinition command, string splitOn = "Id");

        /// <summary>
        /// Query a collection of entity by given sql 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="splitOn"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <returns></returns>
        IEnumerable<T1> Query<T1, T2, T3>(CommandDefinition command, string splitOn = "Id");

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
        IEnumerable<T1> Query<T1, T2, T3, T4>(CommandDefinition command, string splitOn = "Id");

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
        IEnumerable<T1> Query<T1, T2, T3, T4, T5>(CommandDefinition command, string splitOn = "Id");

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
        IEnumerable<T1> Query<T1, T2, T3, T4, T5, T6>(CommandDefinition command, string splitOn = "Id");

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
        IEnumerable<T1> Query<T1, T2, T3, T4, T5, T6, T7>(CommandDefinition command, string splitOn = "Id");

        /// <summary>
        /// Query a collection of entity by given sql async
        /// </summary>
        /// <param name="command"></param>
        /// <param name="splitOn"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        Task<IEnumerable<T1>> QueryAsync<T1, T2>(CommandDefinition command, string splitOn = "Id");

        /// <summary>
        /// Query a collection of entity by given sql async
        /// </summary>
        /// <param name="command"></param>
        /// <param name="splitOn"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <returns></returns>
        Task<IEnumerable<T1>> QueryAsync<T1, T2, T3>(CommandDefinition command, string splitOn = "Id");

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
        Task<IEnumerable<T1>> QueryAsync<T1, T2, T3, T4>(CommandDefinition command, string splitOn = "Id");

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
        Task<IEnumerable<T1>> QueryAsync<T1, T2, T3, T4, T5>(CommandDefinition command, string splitOn = "Id");

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
        Task<IEnumerable<T1>> QueryAsync<T1, T2, T3, T4, T5, T6>(CommandDefinition command, string splitOn = "Id");

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
        Task<IEnumerable<T1>> QueryAsync<T1, T2, T3, T4, T5, T6, T7>(CommandDefinition command, string splitOn = "Id");

        #endregion

        #region Query with command definitions and T2 ~ T7 and TReturn

        /// <summary>
        /// Query a collection of entity by given sql 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="map"></param>
        /// <param name="splitOn"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        IEnumerable<TReturn> Query<T1, T2, TReturn>(CommandDefinition command, Func<T1, T2, TReturn> map, string splitOn = "Id");

        /// <summary>
        /// Query a collection of entity by given sql 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="map"></param>
        /// <param name="splitOn"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        IEnumerable<TReturn> Query<T1, T2, T3, TReturn>(CommandDefinition command, Func<T1, T2, T3, TReturn> map, string splitOn = "Id");

        /// <summary>
        /// Query a collection of entity by given sql 
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
        IEnumerable<TReturn> Query<T1, T2, T3, T4, TReturn>(CommandDefinition command, Func<T1, T2, T3, T4, TReturn> map, string splitOn = "Id");

        /// <summary>
        /// Query a collection of entity by given sql
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
        IEnumerable<TReturn> Query<T1, T2, T3, T4, T5, TReturn>(CommandDefinition command, Func<T1, T2, T3, T4, T5, TReturn> map, string splitOn = "Id");

        /// <summary>
        /// Query a collection of entity by given sql
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
        IEnumerable<TReturn> Query<T1, T2, T3, T4, T5, T6, TReturn>(CommandDefinition command, Func<T1, T2, T3, T4, T5, T6, TReturn> map, string splitOn = "Id");

        /// <summary>
        /// Query a collection of entity by given sql
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
        IEnumerable<TReturn> Query<T1, T2, T3, T4, T5, T6, T7, TReturn>(CommandDefinition command, Func<T1, T2, T3, T4, T5, T6, T7, TReturn> map, string splitOn = "Id");

        /// <summary>
        /// Query a collection of entity by given sql async
        /// </summary>
        /// <param name="command"></param>
        /// <param name="map"></param>
        /// <param name="splitOn"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        Task<IEnumerable<TReturn>> QueryAsync<T1, T2, TReturn>(CommandDefinition command, Func<T1, T2, TReturn> map, string splitOn = "Id");

        /// <summary>
        /// Query a collection of entity by given sql async
        /// </summary>
        /// <param name="command"></param>
        /// <param name="map"></param>
        /// <param name="splitOn"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, TReturn>(CommandDefinition command, Func<T1, T2, T3, TReturn> map, string splitOn = "Id");

        /// <summary>
        /// Query a collection of entity by given sql async
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
        Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, TReturn>(CommandDefinition command, Func<T1, T2, T3, T4, TReturn> map, string splitOn = "Id");

        /// <summary>
        /// Query a collection of entity by given sql async
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
        Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, T5, TReturn>(CommandDefinition command, Func<T1, T2, T3, T4, T5, TReturn> map, string splitOn = "Id");

        /// <summary>
        /// Query a collection of entity by given sql async
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
        Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, T5, T6, TReturn>(CommandDefinition command, Func<T1, T2, T3, T4, T5, T6, TReturn> map, string splitOn = "Id");

        /// <summary>
        /// Query a collection of entity by given sql async
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
        Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, T5, T6, T7, TReturn>(CommandDefinition command, Func<T1, T2, T3, T4, T5, T6, T7, TReturn> map, string splitOn = "Id");

        #endregion

        #region Query First with many parameters

        /// <summary>
        /// Query first object by given sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        object QueryFirst(string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// Query first object by given sql and type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        object QueryFirst(Type type, string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// Query first entity by given sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T QueryFirst<T>(string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// Query first object by given sql async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        Task<object> QueryFirstAsync(string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// Query first object by given sql and type async
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        Task<object> QueryFirstAsync(Type type, string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// Query first entity by given sql async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> QueryFirstAsync<T>(string sql, object param = null, CommandType? commandType = null);

        #endregion

        #region Query First with command definitions

        /// <summary>
        /// Query first entity by given command definition
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        object QueryFirst(CommandDefinition command);

        /// <summary>
        /// Query first object by given command definition and type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        object QueryFirst(Type type, CommandDefinition command);

        /// <summary>
        /// Query first entity by given command definition
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T QueryFirst<T>(CommandDefinition command);

        /// <summary>
        /// Query first entity by given command definition async
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<object> QueryFirstAsync(CommandDefinition command);

        /// <summary>
        /// Query first object by given command definition and type async
        /// </summary>
        /// <param name="type"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<object> QueryFirstAsync(Type type, CommandDefinition command);

        /// <summary>
        /// Query first entity by given command definition async
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> QueryFirstAsync<T>(CommandDefinition command);

        #endregion

        #region Query First or Default with many parameters

        /// <summary>
        /// Query first object or default by given sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        object QueryFirstOrDefault(string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// Query first object or default by given sql and type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        object QueryFirstOrDefault(Type type, string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// Query first object or default by given sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T QueryFirstOrDefault<T>(string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// Query first object or default by given sql async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        Task<object> QueryFirstOrDefaultAsync(string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// Query first object or default by given sql and type async
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        Task<object> QueryFirstOrDefaultAsync(Type type, string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// Query first entity or default by given sql async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, CommandType? commandType = null);

        #endregion

        #region Query First or Default with command definitions

        /// <summary>
        /// Query first entity or default by given command definition
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        object QueryFirstOrDefault(CommandDefinition command);

        /// <summary>
        /// Query first object or default by given command definition and type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        object QueryFirstOrDefault(Type type, CommandDefinition command);

        /// <summary>
        /// Query first entity or default by given command definition
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T QueryFirstOrDefault<T>(CommandDefinition command);

        /// <summary>
        /// Query first entity or default by given command definition async
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<object> QueryFirstOrDefaultAsync(CommandDefinition command);

        /// <summary>
        /// Query first object or default by given command definition and type async
        /// </summary>
        /// <param name="type"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<object> QueryFirstOrDefaultAsync(Type type, CommandDefinition command);

        /// <summary>
        /// Query first entity or default by given command definition async
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> QueryFirstOrDefaultAsync<T>(CommandDefinition command);

        #endregion

        #region Query Single with many parameters

        /// <summary>
        /// Query single object by given sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        object QuerySingle(string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// Query single object by given sql and type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        object QuerySingle(Type type, string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// Query single entity by given sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T QuerySingle<T>(string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// Query single object by given sql async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        Task<object> QuerySingleAsync(string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// Query single object by given sql and type async
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        Task<object> QuerySingleAsync(Type type, string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// Query single entity by given sql async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> QuerySingleAsync<T>(string sql, object param = null, CommandType? commandType = null);

        #endregion

        #region Query Single with command definitions

        /// <summary>
        /// Query single entity by given command definition
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        object QuerySingle(CommandDefinition command);

        /// <summary>
        /// Query single object by given command definition and type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        object QuerySingle(Type type, CommandDefinition command);

        /// <summary>
        /// Query single entity by given command definition
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T QuerySingle<T>(CommandDefinition command);

        /// <summary>
        /// Query single entity by given command definition async
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<object> QuerySingleAsync(CommandDefinition command);

        /// <summary>
        /// Query single object by given command definition and type async
        /// </summary>
        /// <param name="type"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<object> QuerySingleAsync(Type type, CommandDefinition command);

        /// <summary>
        /// Query single entity by given command definition async
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> QuerySingleAsync<T>(CommandDefinition command);

        #endregion

        #region Query Single or Default with many parameters

        /// <summary>
        /// Query single object or default by given sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        object QuerySingleOrDefault(string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// Query single object or default by given sql and type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        object QuerySingleOrDefault(Type type, string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// Query single entity or default by given sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T QuerySingleOrDefault<T>(string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// Query single object or default by given sql and type async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        Task<object> QuerySingleOrDefaultAsync(string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// Query single object or default by given sql and type async
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        Task<object> QuerySingleOrDefaultAsync(Type type, string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// Query single entity or default by given sql async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> QuerySingleOrDefaultAsync<T>(string sql, object param = null, CommandType? commandType = null);

        #endregion

        #region Query Single or Default with command definitions

        /// <summary>
        /// Query single entity or default by given command definition
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        object QuerySingleOrDefault(CommandDefinition command);

        /// <summary>
        /// Query single object or default by given command definition and type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        object QuerySingleOrDefault(Type type, CommandDefinition command);

        /// <summary>
        /// Query single entity or default by given command definition
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T QuerySingleOrDefault<T>(CommandDefinition command);

        /// <summary>
        /// Query single entity or default by given command definition async
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<object> QuerySingleOrDefaultAsync(CommandDefinition command);

        /// <summary>
        /// Query single object or default by given command definition and type async
        /// </summary>
        /// <param name="type"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<object> QuerySingleOrDefaultAsync(Type type, CommandDefinition command);

        /// <summary>
        /// Query single entity or default by given command definition async
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> QuerySingleOrDefaultAsync<T>(CommandDefinition command);

        #endregion

        #region Query Multiple with many parameters

        /// <summary>
        /// Query multiple grid reader by given sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        SqlMapper.GridReader QueryMultiple(string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// Query multiple guild reader by given sql async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object param = null, CommandType? commandType = null);

        #endregion

        #region Query Multiple with command definitions

        /// <summary>
        /// Query multiple grid reader by given command definition
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        SqlMapper.GridReader QueryMultiple(CommandDefinition command);

        /// <summary>
        /// Query multiple guild reader by given command definition async
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<SqlMapper.GridReader> QueryMultipleAsync(CommandDefinition command);

        #endregion
    }
}