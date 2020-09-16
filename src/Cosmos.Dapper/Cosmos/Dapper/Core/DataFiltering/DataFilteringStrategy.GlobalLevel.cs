using System;
using System.Linq.Expressions;
using Cosmos.Dapper.Core.DynamicQuery;
using Cosmos.Data.Statements;
using Cosmos.Models;

namespace Cosmos.Dapper.Core.DataFiltering
{
    /// <summary>
    /// Global level data filter strategy
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class GlobalLevelDataFilteringStrategy<TEntity> : IDataFilteringStrategy where TEntity : class, IEntity
    {
        /// <summary>
        /// Gets type of entity
        /// </summary>
        public Type TypeOfEntity { get; }

        /// <summary>
        /// Gets predicate expression
        /// </summary>
        public Expression<Func<TEntity, bool>> PredicateExpression { get; }

        internal GlobalLevelDataFilteringStrategy(
            Expression<Func<TEntity, bool>> predicateExpression)
        {
            TypeOfEntity = typeof(TEntity);
            PredicateExpression = predicateExpression;
        }

        internal GlobalLevelDataFilteringStrategy(Func<Expression<Func<TEntity, bool>>> predicateFunc)
            : this(predicateFunc?.Invoke()) { }

        /// <summary>
        /// Gets filtering predicate
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
        public (Type, Type) GetSignature() => (TypeOfEntity, TypeOfEntity);
    }
}