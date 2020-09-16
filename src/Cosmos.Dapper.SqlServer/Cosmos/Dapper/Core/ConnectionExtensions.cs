using System;
using Cosmos.Data.Core.Pools;
using Cosmos.Optionals;
using Microsoft.Data.SqlClient;

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
        public static SqlConnection ToConn(this DapperOptions options)
        {
            if (options is null)
                throw new ArgumentNullException(nameof(options));

            return options.ConnectionPoolMode.SafeValue()
                ? ReturnPool(options)
                : ReturnConn(options);
        }

        /// <summary>
        /// To Connection
        /// </summary>
        /// <param name="accessor"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static SqlConnection ToConn(this DapperOptionsAccessor accessor, string name)
        {
            if (accessor is null)
                throw new ArgumentNullException(nameof(accessor));
            return accessor.Get(name).ToConn();
        }

        #region Internal methods

        private static Func<string, Func<string, Func<Action, Func<Action, Func<SqlConnectionPool>>>>> _factory()
            => name => connString => handler0 => handler1 =>
                () => new SqlConnectionPool(name, connString, handler0, handler1);

        private static SqlConnection ReturnPool(DapperOptions options)
        {
            ConnectionPool.Pools.Register<SqlConnection, SqlConnectionPool>(
                _factory()(options.Name)(options.ConnectionString)(null)(null),
                options.ConnectionString);

            using var objectOut = ConnectionPool.Get<SqlConnection>(options.ConnectionString);
            return objectOut.Value;
        }

        private static SqlConnection ReturnConn(DapperOptions options)
        {
            return new SqlConnection(options.ConnectionString);
        }

        #endregion
    }
}