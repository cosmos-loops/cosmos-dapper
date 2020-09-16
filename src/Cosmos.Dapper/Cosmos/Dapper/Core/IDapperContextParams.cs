using Cosmos.Dapper.Operations;
using Cosmos.Data.Statements;
using Dapper;

namespace Cosmos.Dapper.Core
{
    /// <summary>
    /// Interface for Dapper Context params
    /// </summary>
    public interface IDapperContextParams
    {
        /// <summary>
        /// Gets Dapper mapping config
        /// </summary>
        /// <returns></returns>
        DapperConfig GetConfig();

        /// <summary>
        /// Gets Dapper options
        /// </summary>
        /// <returns></returns>
        DapperOptions GetOptions();
        
        /// <summary>
        /// Gets sql generator
        /// </summary>
        /// <returns></returns>
        ISQLGenerator GetSqlGenerator();

        /// <summary>
        /// Gets bulk insert operator
        /// </summary>
        /// <param name="connector"></param>
        /// <returns></returns>
        IDapperBulkInsertOperator GetBulkInsertOperator(IDapperConnector connector);
    }
}