using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cosmos.Data.SqlKata;

namespace Cosmos.Data.Statements
{
    /// <summary>
    /// Sql predicate group
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class SQLPredicateGroup : ISQLPredicateGroup
    {
        /// <summary>
        /// Gets or sets operator
        /// </summary>
        public SQLGroupOperator Operator { get; set; }

        /// <summary>
        /// Gets or sets predicates
        /// </summary>
        public IList<ISQLPredicate> Predicates { get; set; }

        /// <summary>
        /// Get sql
        /// </summary>
        /// <param name="sqlGenerator"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string GetSql(ISQLGenerator sqlGenerator, IDictionary<string, object> parameters)
        {
            var seperator = Operator == SQLGroupOperator.AND ? " AND " : " OR ";
            return "(" +
                   Predicates.Aggregate(
                       new StringBuilder(),
                       (sb, p) => (sb.Length == 0 ? sb : sb.Append(seperator)).Append(p?.GetSql(sqlGenerator, parameters)),
                       sb =>
                       {
                           var s = sb.ToString();
                           return s.Length == 0 ? sqlGenerator.Configuration.Dialect.EmptyExpression : s;
                       }
                   ) +
                   ")";
        }
    }
}