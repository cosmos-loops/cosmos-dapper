using System;
using System.Collections.Generic;
using Cosmos.Dapper.Core;
using Cosmos.Data.Statements;
using Cosmos.Models;
using Cosmos.Reflection;

namespace Cosmos.Dapper.Actions.Update
{
    /// <summary>
    /// Batch Update Action
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BatchUpdateAction<TEntity> : BatchSQLAction<TEntity>, IExecutableSQLAction where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Create a new instance of <see cref="BatchUpdateAction{TEntity}" />
        /// </summary>
        /// <param name="rootActionSet"></param>
        /// <param name="contextParams"></param>
        /// <param name="entities"></param>
        /// <param name="filters"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        public BatchUpdateAction(
            SQLActionSetBase rootActionSet,
            IDapperContextParams contextParams,
            IEnumerable<TEntity> entities,
            ISQLPredicate[] filters = null,
            bool ignoreAllKeyProperties = false)
            : base(rootActionSet, ActionKind.Update, contextParams, filters)
        {
            InternalCommand = (c, f) => _connector.Update(c, TransactionWrapper.GetOrBegin(false), f, ignoreAllKeyProperties);
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