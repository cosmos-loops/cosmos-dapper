using Cosmos.Dapper.EntityMapping;

namespace Cosmos.Dapper.Core.Mapping
{
    /// <summary>
    /// Interface for map
    /// </summary>
    public interface IMap
    {
        /// <summary>
        /// Map
        /// </summary>
        /// <param name="builder"></param>
        void Map(DapperClassBuilder builder);
    }
}