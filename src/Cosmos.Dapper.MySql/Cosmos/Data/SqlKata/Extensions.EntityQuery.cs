using System.Data;
using System.Threading.Tasks;

namespace Cosmos.Data.SqlKata
{
    /// <summary>
    /// Extensions for entity query
    /// </summary>
    public static class EntityQueryExtensions
    {
        /// <summary>
        /// Save insert
        /// </summary>
        /// <param name="query"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T SaveInsert<T>(this EntityQueryBuilder query,
            IDbTransaction transaction = null, CommandType? commandType = null) where T : struct
            => query.SaveInsertForMysql<T>(transaction, commandType);

        /// <summary>
        /// Save insert async
        /// </summary>
        /// <param name="query"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<T> SaveInsertAsync<T>(this EntityQueryBuilder query,
            IDbTransaction transaction = null, CommandType? commandType = null) where T : struct
            => await query.SaveInsertForMysqlAsync<T>(transaction, commandType);
    }
}