using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Cosmos.Data.SqlKata
{
    /// <summary>
    /// Extensions for entity query builder
    /// </summary>
    public static class EntityQueryBuilderExtensions
    {
        /// <summary>
        /// Find one
        /// </summary>
        /// <param name="query"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T FindOne<T>(this EntityQueryBuilder query, IDbTransaction transaction, CommandType? commandType = null)
            => query.FindOne<T>(transaction, commandType);

        /// <summary>
        /// Find one async
        /// </summary>
        /// <param name="query"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<T> FindOneAsync<T>(this EntityQueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
            => await query.FindOneAsync<T>(transaction, commandType);

        /// <summary>
        /// Unique result to int
        /// </summary>
        /// <param name="query"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static int UniqueResultToInt(this EntityQueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
            => UniqueResult<int>(query, transaction, commandType);

        /// <summary>
        /// Unique result to int async
        /// </summary>
        /// <param name="query"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static async Task<int> UniqueResultToIntAsync(this EntityQueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
            => await UniqueResultAsync<int>(query, transaction, commandType);

        /// <summary>
        /// Unique result to long
        /// </summary>
        /// <param name="query"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static long UniqueResultToLong(this EntityQueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
            => UniqueResult<long>(query, transaction, commandType);

        /// <summary>
        /// Unique result to long async
        /// </summary>
        /// <param name="query"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static async Task<long> UniqueResultToLongAsync(this EntityQueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
            => await UniqueResultAsync<long>(query, transaction, commandType);

        /// <summary>
        /// Unique result
        /// </summary>
        /// <param name="query"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T UniqueResult<T>(this EntityQueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
            => query.UniqueResult<T>(transaction, commandType);

        /// <summary>
        /// Unique result async
        /// </summary>
        /// <param name="query"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<T> UniqueResultAsync<T>(this EntityQueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
            => await query.UniqueResultAsync<T>(transaction, commandType);

        /// <summary>
        /// List...
        /// </summary>
        /// <param name="query"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> List<T>(this EntityQueryBuilder query, IDbTransaction transaction = null, bool buffered = true, CommandType? commandType = null)
            => query.List<T>(transaction, buffered, commandType);

        /// <summary>
        /// List async...
        /// </summary>
        /// <param name="query"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> ListAsync<T>(this EntityQueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
            => await query.ListAsync<T>(transaction, commandType);

        /// <summary>
        /// Update...
        /// </summary>
        /// <param name="query"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static bool Update(this EntityQueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
            => query.SaveUpdate(transaction, commandType);

        /// <summary>
        /// Update async...
        /// </summary>
        /// <param name="query"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static async Task<bool> UpdateAsync(this EntityQueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
            => await query.SaveUpdateAsync(transaction, commandType);

        /// <summary>
        /// Insert...
        /// </summary>
        /// <param name="query"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static bool Insert(this EntityQueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
            => query.SaveInsert(transaction, commandType);

        /// <summary>
        /// Insert async...
        /// </summary>
        /// <param name="query"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static async Task<bool> InsertAsync(this EntityQueryBuilder query, IDbTransaction transaction = null, CommandType? commandType = null)
            => await query.SaveInsertAsync(transaction, commandType);
    }
}