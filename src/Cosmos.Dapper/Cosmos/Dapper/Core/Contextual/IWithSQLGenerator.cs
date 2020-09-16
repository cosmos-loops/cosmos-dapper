using Cosmos.Data.Statements;

namespace Cosmos.Dapper.Core.Contextual
{
    /// <summary>
    /// Interface for WithSqlGenerator
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public interface IWithSQLGenerator
    {
        /// <summary>
        /// Gets SqlGenerator
        /// </summary>
        ISQLGenerator SqlGenerator { get; set; }
    }
}