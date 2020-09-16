namespace Cosmos.Data.SqlKata
{
    /// <summary>
    /// Interface for result affected rows
    /// </summary>
    public interface IResultAffectedRows : IResultBase
    {
        /// <summary>
        /// Affected rows
        /// </summary>
        int AffectedRows { get; set; }
    }
}