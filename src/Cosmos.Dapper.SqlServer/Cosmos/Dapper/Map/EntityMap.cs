using Cosmos.Dapper.EntityMapping;
using Cosmos.Models;

namespace Cosmos.Dapper.Map
{
    /// <summary>
    /// Entity Map
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class EntityMap<TEntity> : DapperMapBase<TEntity>, ISqlServerEntityMap where TEntity : class, IEntity, new() { }
}