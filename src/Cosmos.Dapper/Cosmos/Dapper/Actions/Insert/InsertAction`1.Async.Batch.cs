using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Dapper.Core;
using Cosmos.Models;
using Cosmos.Reflection;

namespace Cosmos.Dapper.Actions.Insert
{
    /// <summary>
    /// Asynchronous Batch Insert Action
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class AsynchronousBatchInsertAction<TEntity> : AsynchronousBatchSQLAction<TEntity>, IAsynchronousExecutableSQLAction where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Create a new instance of <see cref="AsynchronousBatchInsertAction{TEntity}" />
        /// </summary>
        /// <param name="rootActionSet"></param>
        /// <param name="contextParams"></param>
        /// <param name="entities"></param>
        public AsynchronousBatchInsertAction(SQLActionSetBase rootActionSet, IDapperContextParams contextParams, IEnumerable<TEntity> entities)
            : base(rootActionSet, ActionKind.Insert, contextParams, null)
        {
            InternalCommand = (c, t) => _connector.InsertAsync(c, TransactionWrapper.GetOrBegin(false), t);
            EntityInstanceColl = entities.DeepCopy();
        }

        private IEnumerable<TEntity> EntityInstanceColl { get; }

        private Func<IEnumerable<TEntity>, CancellationToken, Task> InternalCommand { get; set; }

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

            await InternalCommand.Invoke(EntityInstanceColl, cancellationToken);
            IsExecuted = true;
        }
    }
}