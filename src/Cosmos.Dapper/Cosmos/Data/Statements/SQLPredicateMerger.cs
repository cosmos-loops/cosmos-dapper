using System.Collections.Generic;
using System.Linq;

namespace Cosmos.Data.Statements
{
    /// <summary>
    /// Sql predicate merger
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static class SQLPredicateMerger
    {
        /// <summary>
        /// Merger...
        /// </summary>
        /// <param name="predicates"></param>
        /// <returns></returns>
        public static ISQLPredicate Merge(ISQLPredicate[] predicates)
        {
            return Merge(null, predicates);
        }

        /// <summary>
        /// Merger...
        /// </summary>
        /// <param name="where"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public static ISQLPredicate Merge(ISQLPredicate where, params ISQLPredicate[] filters)
        {
            if (filters is null || !filters.Any())
                return where;

            var group = new SQLPredicateGroup
            {
                Operator = SQLGroupOperator.AND,
                Predicates = new List<ISQLPredicate>()
            };
            
            if (where != null)
                group.Predicates.Add(where);

            foreach (var filter in filters)
            {
                if (filter is null)
                    continue;

                group.Predicates.Add(filter);
            }

            return group;
        }

        /// <summary>
        /// Join...
        /// </summary>
        /// <param name="where"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public static ISQLPredicate Join(this ISQLPredicate where, params ISQLPredicate[] filters)
        {
            return Merge(where, filters);
        }
    }
}