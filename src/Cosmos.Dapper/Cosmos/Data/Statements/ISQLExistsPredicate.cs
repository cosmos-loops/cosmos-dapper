namespace Cosmos.Data.Statements
{
    /// <summary>
    /// Interface for Sql exists predicate
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public interface ISQLExistsPredicate : ISQLPredicate
    {
        /// <summary>
        /// Predicate
        /// </summary>
        ISQLPredicate Predicate { get; set; }

        /// <summary>
        ///  Not
        /// </summary>
        bool Not { get; set; }
    }
}