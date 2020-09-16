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
    /// <typeparam name="TEntity"></typeparam>
    // ReSharper disable once InconsistentNaming
    public interface ISQLActionEntry<TEntity> : ISQLActionEntry where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        InsertAction<TEntity> Insert(TEntity entity);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <returns></returns>
        UpdateAction<TEntity> Update(TEntity entity, bool ignoreAllKeyProperties = false);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        DeleteAction<TEntity> Delete(TEntity entity);

        /// <summary>
        /// Batch insert
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        BatchInsertAction<TEntity> BatchInsert(IEnumerable<TEntity> entities);

        /// <summary>
        /// Batch update
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <returns></returns>
        BatchUpdateAction<TEntity> BatchUpdate(IEnumerable<TEntity> entities, bool ignoreAllKeyProperties = false);

        /// <summary>
        /// Batch delete
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        BatchDeleteAction<TEntity> BatchDelete(IEnumerable<TEntity> entities);
    }
}