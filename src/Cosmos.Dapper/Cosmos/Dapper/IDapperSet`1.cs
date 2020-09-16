using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Data.Common;
using Cosmos.Data.SqlKata;
using Cosmos.Data.Statements;
using Cosmos.Models;

namespace Cosmos.Dapper
{
    /// <summary>
    /// Interface for DapperSet
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IDapperSet<TEntity> : IDapperSet, IDbSet<TEntity> where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entity"></param>
        void Add(TEntity entity);

        /// <summary>
        /// Add async
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Add range
        /// </summary>
        /// <param name="entities"></param>
        void AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Add range async
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// Update async
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update range
        /// </summary>
        /// <param name="entities"></param>
        void UpdateRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Update range async
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);

        /// <summary>
        /// Delete async
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete range
        /// </summary>
        /// <param name="entities"></param>
        void DeleteRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Delete range async
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="predicate"></param>
        void Delete(ISQLPredicate predicate);

        /// <summary>
        /// Delete async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteAsync(ISQLPredicate predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete by given condition
        /// </summary>
        /// <param name="predicate"></param>
        void Delete(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Delete by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete by given condition
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        int Delete(Func<QueryBuilder, QueryBuilder> sqlKataFunc);

        /// <summary>
        /// Delete by given condition async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc);

        /// <summary>
        /// Find an entity by given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity FindById(dynamic id);

        /// <summary>
        /// Find an entity by given id async
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> FindByIdAsync(dynamic id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Find single entity by given sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        TEntity Single(string sql);

        /// <summary>
        /// Find single entity by given sql async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> SingleAsync(string sql, CancellationToken cancellationToken = default);

        /// <summary>
        /// Find single entity or default by given sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        TEntity SingleOrDefault(string sql);

        /// <summary>
        /// Find single entity or default by given sql async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> SingleOrDefaultAsync(string sql, CancellationToken cancellationToken = default);

        /// <summary>
        /// Find first entity by given sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        TEntity First(string sql);

        /// <summary>
        /// Find first entity by given sql async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> FirstAsync(string sql, CancellationToken cancellationToken = default);

        /// <summary>
        /// Find first entity or default by given sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        TEntity FirstOrDefault(string sql);

        /// <summary>
        /// Find first entity or default by given sql async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync(string sql, CancellationToken cancellationToken = default);

        /// <summary>
        /// Find single entity by given predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        TEntity Single(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort);

        /// <summary>
        /// Find single entity by given predicate async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, CancellationToken cancellationToken = default);

        /// <summary>
        /// Find single entity or default by given predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort);

        /// <summary>
        /// Find single entity or default by given predicate async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, CancellationToken cancellationToken = default);

        /// <summary>
        /// Find first entity by given predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        TEntity First(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort);

        /// <summary>
        /// Find first entity by given predicate async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, CancellationToken cancellationToken = default);

        /// <summary>
        /// Find first entity or default by given predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort);

        /// <summary>
        /// Find first entity or default by given predicate async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, CancellationToken cancellationToken = default);

        /// <summary>
        /// Find a list of entity by given sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        IEnumerable<TEntity> FindList(string sql);

        /// <summary>
        /// Find a list of entity by given sql async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> FindListAsync(string sql, CancellationToken cancellationToken = default);

        /// <summary>
        /// Find a list of entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        IEnumerable<TEntity> FindList(ISQLPredicate predicate, SQLSortSet sort);

        /// <summary>
        /// Find a lst of entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        IEnumerable<TEntity> FindList(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort);

        /// <summary>
        /// Find a list of entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> FindListAsync(ISQLPredicate predicate, SQLSortSet sort, CancellationToken cancellationToken = default);

        /// <summary>
        /// Find a lst of entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> FindListAsync(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, CancellationToken cancellationToken = default);

        /// <summary>
        /// Find first entity by given SqlKata function
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        TEntity First(Func<QueryBuilder, QueryBuilder> sqlKataFunc);

        /// <summary>
        /// Find first entity by given SqlKata function async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        Task<TEntity> FirstAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc);

        /// <summary>
        /// Find first entity or default by given SqlKata function
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        TEntity FirstOrDefault(Func<QueryBuilder, QueryBuilder> sqlKataFunc);

        /// <summary>
        /// Find first entity or default by given SqlKata function async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc);

        /// <summary>
        /// Find single entity by given SqlKata function
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        TEntity Single(Func<QueryBuilder, QueryBuilder> sqlKataFunc);

        /// <summary>
        /// Find single entity by given SqlKata function async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        Task<TEntity> SingleAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc);

        /// <summary>
        /// Find single entity or default by given SqlKata function
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        TEntity SingleOrDefault(Func<QueryBuilder, QueryBuilder> sqlKataFunc);

        /// <summary>
        /// Find single entity or default by given SqlKata function async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        Task<TEntity> SingleOrDefaultAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc);

        /// <summary>
        /// Find a list of entity by given SqlKata function
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        IEnumerable<TEntity> FindList(Func<QueryBuilder, QueryBuilder> sqlKataFunc);

        /// <summary>
        /// Find a list of entity by given SqlKata function async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> FindListAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc);

        /// <summary>
        /// Gets count by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count(ISQLPredicate predicate);

        /// <summary>
        /// Gets count by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets count by given condition
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        long Count(Func<QueryBuilder, QueryBuilder> sqlKataFunc);

        /// <summary>
        /// Gets count by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> CountAsync(ISQLPredicate predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets count by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets count by given condition async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        Task<long> CountAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc);

        /// <summary>
        /// Query, and return a SqlKata QueryBuilder instance
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        QueryBuilder Where(Func<QueryBuilder, QueryBuilder> sqlKataFunc);
    }
}