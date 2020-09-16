using Cosmos.Dapper.Core;
using Cosmos.Data.Statements;
using Cosmos.Models;

namespace Cosmos.Dapper.Actions
{
    /// <summary>
    /// SqlAction
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    // ReSharper disable once InconsistentNaming
    public abstract class SQLAction<TEntity> : SQLActionSyncBase where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Create a new instance of <see cref="SQLAction{TEntity}" />
        /// </summary>
        /// <param name="rootActionSet"></param>
        /// <param name="kind"></param>
        /// <param name="contextParams"></param>
        /// <param name="filters"></param>
        protected SQLAction(SQLActionSetBase rootActionSet, ActionKind kind, IDapperContextParams contextParams, ISQLPredicate[] filters)
            : base(rootActionSet, kind, contextParams, filters) { }
    }
}