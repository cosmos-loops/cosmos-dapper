using System.Data;

namespace Cosmos.Dapper.Core.Contextual
{
    /// <summary>
    /// Interface for WithConnection
    /// </summary>
    /// <typeparam name="TConnection"></typeparam>
    public interface IWithConnection<out TConnection>
        where TConnection : class, IDbConnection
    {
        /// <summary>
        /// Gets connector
        /// </summary>
        IDapperConnector<TConnection> Connector { get; }
    }
}