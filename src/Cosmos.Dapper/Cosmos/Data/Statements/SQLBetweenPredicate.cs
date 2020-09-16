using System.Collections.Generic;
using Cosmos.Dapper.Core.Helpers;

namespace Cosmos.Data.Statements
{
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// Sql between predicate
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SQLBetweenPredicate<T> : SQLBasePredicate, ISQLBetweenPredicate where T : class
    {
        /// <summary>
        /// Get sql
        /// </summary>
        /// <param name="sqlGenerator"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public override string GetSql(ISQLGenerator sqlGenerator, IDictionary<string, object> parameters)
        {
            var columnName = GetColumnName(typeof(T), sqlGenerator, PropertyName);
            var propertyName1 = parameters.SetParameterName(PropertyName, Value.Value1, sqlGenerator.Configuration.Dialect.ParameterPrefix);
            var propertyName2 = parameters.SetParameterName(PropertyName, Value.Value2, sqlGenerator.Configuration.Dialect.ParameterPrefix);
            return $"({columnName} {(Not ? "NOT " : string.Empty)}BETWEEN {propertyName1} AND {propertyName2})";
        }

        /// <summary>
        /// Gets or sets value
        /// </summary>
        public SQLBetweenValues Value { get; set; }

        /// <summary>
        /// Not
        /// </summary>
        public bool Not { get; set; }
    }
}