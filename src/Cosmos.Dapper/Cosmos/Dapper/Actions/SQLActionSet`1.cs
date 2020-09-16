using Cosmos.Dapper.Core;
using Cosmos.Models;
using Dapper;

namespace Cosmos.Dapper.Actions
{
    /// <summary>
    /// SqlAction Sets
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    // ReSharper disable once InconsistentNaming
    public class SQLActionSet<TEntity> : SQLActionSetBase where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Create a new instance of <see cref="SQLActionSet{TEntity}" />
        /// </summary>
        /// <param name="connector"></param>
        /// <param name="config"></param>
        public SQLActionSet(IDapperConnector connector, IDapperMappingConfig config)
            : base(connector, config) { }
    }
}