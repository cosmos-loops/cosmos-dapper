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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    /// Dialect of Microsoft SQL Server
    /// </summary>
    public class SqlServerDialect : SQLDialectBase
    {
        /// <summary>
        /// Dialect name
        /// </summary>
        public override string DialectName => "SqlServer";

        /// <summary>
        /// Open quote
        /// </summary>
        public override char OpenQuote => '[';

        /// <summary>
        /// Close quote
        /// </summary>
        public override char CloseQuote => ']';

        /// <summary>
        /// Get identity sql
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public override string GetIdentitySql(string tableName) => "SELECT CAST(SCOPE_IDENTITY() AS BIGINT) AS [Id]";

        /// <summary>
        /// Get paging sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public override string GetPagingSql(string sql, int pageNumber, int pageSize, IDictionary<string, object> parameters)
        {
            var startValue = (pageNumber * pageSize) + 1;
            return GetSetSql(sql, startValue, pageSize, parameters);
        }

        /// <summary>
        /// Get set sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="firstResult"></param>
        /// <param name="maxResults"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public override string GetSetSql(string sql, int firstResult, int maxResults, IDictionary<string, object> parameters)
        {
            if (string.IsNullOrEmpty(sql))
                throw new ArgumentNullException(nameof(sql));

            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var selectIndex = GetSelectEnd(sql) + 1;
            var orderByClause = GetOrderByClause(sql);
            if (orderByClause == null)
                orderByClause = "ORDER BY CURRENT_TIMESTAMP";

            var projectColumns = GetColumnNames(sql).Aggregate(
                new StringBuilder(),
                (sb, s) => (sb.Length == 0 ? sb : sb.Append(", ")).Append(GetColumnName("_proj", s, null)),
                sb => sb.ToString());

            var newSql = sql
               .Replace($" {orderByClause}", string.Empty)
               .Insert(selectIndex,
                    $"ROW_NUMBER() OVER(ORDER BY {orderByClause.Substring(9)} AS {GetColumnName(null, "_row_number", null)}, ");

            var ret = string.Format("SELECT TOP({0}) {1} FROM ({2}) [_proj] WHERE {3} >= @_pageStartRow ORDER BY {3}",
                maxResults, projectColumns.Trim(), newSql, GetColumnName("_proj", "_row_number", null));

            parameters.Add("@_pageStartRow", firstResult);

            return ret;
        }

        /// <summary>
        /// Get order-by clause
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected virtual string GetOrderByClause(string sql)
        {
            var orderByIndex = sql.LastIndexOf(" ORDER BY ", StringComparison.InvariantCultureIgnoreCase);
            if (orderByIndex == -1)
                return null;

            var ret = sql.Substring(orderByIndex).Trim();

            var whereIndex = ret.IndexOf(" WHERE ", StringComparison.InvariantCultureIgnoreCase);
            if (whereIndex == -1)
                return ret;

            return ret.Substring(0, whereIndex).Trim();
        }

        /// <summary>
        /// Get from-start
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected virtual int GetFromStart(string sql)
        {
            var selectCount = 0;
            var words = sql.Split(' ');
            var fromIndex = 0;

            foreach (var word in words)
            {
                if (word.Equals("SELECT", StringComparison.InvariantCultureIgnoreCase))
                    selectCount++;

                if (word.Equals("FROM", StringComparison.InvariantCultureIgnoreCase))
                {
                    selectCount--;
                    if (selectCount == 0)
                        break;
                }

                fromIndex += word.Length + 1;
            }

            return fromIndex;
        }

        /// <summary>
        /// Get select-end
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        protected virtual int GetSelectEnd(string sql)
        {
            if (sql.StartsWith("SELECT DISTINCT", StringComparison.InvariantCultureIgnoreCase))
                return 15;

            if (sql.StartsWith("SELECT", StringComparison.InvariantCultureIgnoreCase))
                return 6;

            throw new ArgumentException("SQL must be a SELECT statement.", nameof(sql));
        }

        /// <summary>
        /// Get column name
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected virtual IList<string> GetColumnNames(string sql)
        {
            var start = GetSelectEnd(sql);
            var stop = GetFromStart(sql);
            var columnSqlSegments = sql.Substring(start, stop - start).Split(',');

            var ret = new List<string>();

            foreach (var c in columnSqlSegments)
            {
                var index = c.IndexOf(" AS ", StringComparison.InvariantCultureIgnoreCase);
                if (index > 0)
                {
                    ret.Add(c.Substring(index + 4).Trim());
                    continue;
                }

                var colParts = c.Split('.');
                ret.Add(colParts[colParts.Length - 1].Trim());
            }

            return ret;
        }
    }
}