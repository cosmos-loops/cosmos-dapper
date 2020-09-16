using System;
using Cosmos.Data.Core.Pools;
using Cosmos.Optionals;
using MySql.Data.MySqlClient;

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
        public static MySqlConnection ToConn(this DapperOptions options)
        {
            if (options is null)
                throw new ArgumentNullException(nameof(options));

            return options.ConnectionPoolMode.SafeValue()
                ? ReturnPool(options)
                : ReturnConn(options);
        }

        /// <summary>
        /// To connection
        /// </summary>
        /// <param name="accessor"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static MySqlConnection ToConn(this DapperOptionsAccessor accessor, string name)
        {
            if (accessor is null)
                throw new ArgumentNullException(nameof(accessor));
            return accessor.Get(name).ToConn();
        }
        
        
        #region Internal methods

        private static Func<string, Func<string, Func<Action, Func<Action, Func<MySqlConnectionPool>>>>> _factory()
            => name => connString => handler0 => handler1 =>
                () => new MySqlConnectionPool(name, connString, handler0, handler1);

        private static MySqlConnection ReturnPool(DapperOptions options)
        {
            ConnectionPool.Pools.Register<MySqlConnection, MySqlConnectionPool>(
                _factory()(options.Name)(options.ConnectionString)(null)(null),
                options.ConnectionString);

            using var objectOut = ConnectionPool.Get<MySqlConnection>(options.ConnectionString);
            return objectOut.Value;
        }

        private static MySqlConnection ReturnConn(DapperOptions options)
        {
            return new MySqlConnection(options.ConnectionString);
        }

        #endregion
    }
}