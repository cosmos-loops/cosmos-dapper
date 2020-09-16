using System;
using System.Collections.Generic;
using System.Linq;

namespace Cosmos.Data.Statements
{
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// Sql base predicate
    /// </summary>
    public abstract class SQLBasePredicate : ISQLBasePredicate
    {
        /// <summary>
        /// Get sql
        /// </summary>
        /// <param name="sqlGenerator"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public abstract string GetSql(ISQLGenerator sqlGenerator, IDictionary<string, object> parameters);

        /// <summary>
        /// Entity type
        /// </summary>
        public Type EntityType { get; set; }

        /// <summary>
        /// Property name
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets column name
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="sqlGenerator"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        protected virtual string GetColumnName(Type entityType, ISQLGenerator sqlGenerator, string propertyName)
        {
            var map = sqlGenerator.Configuration.GetMap(entityType);
            if (map == null)
                throw new NullReferenceException($"Map was not found for '{entityType}'");

            var propertyMap = map.PropertyMaps.SingleOrDefault(p => p.Name == propertyName);
            if (propertyMap == null)
                throw new NullReferenceException($"'{propertyName}' was not found for '{entityType}'");

            return sqlGenerator.GetColumnName(map, propertyMap, false);
        }
    }
}