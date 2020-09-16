using System;
using System.Linq.Expressions;
using Cosmos.Dapper.Core.Contextual;
using Cosmos.Data.Common;
using Cosmos.Models;

namespace Cosmos.Dapper.Store
{
    /// <summary>
    /// Repository base
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class RepositoryBase<TContext, TEntity> : StoreBase<TContext, TEntity>, IRepository
        where TContext : class, IDapperContext, IDbContext, IWithSQLGenerator
        where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Create a new instance of <see cref="RepositoryBase{TContext, TEntity}" />
        /// </summary>
        /// <param name="context"></param>
        /// <param name="bindingExpression"></param>
        /// <param name="includeUnsafeOpt"></param>
        protected RepositoryBase(TContext context, Expression<Func<TContext, IDapperSet<TEntity>>> bindingExpression, bool includeUnsafeOpt = true)
            : base(context, bindingExpression, includeUnsafeOpt) { }

        /// <inheritdoc />
        public string CurrentTraceId { get; set; }

        /// <inheritdoc />
        public IUnitOfWorkEntry UnitOfWork { get; set; }
    }
}