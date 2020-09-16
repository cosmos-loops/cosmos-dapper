using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Dapper.Actions;
using Cosmos.Dapper.Core.Helpers;
using Cosmos.Dapper.Mapper;
using Cosmos.Data.Common;
using Cosmos.Data.Statements;
using Cosmos.Models;

namespace Cosmos.Dapper.Core
{
    /// <summary>
    /// Dapper connector
    /// </summary>
    public partial class DapperConnector : IDapperConnector
    {
        private readonly IDapperMappingConfig _config;
        private readonly IDapperImplementor _proxy;
        private readonly ITransactionWrapper _transactionWrapper;

        private IDbTransaction Transaction => _transactionWrapper.GetOrBegin(false).Compatibility();

        /// <summary>
        /// Create a new instance of <see cref="DapperConnector" />
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="config"></param>
        /// <param name="sqlGenerator"></param>
        public DapperConnector(DbConnection connection, IDapperMappingConfig config, ISQLGenerator sqlGenerator)
        {
            _proxy = new DapperImplementor(config, sqlGenerator);

            _config = config;

            Connection = connection ?? throw new ArgumentNullException(nameof(connection));

            _transactionWrapper = new TransactionWrapper(Connection);

            TryOpenConnection();
        }

        #region Connection

        /// <summary>
        /// Gets connection
        /// </summary>
        public DbConnection Connection { get; }

