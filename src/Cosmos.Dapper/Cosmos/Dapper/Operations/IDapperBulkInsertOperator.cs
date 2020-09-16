using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cosmos.Dapper.Operations
{
    /// <summary>
    /// Interface for dapper bulk insert operator
    /// </summary>
    public interface IDapperBulkInsertOperator
    {
        /// <summary>
        /// Process
        /// </summary>
        /// <param name="dataSet"></param>
        /// <typeparam name="T"></typeparam>
        void Process<T>(IList<T> dataSet) where T : class;

        /// <summary>
        /// Process async
        /// </summary>
        /// <param name="dataSet"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task ProcessAsync<T>(IList<T> dataSet) where T : class;
    }
}