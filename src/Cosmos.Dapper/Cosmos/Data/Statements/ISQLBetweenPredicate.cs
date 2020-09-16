namespace Cosmos.Data.Statements
{
    /// <summary>
    /// Interface for Sql between predicate
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public interface ISQLBetweenPredicate : ISQLPredicate
    {
        /// <summary>
        /// Property name
        /// </summary>
        string PropertyName { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        SQLBetweenValues Value { get; set; }

        /// <summary>
        /// Not
        /// </summary>
        bool Not { get; set; }
    }
}