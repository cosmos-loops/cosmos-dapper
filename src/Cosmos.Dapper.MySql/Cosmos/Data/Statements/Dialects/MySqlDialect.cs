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
    /// Dialect of MySQL
    /// </summary>
    public class MySqlDialect : SQLDialectBase
    {
        /// <summary>
        /// Dialect name
        /// </summary>
        public override string DialectName => "MySql";

        /// <summary>
        /// Open quote
        /// </summary>
        public override char OpenQuote => '`';

        /// <summary>
        /// Close quote
        /// </summary>
        public override char CloseQuote => '`';

        /// <summary>
        /// Get identity sql
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public override string GetIdentitySql(string tableName) => "SELECT CONVERT(LAST_INSERT_ID(), SIGNED INTEGER) AS ID";

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
        public override string GetSetSql(string sql, int firstResult, int maxResults, IDictionary<string, object> parameters)
        {
            var ret = $"{sql} LIMIT @firstResult, @maxResults";
            parameters.Add("@firstResult", firstResult);
            parameters.Add("@maxResults", maxResults);
            return ret;
        }
    }
}