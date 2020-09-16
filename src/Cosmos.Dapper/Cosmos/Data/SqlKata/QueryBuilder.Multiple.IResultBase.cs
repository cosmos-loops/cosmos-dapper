namespace Cosmos.Data.SqlKata
{
    /// <summary>
    /// Interface for result base
    /// </summary>
    public interface IResultBase
    {
        /// <summary>
        /// Result type
        /// </summary>
        ResultType ResultType { get; set; }
    }
}