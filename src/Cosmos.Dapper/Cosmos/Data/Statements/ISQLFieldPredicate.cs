namespace Cosmos.Data.Statements
{
    /// <summary>
    /// Interface sql field predicate
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public interface ISQLFieldPredicate : ISQLComparePredicate
    {
        /// <summary>
        /// Gets ot sets value
        /// </summary>
        object Value { get; set; }
    }
}