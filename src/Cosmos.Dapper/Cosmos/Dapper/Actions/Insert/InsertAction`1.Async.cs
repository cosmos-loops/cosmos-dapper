using System;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Dapper.Core;
using Cosmos.Models;
using Cosmos.Reflection;

namespace Cosmos.Dapper.Actions.Insert
{
    /// <summary>
    /// Asynchronous Insert Action
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class AsynchronousInsertAction<TEntity> : AsynchronousSQLAction<TEntity>, IAsynchronousExecutableSQLAction where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Create a new instance of <see cref="AsynchronousInsertAction{TEntity}" />
        /// </summary>
        /// <param name="rootActionSet"></param>
        /// <param name="contextParams"></param>
        /// <param name="entity"></param>
        public AsynchronousInsertAction(SQLActionSetBase rootActionSet, IDapperContextParams contextParams, TEntity entity)
            : base(rootActionSet, ActionKind.Insert, contextParams, null)
        {
            InternalCommand = (i, t) => _connector.InsertAsync(i, TransactionWrapper.GetOrBegin(false), t);
            EntityInstance = entity.DeepCopy();
        }

        private TEntity EntityInstance { get; }

        private Func<TEntity, CancellationToken, Task> InternalCommand { get; set; }

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

            await InternalCommand.Invoke(EntityInstance, cancellationToken);
            IsExecuted = true;
        }
    }
}