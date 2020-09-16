using System;
using System.Linq.Expressions;
using Cosmos.Dapper.Core.Contextual;
using Cosmos.Dapper.Core.DataFiltering;
using Cosmos.Data.Common;
using Cosmos.Data.Statements;
using Cosmos.Data.Store;
using Cosmos.Models;

namespace Cosmos.Dapper.Store
{
    /// <summary>
    /// Repository base
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class RepositoryBase<TContext, TEntity, TKey> : StoreBase<TContext, TEntity, TKey>, IRepository
        where TContext : class, IDapperContext, IDbContext, IWithSQLGenerator
        where TEntity : class, IEntity<TKey>, new()
    {
        /// <summary>
        /// Create a new instance of <see cref="RepositoryBase{TContext, TEntity, TKey}" />
        /// </summary>
        /// <param name="context"></param>
        /// <param name="bindingExpression"></param>
        /// <param name="includeUnsafeOpt"></param>
        protected RepositoryBase(TContext context, Expression<Func<TContext, IDapperSet<TEntity>>> bindingExpression, bool includeUnsafeOpt = true)
            : base(context, bindingExpression, includeUnsafeOpt) { }

        /// <summary>
        /// Create repository level data filter cache
        /// </summary>
        /// <returns></returns>
        protected override ISQLPredicate CreateRepoLevelDataFilterCache()
        {
            var filter = new RepositoryLevelDataFilteringStrategy<TEntity>(GetType(), FilteringExpression);
            var signature = filter.GetSignature();

            if (!RepoLevelDataFilterManager.IsContainerKey(signature))
            {
                RepoLevelDataFilterManager.Register(signature, filter.GetFilteringPredicate());
            }

            return RepoLevelDataFilterManager.GetFilter(signature);
        }
        
        /// <inheritdoc />
        public string CurrentTraceId { get; set; }

        /// <inheritdoc />
        public IUnitOfWorkEntry UnitOfWork { get; set; }
    }
}