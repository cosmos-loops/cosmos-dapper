using Dapper;

namespace Cosmos.Dapper.Core.Binders
{
    /// <summary>
    /// Sync binding manager
    /// </summary>
    public static class SyncBindingManager
    {
        /// <summary>
        /// Sync...
        /// </summary>
        /// <param name="mappingConfig"></param>
        public static void Sync(IClassMapGetter mappingConfig)
        {
            SqlMapper.TypeMapProvider = t => new BindingTypeMap(t, mappingConfig.GetMap(t));
        }
    }
}