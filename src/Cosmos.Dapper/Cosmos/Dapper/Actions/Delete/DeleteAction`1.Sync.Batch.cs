using System;
using System.Collections.Generic;
using Cosmos.Dapper.Core;
using Cosmos.Data.Statements;
using Cosmos.Models;
using Cosmos.Reflection;

namespace Cosmos.Dapper.Actions.Delete
{
    /// <summary>
    /// Batch Delete Action
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BatchDeleteAction<TEntity> : BatchSQLAction<TEntity>, IExecutableSQLAction where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Create a new instance of <see cref="BatchDeleteAction{TEntity}" />
        /// </summary>
        /// <param name="rootActionSet"></param>
        /// <param name="contextParams"></param>
        /// <param name="entities"></param>
        /// <param name="filters"></param>
        public BatchDeleteAction(
            SQLActionSetBase rootActionSet,
            IDapperContextParams contextParams,
            IEnumerable<TEntity> entities,
            ISQLPredicate[] filters = null)
            : base(rootActionSet, ActionKind.Insert, contextParams, filters)
        {
            InternalCommand = (c, f) => _connector.Delete(c, TransactionWrapper.GetOrBegin(false), f);
            EntityInstanceColl = entities.DeepCopy();
            Filters = filters;
        }

        private IEnumerable<TEntity> EntityInstanceColl { get; }

        private ISQLPredicate[] Filters { get; }

        private Action<IEnumerable<TEntity>, ISQLPredicate[]> InternalCommand { get; set; }

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

            InternalCommand.Invoke(EntityInstanceColl, Filters);
            IsExecuted = true;
        }
    }
}