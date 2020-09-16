using System.Collections.Generic;

namespace Cosmos.Data.Statements
{
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// Interface for Sql predicate
    /// </summary>
    public interface ISQLPredicate
    {
        /// <summary>
        /// Get sql
        /// </summary>
        /// <param name="sqlGenerator"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        string GetSql(ISQLGenerator sqlGenerator, IDictionary<string, object> parameters);
    }
}