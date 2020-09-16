using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Data.Statements;

// ReSharper disable InconsistentNaming

namespace Cosmos.Dapper.Core
{
    /// <summary>
    /// Interface for Dapper Implementor
    /// </summary>
    public partial interface IDapperImplementor
    {
        /// <summary>
        /// Gets sql generator
        /// </summary>
        ISQLGenerator SQLGenerator { get; }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Get<T>(IDbConnection connection, dynamic id, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class;

        /// <summary>
        /// Get async
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> GetAsync<T>(IDbConnection connection, dynamic id, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default)
            where T : class;

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="entities"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        void Insert<T>(IDbConnection connection, IEnumerable<T> entities, IDbTransaction transaction) where T : class;

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        dynamic Insert<T>(IDbConnection connection, T entity, IDbTransaction transaction) where T : class;

        /// <summary>
        /// Insert async
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="entities"></param>
        /// <param name="transaction"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task InsertAsync<T>(IDbConnection connection, IEnumerable<T> entities, IDbTransaction transaction = null, CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Insert async
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<dynamic> InsertAsync<T>(IDbConnection connection, T entity, IDbTransaction transaction = null, CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool Update<T>(IDbConnection connection, T entity, IDbTransaction transaction, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false) where T : class;

        /// <summary>
        /// Update async
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> UpdateAsync<T>(IDbConnection connection, T entity, IDbTransaction transaction, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false,
            CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="entities"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool Update<T>(IDbConnection connection, IEnumerable<T> entities, IDbTransaction transaction, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false)
            where T : class;

        /// <summary>
        /// Update async
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="entities"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> UpdateAsync<T>(IDbConnection connection, IEnumerable<T> entities, IDbTransaction transaction, ISQLPredicate[] filters = null,
            bool ignoreAllKeyProperties = false, CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool Delete<T>(IDbConnection connection, T entity, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class;

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="entities"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool Delete<T>(IDbConnection connection, IEnumerable<T> entities, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class;

        /// <summary>
        /// Delete async
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> DeleteAsync<T>(IDbConnection connection, T entity, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default)
            where T : class;

        /// <summary>
        /// Delete async
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="entities"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> DeleteAsync<T>(IDbConnection connection, IEnumerable<T> entities, IDbTransaction transaction, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class;
    }
}