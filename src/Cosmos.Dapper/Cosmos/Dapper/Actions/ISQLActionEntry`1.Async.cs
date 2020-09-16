using System.Collections.Generic;
using Cosmos.Dapper.Actions.Delete;
using Cosmos.Dapper.Actions.Insert;
using Cosmos.Dapper.Actions.Update;
using Cosmos.Models;

namespace Cosmos.Dapper.Actions
{
    /// <summary>
    /// Interface for asynchronous SqlAction entry
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    // ReSharper disable once InconsistentNaming
    public interface ISQLActionAsyncEntry<TEntity> : ISQLActionAsyncEntry where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        AsynchronousInsertAction<TEntity> Insert(TEntity entity);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <returns></returns>
        AsynchronousUpdateAction<TEntity> Update(TEntity entity, bool ignoreAllKeyProperties = false);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        AsynchronousDeleteAction<TEntity> Delete(TEntity entity);

        /// <summary>
        /// Batch insert
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        AsynchronousBatchInsertAction<TEntity> BatchInsert(IEnumerable<TEntity> entities);

        /// <summary>
        /// Batch update
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <returns></returns>
        AsynchronousBatchUpdateAction<TEntity> BatchUpdate(IEnumerable<TEntity> entities, bool ignoreAllKeyProperties = false);

        /// <summary>
        /// Batch delete
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        AsynchronousBatchDeleteAction<TEntity> BatchDelete(IEnumerable<TEntity> entities);
    }
}