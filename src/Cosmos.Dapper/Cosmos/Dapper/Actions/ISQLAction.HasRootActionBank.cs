namespace Cosmos.Dapper.Actions
{
    /// <summary>
    /// Interface for HasRoot SqlAction bank
    /// </summary>
    public interface IHasRootActionBank
    {
        /// <summary>
        /// Gets or sets root SqlAction bank
        /// </summary>
        SQLActionSetBase RootActionBank { get; set; }
    }
}