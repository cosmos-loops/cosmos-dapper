using System;

namespace Cosmos.Dapper.Core
{
    /// <summary>
    /// Internal dapper registrar
    /// </summary>
    public static class InternalDapperRegistrar
    {
        /// <summary>
        /// Guard dapper options
        /// </summary>
        /// <param name="options"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void GuardDapperOptions(DapperOptions options)
        {
            if (options is null)
                throw new ArgumentNullException(nameof(options));

            if (string.IsNullOrWhiteSpace(options.ConnectionString))
                throw new ArgumentNullException(nameof(options.ConnectionString));
        }
    }
}