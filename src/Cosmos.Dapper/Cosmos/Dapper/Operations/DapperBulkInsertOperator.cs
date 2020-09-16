using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Cosmos.Dapper.Core;
using Cosmos.Dapper.Core.Configs;
using Cosmos.Dapper.Mapper;
using Cosmos.Data.Common;

namespace Cosmos.Dapper.Operations
{
    /// <summary>
    /// Dapper bulk insert operator
    /// </summary>
    public abstract class DapperBulkInsertOperator : IDapperBulkInsertOperator
    {
        private readonly IDapperConnector _connector;

        // ReSharper disable once NotAccessedField.Local
        private readonly ITransactionWrapper _transactionPointer;
        private readonly IDapperMappingConfig _mappingConfig;

        /// <summary>
        /// Create a new instance of <see cref="DapperBulkInsertOperator" />
        /// </summary>
        /// <param name="connector"></param>
        protected DapperBulkInsertOperator(IDapperConnector connector)
        {
            _connector = connector;
            _transactionPointer = connector.TransactionWrapper;
            _mappingConfig = DapperConfigAccessor.Cache(_connector.Connection.ConnectionString);
        }

        /// <summary>
        /// Process
        /// </summary>
        /// <param name="dataSet"></param>
        /// <typeparam name="T"></typeparam>
        public abstract void Process<T>(IList<T> dataSet) where T : class;

        /// <summary>
        /// Process async
        /// </summary>
        /// <param name="dataSet"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public abstract Task ProcessAsync<T>(IList<T> dataSet) where T : class;

        /// <summary>
        /// Gets map
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected IClassMap<T> GetMap<T>() where T : class
        {
            return _mappingConfig.GetMap<T>();
        }

        /// <summary>
        /// Gets table name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected string GetTableName<T>() where T : class
        {
            var classMap = GetMap<T>();
            return _mappingConfig.Dialect.GetTableName(classMap.SchemaName, classMap.TableName, string.Empty);
        }

        /// <summary>
        /// Quote string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string QuoteString(string value)
        {
            return _mappingConfig.Dialect.QuoteString(value);
        }

        /// <summary>
        /// Gets connection
        /// </summary>
        /// <typeparam name="TConnection"></typeparam>
        /// <returns></returns>
        protected TConnection GetConnection<TConnection>() where TConnection : class, IDbConnection
        {
            return _connector.Connection as TConnection;
        }

        /// <summary>
        /// Gets mapping config options
        /// </summary>
        protected DapperOptions Options => _mappingConfig.Options;

        /// <summary>
        /// DataTable Builder...
        /// </summary>
        protected static class DataTableBuilder
        {
            /// <summary>
            /// Build
            /// </summary>
            /// <param name="classMap"></param>
            /// <param name="dataSet"></param>
            /// <param name="tableName"></param>
            /// <typeparam name="T"></typeparam>
            /// <returns></returns>
            public static DataTable Build<T>(IClassMap<T> classMap, IList<T> dataSet, string tableName) where T : class
            {
                var propertyMaps = classMap.PropertyMaps;

                var table = new DataTable();
                table.BeginLoadData();
                table.TableName = tableName;

                UpdateDataColumns(table, propertyMaps);

                foreach (var data in dataSet)
                {
                    table.Rows.Add(BuildDataRow(table, data, propertyMaps));
                }

                table.EndLoadData();
                table.AcceptChanges();

                return table;
            }

            private static void UpdateDataColumns(DataTable table, IEnumerable<IPropertyMap> propertyMaps)
            {
                foreach (var propertyMap in propertyMaps.Where(p => !p.Ignored))
                {
                    var propertyType = propertyMap.PropertyInfo.PropertyType;
                    table.Columns.Add(propertyMap.ColumnName, Nullable.GetUnderlyingType(propertyType) ?? propertyType);
                }
            }

            private static DataRow BuildDataRow<T>(DataTable table, T data, IEnumerable<IPropertyMap> propertyMaps) where T : class
            {
                var row = table.NewRow();
                row.BeginEdit();

                foreach (var propertyMap in propertyMaps.Where(p => !p.Ignored))
                {
                    row[propertyMap.ColumnName] = propertyMap.PropertyInfo.GetValue(data) ?? DBNull.Value;
                }

                row.EndEdit();
                row.AcceptChanges();

                return row;
            }
        }
    }
}