namespace Cosmos.Data.Statements
{
    /// <summary>
    /// Interface for Sql property predicate
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public interface ISQLPropertyPredicate : ISQLComparePredicate
    {
        /// <summary>
        /// Property name
        /// </summary>
        string PropertyName2 { get; set; }
    }
}