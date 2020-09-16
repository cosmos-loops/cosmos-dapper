/*
 * Copyright 2011 Thad Smith, Page Brooks and contributors
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections.Generic;

/*
 * Reference to:
 *      tmsmith/Dapper-Extensions
 *      Author: Thad Smith
 *      Url: https://github.com/tmsmith/Dapper-Extensions
 *      Apache License 2.0
 *          http://www.apache.org/licenses/LICENSE-2.0
 */

namespace Cosmos.Data.Statements.Dialects
{
    /// <summary>
    /// Dialect of PostgreSQL
    /// </summary>
    public class PostgreSqlDialect : SQLDialectBase
    {
        /// <summary>
        /// Dialect name
        /// </summary>
        public override string DialectName => "PostgreSql";

        /// <summary>
        /// Get identity sql
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public override string GetIdentitySql(string tableName) => "SELECT LASTVAL() AS Id";

        /// <summary>
        /// Get paging sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public override string GetPagingSql(string sql, int pageNumber, int pageSize, IDictionary<string, object> parameters)
            => GetSetSql(sql, pageNumber * pageSize, pageSize, parameters);

        /// <summary>
        /// Get set sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="firstResult"></param>
        /// <param name="maxResults"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public override string GetSetSql(string sql, int firstResult, int maxResults,
            IDictionary<string, object> parameters)
        {
            var ret = $"{sql} LIMIT @maxResults OFFSET @pageStartRowNbr";
            parameters.Add("@maxResults", maxResults);
            parameters.Add("@pageStartRowNbr", firstResult * maxResults);
            return ret;
        }

        /// <summary>
        /// Get column name
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="columnName"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public override string GetColumnName(string prefix, string columnName, string alias)
            => base.GetColumnName(null, columnName, alias).ToLower();

        /// <summary>
        /// Get table name
        /// </summary>
        /// <param name="schemaName"></param>
        /// <param name="tableName"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public override string GetTableName(string schemaName, string tableName, string alias)
            => base.GetTableName(schemaName, tableName, alias).ToLower();
    }
}