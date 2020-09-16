using Cosmos.Dapper.Core;
using Cosmos.Data.Statements;
using Cosmos.Models;

namespace Cosmos.Dapper.Actions
{
    /// <summary>
    /// Asynchronous SqlAction
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    // ReSharper disable once InconsistentNaming
    public abstract class AsynchronousSQLAction<TEntity> : SQLActionAsyncBase where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Create a new instance of <see cref="AsynchronousSQLAction{TEntity}" />
        /// </summary>
        /// <param name="rootActionSet"></param>
        /// <param name="kind"></param>
        /// <param name="contextParams"></param>
        /// <param name="filters"></param>
        protected AsynchronousSQLAction(SQLActionSetBase rootActionSet, ActionKind kind, IDapperContextParams contextParams, ISQLPredicate[] filters)
            : base(rootActionSet, kind, contextParams, filters) { }
    }
}