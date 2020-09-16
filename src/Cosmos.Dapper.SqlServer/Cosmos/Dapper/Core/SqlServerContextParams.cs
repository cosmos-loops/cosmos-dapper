using Cosmos.Dapper.Operations;
using Cosmos.Data.Statements;

namespace Cosmos.Dapper.Core
{
    /// <summary>
    /// Context params for Microsoft SQL Server
    /// </summary>
    public sealed class SqlServerContextParams : IDapperContextParams
    {
        private readonly ISQLGenerator _generator;
        private readonly IDapperMappingConfig _mappingConfig;

        /// <summary>
        /// Create a new instance of <see cref="SqlServerContextParams" />
        /// </summary>
        /// <param name="mappingConfig"></param>
        public SqlServerContextParams(IDapperMappingConfig mappingConfig)
        {
            _generator = new SQLGenerator(mappingConfig);
            _mappingConfig = mappingConfig;
        }

        /// <inheritdoc />
        public DapperConfig GetConfig() => (DapperConfig) _mappingConfig;

        /// <inheritdoc />
        public DapperOptions GetOptions() => _mappingConfig.Options;

        /// <inheritdoc />
        public ISQLGenerator GetSqlGenerator() => _generator;

        /// <inheritdoc />
        public IDapperBulkInsertOperator GetBulkInsertOperator(IDapperConnector connector)
        {
            return new SqlServerBulkInsertOperator(connector);
        }
    }
}