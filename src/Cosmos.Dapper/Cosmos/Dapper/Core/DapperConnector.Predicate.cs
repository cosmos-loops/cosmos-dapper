using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Data.Statements;
using QueryOneType = Cosmos.Dapper.Core.DapperImplementor.QueryOneType;

namespace Cosmos.Dapper.Core
{
    public partial class DapperConnector
    {
        /// <summary>
        /// Delete by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Delete<T>(object predicate, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class
            => _proxy.Delete<T>(Connection, predicate, transaction, filters);

        /// <summary>
        /// Delete by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<bool> DeleteAsync<T>(object predicate, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default)
            where T : class
            => _proxy.DeleteAsync<T>(Connection, predicate, transaction, filters, cancellationToken);

        /// <summary>
        /// Delete by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="filters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Delete<T>(object predicate, ISQLPredicate[] filters = null) where T : class
            => _proxy.Delete<T>(Connection, predicate, Transaction, filters);

        /// <summary>
        /// Delete by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<bool> DeleteAsync<T>(object predicate, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class
            => _proxy.DeleteAsync<T>(Connection, predicate, Transaction, filters, cancellationToken);

        /// <summary>
        /// Get one entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <param name="type"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetOne<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true,
            QueryOneType type = QueryOneType.FirstOrDefault) where T : class
            => _proxy.GetOne<T>(Connection, predicate, sortSet, transaction, filters, buffered, type);

