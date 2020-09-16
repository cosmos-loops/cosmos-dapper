using System;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Dapper.Core;
using Cosmos.Data.Statements;
using Cosmos.Models;
using Cosmos.Reflection;

namespace Cosmos.Dapper.Actions.Delete
{
    /// <summary>
    /// Asynchronous Delete Action
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class AsynchronousDeleteAction<TEntity> : AsynchronousSQLAction<TEntity>, IAsynchronousExecutableSQLAction where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Create a new instance of <see cref="AsynchronousDeleteAction{TEntity}" />
        /// </summary>
        /// <param name="rootActionSet"></param>
        /// <param name="contextParams"></param>
        /// <param name="entity"></param>
        /// <param name="filters"></param>
        public AsynchronousDeleteAction(
            SQLActionSetBase rootActionSet,
            IDapperContextParams contextParams,
            TEntity entity,
            ISQLPredicate[] filters = null)
            : base(rootActionSet, ActionKind.Insert, contextParams, filters)
        {
            InternalCommand = (i, f, t) => _connector.DeleteAsync(i, TransactionWrapper.GetOrBegin(false), f, t);
            EntityInstance = entity.DeepCopy();
            Filters = filters;
        }

        private TEntity EntityInstance { get; }

        private ISQLPredicate[] Filters { get; }

        private Func<TEntity, ISQLPredicate[], CancellationToken, Task> InternalCommand { get; set; }

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

            await InternalCommand.Invoke(EntityInstance, Filters, cancellationToken);
            IsExecuted = true;
        }
    }
}