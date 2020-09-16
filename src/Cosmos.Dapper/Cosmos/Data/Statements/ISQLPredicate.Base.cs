namespace Cosmos.Data.Statements
{
    /// <summary>
    /// Interface for sql base predicate
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public interface ISQLBasePredicate : ISQLPredicate
    {
        /// <summary>
        /// Property name
        /// </summary>
        string PropertyName { get; set; }
    }
}