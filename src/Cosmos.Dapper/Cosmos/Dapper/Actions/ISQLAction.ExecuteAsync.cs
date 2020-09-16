using System.Threading;
using System.Threading.Tasks;

namespace Cosmos.Dapper.Actions
{
    /// <summary>
    /// Interface for asynchronous executable SqlAction
    /// </summary>
    // ReSharper disable once InconsistentNaming
    internal interface IAsynchronousExecutableSQLAction
    {
        /// <summary>
        /// Execute called from bank async
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task ExecuteCalledFromBankAsync(CancellationToken cancellationToken);
    }
}