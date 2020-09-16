using System.Collections.Generic;
using Cosmos.Dapper;
using Cosmos.Dapper.Mapper;
using Dapper;

namespace Cosmos.Data.Statements
{
    /// <summary>
    /// Interface for sql generator
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public interface ISQLGenerator
    {
        /// <summary>
        /// Gets configuration
        /// </summary>
        IDapperMappingConfig Configuration { get; }

        /// <summary>
        /// Select...
        /// </summary>
        /// <param name="classMap"></param>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        SQLConvertResult Select(IClassMap classMap, ISQLPredicate predicate, SQLSortSet sort, IDictionary<string, object> parameters);

        /// <summary>
        /// Select paged...
        /// </summary>
        /// <param name="classMap"></param>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        SQLConvertResult SelectPaged(IClassMap classMap, ISQLPredicate predicate, SQLSortSet sort, int pageNumber, int pageSize, IDictionary<string, object> parameters);

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
        SQLConvertResult SelectSet(IClassMap classMap, ISQLPredicate predicate, SQLSortSet sort, int limitFrom, int limitTo, IDictionary<string, object> parameters);

        /// <summary>
        /// Count...
        /// </summary>
        /// <param name="classMap"></param>
        /// <param name="predicate"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        SQLConvertResult Count(IClassMap classMap, ISQLPredicate predicate, IDictionary<string, object> parameters);

        /// <summary>
        /// Insert...
        /// </summary>
        /// <param name="classMap"></param>
        /// <returns></returns>
        SQLConvertResult Insert(IClassMap classMap);

        /// <summary>
        /// Update...
        /// </summary>
        /// <param name="classMap"></param>
        /// <param name="predicate"></param>
        /// <param name="parameters"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <returns></returns>
        SQLConvertResult Update(IClassMap classMap, ISQLPredicate predicate, IDictionary<string, object> parameters, bool ignoreAllKeyProperties);

        /// <summary>
        /// Delete...
        /// </summary>
        /// <param name="classMap"></param>
        /// <param name="predicate"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        SQLConvertResult Delete(IClassMap classMap, ISQLPredicate predicate, IDictionary<string, object> parameters);

        /// <summary>
        /// Identity sql
        /// </summary>
        /// <param name="classMap"></param>
        /// <returns></returns>
        string IdentitySql(IClassMap classMap);

        /// <summary>
        /// Gets table name
        /// </summary>
        /// <param name="classMap"></param>
        /// <returns></returns>
        string GetTableName(IClassMap classMap);

        /// <summary>
        /// Gets column name 
        /// </summary>
        /// <param name="map"></param>
        /// <param name="property"></param>
        /// <param name="includeAlias"></param>
        /// <returns></returns>
        string GetColumnName(IClassMap map, IPropertyMap property, bool includeAlias);

        /// <summary>
        /// Gets column name
        /// </summary>
        /// <param name="map"></param>
        /// <param name="propertyName"></param>
        /// <param name="includeAlias"></param>
        /// <returns></returns>
        string GetColumnName(IClassMap map, string propertyName, bool includeAlias);

        /// <summary>
        /// Support multiple statements
        /// </summary>
        /// <returns></returns>
        bool SupportsMultipleStatements();
    }
}