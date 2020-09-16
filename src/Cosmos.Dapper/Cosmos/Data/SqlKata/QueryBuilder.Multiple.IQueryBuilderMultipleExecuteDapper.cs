using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cosmos.Data.SqlKata
{
    /// <summary>
    /// Interface for Query builder multiple execute dapper...
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IQueryBuilderMultipleExecuteDapper<TResult>
    {
        /// <summary>
        /// Execute multiple
        /// </summary>
        /// <returns></returns>
        IEnumerable<TResult> ExecuteMultiple();

        /// <summary>
        /// Execute multiple async
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TResult>> ExecuteMultipleAsync();
    }
}