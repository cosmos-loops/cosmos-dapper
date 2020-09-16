using System;
using System.Data;

namespace Cosmos.Dapper.Core
{
    /// <summary>
    /// Interface for Dapper Connector
    /// </summary>
    /// <typeparam name="TConnection"></typeparam>
    public interface IDapperConnector<out TConnection> : IDapperConnector where TConnection : class, IDbConnection
    {
        /// <summary>
        /// Gets raw connection
        /// </summary>
        TConnection RawConnection { get; }

        /// <summary>
        /// Gets type of raw connection
        /// </summary>
        Type RawConnectionType { get; }
    }
}