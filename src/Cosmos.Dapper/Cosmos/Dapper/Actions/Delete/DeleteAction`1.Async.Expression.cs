using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Dapper.Core;
using Cosmos.Data.Statements;
using Cosmos.Models;

namespace Cosmos.Dapper.Actions.Delete
{
    /// <summary>
    /// Asynchronous Expression Delete Action
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class AsynchronousExpressionDeleteAction<TEntity> : AsynchronousSQLAction<TEntity>, IAsynchronousExecutableSQLAction where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Create a new instance of <see cref="AsynchronousExpressionDeleteAction{TEntity}" />
        /// </summary>
        /// <param name="rootActionSet"></param>
        /// <param name="contextParams"></param>
        /// <param name="predicateExpression"></param>
        /// <param name="filters"></param>
        public AsynchronousExpressionDeleteAction(
            SQLActionSetBase rootActionSet,
            IDapperContextParams contextParams,
            Expression<Func<TEntity, bool>> predicateExpression,
            ISQLPredicate[] filters = null)
            : base(rootActionSet, ActionKind.Insert, contextParams, filters)
        {
            InternalCommand = (expr, f, t) => _connector.DeleteAsync(expr, TransactionWrapper.GetOrBegin(false), f, t);
            PredicateExpression = predicateExpression;
            Filters = filters;
        }

        private Expression<Func<TEntity, bool>> PredicateExpression { get; }

        private ISQLPredicate[] Filters { get; }

        private Func<Expression<Func<TEntity, bool>>, ISQLPredicate[], CancellationToken, Task> InternalCommand { get; set; }

        /// <summary>
        /// Execute async
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            await ActionBankGetter.RootActionBank.ExecuteAsync(cancellationToken);
        }

        async Task IAsynchronousExecutableSQLAction.ExecuteCalledFromBankAsync(CancellationToken cancellationToken)
        {
            if (IsExecuted)
                return;

            await InternalCommand.Invoke(PredicateExpression, Filters, cancellationToken);
            IsExecuted = true;
        }
    }
}