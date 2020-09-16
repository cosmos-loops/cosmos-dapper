using System;
using System.Collections.Generic;
using System.Linq;
using Cosmos.Dapper.Core;
using Cosmos.Models;
using Cosmos.Reflection;

namespace Cosmos.Dapper.Actions.Insert
{
    /// <summary>
    /// Bulk Insert Action
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BulkInsertAction<TEntity> : BatchSQLAction<TEntity>, IExecutableSQLAction where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Create a new instance of <see cref="BulkInsertAction{TEntity}" />
        /// </summary>
        /// <param name="rootActionSet"></param>
        /// <param name="contextParams"></param>
        /// <param name="entities"></param>
        public BulkInsertAction(SQLActionSetBase rootActionSet, IDapperContextParams contextParams, IEnumerable<TEntity> entities)
            : base(rootActionSet, ActionKind.Insert, contextParams, null)
        {
            var bulkInsertOperator = contextParams.GetBulkInsertOperator(_connector);
            InternalCommand = c => bulkInsertOperator.Process(c.ToList());
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