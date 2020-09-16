namespace Cosmos.Data.SqlKata
{
    /// <summary>
    /// Result affected rows
    /// </summary>
    public class ResultAffectedRows : IResultAffectedRows
    {
        /// <summary>
        /// Affected rows
        /// </summary>
        public int AffectedRows { get; set; }

        /// <summary>
        /// Result type
        /// </summary>
        public ResultType ResultType { get; set; }
    }
}