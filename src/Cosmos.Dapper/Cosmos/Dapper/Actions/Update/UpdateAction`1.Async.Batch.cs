using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Dapper.Core;
using Cosmos.Data.Statements;
using Cosmos.Models;
using Cosmos.Reflection;

namespace Cosmos.Dapper.Actions.Update
{
    /// <summary>
    /// Asynchronous Batch Update Action
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class AsynchronousBatchUpdateAction<TEntity> : AsynchronousBatchSQLAction<TEntity>, IAsynchronousExecutableSQLAction where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Create a new instance of <see cref="AsynchronousBatchUpdateAction{TEntity}" />
        /// </summary>
        /// <param name="rootActionSet"></param>
        /// <param name="contextParams"></param>
        /// <param name="entities"></param>
        /// <param name="filters"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        public AsynchronousBatchUpdateAction(
            SQLActionSetBase rootActionSet,
            IDapperContextParams contextParams,
            IEnumerable<TEntity> entities,
            ISQLPredicate[] filters = null,
            bool ignoreAllKeyProperties = false)
            : base(rootActionSet, ActionKind.Insert, contextParams, filters)
        {
            InternalCommand = (c, f, t) => _connector.UpdateAsync(c, TransactionWrapper.GetOrBegin(false), f, ignoreAllKeyProperties, t);
            EntityInstanceColl = entities.DeepCopy();
            Filters = filters;
        }

        private IEnumerable<TEntity> EntityInstanceColl { get; }

        private ISQLPredicate[] Filters { get; }

        private Func<IEnumerable<TEntity>, ISQLPredicate[], CancellationToken, Task> InternalCommand { get; set; }

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

            await InternalCommand.Invoke(EntityInstanceColl, Filters, cancellationToken);
            IsExecuted = true;
        }
    }
}