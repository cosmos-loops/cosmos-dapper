using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Collections;
using Cosmos.Models.Descriptors.EntityDescriptors;

// ReSharper disable SuspiciousTypeConversion.Global

namespace Cosmos.Dapper.Store
{
    // ReSharper disable once UnusedTypeParameter
    public abstract partial class StoreBase<TContext, TEntity>
    {
        #region Remove # in base IStore.WriteableStore

        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual void Remove(TEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            InternalRemove(entity, IncludeUnsafeOpt);
        }

        /// <summary>
        /// Remove range
        /// </summary>
        /// <param name="entities"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual void Remove(IEnumerable<TEntity> entities)
        {
            if (entities is null)
                throw new ArgumentNullException(nameof(entities));

            InternalRemove(entities, IncludeUnsafeOpt);
        }

        /// <summary>
        /// Remove async
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual async Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            await InternalRemoveAsync(entity, IncludeUnsafeOpt, cancellationToken);
        }

        /// <summary>
        /// Remove range async
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual async Task RemoveAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            if (entities is null)
                throw new ArgumentNullException(nameof(entities));

            await InternalRemoveAsync(entities, IncludeUnsafeOpt, cancellationToken);
        }

        #endregion

        #region Remove # in IStore.WriteableStore.DynamicExpression

        /// <summary>
        /// Remove by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual void Remove(Expression<Func<TEntity, bool>> predicate)
        {
            if (DeletableEntity)
            {
                var entities = Find(predicate);
                InternalRemove(entities);
            }
        }

        /// <summary>
        /// Remove by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task RemoveAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            if (DeletableEntity)
            {
                var entities = await FindAsync(predicate, cancellationToken);
                await InternalRemoveAsync(entities, false, cancellationToken);
            }
        }

        #endregion

        #region Remove # in IStore.WriteableStore.Predicates

        /// <summary>
        /// Remove by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual void Remove(object predicate)
        {
            if (DeletableEntity)
            {
                var entities = Find(predicate, null);
                InternalRemove(entities);
            }
        }

        /// <summary>
        /// Remove by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task RemoveAsync(object predicate, CancellationToken cancellationToken = default)
        {
            if (DeletableEntity)
            {
                var entities = await FindAsync(predicate, null, cancellationToken);
                await InternalRemoveAsync(entities, false, cancellationToken);
            }
        }

        #endregion

        #region Remove by Id # in IStore.IStore.WriteableStore.Predicates

        /// <summary>
        /// Remove entity by given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual void RemoveById(dynamic id)
        {
            if (FindById(id) is TEntity entity)
            {
                InternalRemove(entity, IncludeUnsafeOpt);
            }
        }

        /// <summary>
        /// Remove entity by given id async
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task RemoveByIdAsync(dynamic id, CancellationToken cancellationToken = default)
        {
            if (await FindByIdAsync(id, cancellationToken) is TEntity entity)
            {
                await InternalRemoveAsync(entity, IncludeUnsafeOpt, cancellationToken);
            }
        }

        #endregion

        #region Internal remove

        private void InternalRemove(TEntity entity, bool includeUnsafeOpt = false)
        {
            if (entity is null)
            {
                return;
            }

            if (includeUnsafeOpt)
            {
                RawTypedContext.EntityOperators.Delete(entity, RepoLevelDataFilters);
                return;
            }

            if (entity is IDeletable model)
            {
                model.IsDeleted = true;
                RawTypedContext.EntityOperators.Update(entity, RepoLevelDataFilters);
            }
        }

        private async Task InternalRemoveAsync(TEntity entity, bool includeUnsafeOpt = false, CancellationToken cancellationToken = default)
        {
            if (entity is null)
            {
                return;
            }

            if (includeUnsafeOpt)
            {
                await RawTypedContext.EntityOperators.DeleteAsync(entity, RepoLevelDataFilters, cancellationToken);
                return;
            }

            if (entity is IDeletable model)
            {
                model.IsDeleted = true;
                await RawTypedContext.EntityOperators.UpdateAsync(entity, RepoLevelDataFilters, cancellationToken: cancellationToken);
            }
        }

        private void InternalRemove(IEnumerable<TEntity> entities, bool includeUnsafeOpt = false)
        {
            if (entities is null)
                throw new ArgumentNullException(nameof(entities));

            // ReSharper disable once PossibleMultipleEnumeration
            if (!entities.Any())
                return;

            if (includeUnsafeOpt)
            {
                // ReSharper disable once PossibleMultipleEnumeration
                RawTypedContext.EntityOperators.Delete(entities, RepoLevelDataFilters);
                return;
            }

            if (entities is IEnumerable<IDeletable> models)
            {
                models.ForEach(model => model.IsDeleted = true);

                // ReSharper disable once PossibleMultipleEnumeration
                RawTypedContext.EntityOperators.Update(entities, RepoLevelDataFilters);
            }
        }

        private async Task InternalRemoveAsync(IEnumerable<TEntity> entities, bool includeUnsafeOpt = false, CancellationToken cancellationToken = default)
        {
            if (entities is null)
                throw new ArgumentNullException(nameof(entities));

            // ReSharper disable once PossibleMultipleEnumeration
            if (!entities.Any())
                return;

            if (includeUnsafeOpt)
            {
                // ReSharper disable once PossibleMultipleEnumeration
                await RawTypedContext.EntityOperators.DeleteAsync(entities, RepoLevelDataFilters, cancellationToken);
                return;
            }

            if (entities is IEnumerable<IDeletable> models)
            {
                models.ForEach(model => model.IsDeleted = true);

                // ReSharper disable once PossibleMultipleEnumeration
                await RawTypedContext.EntityOperators.UpdateAsync(entities, RepoLevelDataFilters, cancellationToken: cancellationToken);
            }
        }

        #endregion
    }
}