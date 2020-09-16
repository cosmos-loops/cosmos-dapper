using Cosmos.Dapper.Core;
using Cosmos.Data.Statements;
using Cosmos.Models;

namespace Cosmos.Dapper.Actions
{
    /// <summary>
    /// Batch SQLAction
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    // ReSharper disable once InconsistentNaming
    public abstract class BatchSQLAction<TEntity> : SQLActionSyncBase where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Create a new instance of <see cref="BatchSQLAction{TEntity}" />
        /// </summary>
        /// <param name="rootActionSet"></param>
        /// <param name="kind"></param>
        /// <param name="contextParams"></param>
        /// <param name="filters"></param>
        protected BatchSQLAction(SQLActionSetBase rootActionSet, ActionKind kind, IDapperContextParams contextParams, ISQLPredicate[] filters)
            : base(rootActionSet, kind, contextParams, filters) { }
    }
}