        // ReSharper disable once UnusedMethodReturnValue.Local
        private bool TryOpenConnection()
        {
            try
            {
                if (Connection.State != ConnectionState.Open)
                    Connection.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Transaction

        /// <summary>
        /// Has active transaction
        /// </summary>
        public bool HasActiveTransaction => Transaction != null;

        /// <summary>
        /// Begin transaction
        /// </summary>
        /// <param name="il"></param>
        public void BeginTransaction(IsolationLevel il = IsolationLevel.ReadCommitted)
        {
            _transactionWrapper.IsolationLevel = il;
            _transactionWrapper.GetOrBegin();
        }

        /// <summary>
        /// Gets transaction wrapper
        /// </summary>
        public ITransactionWrapper TransactionWrapper => _transactionWrapper;

        /// <summary>
        /// Commit
        /// </summary>
        public void Commit()
        {
            _transactionWrapper.Commit();
        }

        /// <summary>
        /// Rollback
        /// </summary>
        public void Rollback()
        {
            _transactionWrapper.Rollback();
        }

        #endregion

        #region Simple transaction action

        /// <summary>
        /// Run in transaction
        /// </summary>
        /// <param name="action"></param>
        public void RunInTransaction(Action action)
        {
            BeginTransaction();
            try
            {
                action();
                Commit();
            }
            catch (Exception)
            {
                if (HasActiveTransaction)
                    Rollback();
                throw;
            }
        }

        /// <summary>
        /// Run in transaction
        /// </summary>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T RunInTransaction<T>(Func<T> func)
        {
            BeginTransaction();
            try
            {
                T result = func();
                Commit();
                return result;
            }
            catch (Exception)
            {
                if (HasActiveTransaction)
                    Rollback();
                throw;
            }
        }

        #endregion

        #region Insert

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        public void Insert<T>(IEnumerable<T> entities, IDbTransaction transaction) where T : class
            => _proxy.Insert(Connection, entities, transaction);

        /// <summary>
        /// Insert async
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="transaction"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task InsertAsync<T>(IEnumerable<T> entities, IDbTransaction transaction, CancellationToken cancellationToken = default) where T : class
            => _proxy.InsertAsync(Connection, entities, transaction, cancellationToken);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entities"></param>
        /// <typeparam name="T"></typeparam>
        public void Insert<T>(IEnumerable<T> entities) where T : class
            => _proxy.Insert(Connection, entities, Transaction);

        /// <summary>
        /// Insert async
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task InsertAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken = default) where T : class
            => _proxy.InsertAsync(Connection, entities, Transaction, cancellationToken);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public dynamic Insert<T>(T entity, IDbTransaction transaction) where T : class
            => _proxy.Insert(Connection, entity, transaction);

        /// <summary>
        /// Insert async
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<dynamic> InsertAsync<T>(T entity, IDbTransaction transaction, CancellationToken cancellationToken = default) where T : class
            => _proxy.InsertAsync(Connection, entity, transaction, cancellationToken);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public dynamic Insert<T>(T entity) where T : class
            => _proxy.Insert(Connection, entity, Transaction);

        /// <summary>
        /// Insert async
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<dynamic> InsertAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class
            => _proxy.InsertAsync(Connection, entity, Transaction, cancellationToken);

        #endregion

        #region Update

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Update<T>(T entity, IDbTransaction transaction, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false) where T : class
            => _proxy.Update(Connection, entity, transaction, filters, ignoreAllKeyProperties);

        /// <summary>
        /// Update async
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<bool> UpdateAsync<T>(T entity, IDbTransaction transaction, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false,
            CancellationToken cancellationToken = default) where T : class
            => _proxy.UpdateAsync(Connection, entity, transaction, filters, ignoreAllKeyProperties, cancellationToken);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="filters"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Update<T>(T entity, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false) where T : class
            => _proxy.Update(Connection, entity, Transaction, filters, ignoreAllKeyProperties);

        /// <summary>
        /// Update async
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="filters"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<bool> UpdateAsync<T>(T entity, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false, CancellationToken cancellationToken = default)
            where T : class
            => _proxy.UpdateAsync(Connection, entity, Transaction, filters, ignoreAllKeyProperties, cancellationToken);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Update<T>(IEnumerable<T> entities, IDbTransaction transaction, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false) where T : class
            => _proxy.Update(Connection, entities, transaction, filters, ignoreAllKeyProperties);

        /// <summary>
        /// Update async
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<bool> UpdateAsync<T>(IEnumerable<T> entities, IDbTransaction transaction, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false,
            CancellationToken cancellationToken = default) where T : class
            => _proxy.UpdateAsync(Connection, entities, transaction, filters, ignoreAllKeyProperties, cancellationToken);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="filters"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Update<T>(IEnumerable<T> entities, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false) where T : class
            => _proxy.Update(Connection, entities, Transaction, filters, ignoreAllKeyProperties);

        /// <summary>
        /// Update async
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="filters"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<bool> UpdateAsync<T>(IEnumerable<T> entities, ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false,
            CancellationToken cancellationToken = default) where T : class
            => _proxy.UpdateAsync(Connection, entities, Transaction, filters, ignoreAllKeyProperties, cancellationToken);

        #endregion

        #region Delete/Remove

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Delete<T>(T entity, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class
            => _proxy.Delete(Connection, entity, transaction, filters);

        /// <summary>
        /// Delete async
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<bool> DeleteAsync<T>(T entity, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default)
            where T : class
            => _proxy.DeleteAsync(Connection, entity, transaction, filters, cancellationToken);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="filters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Delete<T>(T entity, ISQLPredicate[] filters = null) where T : class
            => _proxy.Delete(Connection, entity, Transaction, filters);

        /// <summary>
        /// Delete async
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<bool> DeleteAsync<T>(T entity, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class
            => _proxy.DeleteAsync(Connection, entity, Transaction, filters, cancellationToken);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Delete<T>(IEnumerable<T> entities, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class
            => _proxy.Delete(Connection, entities, transaction, filters);

        /// <summary>
        /// Delete async
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<bool> DeleteAsync<T>(IEnumerable<T> entities, IDbTransaction transaction, ISQLPredicate[] filters = null,
            CancellationToken cancellationToken = default) where T : class
            => _proxy.DeleteAsync(Connection, entities, transaction, filters, cancellationToken);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="filters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Delete<T>(IEnumerable<T> entities, ISQLPredicate[] filters = null) where T : class
            => _proxy.Delete(Connection, entities, Transaction, filters);

        /// <summary>
        /// Delete async
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<bool> DeleteAsync<T>(IEnumerable<T> entities, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class
            => _proxy.DeleteAsync(Connection, entities, Transaction, filters, cancellationToken);

        #endregion

        #region Get

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>(dynamic id, IDbTransaction transaction, ISQLPredicate[] filters = null) where T : class
            => _proxy.Get<T>(Connection, id, transaction, filters);

        /// <summary>
        /// Get async
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<T> GetAsync<T>(dynamic id, IDbTransaction transaction, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class
            => _proxy.GetAsync<T>(Connection, id, transaction, filters, cancellationToken);

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>(dynamic id, ISQLPredicate[] filters = null) where T : class
            => _proxy.Get<T>(Connection, id, Transaction, filters);

        /// <summary>
        /// Get async
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<T> GetAsync<T>(dynamic id, ISQLPredicate[] filters = null, CancellationToken cancellationToken = default) where T : class
            => _proxy.GetAsync<T>(Connection, id, Transaction, filters, cancellationToken);

        #endregion

        #region GetMultiple

        /// <summary>
        /// Get multiple
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public IMultipleResultReader GetMultiple(SQLMultiplePredicate predicate, IDbTransaction transaction)
            => _proxy.GetMultiple(Connection, predicate, transaction);

        /// <summary>
        /// Get multiple async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IMultipleResultReader> GetMultipleAsync(SQLMultiplePredicate predicate, IDbTransaction transaction, CancellationToken cancellationToken = default)
            => _proxy.GetMultipleAsync(Connection, predicate, transaction, cancellationToken);

        /// <summary>
        /// Get multiple
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IMultipleResultReader GetMultiple(SQLMultiplePredicate predicate)
            => _proxy.GetMultiple(Connection, predicate, Transaction);

        /// <summary>
        /// Get multiple async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IMultipleResultReader> GetMultipleAsync(SQLMultiplePredicate predicate, CancellationToken cancellationToken = default)
            => _proxy.GetMultipleAsync(Connection, predicate, Transaction, cancellationToken);

        #endregion

        /// <summary>
        /// Clear cache
        /// </summary>
        public void ClearCache() => _proxy.SQLGenerator.Configuration.ClearCache();

        /// <summary>
        /// Get next guid
        /// </summary>
        /// <returns></returns>
        public Guid GetNextGuid() => _proxy.SQLGenerator.Configuration.GetNextGuid();

        /// <summary>
        /// Get map
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IClassMap GetMap<T>() where T : class => _proxy.SQLGenerator.Configuration.GetMap<T>();

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
            {
                if (HasActiveTransaction)
                    Transaction.Rollback();
                Connection.Close();
            }
        }

        #region Actions

        /// <summary>
        /// Get DapperAction entry
        /// </summary>
        /// <param name="contextParams"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public ISQLActionEntry GetActionEntry(IDapperContextParams contextParams, ISQLPredicate[] filters)
        {
            return new SQLActionSyncEntry(new SQLActionSet(this, _config), contextParams, filters);
        }

        /// <summary>
        /// Get DapperAction entry
        /// </summary>
        /// <param name="contextParams"></param>
        /// <param name="filters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ISQLActionEntry<T> GetActionEntry<T>(IDapperContextParams contextParams, ISQLPredicate[] filters) where T : class, IEntity, new()
        {
            return new SQLActionSyncEntry<T>(new SQLActionSet<T>(this, _config), contextParams, filters);
        }

        /// <summary>
        /// Get Asynchronous DapperAction entry
        /// </summary>
        /// <param name="contextParams"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public ISQLActionAsyncEntry GetAsynchronousActionEntry(IDapperContextParams contextParams, ISQLPredicate[] filters)
        {
            return new SQLActionAsyncEntry(new SQLActionSet(this, _config), contextParams, filters);
        }

        /// <summary>
        /// Get Asynchronous DapperAction entry
        /// </summary>
        /// <param name="contextParams"></param>
        /// <param name="filters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ISQLActionAsyncEntry<T> GetAsynchronousActionEntry<T>(IDapperContextParams contextParams, ISQLPredicate[] filters) where T : class, IEntity, new()
        {
            return new SQLActionAsyncEntry<T>(new SQLActionSet<T>(this, _config), contextParams, filters);
        }

        #endregion
    }
}