using System;
using System.Linq.Expressions;
using Cosmos.Dapper.Core.DynamicQuery;
using Cosmos.Data.Statements;
using Cosmos.Models;

namespace Cosmos.Dapper.Core.DataFiltering
{
    /// <summary>
    /// Repository level data filter strategy
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class RepositoryLevelDataFilteringStrategy<TEntity> : IDataFilteringStrategy where TEntity : class, IEntity
    {
        /// <summary>
        /// Gets type of repository
        /// </summary>
        public Type TypeOfRepository { get; }

        /// <summary>
        /// Gets type of entity
        /// </summary>
        public Type TypeOfEntity { get; }

        /// <summary>
        /// Gets predicate expresson
        /// </summary>
        public Expression<Func<TEntity, bool>> PredicateExpression { get; }

        internal RepositoryLevelDataFilteringStrategy(
            Type typeOfRepository,
            Expression<Func<TEntity, bool>> predicateExpression)
        {
            TypeOfRepository = typeOfRepository ?? throw new ArgumentNullException(nameof(typeOfRepository));
            TypeOfEntity = typeof(TEntity);
            PredicateExpression = predicateExpression;
        }

        /// <summary>
        /// Gets filting predicate
        /// </summary>
        /// <returns></returns>
        public ISQLPredicate GetFilteringPredicate()
        {
            return DynamicExpressionResolver.ResolveExprTree(PredicateExpression);
        }

        /// <summary>
        /// Gets signature
        /// </summary>
        /// <returns></returns>
        public (Type, Type) GetSignature() => (TypeOfRepository, TypeOfEntity);
    }
}