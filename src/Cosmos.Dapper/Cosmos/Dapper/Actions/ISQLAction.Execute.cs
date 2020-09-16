namespace Cosmos.Dapper.Actions
{
    /// <summary>
    /// Interface for executable SqlAction
    /// </summary>
    // ReSharper disable once InconsistentNaming
    internal interface IExecutableSQLAction
    {
        /// <summary>
        /// Execute called from bank...
        /// </summary>
        void ExecuteCalledFromBank();
    }
}