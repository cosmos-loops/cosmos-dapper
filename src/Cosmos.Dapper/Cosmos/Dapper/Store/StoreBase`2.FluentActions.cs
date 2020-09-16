using Cosmos.Dapper.Actions;

namespace Cosmos.Dapper.Store
{
    // ReSharper disable UnusedTypeParameter
    public abstract partial class StoreBase<TContext, TEntity>
    {
        /// <summary>
        /// Gets DapperAction entry
        /// </summary>
        public ISQLActionEntry ActionEntry => RawTypedContext.GetActionEntry(RepoLevelDataFilters);

        /// <summary>
        /// Gets asynchronous DapperAction entry
        /// </summary>
        public ISQLActionAsyncEntry AsynchronousActionEntry => RawTypedContext.GetAsynchronousActionEntry(RepoLevelDataFilters);
    }
}