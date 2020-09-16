using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.Dapper;
using Cosmos.Dapper.Mapper;
using Dapper;

namespace Cosmos.Data.Statements
{
    /// <summary>
    /// Sql exists predicate
    /// </summary>
    /// <typeparam name="TSub"></typeparam>
    public class SQLExistsPredicate<TSub> : ISQLExistsPredicate where TSub : class
    {
        /// <summary>
        /// Get sql
        /// </summary>
        /// <param name="sqlGenerator"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string GetSql(ISQLGenerator sqlGenerator, IDictionary<string, object> parameters)
        {
            var mapSub = GetClassMapper(typeof(TSub), sqlGenerator.Configuration);
            var sql = $"({(Not ? "NOT " : string.Empty)}EXISTS (SELECT 1 FROM {sqlGenerator.GetTableName(mapSub)} WHERE {Predicate.GetSql(sqlGenerator, parameters)}))";
            return sql;
        }

        /// <summary>
        /// Gets or sets predicate
        /// </summary>
        public ISQLPredicate Predicate { get; set; }

        /// <summary>
        /// Not
        /// </summary>
        public bool Not { get; set; }

        /// <summary>
        /// Get class mapper
        /// </summary>
        /// <param name="type"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        protected virtual IClassMap GetClassMapper(Type type, IDapperMappingConfig config)
        {
            var map = config.GetMap(type);

            if (map == null)
                throw new NullReferenceException($"Map was not found for '{type}'");

            return map;
        }
    }
}