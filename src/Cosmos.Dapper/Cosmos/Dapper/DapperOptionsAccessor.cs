using Cosmos.Dapper.Core.Configs;

namespace Cosmos.Dapper
{
    /// <summary>
    /// Dapper options accessor
    /// </summary>
    public class DapperOptionsAccessor
    {
        /// <summary>
        /// Gets dapper options by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DapperOptions Get(string name) => DapperOptionManager.Get(name);

        /// <summary>
        /// Gets dapper options by type
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <returns></returns>
        public DapperOptions<TContext> Get<TContext>() where TContext : class, IDapperContext => DapperOptionManager.Get<TContext>();

        /// <summary>
        /// Gets dapper options by type and name
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="TContext"></typeparam>
        /// <returns></returns>
        public DapperOptions<TContext> Get<TContext>(string name) where TContext : class, IDapperContext => DapperOptionManager.Get<TContext>(name);
    }
}