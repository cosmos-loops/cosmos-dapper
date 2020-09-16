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
    /// Dialect of Oracle
    /// </summary>
    public class OracleDialect : SQLDialectBase
    {
        /// <summary>
        /// Dialect name
        /// </summary>
        public override string DialectName => "Oracle";

        /// <summary>
        /// Open quote
        /// </summary>
        public override char OpenQuote => '"';

        /// <summary>
        /// Close quote
        /// </summary>
        public override char CloseQuote => '"';

        /// <summary>
        /// Parameter prefix
        /// </summary>
        public override char ParameterPrefix => ':';

        /// <summary>
        /// Support multiple statements
        /// </summary>
        public override bool SupportsMultipleStatements => false;

        /// <summary>
        /// Quote string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override string QuoteString(string value)
        {
            if (value != null && value[0] == '`')
                return $"{OpenQuote}{value.Substring(1, value.Length - 2)}{CloseQuote}";
            return value.ToUpper();
        }

        /// <summary>
        /// Get identity sql
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override string GetIdentitySql(string tableName)
        {
            throw new NotImplementedException("Oracle does not support get last inserted identity.");
        }

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
            var toSkip = pageNumber * pageSize;
            var topLimit = (pageNumber + 1) * pageSize;

            var sb = new StringBuilder();

            sb.AppendLine("SELECT * FROM (");
            sb.AppendLine("SELECT \"_ss_dapper_1_\".* ROWNUM RNUM FROM(");
            sb.Append(sql);
            sb.AppendLine(") \"_ss_dapper_1_\"");
            sb.AppendLine("WHERE ROWNUM <= :topLimit) \"_ss_dapper_2_\" ");
            sb.AppendLine("WHERE \"_ss_dapper_2_\".RNUM > :topSkip");

            parameters.Add(":topLimit", topLimit);
            parameters.Add(":toSkip", toSkip);

            return sb.ToString();
        }

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
            var sb = new StringBuilder();

            sb.AppendLine("SELECT * FROM (");
            sb.AppendLine("SELECT \"_ss_dapper_1_\".* ROWNUM RNUM FROM(");
            sb.Append(sql);
            sb.AppendLine(") \"_ss_dapper_1_\"");
            sb.AppendLine("WHERE ROWNUM <= :topLimit) \"_ss_dapper_2_\" ");
            sb.AppendLine("WHERE \"_ss_dapper_2_\".RNUM > :topSkip");

            parameters.Add(":topLimit", maxResults + firstResult);
            parameters.Add(":toSkip", firstResult);

            return sb.ToString();
        }
    }
}