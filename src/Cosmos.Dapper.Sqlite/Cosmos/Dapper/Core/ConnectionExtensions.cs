using System;
using System.Data.SQLite;

namespace Cosmos.Dapper.Core
{
    /// <summary>
    /// Extensions of connection
    /// </summary>
    public static class ConnectionExtensions
    {
        /// <summary>
        /// To Conn
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static SQLiteConnection ToConn(this DapperOptions options)
        {
            if (options is null)
                throw new ArgumentNullException(nameof(options));
            return new SQLiteConnection(options.ConnectionString);
        }

        /// <summary>
        /// To Conn
        /// </summary>
        /// <param name="accessor"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static SQLiteConnection ToConn(this DapperOptionsAccessor accessor, string name)
        {
            if (accessor is null)
                throw new ArgumentNullException(nameof(accessor));
            return accessor.Get(name).ToConn();
        }
    }
}