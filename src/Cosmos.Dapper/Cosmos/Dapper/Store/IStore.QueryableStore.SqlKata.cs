using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cosmos.Data.SqlKata;
using Cosmos.Models;
using Cosmos.Validation.Parameters;
using DotNetCore.Collections.Paginable;

namespace Cosmos.Dapper.Store
{
    /// <summary>
    /// Interface 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface ISqlKataQueryableStore<TEntity> where TEntity : class, IEntity, new()
    {
        #region Count

        /// <summary>
        /// Gets count by given SqlKata function
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <param name="columnNames"></param>
        /// <returns></returns>
        long Count([NotNull] Func<QueryBuilder, QueryBuilder> sqlKataFunc, params string[] columnNames);

        /// <summary>
        /// Gets count by given SqlKata function async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <param name="columnNames"></param>
        /// <returns></returns>
        Task<long> CountAsync([NotNull] Func<QueryBuilder, QueryBuilder> sqlKataFunc, params string[] columnNames);

        #endregion

        #region Exist

        /// <summary>
        /// Exist
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <param name="columnNames"></param>
        /// <returns></returns>
        bool Exist([NotNull] Func<QueryBuilder, QueryBuilder> sqlKataFunc, params string[] columnNames);

        /// <summary>
        /// Exist async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <param name="columnNames"></param>
        /// <returns></returns>
        Task<bool> ExistAsync([NotNull] Func<QueryBuilder, QueryBuilder> sqlKataFunc, params string[] columnNames);

        #endregion

        #region Find First

        /// <summary>
        /// Find first entity by given SqlKata function
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        TEntity FindFirst([NotNull] Func<QueryBuilder, QueryBuilder> sqlKataFunc);

        /// <summary>
        /// Find first entity by given SqlKata function async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        Task<TEntity> FindFirstAsync([NotNull] Func<QueryBuilder, QueryBuilder> sqlKataFunc);

        #endregion

        #region Find First or Default

        /// <summary>
        /// Find first entity or default by given SqlKata function
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        TEntity FindFirstOrDefault([NotNull] Func<QueryBuilder, QueryBuilder> sqlKataFunc);

        /// <summary>
        /// Find first entity or default by given SqlKata function async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        Task<TEntity> FindFirstOrDefaultAsync([NotNull] Func<QueryBuilder, QueryBuilder> sqlKataFunc);

        #endregion

        #region Find Single

        /// <summary>
        /// Find single entity by given SqlKata function
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        TEntity FindSingle([NotNull] Func<QueryBuilder, QueryBuilder> sqlKataFunc);

        /// <summary>
        /// Find single entity by given SqlKata function async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        Task<TEntity> FindSingleAsync([NotNull] Func<QueryBuilder, QueryBuilder> sqlKataFunc);

        #endregion

        #region Find Single or Default

        /// <summary>
        /// Find single entity or default by given SqlKata function
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        TEntity FindSingleOrDefault([NotNull] Func<QueryBuilder, QueryBuilder> sqlKataFunc);

        /// <summary>
        /// Find single entity or default by given SqlKata function async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        Task<TEntity> FindSingleOrDefaultAsync([NotNull] Func<QueryBuilder, QueryBuilder> sqlKataFunc);

        #endregion

        #region Find

        /// <summary>
        /// Find a collection of entity by given SqlKata function
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Find([NotNull] Func<QueryBuilder, QueryBuilder> sqlKataFunc);

        /// <summary>
        ///  Find a collection of entity by given SqlKata function async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> FindAsync([NotNull] Func<QueryBuilder, QueryBuilder> sqlKataFunc);

        #endregion

        #region Get Page

        /// <summary>
        /// Query a paged list of entity by given SqlKata function
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IPage<TEntity> GetPage([NotNull] Func<QueryBuilder, QueryBuilder> sqlKataFunc, int pageNumber, int pageSize);

        /// <summary>
        /// Query a paged list of entity by given SqlKata function async
        /// </summary>
        /// <param name="sqlKataFunc"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IPage<TEntity>> GetPageAsync([NotNull] Func<QueryBuilder, QueryBuilder> sqlKataFunc, int pageNumber, int pageSize);

        #endregion
    }
}