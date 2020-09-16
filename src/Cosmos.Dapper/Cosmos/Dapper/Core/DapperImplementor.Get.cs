using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Data.Statements;

namespace Cosmos.Dapper.Core
{
    public partial class DapperImplementor
    {
        #region Get one entity by dynamic id

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>(IDbConnection connection, dynamic id, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class
        {
            var classMap = GetClassMap<T>();
            var predicate = GetIdPredicate(classMap, id).Join(filters);
            var list = ExecuteQueryListCommand<T>(connection, classMap, predicate, null, transaction, Options.Timeout, true);
            return list.SingleOrDefault();
        }

        /// <summary>
        /// Get async
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(IDbConnection connection, dynamic id, IDbTransaction transaction = null, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class
        {
            var classMap = GetClassMap<T>();
            var predicate = GetIdPredicate(classMap, id).Join(filters);
            var list = await ExecuteQueryListCommandAsync<T>(connection, classMap, predicate, null, transaction, Options.Timeout, cancellationToken);
            return list.SingleOrDefault();
        }

        #endregion
    }
}