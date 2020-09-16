using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Dapper.Core.SqlKata;
using Cosmos.Data;
using Cosmos.Data.SqlKata;
using Cosmos.Data.Statements;
using DotNetCore.Collections.Paginable;
using SqlKata.Execution;

namespace Cosmos.Dapper.Store
{
    // ReSharper disable once UnusedTypeParameter
    public abstract partial class StoreBase<TContext, TEntity>
    {
        #region Find # in base IStore.QueryableStore

        /// <summary>
        /// Find a collection of paged result by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return RawTypedContext.EntityOperators.GetList(predicate, SQLOrder.Default, RepoLevelDataFilters);
        }

        /// <summary>
        /// Find a collection of paged results by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return RawTypedContext.EntityOperators.GetListAsync(predicate, SQLOrder.Default, RepoLevelDataFilters, cancellationToken);
        }

        #endregion

        #region Find # in IStore.QueryableStore.Predicates

        /// <summary>
        /// Find a collection of entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Find(object predicate, SQLSortSet sort, bool buffered = true)
        {
            return RawTypedContext.EntityOperators.GetList<TEntity>(predicate, sort, RepoLevelDataFilters, buffered);
        }

        /// <summary>
        /// Find a collection of entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<IEnumerable<TEntity>> FindAsync(object predicate, SQLSortSet sort, CancellationToken cancellationToken = default)
        {
            return RawTypedContext.EntityOperators.GetListAsync<TEntity>(predicate, sort, RepoLevelDataFilters, cancellationToken);
        }

        /// <summary>
        /// Find a collection of entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="limitFrom"></param>
        /// <param name="limitTo"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Find(object predicate, SQLSortSet sort, int limitFrom, int limitTo, bool buffered = true)
        {
            return RawTypedContext.EntityOperators.GetSet<TEntity>(predicate, sort, limitFrom, limitTo, RepoLevelDataFilters, buffered);
        }

        /// <summary>
        /// Find a collection of entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="limitFrom"></param>
        /// <param name="limitTo"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<IEnumerable<TEntity>> FindAsync(object predicate, SQLSortSet sort, int limitFrom, int limitTo,
            CancellationToken cancellationToken = default)
        {
            return RawTypedContext.EntityOperators.GetSetAsync<TEntity>(predicate, sort, limitFrom, limitTo, RepoLevelDataFilters, cancellationToken);
        }

        #endregion

        #region Find # in IStore.QueryableStore.DynamicExpression

        /// <summary>
        /// Find a collection of entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, bool buffered = true)
        {
            return RawTypedContext.EntityOperators.GetList(predicate, sort, RepoLevelDataFilters, buffered);
        }

        /// <summary>
        /// Find a collection of entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, CancellationToken cancellationToken = default)
        {
            return RawTypedContext.EntityOperators.GetListAsync(predicate, sort, RepoLevelDataFilters, cancellationToken);
        }

        /// <summary>
        /// Find a collection of entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="limitFrom"></param>
        /// <param name="limitTo"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, int limitFrom, int limitTo, bool buffered = true)
        {
            return RawTypedContext.EntityOperators.GetSet(predicate, sort, limitFrom, limitTo, RepoLevelDataFilters, buffered);
        }

        /// <summary>
        /// Find a collection of entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="limitFrom"></param>
        /// <param name="limitTo"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, int limitFrom, int limitTo,
            CancellationToken cancellationToken = default)
        {
            return RawTypedContext.EntityOperators.GetSetAsync(predicate, sort, limitFrom, limitTo, RepoLevelDataFilters, cancellationToken);
        }

        #endregion

        #region Find # in IStore.QueryableStore.SqlKata

        /// <summary>
        /// Find a collection of entity by given SqlKata function
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Find(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()())
               .WhereRawSafety(SqlKataRepoLevelDataFilters)
               .Get<TEntity>();
        }

        /// <summary>
        /// Find a collection of entity by given SqlKata function async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        public virtual Task<IEnumerable<TEntity>> FindAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()())
               .WhereRawSafety(SqlKataRepoLevelDataFilters)
               .GetAsync<TEntity>();
        }

        #endregion

        #region Find by id # in IStore.QueryableStore

        /// <summary>
        /// Find an entity by given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity FindById(dynamic id)
        {
            return RawTypedContext.EntityOperators.Get<TEntity>(id, RepoLevelDataFilters);
        }

        /// <summary>
        /// Find an entity by given id async
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> FindByIdAsync(dynamic id, CancellationToken cancellationToken = default)
        {
            return RawTypedContext.EntityOperators.GetAsync<TEntity>(id, RepoLevelDataFilters, cancellationToken);
        }

        #endregion

        #region Find First # in base IStore.QueryableStore

        /// <summary>
        /// Find first result by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual TEntity FindFirst(Expression<Func<TEntity, bool>> predicate)
        {
            return RawTypedContext.EntityOperators.First(predicate, SQLOrder.Default, RepoLevelDataFilters);
        }

        /// <summary>
        /// Find first  result by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> FindFirstAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return RawTypedContext.EntityOperators.FirstAsync(predicate, SQLOrder.Default, RepoLevelDataFilters, cancellationToken);
        }

        #endregion

        #region Find First # in IStore.QueryableStore.Predicates

        /// <summary>
        /// Find first entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public virtual TEntity FindFirst(object predicate, SQLSortSet sort, bool buffered = true)
        {
            return RawTypedContext.EntityOperators.First<TEntity>(predicate, sort, RepoLevelDataFilters, buffered);
        }

        /// <summary>
        /// Find first entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> FindFirstAsync(object predicate, SQLSortSet sort, CancellationToken cancellationToken = default)
        {
            return RawTypedContext.EntityOperators.FirstAsync<TEntity>(predicate, sort, RepoLevelDataFilters, cancellationToken);
        }

        #endregion

        #region Find First # in IStore.QueryableStore.DynamicExpression

        /// <summary>
        /// Find first entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public virtual TEntity FindFirst(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, bool buffered = true)
        {
            return RawTypedContext.EntityOperators.First(predicate, sort, RepoLevelDataFilters, buffered);
        }

        /// <summary>
        /// Find first entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> FindFirstAsync(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, CancellationToken cancellationToken = default)
        {
            return RawTypedContext.EntityOperators.FirstAsync(predicate, sort, RepoLevelDataFilters, cancellationToken);
        }

        #endregion

        #region Find First # in IStore.QueryableStore.SqlKata

        /// <summary>
        /// Find first entity by given SqlKata function
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        public virtual TEntity FindFirst(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()())
               .WhereRawSafety(SqlKataRepoLevelDataFilters)
               .QueryFirst<TEntity>();
        }

        /// <summary>
        /// Find first entity by given SqlKata function async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        public virtual Task<TEntity> FindFirstAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()())
               .WhereRawSafety(SqlKataRepoLevelDataFilters)
               .QueryFirstAsync<TEntity>();
        }

        #endregion

        #region Find First Or Default # in base IStore.QueryableStore

        /// <summary>
        /// Find first or default result by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual TEntity FindFirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return RawTypedContext.EntityOperators.FirstOrDefault(predicate, null, RepoLevelDataFilters);
        }

        /// <summary>
        /// Find first or default result by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> FindFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return RawTypedContext.EntityOperators.FirstOrDefaultAsync(predicate, null, RepoLevelDataFilters, cancellationToken);
        }

        #endregion

        #region Find First or Default # in IStore.QueryableStore.Predicates

        /// <summary>
        /// Find first entity or default by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public virtual TEntity FindFirstOrDefault(object predicate, SQLSortSet sort, bool buffered = true)
        {
            return RawTypedContext.EntityOperators.FirstOrDefault<TEntity>(predicate, sort, RepoLevelDataFilters, buffered);
        }

        /// <summary>
        /// Find first entity or default by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> FindFirstOrDefaultAsync(object predicate, SQLSortSet sort, CancellationToken cancellationToken = default)
        {
            return RawTypedContext.EntityOperators.SingleOrDefaultAsync<TEntity>(predicate, sort, RepoLevelDataFilters, cancellationToken);
        }

        #endregion

        #region Find First or Default # in IStore.QueryableStore.DynamicExpression

        /// <summary>
        /// Find first entity or default by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public virtual TEntity FindFirstOrDefault(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, bool buffered = true)
        {
            return RawTypedContext.EntityOperators.FirstOrDefault(predicate, sort, RepoLevelDataFilters, buffered);
        }

        /// <summary>
        /// Find first entity or default by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> FindFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, CancellationToken cancellationToken = default)
        {
            return RawTypedContext.EntityOperators.SingleOrDefaultAsync(predicate, sort, RepoLevelDataFilters, cancellationToken);
        }

        #endregion

        #region Find First or Default # in IStore.QueryableStore.SqlKata

        /// <summary>
        /// Find first entity or default by given SqlKata function
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        public virtual TEntity FindFirstOrDefault(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()())
               .WhereRawSafety(SqlKataRepoLevelDataFilters)
               .QueryFirstOrDefault<TEntity>();
        }

        /// <summary>
        /// Find first entity or default by given SqlKata function async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        public virtual Task<TEntity> FindFirstOrDefaultAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()())
               .WhereRawSafety(SqlKataRepoLevelDataFilters)
               .QueryFirstOrDefaultAsync<TEntity>();
        }

        #endregion

        #region Find Single # in base IStore.QueryableStore

        /// <summary>
        /// Find Single result by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual TEntity FindSingle(Expression<Func<TEntity, bool>> predicate)
        {
            return RawTypedContext.EntityOperators.First(predicate, SQLOrder.Default, RepoLevelDataFilters);
        }

        /// <summary>
        /// Find Single result by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await RawTypedContext.EntityOperators.FirstAsync(predicate, SQLOrder.Default, RepoLevelDataFilters, cancellationToken);
        }

        #endregion

        #region Find Single # in IStore.QueryableStore.Predicates

        /// <summary>
        /// Find single entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public virtual TEntity FindSingle(object predicate, SQLSortSet sort, bool buffered = true)
        {
            return RawTypedContext.EntityOperators.Single<TEntity>(predicate, sort, RepoLevelDataFilters, buffered);
        }

        /// <summary>
        /// Find single entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> FindSingleAsync(object predicate, SQLSortSet sort, CancellationToken cancellationToken = default)
        {
            return RawTypedContext.EntityOperators.SingleAsync<TEntity>(predicate, sort, RepoLevelDataFilters, cancellationToken);
        }

        #endregion

        #region Find Single # in IStore.QueryableStore.DynamicExpression

        /// <summary>
        /// Find single entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public virtual TEntity FindSingle(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, bool buffered = true)
        {
            return RawTypedContext.EntityOperators.Single(predicate, sort, RepoLevelDataFilters, buffered);
        }

        /// <summary>
        /// Find single entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, CancellationToken cancellationToken = default)
        {
            return RawTypedContext.EntityOperators.SingleAsync(predicate, sort, RepoLevelDataFilters, cancellationToken);
        }

        #endregion

        #region Find Single # in IStore.QueryableStore.SqlKata

        /// <summary>
        /// Find single entity by given SqlKata function
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        public virtual TEntity FindSingle(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()())
               .WhereRawSafety(SqlKataRepoLevelDataFilters)
               .QuerySingle<TEntity>();
        }

        /// <summary>
        /// Find single entity by given SqlKata function async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        public virtual Task<TEntity> FindSingleAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()())
               .WhereRawSafety(SqlKataRepoLevelDataFilters)
               .QuerySingleAsync<TEntity>();
        }

        #endregion

        #region Find Single Or Default # in base IStore.QueryableStore

        /// <summary>
        /// Find single or default result by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual TEntity FindSingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return RawTypedContext.EntityOperators.SingleOrDefault(predicate, SQLOrder.Default, RepoLevelDataFilters);
        }

        /// <summary>
        /// Find single or default result by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> FindSingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return RawTypedContext.EntityOperators.SingleOrDefaultAsync(predicate, SQLOrder.Default, RepoLevelDataFilters, cancellationToken);
        }

        #endregion

        #region Find Single or Default # in IStore.QueryableStore.Predicates

        /// <summary>
        /// Find single entity or default by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public virtual TEntity FindSingleOrDefault(object predicate, SQLSortSet sort, bool buffered = true)
        {
            return RawTypedContext.EntityOperators.SingleOrDefault<TEntity>(predicate, sort, RepoLevelDataFilters, buffered);
        }

        /// <summary>
        /// Find single entity or default by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> FindSingleOrDefaultAsync(object predicate, SQLSortSet sort, CancellationToken cancellationToken = default)
        {
            return RawTypedContext.EntityOperators.SingleOrDefaultAsync<TEntity>(predicate, sort, RepoLevelDataFilters, cancellationToken);
        }

        #endregion

        #region Find Single or Default # in IStore.QueryableStore.DynamicExpression

        /// <summary>
        /// Find single entity or default by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public virtual TEntity FindSingleOrDefault(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, bool buffered = true)
        {
            return RawTypedContext.EntityOperators.SingleOrDefault(predicate, sort, RepoLevelDataFilters, buffered);
        }

        /// <summary>
        /// Find single entity or default by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> FindSingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, CancellationToken cancellationToken = default)
        {
            return RawTypedContext.EntityOperators.SingleOrDefaultAsync(predicate, sort, RepoLevelDataFilters, cancellationToken);
        }

        #endregion

        #region Find Single or Default # in IStore.QueryableStore.SqlKata

        /// <summary>
        /// Find single entity or default by given SqlKata function
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        public virtual TEntity FindSingleOrDefault(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()())
               .WhereRawSafety(SqlKataRepoLevelDataFilters)
               .QuerySingleOrDefault<TEntity>();
        }

        /// <summary>
        /// Find single entity or default by given SqlKata function async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        public virtual Task<TEntity> FindSingleOrDefaultAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()())
               .WhereRawSafety(SqlKataRepoLevelDataFilters)
               .QuerySingleOrDefaultAsync<TEntity>();
        }

        #endregion

        #region Get One # in base IStore.QueryableStore

        /// <summary>
        /// Get one or null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity GetOne(dynamic id) => FindById(id);

        /// <summary>
        /// Get one or null
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> GetOneAsync(dynamic id, CancellationToken cancellationToken = default) => FindByIdAsync(id, cancellationToken);

        /// <summary>
        /// Get one or null...
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual TEntity GetOne(Expression<Func<TEntity, bool>> predicate) => FindFirstOrDefault(predicate);

        /// <summary>
        /// Get one or null...
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
            => FindFirstOrDefaultAsync(predicate, cancellationToken);

        #endregion

        #region Get page # in base IStore.QueryableStore

        /// <summary>
        /// Query pageable results by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual IPage<TEntity> GetPage(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize)
        {
            return GetPage(predicate, SQLOrder.Default, pageNumber, pageSize);
        }

        /// <summary>
        /// Query pageable results by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<IPage<TEntity>> GetPageAsync(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            return GetPageAsync(predicate, SQLOrder.Default, pageNumber, pageSize, cancellationToken);
        }

        #endregion

        #region Get Page # in IStore.QueryableStore.Predicates

        /// <summary>
        /// Query a paged list of entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public virtual IPage<TEntity> GetPage(object predicate, SQLSortSet sort, int pageNumber, int pageSize, bool buffered = true)
        {
            var totalMemberCount = RawTypedContext.EntityOperators.Count<TEntity>(predicate, RepoLevelDataFilters);
            var page = RawTypedContext.EntityOperators.GetPage<TEntity>(predicate, sort, pageNumber, pageSize, RepoLevelDataFilters, buffered);
            return new EnumerablePage<TEntity>(page, pageNumber, pageSize, totalMemberCount, false);
        }

        /// <summary>
        /// Query a paged list of entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<IPage<TEntity>> GetPageAsync(object predicate, SQLSortSet sort, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var totalMemberCount = await RawTypedContext.EntityOperators.CountAsync<TEntity>(predicate, RepoLevelDataFilters, cancellationToken);
            var page = await RawTypedContext.EntityOperators.GetPageAsync<TEntity>(predicate, sort, pageNumber, pageSize, RepoLevelDataFilters, cancellationToken);
            return new EnumerablePage<TEntity>(page, pageNumber, pageSize, totalMemberCount, false);
        }

        #endregion

        #region Get Page # in IStore.QueryableStore.DynamicExpression

        /// <summary>
        /// Query a paged list of entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public virtual IPage<TEntity> GetPage(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, int pageNumber, int pageSize, bool buffered = true)
        {
            var totalMemberCount = RawTypedContext.EntityOperators.Count(predicate, RepoLevelDataFilters);
            var page = RawTypedContext.EntityOperators.GetPage(predicate, sort, pageNumber, pageSize, RepoLevelDataFilters, buffered);
            return new EnumerablePage<TEntity>(page, pageNumber, pageSize, totalMemberCount, false);
        }

        /// <summary>
        /// Query a paged list of entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<IPage<TEntity>> GetPageAsync(Expression<Func<TEntity, bool>> predicate, SQLSortSet sort, int pageNumber, int pageSize,
            CancellationToken cancellationToken = default)
        {
            var totalMemberCount = await RawTypedContext.EntityOperators.CountAsync(predicate, RepoLevelDataFilters, cancellationToken);
            var page = await RawTypedContext.EntityOperators.GetPageAsync(predicate, sort, pageNumber, pageSize, RepoLevelDataFilters, cancellationToken);
            return new EnumerablePage<TEntity>(page, pageNumber, pageSize, totalMemberCount, false);
        }

        #endregion

        #region Get Page # in IStore.QueryableStore.SqlKata

        /// <summary>
        /// Query a paged list of entity by given SqlKata function
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual IPage<TEntity> GetPage(Func<QueryBuilder, QueryBuilder> sqlKataFunc, int pageNumber, int pageSize)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()())
               .WhereRawSafety(SqlKataRepoLevelDataFilters)
               .GetPage<TEntity>(pageNumber, pageSize);
        }

        /// <summary>
        /// Query a paged list of entity by given SqlKata function async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual Task<IPage<TEntity>> GetPageAsync(Func<QueryBuilder, QueryBuilder> sqlKataFunc, int pageNumber, int pageSize)
        {
            return sqlKataFunc.Invoke(RawTypedContext.SqlKataQueryBuilderFunc()())
               .WhereRawSafety(SqlKataRepoLevelDataFilters)
               .GetPageAsync<TEntity>(pageNumber, pageSize);
        }

        #endregion
    }
}