using System.Collections.Concurrent;

namespace Cosmos.Dapper.Core.Configs
{
    /// <summary>
    /// Dapper configuration accessor
    /// </summary>
    public static class DapperConfigAccessor
    {
        // ReSharper disable once InconsistentNaming
        private static readonly ConcurrentDictionary<int, DapperConfig> _dapperConfigCache = new ConcurrentDictionary<int, DapperConfig>();
        private static DapperConfig DapperMappingConfig { get; set; }

        /// <summary>
        /// Gets cache by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static DapperConfig Cache(string key)
        {
            return key is null
                ? null
                : _dapperConfigCache.TryGetValue(key.GetHashCode(), out var ret)
                    ? ret
                    : null;
        }

        /// <summary>
        /// Refresh cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="config"></param>
        public static void RefreshCache(string key, DapperConfig config)
        {
            if (key is null)
                return;

            _dapperConfigCache.AddOrUpdate(key.GetHashCode(), config, (h, c) => config);
        }
    }
}