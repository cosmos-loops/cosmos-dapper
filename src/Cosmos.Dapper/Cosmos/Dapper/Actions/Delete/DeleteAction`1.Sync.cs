using System;
using Cosmos.Dapper.Core;
using Cosmos.Data.Statements;
using Cosmos.Models;
using Cosmos.Reflection;

namespace Cosmos.Dapper.Actions.Delete
{
    /// <summary>
    /// Delete Action
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class DeleteAction<TEntity> : SQLAction<TEntity>, IExecutableSQLAction where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Create a new instance of <see cref="DeleteAction{TEntity}" />
        /// </summary>
        /// <param name="rootActionSet"></param>
        /// <param name="contextParams"></param>
        /// <param name="entity"></param>
        /// <param name="filters"></param>
        public DeleteAction(
            SQLActionSetBase rootActionSet,
            IDapperContextParams contextParams,
            TEntity entity,
            ISQLPredicate[] filters = null)
            : base(rootActionSet, ActionKind.Insert, contextParams, filters)
        {
            InternalCommand = (i, f) => _connector.Delete(i, TransactionWrapper.GetOrBegin(false), f);
            EntityInstance = entity.DeepCopy();
            Filters = filters;
        }

        private TEntity EntityInstance { get; }

        private ISQLPredicate[] Filters { get; }

        private Action<TEntity, ISQLPredicate[]> InternalCommand { get; set; }

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

            InternalCommand.Invoke(EntityInstance, Filters);
            IsExecuted = true;
        }
    }
}