namespace Cosmos.Dapper.Actions
{
    /// <summary>
    /// Interface for SqlAction
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public interface ISQLAction
    {
        /// <summary>
        /// Gets dialect
        /// </summary>
        string Dialect { get; }

        /// <summary>
        /// Gets DapperAction kind
        /// </summary>
        ActionKind Kind { get; }

        /// <summary>
        /// Gets DapperAction calling mode
        /// </summary>
        ActionCallingMode CallingMode { get; }

        /// <summary>
        /// Gets Dapper Options
        /// </summary>
        DapperOptions Options { get; }

        /// <summary>
        /// Is executed
        /// </summary>
        bool IsExecuted { get; set; }
    }
}