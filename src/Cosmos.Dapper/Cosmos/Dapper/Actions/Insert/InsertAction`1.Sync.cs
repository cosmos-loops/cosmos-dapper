using System;
using Cosmos.Dapper.Core;
using Cosmos.Models;
using Cosmos.Reflection;

namespace Cosmos.Dapper.Actions.Insert
{
    /// <summary>
    /// Insert Action
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class InsertAction<TEntity> : SQLAction<TEntity>, IExecutableSQLAction where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Create a new instance of <see cref="InsertAction{TEntity}" />
        /// </summary>
        /// <param name="rootActionSet"></param>
        /// <param name="contextParams"></param>
        /// <param name="entity"></param>
        public InsertAction(SQLActionSetBase rootActionSet, IDapperContextParams contextParams, TEntity entity)
            : base(rootActionSet, ActionKind.Insert, contextParams, null)
        {
            InternalCommand = i => _connector.Insert(i, TransactionWrapper.GetOrBegin(false));
            EntityInstance = entity.DeepCopy();
        }

        private TEntity EntityInstance { get; }

        private Action<TEntity> InternalCommand { get; set; }

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

            InternalCommand.Invoke(EntityInstance);
            IsExecuted = true;
        }
    }
}