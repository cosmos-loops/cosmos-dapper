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
    // ReSharper disable once InconsistentNaming
    public interface ISQLActionAsyncEntry
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        AsynchronousInsertAction<TEntity> Insert<TEntity>(TEntity entity) where TEntity : class, IEntity, new();

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        AsynchronousUpdateAction<TEntity> Update<TEntity>(TEntity entity, bool ignoreAllKeyProperties = false) where TEntity : class, IEntity, new();

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        AsynchronousDeleteAction<TEntity> Delete<TEntity>(TEntity entity) where TEntity : class, IEntity, new();

        /// <summary>
        /// Batch insert
        /// </summary>
        /// <param name="entities"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        AsynchronousBatchInsertAction<TEntity> BatchInsert<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity, new();

        /// <summary>
        /// Batch update
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        AsynchronousBatchUpdateAction<TEntity> BatchUpdate<TEntity>(IEnumerable<TEntity> entities, bool ignoreAllKeyProperties = false) where TEntity : class, IEntity, new();

        /// <summary>
        /// Batch delete
        /// </summary>
        /// <param name="entities"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        AsynchronousBatchDeleteAction<TEntity> BatchDelete<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity, new();
    }
}