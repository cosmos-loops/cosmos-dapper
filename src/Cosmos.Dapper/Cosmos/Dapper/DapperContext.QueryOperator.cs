using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Cosmos.Dapper
{
    /// <summary>
    /// Dapper context
    /// </summary>
    public abstract partial class DapperContext<TContext, TConnection>
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
        public virtual IEnumerable<object> Query(string sql, object param = null, bool buffered = true, CommandType? commandType = null) =>
            QueryOperators.Query(sql, param, buffered, commandType);

        /// <summary>
        /// Query a collection of object by given sql and type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="buffered"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual IEnumerable<object> Query(Type type, string sql, object param = null, bool buffered = true, CommandType? commandType = null) =>
            QueryOperators.Query(type, sql, param, buffered, commandType);

        /// <summary>
        /// Query a collection of entity by given sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="buffered"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual IEnumerable<T> Query<T>(string sql, object param = null, bool buffered = true, CommandType? commandType = null) =>
            QueryOperators.Query<T>(sql, param, buffered, commandType);

        /// <summary>
        /// Query a collection of entity by given sql async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual Task<IEnumerable<object>> QueryAsync(string sql, object param = null, CommandType? commandType = null) =>
            QueryOperators.QueryAsync(sql, param, commandType);

        /// <summary>
        /// Query a collection of object by given sql and type async
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual Task<IEnumerable<object>> QueryAsync(Type type, string sql, object param = null, CommandType? commandType = null) =>
            QueryOperators.QueryAsync(type, sql, param, commandType);

        /// <summary>
        /// Query a collection of entity by given sql async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, CommandType? commandType = null) =>
            QueryOperators.QueryAsync<T>(sql, param, commandType);

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
        public virtual IEnumerable<TReturn> Query<TReturn>(string sql, Type[] types, Func<object[], TReturn> map, object param = null, bool buffered = true, string splitOn = "Id",
            CommandType? commandType = null) =>
            QueryOperators.Query(sql, types, map, param, buffered, splitOn, commandType);

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
        public virtual Task<IEnumerable<TReturn>> QueryAsync<TReturn>(string sql, Type[] types, Func<object[], TReturn> map, object param = null, bool buffered = true,
            string splitOn = "Id", CommandType? commandType = null) =>
            QueryOperators.QueryAsync(sql, types, map, param, buffered, splitOn, commandType);

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
        public virtual IEnumerable<T1> Query<T1, T2>(string sql, dynamic param = null, bool buffered = true, string splitOn = "Id", CommandType? commandType = null) =>
            QueryOperators.Query<T1, T2>(sql, param, buffered, splitOn, commandType);

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
        public virtual IEnumerable<T1> Query<T1, T2, T3>(string sql, dynamic param = null, bool buffered = true, string splitOn = "Id", CommandType? commandType = null) =>
            QueryOperators.Query<T1, T2, T3>(sql, param, buffered, splitOn, commandType);

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
        public virtual IEnumerable<T1> Query<T1, T2, T3, T4>(string sql, dynamic param = null, bool buffered = true, string splitOn = "Id", CommandType? commandType = null) =>
            QueryOperators.Query<T1, T2, T3, T4>(sql, param, buffered, splitOn, commandType);

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
        public virtual IEnumerable<T1> Query<T1, T2, T3, T4, T5>(string sql, dynamic param = null, bool buffered = true, string splitOn = "Id", CommandType? commandType = null) =>
            QueryOperators.Query<T1, T2, T3, T4, T5>(sql, param, buffered, splitOn, commandType);

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
        public virtual IEnumerable<T1> Query<T1, T2, T3, T4, T5, T6>(string sql, dynamic param = null, bool buffered = true, string splitOn = "Id",
            CommandType? commandType = null) =>
            QueryOperators.Query<T1, T2, T3, T4, T5, T6>(sql, param, buffered, splitOn, commandType);

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
        public virtual IEnumerable<T1> Query<T1, T2, T3, T4, T5, T6, T7>(string sql, dynamic param = null, bool buffered = true, string splitOn = "Id",
            CommandType? commandType = null) =>
            QueryOperators.Query<T1, T2, T3, T4, T5, T6, T7>(sql, param, buffered, splitOn, commandType);

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
        public virtual Task<IEnumerable<T1>> QueryAsync<T1, T2>(string sql, dynamic param = null, bool buffered = true, string splitOn = "Id", CommandType? commandType = null) =>
            QueryOperators.QueryAsync<T1, T2>(sql, param, buffered, splitOn, commandType);

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
        public virtual Task<IEnumerable<T1>> QueryAsync<T1, T2, T3>(string sql, dynamic param = null, bool buffered = true, string splitOn = "Id",
            CommandType? commandType = null) =>
            QueryOperators.QueryAsync<T1, T2, T3>(sql, param, buffered, splitOn, commandType);

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
        public virtual Task<IEnumerable<T1>> QueryAsync<T1, T2, T3, T4>(string sql, dynamic param = null, bool buffered = true, string splitOn = "Id",
            CommandType? commandType = null) =>
            QueryOperators.QueryAsync<T1, T2, T3, T4>(sql, param, buffered, splitOn, commandType);

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
        public virtual Task<IEnumerable<T1>> QueryAsync<T1, T2, T3, T4, T5>(string sql, dynamic param = null, bool buffered = true, string splitOn = "Id",
            CommandType? commandType = null) =>
            QueryOperators.QueryAsync<T1, T2, T3, T4, T5>(sql, param, buffered, splitOn, commandType);

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
        public virtual Task<IEnumerable<T1>> QueryAsync<T1, T2, T3, T4, T5, T6>(string sql, dynamic param = null, bool buffered = true, string splitOn = "Id",
            CommandType? commandType = null) =>
            QueryOperators.QueryAsync<T1, T2, T3, T4, T5, T6>(sql, param, buffered, splitOn, commandType);

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
        public virtual Task<IEnumerable<T1>> QueryAsync<T1, T2, T3, T4, T5, T6, T7>(string sql, dynamic param = null, bool buffered = true, string splitOn = "Id",
            CommandType? commandType = null) =>
            QueryOperators.QueryAsync<T1, T2, T3, T4, T5, T6, T7>(sql, param, buffered, splitOn, commandType);

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
        public virtual IEnumerable<TReturn> Query<T1, T2, TReturn>(string sql, Func<T1, T2, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id",
            CommandType? commandType = null) =>
            QueryOperators.Query(sql, map, param, buffered, splitOn, commandType);

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
        public virtual IEnumerable<TReturn> Query<T1, T2, T3, TReturn>(string sql, Func<T1, T2, T3, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id",
            CommandType? commandType = null) =>
            QueryOperators.Query(sql, map, param, buffered, splitOn, commandType);

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
        public virtual IEnumerable<TReturn> Query<T1, T2, T3, T4, TReturn>(string sql, Func<T1, T2, T3, T4, TReturn> map, object param = null, bool buffered = true,
            string splitOn = "Id", CommandType? commandType = null) =>
            QueryOperators.Query(sql, map, param, buffered, splitOn, commandType);

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
        public virtual IEnumerable<TReturn> Query<T1, T2, T3, T4, T5, TReturn>(string sql, Func<T1, T2, T3, T4, T5, TReturn> map, object param = null, bool buffered = true,
            string splitOn = "Id", CommandType? commandType = null) =>
            QueryOperators.Query(sql, map, param, buffered, splitOn, commandType);

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
        public virtual IEnumerable<TReturn> Query<T1, T2, T3, T4, T5, T6, TReturn>(string sql, Func<T1, T2, T3, T4, T5, T6, TReturn> map, object param = null, bool buffered = true,
            string splitOn = "Id", CommandType? commandType = null) =>
            QueryOperators.Query(sql, map, param, buffered, splitOn, commandType);

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
        public virtual IEnumerable<TReturn> Query<T1, T2, T3, T4, T5, T6, T7, TReturn>(string sql, Func<T1, T2, T3, T4, T5, T6, T7, TReturn> map, object param = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null) =>
            QueryOperators.Query(sql, map, param, buffered, splitOn, commandType);

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
        public virtual Task<IEnumerable<TReturn>> QueryAsync<T1, T2, TReturn>(string sql, Func<T1, T2, TReturn> map, object param = null, bool buffered = true,
            string splitOn = "Id", CommandType? commandType = null) =>
            QueryOperators.QueryAsync(sql, map, param, buffered, splitOn, commandType);

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
        public virtual Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, TReturn>(string sql, Func<T1, T2, T3, TReturn> map, object param = null, bool buffered = true,
            string splitOn = "Id", CommandType? commandType = null) =>
            QueryOperators.QueryAsync(sql, map, param, buffered, splitOn, commandType);

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
        public virtual Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, TReturn>(string sql, Func<T1, T2, T3, T4, TReturn> map, object param = null, bool buffered = true,
            string splitOn = "Id", CommandType? commandType = null) =>
            QueryOperators.QueryAsync(sql, map, param, buffered, splitOn, commandType);

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
        public virtual Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, T5, TReturn>(string sql, Func<T1, T2, T3, T4, T5, TReturn> map, object param = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null) =>
            QueryOperators.QueryAsync(sql, map, param, buffered, splitOn, commandType);

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
        public virtual Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, T5, T6, TReturn>(string sql, Func<T1, T2, T3, T4, T5, T6, TReturn> map, object param = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null) =>
            QueryOperators.QueryAsync(sql, map, param, buffered, splitOn, commandType);

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
        public virtual Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, T5, T6, T7, TReturn>(string sql, Func<T1, T2, T3, T4, T5, T6, T7, TReturn> map, object param = null,
            bool buffered = true, string splitOn = "Id", CommandType? commandType = null) =>
            QueryOperators.QueryAsync(sql, map, param, buffered, splitOn, commandType);

        #endregion

        #region Query with command definitions

        /// <summary>
        /// Query a collection of entity by given command definition
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual IEnumerable<object> Query(CommandDefinition command) =>
            QueryOperators.Query(command);

        /// <summary>
        /// Query a collection of entity by given command definition
        /// </summary>
        /// <param name="type"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual IEnumerable<object> Query(Type type, CommandDefinition command) =>
            QueryOperators.Query(type, command);

        /// <summary>
        /// Query a collection of entity by given command definition
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual IEnumerable<T> Query<T>(CommandDefinition command) =>
            QueryOperators.Query<T>(command);

        /// <summary>
        /// Query a collection of entity by given command definition async
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual Task<IEnumerable<object>> QueryAsync(CommandDefinition command) =>
            QueryOperators.QueryAsync(command);

        /// <summary>
        /// Query a collection object by given command definition and type async
        /// </summary>
        /// <param name="type"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual Task<IEnumerable<object>> QueryAsync(Type type, CommandDefinition command) =>
            QueryOperators.QueryAsync(type, command);

        /// <summary>
        /// Query a collection entity by given command definition async
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual Task<IEnumerable<T>> QueryAsync<T>(CommandDefinition command) =>
            QueryOperators.QueryAsync<T>(command);

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
        public virtual IEnumerable<T1> Query<T1, T2>(CommandDefinition command, string splitOn = "Id") =>
            QueryOperators.Query<T1, T2>(command, splitOn);

        /// <summary>
        /// Query a collection of entity by given sql 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="splitOn"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <returns></returns>
        public virtual IEnumerable<T1> Query<T1, T2, T3>(CommandDefinition command, string splitOn = "Id") =>
            QueryOperators.Query<T1, T2, T3>(command, splitOn);

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
        public virtual IEnumerable<T1> Query<T1, T2, T3, T4>(CommandDefinition command, string splitOn = "Id") =>
            QueryOperators.Query<T1, T2, T3, T4>(command, splitOn);

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
        public virtual IEnumerable<T1> Query<T1, T2, T3, T4, T5>(CommandDefinition command, string splitOn = "Id") =>
            QueryOperators.Query<T1, T2, T3, T4, T5>(command, splitOn);

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
        public virtual IEnumerable<T1> Query<T1, T2, T3, T4, T5, T6>(CommandDefinition command, string splitOn = "Id") =>
            QueryOperators.Query<T1, T2, T3, T4, T5, T6>(command, splitOn);

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
        public virtual IEnumerable<T1> Query<T1, T2, T3, T4, T5, T6, T7>(CommandDefinition command, string splitOn = "Id") =>
            QueryOperators.Query<T1, T2, T3, T4, T5, T6, T7>(command, splitOn);

        /// <summary>
        /// Query a collection of entity by given sql async
        /// </summary>
        /// <param name="command"></param>
        /// <param name="splitOn"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        public virtual Task<IEnumerable<T1>> QueryAsync<T1, T2>(CommandDefinition command, string splitOn = "Id") =>
            QueryOperators.QueryAsync<T1, T2>(command, splitOn);

        /// <summary>
        /// Query a collection of entity by given sql async
        /// </summary>
        /// <param name="command"></param>
        /// <param name="splitOn"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <returns></returns>
        public virtual Task<IEnumerable<T1>> QueryAsync<T1, T2, T3>(CommandDefinition command, string splitOn = "Id") =>
            QueryOperators.QueryAsync<T1, T2, T3>(command, splitOn);

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
        public virtual Task<IEnumerable<T1>> QueryAsync<T1, T2, T3, T4>(CommandDefinition command, string splitOn = "Id") =>
            QueryOperators.QueryAsync<T1, T2, T3, T4>(command, splitOn);

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
        public virtual Task<IEnumerable<T1>> QueryAsync<T1, T2, T3, T4, T5>(CommandDefinition command, string splitOn = "Id") =>
            QueryOperators.QueryAsync<T1, T2, T3, T4, T5>(command, splitOn);

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
        public virtual Task<IEnumerable<T1>> QueryAsync<T1, T2, T3, T4, T5, T6>(CommandDefinition command, string splitOn = "Id") =>
            QueryOperators.QueryAsync<T1, T2, T3, T4, T5, T6>(command, splitOn);

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
        public virtual Task<IEnumerable<T1>> QueryAsync<T1, T2, T3, T4, T5, T6, T7>(CommandDefinition command, string splitOn = "Id") =>
            QueryOperators.QueryAsync<T1, T2, T3, T4, T5, T6, T7>(command, splitOn);

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
        public virtual IEnumerable<TReturn> Query<T1, T2, TReturn>(CommandDefinition command, Func<T1, T2, TReturn> map, string splitOn = "Id") =>
            QueryOperators.Query(command, map, splitOn);

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
        public virtual IEnumerable<TReturn> Query<T1, T2, T3, TReturn>(CommandDefinition command, Func<T1, T2, T3, TReturn> map, string splitOn = "Id") =>
            QueryOperators.Query(command, map, splitOn);

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
        public virtual IEnumerable<TReturn> Query<T1, T2, T3, T4, TReturn>(CommandDefinition command, Func<T1, T2, T3, T4, TReturn> map, string splitOn = "Id") =>
            QueryOperators.Query(command, map, splitOn);

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
        public virtual IEnumerable<TReturn> Query<T1, T2, T3, T4, T5, TReturn>(CommandDefinition command, Func<T1, T2, T3, T4, T5, TReturn> map, string splitOn = "Id") =>
            QueryOperators.Query(command, map, splitOn);

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
        public virtual IEnumerable<TReturn> Query<T1, T2, T3, T4, T5, T6, TReturn>(CommandDefinition command, Func<T1, T2, T3, T4, T5, T6, TReturn> map, string splitOn = "Id") =>
            QueryOperators.Query(command, map, splitOn);

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
        public virtual IEnumerable<TReturn> Query<T1, T2, T3, T4, T5, T6, T7, TReturn>(CommandDefinition command, Func<T1, T2, T3, T4, T5, T6, T7, TReturn> map,
            string splitOn = "Id") =>
            QueryOperators.Query(command, map, splitOn);

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
        public virtual Task<IEnumerable<TReturn>> QueryAsync<T1, T2, TReturn>(CommandDefinition command, Func<T1, T2, TReturn> map, string splitOn = "Id") =>
            QueryOperators.QueryAsync(command, map, splitOn);

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
        public virtual Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, TReturn>(CommandDefinition command, Func<T1, T2, T3, TReturn> map, string splitOn = "Id") =>
            QueryOperators.QueryAsync(command, map, splitOn);

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
        public virtual Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, TReturn>(CommandDefinition command, Func<T1, T2, T3, T4, TReturn> map, string splitOn = "Id") =>
            QueryOperators.QueryAsync(command, map, splitOn);

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
        public virtual Task<IEnumerable<TReturn>>
            QueryAsync<T1, T2, T3, T4, T5, TReturn>(CommandDefinition command, Func<T1, T2, T3, T4, T5, TReturn> map, string splitOn = "Id") =>
            QueryOperators.QueryAsync(command, map, splitOn);

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
        public virtual Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, T5, T6, TReturn>(CommandDefinition command, Func<T1, T2, T3, T4, T5, T6, TReturn> map,
            string splitOn = "Id") =>
            QueryOperators.QueryAsync(command, map, splitOn);

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
        public virtual Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, T5, T6, T7, TReturn>(CommandDefinition command, Func<T1, T2, T3, T4, T5, T6, T7, TReturn> map,
            string splitOn = "Id") =>
            QueryOperators.QueryAsync(command, map, splitOn);

        #endregion

        #region Query First with many parameters

        /// <summary>
        /// Query first object by given sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual object QueryFirst(string sql, object param = null, CommandType? commandType = null) =>
            QueryOperators.QueryFirst(sql, param, commandType);

        /// <summary>
        /// Query first object by given sql and type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual object QueryFirst(Type type, string sql, object param = null, CommandType? commandType = null) =>
            QueryOperators.QueryFirst(type, sql, param, commandType);

        /// <summary>
        /// Query first entity by given sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T QueryFirst<T>(string sql, object param = null, CommandType? commandType = null) =>
            QueryOperators.QueryFirst<T>(sql, param, commandType);

        /// <summary>
        /// Query first object by given sql async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual Task<object> QueryFirstAsync(string sql, object param = null, CommandType? commandType = null) =>
            QueryOperators.QueryFirstAsync(sql, param, commandType);

        /// <summary>
        /// Query first object by given sql and type async
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual Task<object> QueryFirstAsync(Type type, string sql, object param = null, CommandType? commandType = null) =>
            QueryOperators.QueryFirstAsync(type, sql, param, commandType);

        /// <summary>
        /// Query first entity by given sql async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual Task<T> QueryFirstAsync<T>(string sql, object param = null, CommandType? commandType = null) =>
            QueryOperators.QueryFirstAsync<T>(sql, param, commandType);

        #endregion

        #region Query First with command definitions

        /// <summary>
        /// Query first entity by given command definition
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual object QueryFirst(CommandDefinition command) =>
            QueryOperators.QueryFirst(command);

        /// <summary>
        /// Query first object by given command definition and type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual object QueryFirst(Type type, CommandDefinition command) =>
            QueryOperators.QueryFirst(type, command);

        /// <summary>
        /// Query first entity by given command definition
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T QueryFirst<T>(CommandDefinition command) =>
            QueryOperators.QueryFirst<T>(command);

        /// <summary>
        /// Query first entity by given command definition async
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual Task<object> QueryFirstAsync(CommandDefinition command) =>
            QueryOperators.QueryFirstAsync(command);

        /// <summary>
        /// Query first object by given command definition and type async
        /// </summary>
        /// <param name="type"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual Task<object> QueryFirstAsync(Type type, CommandDefinition command) =>
            QueryOperators.QueryFirstAsync(type, command);

        /// <summary>
        /// Query first entity by given command definition async
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual Task<T> QueryFirstAsync<T>(CommandDefinition command) =>
            QueryOperators.QueryFirstAsync<T>(command);

        #endregion

        #region Query First or Default with many parameters

        /// <summary>
        /// Query first object or default by given sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual object QueryFirstOrDefault(string sql, object param = null, CommandType? commandType = null) =>
            QueryOperators.QueryFirstOrDefault(sql, param, commandType);

        /// <summary>
        /// Query first object or default by given sql and type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual object QueryFirstOrDefault(Type type, string sql, object param = null, CommandType? commandType = null) =>
            QueryOperators.QueryFirstOrDefault(type, sql, param, commandType);

        /// <summary>
        /// Query first object or default by given sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T QueryFirstOrDefault<T>(string sql, object param = null, CommandType? commandType = null) =>
            QueryOperators.QueryFirstOrDefault<T>(sql, param, commandType);

        /// <summary>
        /// Query first object or default by given sql async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual Task<object> QueryFirstOrDefaultAsync(string sql, object param = null, CommandType? commandType = null) =>
            QueryOperators.QueryFirstOrDefaultAsync(sql, param, commandType);

        /// <summary>
        /// Query first object or default by given sql and type async
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual Task<object> QueryFirstOrDefaultAsync(Type type, string sql, object param = null, CommandType? commandType = null) =>
            QueryOperators.QueryFirstOrDefaultAsync(type, sql, param, commandType);

        /// <summary>
        /// Query first entity or default by given sql async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, CommandType? commandType = null) =>
            QueryOperators.QueryFirstOrDefaultAsync<T>(sql, param, commandType);

        #endregion

        #region Query First or Default with command definitions

        /// <summary>
        /// Query first entity or default by given command definition
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual object QueryFirstOrDefault(CommandDefinition command) =>
            QueryOperators.QueryFirstOrDefault(command);

        /// <summary>
        /// Query first object or default by given command definition and type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual object QueryFirstOrDefault(Type type, CommandDefinition command) =>
            QueryOperators.QueryFirstOrDefault(type, command);

        /// <summary>
        /// Query first entity or default by given command definition
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T QueryFirstOrDefault<T>(CommandDefinition command) =>
            QueryOperators.QueryFirstOrDefault<T>(command);

        /// <summary>
        /// Query first entity or default by given command definition async
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual Task<object> QueryFirstOrDefaultAsync(CommandDefinition command) =>
            QueryOperators.QueryFirstOrDefaultAsync(command);

        /// <summary>
        /// Query first object or default by given command definition and type async
        /// </summary>
        /// <param name="type"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual Task<object> QueryFirstOrDefaultAsync(Type type, CommandDefinition command) =>
            QueryOperators.QueryFirstOrDefaultAsync(type, command);

        /// <summary>
        /// Query first entity or default by given command definition async
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual Task<T> QueryFirstOrDefaultAsync<T>(CommandDefinition command) =>
            QueryOperators.QueryFirstOrDefaultAsync<T>(command);

        #endregion

        #region Query Single with many parameters

        /// <summary>
        /// Query single object by given sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual object QuerySingle(string sql, object param = null, CommandType? commandType = null) =>
            QueryOperators.QuerySingle(sql, param, commandType);

        /// <summary>
        /// Query single object by given sql and type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual object QuerySingle(Type type, string sql, object param = null, CommandType? commandType = null) =>
            QueryOperators.QuerySingle(type, sql, param, commandType);

        /// <summary>
        /// Query single entity by given sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T QuerySingle<T>(string sql, object param = null, CommandType? commandType = null) =>
            QueryOperators.QuerySingle<T>(sql, param, commandType);

        /// <summary>
        /// Query single object by given sql async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual Task<object> QuerySingleAsync(string sql, object param = null, CommandType? commandType = null) =>
            QueryOperators.QuerySingleAsync(sql, param, commandType);

        /// <summary>
        /// Query single object by given sql and type async
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual Task<object> QuerySingleAsync(Type type, string sql, object param = null, CommandType? commandType = null) =>
            QueryOperators.QuerySingleAsync(type, sql, param, commandType);

        /// <summary>
        /// Query single entity by given sql async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual Task<T> QuerySingleAsync<T>(string sql, object param = null, CommandType? commandType = null) =>
            QueryOperators.QuerySingleAsync<T>(sql, param, commandType);

        #endregion

        #region Query Single with command definitions

        /// <summary>
        /// Query single entity by given command definition
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual object QuerySingle(CommandDefinition command) =>
            QueryOperators.QuerySingle(command);

        /// <summary>
        /// Query single object by given command definition and type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual object QuerySingle(Type type, CommandDefinition command) =>
            QueryOperators.QuerySingle(type, command);

        /// <summary>
        /// Query single entity by given command definition
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T QuerySingle<T>(CommandDefinition command) =>
            QueryOperators.QuerySingle<T>(command);

        /// <summary>
        /// Query single entity by given command definition async
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual Task<object> QuerySingleAsync(CommandDefinition command) =>
            QueryOperators.QuerySingleAsync(command);

        /// <summary>
        /// Query single object by given command definition and type async
        /// </summary>
        /// <param name="type"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual Task<object> QuerySingleAsync(Type type, CommandDefinition command) =>
            QueryOperators.QuerySingleAsync(type, command);

        /// <summary>
        /// Query single entity by given command definition async
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual Task<T> QuerySingleAsync<T>(CommandDefinition command) =>
            QueryOperators.QuerySingleAsync<T>(command);

        #endregion

        #region Query Single or Default with many parameters

        /// <summary>
        /// Query single object or default by given sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual object QuerySingleOrDefault(string sql, object param = null, CommandType? commandType = null) =>
            QueryOperators.QuerySingleOrDefault(sql, param, commandType);

        /// <summary>
        /// Query single object or default by given sql and type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual object QuerySingleOrDefault(Type type, string sql, object param = null, CommandType? commandType = null) =>
            QueryOperators.QuerySingleOrDefault(type, sql, param, commandType);

        /// <summary>
        /// Query single entity or default by given sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T QuerySingleOrDefault<T>(string sql, object param = null, CommandType? commandType = null) =>
            QueryOperators.QuerySingleOrDefault<T>(sql, param, commandType);

        /// <summary>
        /// Query single object or default by given sql and type async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual Task<object> QuerySingleOrDefaultAsync(string sql, object param = null, CommandType? commandType = null) =>
            QueryOperators.QuerySingleOrDefaultAsync(sql, param, commandType);

        /// <summary>
        /// Query single object or default by given sql and type async
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual Task<object> QuerySingleOrDefaultAsync(Type type, string sql, object param = null, CommandType? commandType = null) =>
            QueryOperators.QuerySingleOrDefaultAsync(type, sql, param, commandType);

        /// <summary>
        /// Query single entity or default by given sql async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual Task<T> QuerySingleOrDefaultAsync<T>(string sql, object param = null, CommandType? commandType = null) =>
            QueryOperators.QuerySingleOrDefaultAsync<T>(sql, param, commandType);

        #endregion

        #region Query Single or Default with command definitions

        /// <summary>
        /// Query single entity or default by given command definition
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual object QuerySingleOrDefault(CommandDefinition command) =>
            QueryOperators.QuerySingleOrDefault(command);

        /// <summary>
        /// Query single object or default by given command definition and type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual object QuerySingleOrDefault(Type type, CommandDefinition command) =>
            QueryOperators.QuerySingleOrDefault(type, command);

        /// <summary>
        /// Query single entity or default by given command definition
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T QuerySingleOrDefault<T>(CommandDefinition command) =>
            QueryOperators.QuerySingleOrDefault<T>(command);

        /// <summary>
        /// Query single entity or default by given command definition async
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual Task<object> QuerySingleOrDefaultAsync(CommandDefinition command) =>
            QueryOperators.QuerySingleOrDefaultAsync(command);

        /// <summary>
        /// Query single object or default by given command definition and type async
        /// </summary>
        /// <param name="type"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual Task<object> QuerySingleOrDefaultAsync(Type type, CommandDefinition command) =>
            QueryOperators.QuerySingleOrDefaultAsync(type, command);

        /// <summary>
        /// Query single entity or default by given command definition async
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual Task<T> QuerySingleOrDefaultAsync<T>(CommandDefinition command) =>
            QueryOperators.QuerySingleOrDefaultAsync<T>(command);

        #endregion

        #region Query Multiple with many parameters

        /// <summary>
        /// Query multiple grid reader by given sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual SqlMapper.GridReader QueryMultiple(string sql, object param = null, CommandType? commandType = null) =>
            QueryOperators.QueryMultiple(sql, param, commandType);

        /// <summary>
        /// Query multiple guild reader by given sql async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object param = null, CommandType? commandType = null) =>
            QueryOperators.QueryMultipleAsync(sql, param, commandType);

        #endregion

        #region Query Multiple with command definitions

        /// <summary>
        /// Query multiple grid reader by given command definition
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual SqlMapper.GridReader QueryMultiple(CommandDefinition command) =>
            QueryOperators.QueryMultiple(command);

        /// <summary>
        /// Query multiple guild reader by given command definition async
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual Task<SqlMapper.GridReader> QueryMultipleAsync(CommandDefinition command) =>
            QueryOperators.QueryMultipleAsync(command);

        #endregion
    }
}