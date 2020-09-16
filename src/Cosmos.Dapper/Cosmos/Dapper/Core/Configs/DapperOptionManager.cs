namespace Cosmos.Dapper.Core.Configs
{
    /// <summary>
    /// Dapper option manager
    /// </summary>
    public static class DapperOptionManager
    {
        // ReSharper disable once InconsistentNaming
        private static readonly DapperOptionCollection _collectionCache = new DapperOptionCollection();

        /// <summary>
        /// Get dapper option by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DapperOptions Get(string name) => _collectionCache.GetOptions(name);

        /// <summary>
        /// Get dapper option by typed context
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <returns></returns>
        public static DapperOptions<TContext> Get<TContext>() where TContext : class, IDapperContext => _collectionCache.GetOptions<TContext>();

        /// <summary>
        /// Get dapper option by typed context and name
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="TContext"></typeparam>
        /// <returns></returns>
        public static DapperOptions<TContext> Get<TContext>(string name) where TContext : class, IDapperContext => _collectionCache.GetOptions<TContext>(name);

        /// <summary>
        /// Sets dapper option
        /// </summary>
        /// <param name="options"></param>
        public static void Set(DapperOptions options) => _collectionCache.Add(options.Name, options);

        /// <summary>
        /// Sets dapper options
        /// </summary>
        /// <param name="name"></param>
        /// <param name="options"></param>
        /// <typeparam name="TContext"></typeparam>
        public static void Set<TContext>(string name, DapperOptions<TContext> options) where TContext : class, IDapperContext => _collectionCache.Add(name, options);
    }
}