using Cosmos.Dapper.Core;
using Dapper;

namespace Cosmos.Dapper.Actions
{
    /// <summary>
    /// SqlAction Sets
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class SQLActionSet : SQLActionSetBase
    {
        /// <summary>
        /// Create a new instance of <see cref="SQLActionSet" />
        /// </summary>
        /// <param name="connector"></param>
        /// <param name="config"></param>
        public SQLActionSet(IDapperConnector connector, IDapperMappingConfig config)
            : base(connector, config) { }
    }
}