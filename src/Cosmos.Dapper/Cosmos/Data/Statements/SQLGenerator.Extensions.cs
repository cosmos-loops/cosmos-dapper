using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.Dapper.Core.Helpers;

namespace Cosmos.Data.Statements
{
    /// <summary>
    /// Extensions for Sql generator
    /// </summary>
    // ReSharper disable once InconsistentNaming
    internal static class SQLGeneratorExtensions
    {
        public static StringBuilder Select(this StringBuilder sql)
        {
            return sql.Append("SELECT");
        }

        public static StringBuilder Select(this StringBuilder sql, string columnNames)
        {
            return sql.Append($"SELECT {columnNames}");
        }

        public static StringBuilder Count(this StringBuilder sql, string count)
        {
            return sql.Append($" COUNT({count})");
        }

        public static StringBuilder As(this StringBuilder sql, string alias)
        {
            return sql.Append($" AS {alias}");
        }

        public static StringBuilder Insert(this StringBuilder sql)
        {
            return sql.Append("INSERT");
        }

        public static StringBuilder Into(this StringBuilder sql, string tableName)
        {
            return sql.Append($" INTO {tableName}");
        }

        public static StringBuilder Columns(this StringBuilder sql, IEnumerable<string> columnNames)
        {
            return sql.Append($" ({columnNames.AppendStrings()})");
        }

        public static StringBuilder Values(this StringBuilder sql, IEnumerable<string> parameters)
        {
            return sql.Append($" VALUES ({parameters.AppendStrings()})");
        }

        public static StringBuilder Update(this StringBuilder sql, string tableName)
        {
            return sql.Append($"UPDATE {tableName}");
        }

        public static StringBuilder Set(this StringBuilder sql, string setSql)
        {
            return sql.Append($" SET {setSql}");
        }

        public static StringBuilder Delete(this StringBuilder sql)
        {
            return sql.Append("DELETE");
        }

        public static StringBuilder From(this StringBuilder sql, string from)
        {
            return sql.Append($" FROM {from}");
        }

        public static StringBuilder Where(this StringBuilder sql, string conditionSql)
        {
            return sql.Append($" WHERE {conditionSql}");
        }

        public static StringBuilder Where(this StringBuilder sql, bool needRun, Func<string> conditionSqlFunc)
        {
            if (needRun)
            {
                sql.Where(conditionSqlFunc());
            }

            return sql;
        }

        public static StringBuilder OrderBy(this StringBuilder sql, string orderBySql)
        {
            return string.IsNullOrWhiteSpace(orderBySql)
                ? sql
                : sql.Append($" ORDER BY {orderBySql}");
        }
    }
}