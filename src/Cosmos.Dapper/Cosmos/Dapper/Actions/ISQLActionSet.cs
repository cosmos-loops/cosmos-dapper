namespace Cosmos.Dapper.Actions
{
    /// <summary>
    /// Interface for SqlAction sets
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public interface ISQLActionSet
    {
        /// <summary>
        /// Gets calling mode
        /// </summary>
        ActionCallingMode CallingMode { get; }

        /// <summary>
        /// Get dapper options
        /// </summary>
        DapperOptions Options { get; }
    }
}