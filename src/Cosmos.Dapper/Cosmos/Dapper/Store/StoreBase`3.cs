using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Dapper.Core.Contextual;
using Cosmos.Data.Common;
using Cosmos.Models;

namespace Cosmos.Dapper.Store
{
    /// <summary>
    /// Store base
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class StoreBase<TContext, TEntity, TKey> : StoreBase<TContext, TEntity>, IStore<TEntity, TKey>
        where TContext : class, IDapperContext, IDbContext, IWithSQLGenerator
        where TEntity : class, IEntity<TKey>, new()
    {
        /// <summary>
        /// Store base
        /// </summary>
        /// <param name="context"></param>
        /// <param name="bindingExpression"></param>
        /// <param name="includeUnsafeOpt"></param>
        protected StoreBase(TContext context, Expression<Func<TContext, IDapperSet<TEntity>>> bindingExpression, bool includeUnsafeOpt)
            : base(context, bindingExpression, includeUnsafeOpt)
        {
            KeyType = typeof(TKey);
        }

        #region Key type

        /// <summary>
        /// Key type
        /// </summary>
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public Type KeyType { get; }

        #endregion

        #region Exist # in IStore.QueryableStore

        /// <summary>
        /// Exist entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ExistById(TKey id)
        {
            return FindById(id) != null;
        }

        /// <summary>
        /// Exist entity by id async
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> ExistByIdAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return await FindByIdAsync(id, cancellationToken) != null;
        }

        #endregion

        #region Find by id # in IStore.QueryableStore

        /// <summary>
        /// Find entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity FindById(TKey id)
        {
            return RawTypedContext.EntityOperators.Get<TEntity>(id, RepoLevelDataFilters);
        }

        /// <summary>
        /// Find entity by id async
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> FindByIdAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return RawTypedContext.EntityOperators.GetAsync<TEntity>(id, RepoLevelDataFilters, cancellationToken);
        }

        #endregion

        #region Get One # in base IStore.QueryableStore

        /// <summary>
        /// Get one or null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity GetOne(TKey id) => FindById(id);

        /// <summary>
        /// Get one or null
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> GetOneAsync(TKey id, CancellationToken cancellationToken = default) => FindByIdAsync(id, cancellationToken);

        #endregion

        #region Remove # in IStore.WriteableStore

        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual void Remove(TKey id)
        {
            var entity = FindById(id);
            if (entity is null)
                return;
            Remove(entity);
        }

        /// <summary>
        /// Remove async
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task RemoveAsync(TKey id, CancellationToken cancellationToken = default)
        {
            var entity = await FindByIdAsync(id, cancellationToken);
            if (entity is null)
                return;
            await RemoveAsync(entity, cancellationToken);
        }

        #endregion

        #region Remove unsafe

        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual void UnsafeRemove(TKey id)
        {
            var entity = FindById(id);
            if (entity is null)
                return;
            UnsafeRemove(entity);
        }

        /// <summary>
        /// Remove async
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task UnsafeRemoveAsync(TKey id, CancellationToken cancellationToken = default)
        {
            var entity = await FindByIdAsync(id, cancellationToken);
            if (entity is null)
                return;
            await UnsafeRemoveAsync(entity, cancellationToken);
        }

        #endregion
    }
}