using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cosmos.Dapper;
using Cosmos.Dapper.Core.Helpers;
using Cosmos.Dapper.Mapper;

namespace Cosmos.Data.Statements
{
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// Get generator
    /// </summary>
    public class SQLGenerator : ISQLGenerator
    {
        /// <summary>
        /// Create a new instance of <see cref="SQLGenerator" />
        /// </summary>
        /// <param name="config"></param>
        public SQLGenerator(IDapperMappingConfig config)
        {
            Configuration = config;
        }

        /// <summary>
        /// Gets configuration
        /// </summary>
        public IDapperMappingConfig Configuration { get; }

        /// <summary>
        /// Select
        /// </summary>
        /// <param name="classMap"></param>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public SQLConvertResult Select(IClassMap classMap, ISQLPredicate predicate, SQLSortSet sort, IDictionary<string, object> parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var sql = Sql.Select(BuildSelectColumns(classMap))
               .From(GetTableName(classMap))
               .Where(predicate != null, () => predicate.GetSql(this, parameters))
               .OrderBy(sort?.ToStrings(classMap, GetColumnName).AppendStrings())
               .ToString();

            return new SQLConvertResult(sql, parameters);
        }

        /// <summary>
        /// Select paged
        /// </summary>
        /// <param name="classMap"></param>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public SQLConvertResult SelectPaged(IClassMap classMap, ISQLPredicate predicate, SQLSortSet sort, int pageNumber, int pageSize, IDictionary<string, object> parameters)
        {
            if (sort == null || !sort.Any())
                throw new ArgumentNullException(nameof(sort), "Sort cannot be null or empty.");

            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var innerSql = Sql.Select(BuildSelectColumns(classMap))
               .From(GetTableName(classMap))
               .Where(predicate != null, () => predicate.GetSql(this, parameters))
               .OrderBy(sort.ToStrings(classMap, GetColumnName).AppendStrings());

            var sql = Configuration.Dialect.GetPagingSql(innerSql.ToString(), pageNumber, pageSize, parameters);

            return new SQLConvertResult(sql, parameters);
        }

        /// <summary>
        /// Select set...
        /// </summary>
        /// <param name="classMap"></param>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="limitFrom"></param>
        /// <param name="limitTo"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public SQLConvertResult SelectSet(IClassMap classMap, ISQLPredicate predicate, SQLSortSet sort, int limitFrom, int limitTo, IDictionary<string, object> parameters)
        {
            if (sort == null || !sort.Any())
                throw new ArgumentNullException(nameof(sort), "Sort cannot be null or empty.");

            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var innerSql = Sql.Select(BuildSelectColumns(classMap))
               .From(GetTableName(classMap))
               .Where(predicate != null, () => predicate.GetSql(this, parameters))
               .OrderBy(sort.ToStrings(classMap, GetColumnName).AppendStrings());

            var sql = Configuration.Dialect.GetSetSql(innerSql.ToString(), limitFrom, limitTo, parameters);

            return new SQLConvertResult(sql, parameters);
        }

        /// <summary>
        /// Count...
        /// </summary>
        /// <param name="classMap"></param>
        /// <param name="predicate"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public SQLConvertResult Count(IClassMap classMap, ISQLPredicate predicate, IDictionary<string, object> parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var sql = Sql.Select()
               .Count("*").As($"{Configuration.Dialect.OpenQuote}Total{Configuration.Dialect.CloseQuote}")
               .From(GetTableName(classMap))
               .Where(predicate != null, () => predicate.GetSql(this, parameters))
               .ToString();

