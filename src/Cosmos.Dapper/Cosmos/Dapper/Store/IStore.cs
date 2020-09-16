using Cosmos.Models;

namespace Cosmos.Dapper.Store
{
    /// <summary>
    /// Interface for store
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IStore<TEntity> :
        IQueryableStore<TEntity>,
        IDynamicExpressionQueryableStore<TEntity>,
        IPredicateQueryableStore<TEntity>,
        ISqlKataQueryableStore<TEntity>,
        IWriteableStore<TEntity>,
        IDynamicExpressionWriteableStore<TEntity>,
        IPredicateWriteableStore<TEntity>
        where TEntity : class, IEntity, new() { }

    /// <summary>
    /// Interface for store
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IStore<TEntity, in TKey> :
        IStore<TEntity>,
        IQueryableStore<TEntity, TKey>,
        IWriteableStore<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, new() { }
}