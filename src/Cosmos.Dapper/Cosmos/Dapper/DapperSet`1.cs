using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Dapper.Core;
using Cosmos.Dapper.Operations;
using Cosmos.Data.SqlKata;
using Cosmos.Data.Statements;
using Cosmos.Models;
using SqlKata.Execution;

namespace Cosmos.Dapper
{
    /// <summary>
    /// Dapper set
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class DapperSet<TEntity> : DapperSet, IDapperSet<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly IDapperConnector _connector;
        private readonly IDapperQueryOperator _queryOperator;
        private readonly IDapperCommandOperator _commandOperator;
        private readonly IDapperEntityOperator _entityOperator;
        private readonly IDapperBulkInsertOperator _bulkInsertOperator;
        private Func<QueryBuilder> SqlKataEntityQueryBuilderFunc { get; }

        internal DapperSet(
            IDapperConnector connector,
            IDapperQueryOperator queryOperator,
            IDapperCommandOperator commandOperator,
            IDapperEntityOperator entityOperator,
            IDapperBulkInsertOperator bulkInsertOperator,
            Func<QueryBuilder> sqlKataEntityQueryBuilderFunc)
        {
            _connector = connector ?? throw new ArgumentNullException(nameof(connector));
            _queryOperator = queryOperator ?? throw new ArgumentNullException(nameof(queryOperator));
            _commandOperator = commandOperator ?? throw new ArgumentNullException(nameof(commandOperator));
            _entityOperator = entityOperator ?? throw new ArgumentNullException(nameof(entityOperator));
            _bulkInsertOperator = bulkInsertOperator ?? throw new ArgumentNullException(nameof(bulkInsertOperator));
            SqlKataEntityQueryBuilderFunc = sqlKataEntityQueryBuilderFunc ?? throw new ArgumentNullException(nameof(sqlKataEntityQueryBuilderFunc));
        }

        #region Add

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entity"></param>
        public void Add(TEntity entity)
        {
            _entityOperator.Insert(entity);
        }

        /// <summary>
        /// Add yasnc
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return _entityOperator.InsertAsync(entity, cancellationToken);
        }

        /// <summary>
        /// Add range
        /// </summary>
        /// <param name="entities"></param>
        public void AddRange(IEnumerable<TEntity> entities)
        {
            _entityOperator.Insert(entities);
        }

        /// <summary>
        /// Add range async
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            return _entityOperator.InsertAsync(entities, cancellationToken);
        }

        #endregion

        #region Update

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        public void Update(TEntity entity)
        {
            _entityOperator.Update(entity, RepoLevelDataFilter);
        }

        /// <summary>
        /// Update async
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return _entityOperator.UpdateAsync(entity, RepoLevelDataFilter, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Update range
        /// </summary>
        /// <param name="entities"></param>
        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _entityOperator.Update(entities, RepoLevelDataFilter);
        }

        /// <summary>
        /// Update range async
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            return _entityOperator.UpdateAsync(entities, RepoLevelDataFilter, cancellationToken: cancellationToken);
        }

        #endregion

        #region Delete

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(TEntity entity)
        {
            _entityOperator.Delete(entity, RepoLevelDataFilter);
        }

        /// <summary>
        /// Delete async
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task DeleteAsync(TEntity entities, CancellationToken cancellationToken = default)
        {
            return _entityOperator.DeleteAsync(entities, RepoLevelDataFilter, cancellationToken);
        }

        /// <summary>
        /// Delete range
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteRange(IEnumerable<TEntity> entity)
        {
            _entityOperator.Delete(entity, RepoLevelDataFilter);
        }

        /// <summary>
        /// Delete range async
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            return _entityOperator.DeleteAsync(entities, RepoLevelDataFilter, cancellationToken);
        }

        /// <summary>
        /// Delete by given condition
        /// </summary>
        /// <param name="predicate"></param>
        public void Delete(ISQLPredicate predicate)
        {
            _entityOperator.Delete<TEntity>(predicate, RepoLevelDataFilter);
        }

        /// <summary>
        /// Delete by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task DeleteAsync(ISQLPredicate predicate, CancellationToken cancellationToken = default)
        {
            return _entityOperator.DeleteAsync<TEntity>(predicate, RepoLevelDataFilter, cancellationToken);
        }

        /// <summary>
        /// Delete by given condition
        /// </summary>
        /// <param name="predicate"></param>
        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            _entityOperator.Delete(predicate, RepoLevelDataFilter);
        }

        /// <summary>
        /// Delete by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return _entityOperator.DeleteAsync(predicate, RepoLevelDataFilter, cancellationToken);
        }

        /// <summary>
        /// Delete by given condition
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        public int Delete(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc(SqlKataEntityQueryBuilderFunc.Invoke()).Delete();
        }

        /// <summary>
        /// Delete by given condition async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        public Task<int> DeleteAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc(SqlKataEntityQueryBuilderFunc.Invoke()).DeleteAsync();
        }

        #endregion

        #region Find by id

        /// <summary>
        /// Find an entity by given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity FindById(dynamic id)
        {
            return _entityOperator.Get<TEntity>(id, RepoLevelDataFilter);
        }

        /// <summary>
        /// Find an entity by given id async
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<TEntity> FindByIdAsync(dynamic id, CancellationToken cancellationToken = default)
        {
            return _entityOperator.GetAsync<TEntity>(id, RepoLevelDataFilter, cancellationToken);
        }

        #endregion

        #region Find by sql

        /// <summary>
        /// Find single entity by given sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public TEntity Single(string sql)
        {
            return _queryOperator.QuerySingle<TEntity>(sql);
        }

        /// <summary>
        /// Find single entity by given sql async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<TEntity> SingleAsync(string sql, CancellationToken cancellationToken = default)
        {
            return _queryOperator.QuerySingleAsync<TEntity>(sql, cancellationToken);
        }

        /// <summary>
        /// Find single entity or default by given sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public TEntity SingleOrDefault(string sql)
        {
            return _queryOperator.QuerySingleOrDefault<TEntity>(sql);
        }

        /// <summary>
        /// Find single entity or default by given sql async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<TEntity> SingleOrDefaultAsync(string sql, CancellationToken cancellationToken = default)
        {
            return _queryOperator.QuerySingleOrDefaultAsync<TEntity>(sql, cancellationToken);
        }

        /// <summary>
        /// Find first entity by given sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public TEntity First(string sql)
        {
            return _queryOperator.QueryFirst<TEntity>(sql);
        }

        /// <summary>
        /// Find first entity by given sql async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<TEntity> FirstAsync(string sql, CancellationToken cancellationToken = default)
        {
            return _queryOperator.QueryFirstAsync<TEntity>(sql, cancellationToken);
        }

        /// <summary>
        /// Find first entity or default by given sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public TEntity FirstOrDefault(string sql)
        {
            return _queryOperator.QueryFirstOrDefault<TEntity>(sql);
        }

        /// <summary>
        /// Find first entity or default by given sql async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<TEntity> FirstOrDefaultAsync(string sql, CancellationToken cancellationToken = default)
        {
            return _queryOperator.QueryFirstOrDefaultAsync<TEntity>(sql, cancellationToken);
        }

        /// <summary>
        /// Find a list of entity by given sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> FindList(string sql)
        {
            return _queryOperator.Query<TEntity>(sql);
        }

        /// <summary>
        /// Find a list of entity by given sql async
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IEnumerable<TEntity>> FindListAsync(string sql, CancellationToken cancellationToken = default)
        {
            return _queryOperator.QueryAsync<TEntity>(sql, cancellationToken);
        }

        #endregion

        #region Fild by SqlKata

        /// <summary>
        /// Find first entity by given sqlkata function
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        public TEntity First(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc(SqlKataEntityQueryBuilderFunc.Invoke()).QueryFirst<TEntity>();
        }

        /// <summary>
        /// Find first entity by given sqlkata function async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        public Task<TEntity> FirstAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc(SqlKataEntityQueryBuilderFunc.Invoke()).QueryFirstAsync<TEntity>();
        }

        /// <summary>
        /// Find first entity or default by given sqlkata function
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        public TEntity FirstOrDefault(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc(SqlKataEntityQueryBuilderFunc.Invoke()).QueryFirstOrDefault<TEntity>();
        }

        /// <summary>
        /// Find first entity or default by given sqlkata function async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        public Task<TEntity> FirstOrDefaultAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc(SqlKataEntityQueryBuilderFunc.Invoke()).QueryFirstOrDefaultAsync<TEntity>();
        }

        /// <summary>
        /// Find single entity by given sqlkata function
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        public TEntity Single(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc(SqlKataEntityQueryBuilderFunc.Invoke()).QuerySingle<TEntity>();
        }

        /// <summary>
        /// Find single entity by given sqlkata function async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        public Task<TEntity> SingleAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc(SqlKataEntityQueryBuilderFunc.Invoke()).QuerySingleAsync<TEntity>();
        }

        /// <summary>
        /// Find single entity or default by given sqlkata function
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        public TEntity SingleOrDefault(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc(SqlKataEntityQueryBuilderFunc.Invoke()).QuerySingleOrDefault<TEntity>();
        }

        /// <summary>
        /// Find single entity or default by given sqlkata function async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        public Task<TEntity> SingleOrDefaultAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc(SqlKataEntityQueryBuilderFunc.Invoke()).QuerySingleOrDefaultAsync<TEntity>();
        }

        /// <summary>
        /// Find a list of entity by given sqlkata function
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> FindList(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc(SqlKataEntityQueryBuilderFunc.Invoke()).Query<TEntity>();
        }

        /// <summary>
        /// Find a list of entity by given sqlkata function async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        public Task<IEnumerable<TEntity>> FindListAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc(SqlKataEntityQueryBuilderFunc.Invoke()).QueryAsync<TEntity>();
        }

        #endregion

        #region Find by predicate

        /// <summary>
        /// Find single entity by given predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public TEntity Single(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort)
        {
            return _entityOperator.Single(predicate, sort, RepoLevelDataFilter);
        }

        /// <summary>
        /// Find single entity by given predicate async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, CancellationToken cancellationToken = default)
        {
            return _entityOperator.SingleAsync(predicate, sort, RepoLevelDataFilter, cancellationToken);
        }

        /// <summary>
        /// Find single entity or default by given predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort)
        {
            return _entityOperator.SingleOrDefault(predicate, sort, RepoLevelDataFilter);
        }

        /// <summary>
        /// Find single entity or default by given predicate async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, CancellationToken cancellationToken = default)
        {
            return _entityOperator.SingleOrDefaultAsync(predicate, sort, RepoLevelDataFilter, cancellationToken);
        }

        /// <summary>
        /// Find first entity by given predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public TEntity First(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort)
        {
            return _entityOperator.First(predicate, sort, RepoLevelDataFilter);
        }

        /// <summary>
        /// Find first entity by given predicate async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, CancellationToken cancellationToken = default)
        {
            return _entityOperator.FirstAsync(predicate, sort, RepoLevelDataFilter, cancellationToken);
        }

        /// <summary>
        /// Find first entity or default by given predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort)
        {
            return _entityOperator.FirstOrDefault(predicate, sort, RepoLevelDataFilter);
        }

        /// <summary>
        /// Find first entity or default by given predicate async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, CancellationToken cancellationToken = default)
        {
            return _entityOperator.FirstOrDefaultAsync(predicate, sort, RepoLevelDataFilter, cancellationToken);
        }

        /// <summary>
        /// Find a lst of entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> FindList(ISQLPredicate predicate, SQLSortSet sort)
        {
            return _entityOperator.GetList<TEntity>(predicate, sort, RepoLevelDataFilter);
        }

        /// <summary>
        /// Find a lst of entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> FindList(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort)
        {
            return _entityOperator.GetList(predicate, sort, RepoLevelDataFilter);
        }

        /// <summary>
        /// Find a lst of entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IEnumerable<TEntity>> FindListAsync(ISQLPredicate predicate, SQLSortSet sort, CancellationToken cancellationToken = default)
        {
            return _entityOperator.GetListAsync<TEntity>(predicate, sort, RepoLevelDataFilter, cancellationToken);
        }

        /// <summary>
        /// Find a lst of entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IEnumerable<TEntity>> FindListAsync(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, CancellationToken cancellationToken = default)
        {
            return _entityOperator.GetListAsync(predicate, sort, RepoLevelDataFilter, cancellationToken);
        }

        #endregion

        #region Count

        /// <summary>
        /// Gets count by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int Count(ISQLPredicate predicate)
        {
            return _entityOperator.Count<TEntity>(predicate, RepoLevelDataFilter);
        }

        /// <summary>
        /// Gets count by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _entityOperator.Count(predicate, RepoLevelDataFilter);
        }

        /// <summary>
        /// Gets count by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<int> CountAsync(ISQLPredicate predicate, CancellationToken cancellationToken = default)
        {
            return _entityOperator.CountAsync<TEntity>(predicate, RepoLevelDataFilter, cancellationToken);
        }

        /// <summary>
        /// Gets count by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return _entityOperator.CountAsync(predicate, RepoLevelDataFilter, cancellationToken);
        }

        /// <summary>
        /// Gets count by given condition
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        public long Count(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc(SqlKataEntityQueryBuilderFunc.Invoke()).Count<long>();
        }

        /// <summary>
        /// Gets count by given condition async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        public Task<long> CountAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc(SqlKataEntityQueryBuilderFunc.Invoke()).CountAsync<long>();
        }

        #endregion

        #region Query

        /// <summary>
        /// Query, and return a SqlKata QueryBuilder instance
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        public QueryBuilder Where(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc(SqlKataEntityQueryBuilderFunc.Invoke());
        }

        #endregion
    }
}