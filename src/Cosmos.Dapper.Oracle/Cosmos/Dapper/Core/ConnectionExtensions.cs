using System;
using Oracle.ManagedDataAccess.Client;

namespace Cosmos.Dapper.Core
{
    /// <summary>
    /// Extensions of connection
    /// </summary>
    public static class ConnectionExtensions
    {
        /// <summary>
        /// To connection
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static OracleConnection ToConn(this DapperOptions options)
        {
            if (options is null)
                throw new ArgumentNullException(nameof(options));
            return new OracleConnection(options.ConnectionString);
        }

        /// <summary>
        /// To Connection
        /// </summary>
        /// <param name="accessor"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static OracleConnection ToConn(this DapperOptionsAccessor accessor, string name)
        {
            if (accessor is null)
                throw new ArgumentNullException(nameof(accessor));
            return accessor.Get(name).ToConn();
        }
    }
}