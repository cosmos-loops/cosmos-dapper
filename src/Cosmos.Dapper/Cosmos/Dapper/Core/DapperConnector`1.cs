using System;
using System.Data;
using System.Data.Common;
using Cosmos.Data.Statements;
using Dapper;

namespace Cosmos.Dapper.Core
{
    /// <summary>
    /// Dapper connector
    /// </summary>
    /// <typeparam name="TConnection"></typeparam>
    public class DapperConnector<TConnection> : DapperConnector, IDapperConnector<TConnection>
        where TConnection : DbConnection
    {
        /// <summary>
        /// Create a new instance of <see cref="DapperConnector" />
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="config"></param>
        /// <param name="sqlGenerator"></param>
        public DapperConnector(TConnection connection, IDapperMappingConfig config, ISQLGenerator sqlGenerator)
            : base(connection, config, sqlGenerator)
        {
            RawConnectionType = typeof(TConnection);
        }

        /// <summary>
        /// Gets raw connection
        /// </summary>
        public TConnection RawConnection => Connection as TConnection;

        /// <summary>
        /// Gets type of raw connection
        /// </summary>
        public Type RawConnectionType { get; }
    }
}