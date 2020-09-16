using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cosmos.Dapper.Core;
using Cosmos.Dapper.Mapper;
using Cosmos.Optionals;
using Microsoft.Data.SqlClient;

namespace Cosmos.Dapper.Operations
{
    /// <summary>
    /// Bulk insert operator for SqlServer
    /// </summary>
    public class SqlServerBulkInsertOperator : DapperBulkInsertOperator
    {
        /// <summary>
        /// Create a new instance of <see cref="SqlServerBulkInsertOperator" />
        /// </summary>
        /// <param name="connector"></param>
        public SqlServerBulkInsertOperator(IDapperConnector connector) : base(connector) { }

        /// <summary>
        /// Process
        /// </summary>
        /// <param name="dataSet"></param>
        /// <typeparam name="T"></typeparam>
        public override void Process<T>(IList<T> dataSet)
        {
            if (dataSet is null || !dataSet.Any())
                return;

            var options = Options;

            var classMap = GetMap<T>();
            var tableName = GetTableName<T>();
            var dt = DataTableBuilder.Build(classMap, dataSet, tableName);

            using (var bulkCopy = new SqlBulkCopy(GetConnection<SqlConnection>()))
            {
                bulkCopy.DestinationTableName = tableName;
                UpdateBulkColumnMapping(bulkCopy, classMap);
                bulkCopy.BatchSize = options.BatchSize;
                bulkCopy.BulkCopyTimeout = options.Timeout.SafeValue(30);
                bulkCopy.WriteToServer(dt);
                bulkCopy.Close();
            }
        }

        /// <summary>
        /// Process async
        /// </summary>
        /// <param name="dataSet"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public override async Task ProcessAsync<T>(IList<T> dataSet)
        {
            if (dataSet is null || !dataSet.Any())
                return;

            var options = Options;

            var classMap = GetMap<T>();
            var tableName = GetTableName<T>();
            var dt = DataTableBuilder.Build(classMap, dataSet, tableName);

            using (var bulkCopy = new SqlBulkCopy(GetConnection<SqlConnection>()))
            {
                bulkCopy.DestinationTableName = tableName;
                UpdateBulkColumnMapping(bulkCopy, classMap);
                bulkCopy.BatchSize = options.BatchSize;
                bulkCopy.BulkCopyTimeout = options.Timeout.SafeValue(30);
                await bulkCopy.WriteToServerAsync(dt);
                bulkCopy.Close();
            }
        }

        private static void UpdateBulkColumnMapping(SqlBulkCopy bulkCopy, IClassMap classMap)
        {
            foreach (var propertyMap in classMap.PropertyMaps)
                bulkCopy.ColumnMappings.Add(propertyMap.ColumnName, propertyMap.ColumnName);
        }
    }
}