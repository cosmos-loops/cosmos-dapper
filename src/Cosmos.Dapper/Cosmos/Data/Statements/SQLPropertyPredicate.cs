using System.Collections.Generic;

namespace Cosmos.Data.Statements
{
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// Sql property predicate
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public class SQLPropertyPredicate<T1, T2> : SQLComparePredicate, ISQLPropertyPredicate
        where T1 : class where T2 : class
    {
        /// <summary>
        /// Property name 2
        /// </summary>
        public string PropertyName2 { get; set; }

        /// <summary>
        /// Get sql
        /// </summary>
        /// <param name="sqlGenerator"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public override string GetSql(ISQLGenerator sqlGenerator, IDictionary<string, object> parameters)
        {
            var columnName = GetColumnName(typeof(T1), sqlGenerator, PropertyName);
            var columnName2 = GetColumnName(typeof(T2), sqlGenerator, PropertyName2);
            return $"({columnName} {GetOperatorString()} {columnName2})";
        }
    }
}