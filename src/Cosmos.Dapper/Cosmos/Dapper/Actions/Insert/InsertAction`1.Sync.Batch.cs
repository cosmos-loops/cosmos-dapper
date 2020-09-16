using System;
using System.Collections.Generic;
using Cosmos.Dapper.Core;
using Cosmos.Models;
using Cosmos.Reflection;

namespace Cosmos.Dapper.Actions.Insert
{
    /// <summary>
    /// Batch Insert Action
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BatchInsertAction<TEntity> : BatchSQLAction<TEntity>, IExecutableSQLAction where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Create a new instance of <see cref="BatchInsertAction{TEntity}" />
        /// </summary>
        /// <param name="rootActionSet"></param>
        /// <param name="contextParams"></param>
        /// <param name="entities"></param>
        public BatchInsertAction(SQLActionSetBase rootActionSet, IDapperContextParams contextParams, IEnumerable<TEntity> entities)
            : base(rootActionSet, ActionKind.Insert, contextParams, null)
        {
            InternalCommand = c => _connector.Insert(c, TransactionWrapper.GetOrBegin(false));
            EntityInstanceColl = entities.DeepCopy();
        }

        private IEnumerable<TEntity> EntityInstanceColl { get; }

        private Action<IEnumerable<TEntity>> InternalCommand { get; set; }

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

            InternalCommand.Invoke(EntityInstanceColl);
            IsExecuted = true;
        }
    }
}