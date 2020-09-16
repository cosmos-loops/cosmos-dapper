using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Data.SqlKata;
using Cosmos.Models;
using Cosmos.Validation.Parameters;

namespace Cosmos.Dapper.Store
{
    /// <summary>
    /// Interface for writeable store
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IWriteableStore<in TEntity> where TEntity : class, IEntity, new()
    {
        #region Add

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add([NotNull] Func<QueryBuilder, QueryBuilder> sqlKataFunc, [NotNull] TEntity data);

        /// <summary>
        /// Add async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<int> AddAsync([NotNull] Func<QueryBuilder, QueryBuilder> sqlKataFunc, [NotNull] TEntity data);

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <param name="columnNames"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add([NotNull] Func<QueryBuilder, QueryBuilder> sqlKataFunc, [NotNull] IEnumerable<string> columnNames, [NotNull] IEnumerable<IEnumerable<object>> data);

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <param name="newValues"></param>
        /// <returns></returns>
        int Add([NotNull] Func<QueryBuilder, QueryBuilder> sqlKataFunc, [NotNull] IReadOnlyDictionary<string, object> newValues);

        /// <summary>
        /// Add async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <param name="newValues"></param>
        /// <returns></returns>
        Task<int> AddAsync([NotNull] Func<QueryBuilder, QueryBuilder> sqlKataFunc, [NotNull] IReadOnlyDictionary<string, object> newValues);

        #endregion

        #region Update

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <param name="newValues"></param>
        /// <returns></returns>
        int Update([NotNull] Func<QueryBuilder, QueryBuilder> sqlKataFunc, [NotNull] object newValues);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <param name="newValues"></param>
        /// <returns></returns>
        int Update([NotNull] Func<QueryBuilder, QueryBuilder> sqlKataFunc, [NotNull] IReadOnlyDictionary<string, object> newValues);

        /// <summary>
        /// Update async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <param name="newValues"></param>
        /// <returns></returns>
        Task<int> UpdateAsync([NotNull] Func<QueryBuilder, QueryBuilder> sqlKataFunc, [NotNull] object newValues);

        /// <summary>
        /// Update async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <param name="newValues"></param>
        /// <returns></returns>
        Task<int> UpdateAsync([NotNull] Func<QueryBuilder, QueryBuilder> sqlKataFunc, [NotNull] IReadOnlyDictionary<string, object> newValues);

        #endregion

        #region Remove unsafe

        /// <summary>
        /// Unsafe remove
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        void UnsafeRemove([NotNull] Func<QueryBuilder, QueryBuilder> sqlKataFunc);

        /// <summary>
        /// Unsafe remove async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        Task UnsafeRemoveAsync([NotNull] Func<QueryBuilder, QueryBuilder> sqlKataFunc);

        #endregion
    }

    /// <summary>
    /// Interface for queryable store
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IWriteableStore<TEntity, in TKey> : IWriteableStore<TEntity>
        where TEntity : class, IEntity<TKey>, new()
    {
        #region Remove

        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void Remove(TKey id);

        /// <summary>
        /// Remove async
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RemoveAsync(TKey id, CancellationToken cancellationToken = default);

        #endregion

        #region Remove unsafe

        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void UnsafeRemove(TKey id);

        /// <summary>
        /// Remove async
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UnsafeRemoveAsync(TKey id, CancellationToken cancellationToken = default);

        #endregion
    }
}