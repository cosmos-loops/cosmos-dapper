using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Data.Statements;

namespace Cosmos.Dapper.Operations
{
    /// <summary>
    /// Interface for Dapper entity operator
    /// </summary>
    public partial interface IDapperEntityOperator
    {
        /// <summary>
        /// RUn in transaction
        /// </summary>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T RunInTransaction<T>(Func<T> func);

        /// <summary>
        /// get entity by given id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Get<T>(dynamic id, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class;

        /// <summary>
        /// get entity by given id async
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> GetAsync<T>(dynamic id, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// get entity by given id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Get<T>(dynamic id, ISQLPredicate[] filters = null) where T : class;

        /// <summary>
        /// get entity by given id async
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> GetAsync<T>(dynamic id, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Insert a collection of entity
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        void Insert<T>(IEnumerable<T> entities, IDbTransaction transaction) where T : class;

        /// <summary>
        /// Insert a collection of entity async
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="transaction"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task InsertAsync<T>(IEnumerable<T> entities, IDbTransaction transaction, CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Insert a collection of entity
        /// </summary>
        /// <param name="entities"></param>
        /// <typeparam name="T"></typeparam>
        void Insert<T>(IEnumerable<T> entities) where T : class;

        /// <summary>
        /// Insert a collection of entity async
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task InsertAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Insert an entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        dynamic Insert<T>(T entity, IDbTransaction transaction) where T : class;

        /// <summary>
        /// Insert an entity async
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<dynamic> InsertAsync<T>(T entity, IDbTransaction transaction, CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Insert an entity
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        dynamic Insert<T>(T entity) where T : class;

        /// <summary>
        /// Insert an entity async
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<dynamic> InsertAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Update an entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool Update<T>(T entity, IDbTransaction transaction, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false) where T : class;

        /// <summary>
        /// Update an entity async
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> UpdateAsync<T>(T entity, IDbTransaction transaction, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false,
            CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Update an entity 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="filters"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool Update<T>(T entity, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false) where T : class;

        /// <summary>
        /// Update an entity async
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="filters"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> UpdateAsync<T>(T entity, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false, CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Update a collection of entity
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool Update<T>(IEnumerable<T> entities, IDbTransaction transaction, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false) where T : class;

        /// <summary>
        /// Update a collection of entity async
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> UpdateAsync<T>(IEnumerable<T> entities, IDbTransaction transaction, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false,
            CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Update a collection of entity
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="filters"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool Update<T>(IEnumerable<T> entities, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false) where T : class;

        /// <summary>
        /// Update a collection of entity async
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="filters"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> UpdateAsync<T>(IEnumerable<T> entities, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false, CancellationToken cancellationToken = default)
            where T : class;

        /// <summary>
        /// Delete an entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool Delete<T>(T entity, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class;

        /// <summary>
        /// Delete an entity async
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> DeleteAsync<T>(T entity, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Delete an entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="filters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool Delete<T>(T entity, ISQLPredicate[] filters = null) where T : class;

        /// <summary>
        /// Delete an entity async
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> DeleteAsync<T>(T entity, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Delete a collection of entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool Delete<T>(IEnumerable<T> entity, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class;

        /// <summary>
        /// Delete a collection of entity async
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> DeleteAsync<T>(IEnumerable<T> entity, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Delete a collection of entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="filters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool Delete<T>(IEnumerable<T> entity, ISQLPredicate[] filters = null) where T : class;

        /// <summary>
        /// Delete a collection of entity async
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> DeleteAsync<T>(IEnumerable<T> entity, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class;
    }
}