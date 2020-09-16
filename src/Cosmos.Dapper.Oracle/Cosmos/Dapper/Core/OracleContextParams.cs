using Cosmos.Dapper.Operations;
using Cosmos.Data.Statements;

namespace Cosmos.Dapper.Core
{
    /// <summary>
    /// Context params for Oracle
    /// </summary>
    public sealed class OracleContextParams : IDapperContextParams
    {
        private readonly ISQLGenerator _generator;
        private readonly IDapperMappingConfig _mappingConfig;

        /// <summary>
        /// Create a new instance of <see cref="OracleContextParams" />
        /// </summary>
        /// <param name="mappingConfig"></param>
        public OracleContextParams(IDapperMappingConfig mappingConfig)
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
            return new OracleBulkInsertOperator(connector);
        }
    }
}