using Cosmos.Data.Statements;

namespace Cosmos.Dapper.Actions
{
    /// <summary>
    /// Interface for Has DataFilter
    /// </summary>
    public interface IHasDataFilter
    {
        /// <summary>
        /// Gets or sets filters
        /// </summary>
        ISQLPredicate[] Filters { get; set; }
    }
}