        /// <summary>
        /// Get one entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="type"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<T> GetOneAsync<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null,
            QueryOneType type = QueryOneType.FirstOrDefault, CancellationToken cancellationToken = default) where T : class
            => _proxy.GetOneAsync<T>(Connection, predicate, sortSet, transaction, filters, type, cancellationToken);

        /// <summary>
        /// Get one entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <param name="type"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetOne<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, bool buffered = true, QueryOneType type = QueryOneType.FirstOrDefault)
            where T : class
            => _proxy.GetOne<T>(Connection, predicate, sortSet, Transaction, filters, buffered, type);

        /// <summary>
        /// Get one entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="filters"></param>
        /// <param name="type"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<T> GetOneAsync<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, QueryOneType type = QueryOneType.FirstOrDefault,
            CancellationToken cancellationToken = default) where T : class
            => _proxy.GetOneAsync<T>(Connection, predicate, sortSet, Transaction, filters, type, cancellationToken);

        /// <summary>
        /// Get first entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T First<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true) where T : class
            => _proxy.GetOne<T>(Connection, predicate, sortSet, transaction, filters, buffered, QueryOneType.First);

        /// <summary>
        /// Get first entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<T> FirstAsync<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class
            => _proxy.GetOneAsync<T>(Connection, predicate, sortSet, transaction, filters, QueryOneType.First, cancellationToken);

        /// <summary>
        /// Get first entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T First<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, bool buffered = true) where T : class
            => _proxy.GetOne<T>(Connection, predicate, sortSet, Transaction, filters, buffered, QueryOneType.First);

        /// <summary>
        /// Get first entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<T> FirstAsync<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class
            => _proxy.GetOneAsync<T>(Connection, predicate, sortSet, Transaction, filters, QueryOneType.First, cancellationToken);

        /// <summary>
        /// Get first entity or default by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T FirstOrDefault<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true) where T : class
            => _proxy.GetOne<T>(Connection, predicate, sortSet, transaction, filters, buffered, QueryOneType.FirstOrDefault);

        /// <summary>
        /// Get first entity or default by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<T> FirstOrDefaultAsync<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class
            => _proxy.GetOneAsync<T>(Connection, predicate, sortSet, transaction, filters, QueryOneType.FirstOrDefault, cancellationToken);

        /// <summary>
        /// Get first entity or default by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T FirstOrDefault<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, bool buffered = true) where T : class
            => _proxy.GetOne<T>(Connection, predicate, sortSet, Transaction, filters, buffered, QueryOneType.FirstOrDefault);

        /// <summary>
        /// Get first entity or default by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<T> FirstOrDefaultAsync<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default)
            where T : class
            => _proxy.GetOneAsync<T>(Connection, predicate, sortSet, Transaction, filters, QueryOneType.FirstOrDefault, cancellationToken);

        /// <summary>
        /// Get single entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Single<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true) where T : class
            => _proxy.GetOne<T>(Connection, predicate, sortSet, transaction, filters, buffered, QueryOneType.Single);

        /// <summary>
        /// Get single entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<T> SingleAsync<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class
            => _proxy.GetOneAsync<T>(Connection, predicate, sortSet, transaction, filters, QueryOneType.Single, cancellationToken);

        /// <summary>
        /// Get single entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Single<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, bool buffered = true) where T : class
            => _proxy.GetOne<T>(Connection, predicate, sortSet, Transaction, filters, buffered, QueryOneType.Single);

        /// <summary>
        /// Get single entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<T> SingleAsync<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class
            => _proxy.GetOneAsync<T>(Connection, predicate, sortSet, Transaction, filters, QueryOneType.Single, cancellationToken);

        /// <summary>
        /// Get single entity or default by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T SingleOrDefault<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true)
            where T : class
            => _proxy.GetOne<T>(Connection, predicate, sortSet, transaction, filters, buffered, QueryOneType.SingleOrDefault);

        /// <summary>
        /// Get single entity or default by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<T> SingleOrDefaultAsync<T>(object predicate, SQLSortSet sortSet, IDbTransaction transaction, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class
            => _proxy.GetOneAsync<T>(Connection, predicate, sortSet, transaction, filters, QueryOneType.SingleOrDefault, cancellationToken);

        /// <summary>
        /// Get single entity or default by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T SingleOrDefault<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, bool buffered = true) where T : class
            => _proxy.GetOne<T>(Connection, predicate, sortSet, Transaction, filters, buffered, QueryOneType.SingleOrDefault);

        /// <summary>
        /// Get single entity or default by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortSet"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<T> SingleOrDefaultAsync<T>(object predicate, SQLSortSet sortSet, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default)
            where T : class
            => _proxy.GetOneAsync<T>(Connection, predicate, sortSet, Transaction, filters, QueryOneType.SingleOrDefault, cancellationToken);

        /// <summary>
        /// Get a list of entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetList<T>(object predicate, SQLSortSet sort, IDbTransaction transaction, ISQLPredicate[] filters = null, bool buffered = true)
            where T : class
            => _proxy.GetList<T>(Connection, predicate, sort, transaction, filters, buffered);

        /// <summary>
        /// Get a list of entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<IEnumerable<T>> GetListAsync<T>(object predicate, SQLSortSet sort, IDbTransaction transaction, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class
            => _proxy.GetListAsync<T>(Connection, predicate, sort, transaction, filters, cancellationToken);

        /// <summary>
        /// Get a list of entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetList<T>(object predicate, SQLSortSet sort, ISQLPredicate[] filters = null, bool buffered = true) where T : class
            => _proxy.GetList<T>(Connection, predicate, sort, Transaction, filters, buffered);

        /// <summary>
        /// Get a list of entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<IEnumerable<T>> GetListAsync<T>(object predicate, SQLSortSet sort, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default)
            where T : class
            => _proxy.GetListAsync<T>(Connection, predicate, sort, Transaction, filters, cancellationToken);

        /// <summary>
        /// Get a paged list of entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetPage<T>(object predicate, SQLSortSet sort, int pageNumber, int pageSize, IDbTransaction transaction, ISQLPredicate[] filters = null,
            bool buffered = true) where T : class
            => _proxy.GetPage<T>(Connection, predicate, sort, pageNumber, pageSize, transaction, filters, buffered);

        /// <summary>
        /// Get a paged list of entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<IEnumerable<T>> GetPageAsync<T>(object predicate, SQLSortSet sort, int pageNumber, int pageSize, IDbTransaction transaction,
            ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class
            => _proxy.GetPageAsync<T>(Connection, predicate, sort, pageNumber, pageSize, transaction, filters, cancellationToken);

        /// <summary>
        /// Get a paged list of entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetPage<T>(object predicate, SQLSortSet sort, int pageNumber, int pageSize, ISQLPredicate[] filters = null, bool buffered = true)
            where T : class
            => _proxy.GetPage<T>(Connection, predicate, sort, pageNumber, pageSize, Transaction, filters, buffered);

        /// <summary>
        /// Get a paged list of entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<IEnumerable<T>> GetPageAsync<T>(object predicate, SQLSortSet sort, int pageNumber, int pageSize, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class
            => _proxy.GetPageAsync<T>(Connection, predicate, sort, pageNumber, pageSize, Transaction, filters, cancellationToken);

        /// <summary>
        /// Get a collection of entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="limitFrom"></param>
        /// <param name="limitTo"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetSet<T>(object predicate, SQLSortSet sort, int limitFrom, int limitTo, IDbTransaction transaction, ISQLPredicate[] filters = null,
            bool buffered = true) where T : class
            => _proxy.GetSet<T>(Connection, predicate, sort, limitFrom, limitTo, transaction, filters, buffered);

        /// <summary>
        /// Get a collection of entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="limitFrom"></param>
        /// <param name="limitTo"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<IEnumerable<T>> GetSetAsync<T>(object predicate, SQLSortSet sort, int limitFrom, int limitTo, IDbTransaction transaction,
            ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default)
            where T : class
            => _proxy.GetSetAsync<T>(Connection, predicate, sort, limitFrom, limitTo, transaction, filters, cancellationToken);

        /// <summary>
        /// Get a collection of entity by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="limitFrom"></param>
        /// <param name="limitTo"></param>
        /// <param name="filters"></param>
        /// <param name="buffered"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetSet<T>(object predicate, SQLSortSet sort, int limitFrom, int limitTo, ISQLPredicate[] filters = null, bool buffered = true)
            where T : class
            => _proxy.GetSet<T>(Connection, predicate, sort, limitFrom, limitTo, Transaction, filters, buffered);

        /// <summary>
        /// Get a collection of entity by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="limitFrom"></param>
        /// <param name="limitTo"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<IEnumerable<T>> GetSetAsync<T>(object predicate, SQLSortSet sort, int limitFrom, int limitTo, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default)
            where T : class
            => _proxy.GetSetAsync<T>(Connection, predicate, sort, limitFrom, limitTo, Transaction, filters, cancellationToken);

        /// <summary>
        /// Get count by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public int Count<T>(object predicate, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class
            => _proxy.Count<T>(Connection, predicate, transaction, filters);

        /// <summary>
        /// Get count by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<int> CountAsync<T>(object predicate, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default)
            where T : class
            => _proxy.CountAsync<T>(Connection, predicate, transaction, filters, cancellationToken);

        /// <summary>
        /// Get count by given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="filters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public int Count<T>(object predicate, ISQLPredicate[] filters = null) where T : class
            => _proxy.Count<T>(Connection, predicate, Transaction, filters);

        /// <summary>
        /// Get count by given condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<int> CountAsync<T>(object predicate, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class
            => _proxy.CountAsync<T>(Connection, predicate, Transaction, filters, cancellationToken);
    }
}