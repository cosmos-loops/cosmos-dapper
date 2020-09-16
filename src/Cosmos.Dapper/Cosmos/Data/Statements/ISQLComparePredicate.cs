namespace Cosmos.Data.Statements
{
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// Interface Sql compare predicate
    /// </summary>
    public interface ISQLComparePredicate : ISQLBasePredicate
    {
        /// <summary>
        /// Operator
        /// </summary>
        SQLOperatorSlim Operator { get; set; }

        /// <summary>
        /// Not
        /// </summary>
        bool Not { get; set; }
    }
}