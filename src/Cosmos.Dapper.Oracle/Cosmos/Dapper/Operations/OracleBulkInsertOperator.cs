using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cosmos.Asynchronous;
using Cosmos.Dapper.Core;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Client.SqlBulkCopy;

namespace Cosmos.Dapper.Operations
{
    /// <summary>
    /// Bulk insert operator for Oracle
    /// </summary>
    public class OracleBulkInsertOperator : DapperBulkInsertOperator
    {
        /// <summary>
        /// Create a new instance of <see cref="OracleBulkInsertOperator" />
        /// </summary>
        /// <param name="connector"></param>
        public OracleBulkInsertOperator(IDapperConnector connector) : base(connector) { }

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

            using (var bulkCopy = new OracleBulkCopy(GetConnection<OracleConnection>()))
            {
                bulkCopy.DestinationTableName = tableName;
                bulkCopy.BatchSize = Options.BatchSize;
                bulkCopy.BulkCopyTimeout = Options.Timeout;
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
        public override Task ProcessAsync<T>(IList<T> dataSet)
        {
            Process(dataSet);
            return Tasks.CompletedTask();
        }
    }
}