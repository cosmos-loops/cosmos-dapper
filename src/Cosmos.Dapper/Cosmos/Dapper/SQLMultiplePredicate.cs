using System;
using System.Collections.Generic;
using Cosmos.Data.Statements;

// ReSharper disable InconsistentNaming

namespace Cosmos.Dapper
{
    /// <summary>
    /// SQL Multiple Predicate
    /// </summary>
    public class SQLMultiplePredicate
    {
        private readonly List<SQLMultiplePredicateItem> _items;

        /// <summary>
        /// Create a new instance of <see cref="SQLMultiplePredicate" />
        /// </summary>
        public SQLMultiplePredicate()
        {
            _items = new List<SQLMultiplePredicateItem>();
        }

        /// <summary>
        /// Gets items of <see cref="SQLMultiplePredicate" />
        /// </summary>
        public IEnumerable<SQLMultiplePredicateItem> Items => _items.AsReadOnly();

        /// <summary>
        /// Add Sql predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <typeparam name="T"></typeparam>
        public void Add<T>(ISQLPredicate predicate, SQLSortSet sort = null) where T : class
        {
            _items.Add(new SQLMultiplePredicateItem
            {
                Value = predicate,
                Type = typeof(T),
                SortSet = sort
            });
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="id"></param>
        /// <typeparam name="T"></typeparam>
        public void Add<T>(object id) where T : class
        {
            _items.Add(new SQLMultiplePredicateItem
            {
                Value = id,
                Type = typeof(T)
            });
        }

        /// <summary>
        /// Item of SQL Multiple Predicate
        /// </summary>
        public class SQLMultiplePredicateItem
        {
            /// <summary>
            /// Value
            /// </summary>
            public object Value { get; set; }

            /// <summary>
            /// Type
            /// </summary>
            public Type Type { get; set; }

            /// <summary>
            /// Sort set
            /// </summary>
            public SQLSortSet SortSet { get; set; }
        }
    }
}