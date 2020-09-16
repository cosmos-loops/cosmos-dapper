using Cosmos.Dapper.Operations;
using Cosmos.Data.Statements;

namespace Cosmos.Dapper.Core
{
    /// <summary>
    /// Context params for SQLite
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public sealed class SQLiteContextParams : IDapperContextParams
    {
        private readonly ISQLGenerator _generator;
        private readonly IDapperMappingConfig _mappingConfig;

        /// <summary>
        /// Create a new instance of <see cref="SQLiteContextParams" />
        /// </summary>
        /// <param name="mappingConfig"></param>
        public SQLiteContextParams(IDapperMappingConfig mappingConfig)
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
            return new SQLiteBulkInsertOperator(connector);
        }
    }
}