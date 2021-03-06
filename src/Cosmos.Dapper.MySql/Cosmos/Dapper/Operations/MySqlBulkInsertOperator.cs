﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cosmos.Dapper.Core;
using Cosmos.Optionals;
using MySql.Data.MySqlClient;
using MySql.Data.MySqlClient.SqlBulkCopy;

namespace Cosmos.Dapper.Operations
{
    /// <summary>
    /// Bulk insert operator for MySQL
    /// </summary>
    public class MySqlBulkInsertOperator : DapperBulkInsertOperator
    {
        /// <summary>
        /// Create a new instance of <see cref="MySqlBulkInsertOperator" />
        /// </summary>
        /// <param name="connector"></param>
        public MySqlBulkInsertOperator(IDapperConnector connector) : base(connector) { }

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
            
            using (var bulkCopy = new MySqlBulkCopy(GetConnection<MySqlConnection>()))
            {
                bulkCopy.DestinationTableName = tableName;
                bulkCopy.BulkCopyTimeout = options.Timeout.SafeValue(30);
                bulkCopy.SecureFilePriv = options.SecureFilePriv ?? AppDomain.CurrentDomain.BaseDirectory;
                bulkCopy.ClearTempCsvAfterWriting = true;
                bulkCopy.WriteToServer(dt);
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

            using (var bulkCopy = new MySqlBulkCopy(GetConnection<MySqlConnection>()))
            {
                bulkCopy.DestinationTableName = tableName;
                bulkCopy.BulkCopyTimeout = options.Timeout.SafeValue(30);
                bulkCopy.SecureFilePriv = options.SecureFilePriv ?? AppDomain.CurrentDomain.BaseDirectory;
                bulkCopy.ClearTempCsvAfterWriting = true;
                await bulkCopy.WriteToServerAsync(dt);
            }
        }
    }
}