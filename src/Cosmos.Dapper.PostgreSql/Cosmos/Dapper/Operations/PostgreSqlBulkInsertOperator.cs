using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cosmos.Asynchronous;
using Cosmos.Dapper.Core;
using Npgsql;

namespace Cosmos.Dapper.Operations
{
    /// <summary>
    /// Bulk insert operator for PostgreSql
    /// </summary>
    public class PostgreSqlBulkInsertOperator : DapperBulkInsertOperator
    {
        /// <summary>
        /// Create a new instance of <see cref="PostgreSqlBulkInsertOperator" />
        /// </summary>
        /// <param name="connector"></param>
        public PostgreSqlBulkInsertOperator(IDapperConnector connector) : base(connector) { }

        /// <summary>
        /// Process
        /// </summary>
        /// <param name="dataSet"></param>
        /// <typeparam name="T"></typeparam>
        public override void Process<T>(IList<T> dataSet)
        {
            if (dataSet is null || !dataSet.Any()) return;

            var classMap = GetMap<T>();
            var tableName = GetTableName<T>();
            var dt = DataTableBuilder.Build(classMap, dataSet, tableName);

            using (var writer = GetConnection<NpgsqlConnection>().BeginBinaryImport(BuildCopyCommand(dt)))
            {
                foreach (DataRow row in dt.Rows)
                {
                    writer.WriteRow(row.ItemArray);
                }
            }
        }

        /// <summary>
        /// Process async
        /// </summary>
        /// <param name="dataSet"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public override Task ProcessAsync<T>(IList<T> dataSet)
        {
            Process(dataSet);
            return Tasks.CompletedTask();
        }

        private string BuildCopyCommand(DataTable table)
        {
            var sb = new StringBuilder();
            sb.Append($"COPY {table.TableName} (");

            for (var i = 0; i < table.Columns.Count; i++)
            {
                if (i != 0) sb.Append(",");
                sb.Append(QuoteString(table.Columns[i].ColumnName));
            }

            sb.Append(") FROM STDIN (FORMAT BINARY)");

            return sb.ToString();
        }
    }
}