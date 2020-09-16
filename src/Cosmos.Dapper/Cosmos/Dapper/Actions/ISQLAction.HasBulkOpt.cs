using Cosmos.Dapper.Core;

namespace Cosmos.Dapper.Actions
{
    /// <summary>
    /// Interface for HasBulkOpt
    /// </summary>
    public interface IHasBulkOpt
    {
        /// <summary>
        /// Gets or sets Context params
        /// </summary>
        IDapperContextParams ContextParams { get; set; }
    }
}