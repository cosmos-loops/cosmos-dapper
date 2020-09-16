using System.Collections.Generic;

namespace Cosmos.Data.Statements
{
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// Interface for Sql predicate group
    /// </summary>
    public interface ISQLPredicateGroup : ISQLPredicate
    {
        /// <summary>
        /// Operator
        /// </summary>
        SQLGroupOperator Operator { get; set; }

        /// <summary>
        /// Predicates
        /// </summary>
        IList<ISQLPredicate> Predicates { get; set; }
    }
}