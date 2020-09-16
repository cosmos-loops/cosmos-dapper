using System.Collections.Generic;
using Cosmos.Dapper.Actions.Delete;
using Cosmos.Dapper.Actions.Insert;
using Cosmos.Dapper.Actions.Update;
using Cosmos.Models;

namespace Cosmos.Dapper.Actions
{
    /// <summary>
    /// Interface for SqlAction entry
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public interface ISQLActionEntry
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        InsertAction<TEntity> Insert<TEntity>(TEntity entity) where TEntity : class, IEntity, new();

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        UpdateAction<TEntity> Update<TEntity>(TEntity entity, bool ignoreAllKeyProperties = false) where TEntity : class, IEntity, new();

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        DeleteAction<TEntity> Delete<TEntity>(TEntity entity) where TEntity : class, IEntity, new();

        /// <summary>
        /// Batch insert
        /// </summary>
        /// <param name="entities"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        BatchInsertAction<TEntity> BatchInsert<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity, new();

        /// <summary>
        /// Batch update
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        BatchUpdateAction<TEntity> BatchUpdate<TEntity>(IEnumerable<TEntity> entities, bool ignoreAllKeyProperties = false) where TEntity : class, IEntity, new();

        /// <summary>
        /// Batch delete
        /// </summary>
        /// <param name="entities"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        BatchDeleteAction<TEntity> BatchDelete<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity, new();
    }
}