            return new SQLConvertResult(sql, parameters);
        }

        /// <summary>
        /// Insert...
        /// </summary>
        /// <param name="classMap"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public SQLConvertResult Insert(IClassMap classMap)
        {
            var columns = classMap.PropertyMaps.Where(p => !(p.Ignored || p.IsReadOnly || p.KeyType == KeyType.Identity || p.KeyType == KeyType.TriggerIdentity));

            if (!columns.Any())
                throw new ArgumentException("No columns were mapped.");

            var columnNames = columns.Select(p => GetColumnName(classMap, p, false));
            var parameters = columns.Select(p => Configuration.Dialect.ParameterPrefix + p.Name);

            var sql = Sql.Insert().Into(GetTableName(classMap)).Columns(columnNames).Values(parameters);

            var triggerIdentityColumn = classMap.PropertyMaps.Where(p => p.KeyType == KeyType.TriggerIdentity).ToList();

            if (triggerIdentityColumn.Count > 0)
            {
                if (triggerIdentityColumn.Count > 1)
                    throw new ArgumentException("TriggerIdentity generator cannot be used with multi-column keys.");

                sql.Append($" RETURNING {triggerIdentityColumn.Select(p => GetColumnName(classMap, p, false)).First()} INTO {Configuration.Dialect.ParameterPrefix}IdOutParam");
            }

            return new SQLConvertResult(sql.ToString(), (IDictionary<string, object>) null);
        }

        /// <summary>
        /// Update...
        /// </summary>
        /// <param name="classMap"></param>
        /// <param name="predicate"></param>
        /// <param name="parameters"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public SQLConvertResult Update(IClassMap classMap, ISQLPredicate predicate, IDictionary<string, object> parameters, bool ignoreAllKeyProperties)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var columns = ignoreAllKeyProperties
                ? classMap.PropertyMaps.Where(p => !(p.Ignored || p.IsReadOnly) && p.KeyType == KeyType.NotAKey)
                : classMap.PropertyMaps.Where(p => !(p.Ignored || p.IsReadOnly || p.KeyType == KeyType.Identity || p.KeyType == KeyType.Assigned));

            if (!columns.Any())
                throw new ArgumentException("Co columns were mapped.");

            var setSql = columns.Select(p => $"{GetColumnName(classMap, p, false)} = {Configuration.Dialect.ParameterPrefix}{p.Name}");

            var sql = Sql.Update(GetTableName(classMap)).Set(setSql.AppendStrings()).Where(predicate.GetSql(this, parameters)).ToString();

            return new SQLConvertResult(sql, parameters);
        }

        /// <summary>
        /// Delete...
        /// </summary>
        /// <param name="classMap"></param>
        /// <param name="predicate"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public SQLConvertResult Delete(IClassMap classMap, ISQLPredicate predicate, IDictionary<string, object> parameters)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var sql = Sql.Delete().From(GetTableName(classMap)).Where(predicate.GetSql(this, parameters)).ToString();

            return new SQLConvertResult(sql, parameters);
        }

        /// <summary>
        /// Identity sql
        /// </summary>
        /// <param name="classMap"></param>
        /// <returns></returns>
        public string IdentitySql(IClassMap classMap)
        {
            return Configuration.Dialect.GetIdentitySql(GetTableName(classMap));
        }

        /// <summary>
        /// Gets table name
        /// </summary>
        /// <param name="classMap"></param>
        /// <returns></returns>
        public string GetTableName(IClassMap classMap)
        {
            return Configuration.Dialect.GetTableName(classMap.SchemaName, classMap.TableName, null);
        }

        /// <summary>
        /// Gets column name
        /// </summary>
        /// <param name="map"></param>
        /// <param name="property"></param>
        /// <param name="includeAlias"></param>
        /// <returns></returns>
        public string GetColumnName(IClassMap map, IPropertyMap property, bool includeAlias)
        {
            string alias = null;
            if (property.ColumnName != property.Name && includeAlias)
                alias = property.Name;
            return Configuration.Dialect.GetColumnName(GetTableName(map), property.ColumnName, alias);
        }

        /// <summary>
        /// Get column name
        /// </summary>
        /// <param name="map"></param>
        /// <param name="propertyName"></param>
        /// <param name="includeAlias"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public string GetColumnName(IClassMap map, string propertyName, bool includeAlias)
        {
            var propertyMap = map.PropertyMaps.SingleOrDefault(p => p.Name.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase));
            if (propertyMap == null)
                throw new ArgumentException($"Cloud not find '{propertyName}' in Mapping.");

            return GetColumnName(map, propertyMap, includeAlias);
        }

        /// <summary>
        /// Supports multiple statements
        /// </summary>
        /// <returns></returns>
        public bool SupportsMultipleStatements()
        {
            return Configuration.Dialect.SupportsMultipleStatements;
        }

        /// <summary>
        /// Build select columns
        /// </summary>
        /// <param name="classMap"></param>
        /// <returns></returns>
        public virtual string BuildSelectColumns(IClassMap classMap)
        {
            var columns = classMap.PropertyMaps.Where(p => !p.Ignored).Select(p => GetColumnName(classMap, p, true));
            return columns.AppendStrings();
        }

        private StringBuilder Sql => new StringBuilder();
    }
}