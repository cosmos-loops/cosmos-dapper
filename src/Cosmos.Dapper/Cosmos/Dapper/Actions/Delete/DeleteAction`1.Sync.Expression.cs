using System;
using System.Linq.Expressions;
using Cosmos.Dapper.Core;
using Cosmos.Data.Statements;
using Cosmos.Models;

namespace Cosmos.Dapper.Actions.Delete
{
    /// <summary>
    /// Expression Delete Action
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class ExpressionDeleteAction<TEntity> : SQLAction<TEntity>, IExecutableSQLAction where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Create a new instance of <see cref="ExpressionDeleteAction{TEntity}" />
        /// </summary>
        /// <param name="rootActionSet"></param>
        /// <param name="contextParams"></param>
        /// <param name="predicateExpression"></param>
        /// <param name="filters"></param>
        public ExpressionDeleteAction(
            SQLActionSetBase rootActionSet,
            IDapperContextParams contextParams,
            Expression<Func<TEntity, bool>> predicateExpression,
            ISQLPredicate[] filters = null)
            : base(rootActionSet, ActionKind.Insert, contextParams, filters)
        {
            InternalCommand = (expr, f) => _connector.Delete(expr, TransactionWrapper.GetOrBegin(false), f);
            PredicateExpression = predicateExpression;
            Filters = filters;
        }

        private Expression<Func<TEntity, bool>> PredicateExpression { get; }

        private ISQLPredicate[] Filters { get; }

        private Action<Expression<Func<TEntity, bool>>, ISQLPredicate[]> InternalCommand { get; set; }

        /// <summary>
        /// Execute
        /// </summary>
        public void Execute()
        {
            ActionBankGetter.RootActionBank.Execute();
        }

        void IExecutableSQLAction.ExecuteCalledFromBank()
        {
            if (IsExecuted)
                return;

            InternalCommand.Invoke(PredicateExpression, Filters);
            IsExecuted = true;
        }
    }
}