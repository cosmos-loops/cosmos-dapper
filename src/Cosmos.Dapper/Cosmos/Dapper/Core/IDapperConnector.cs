using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Dapper.Actions;
using Cosmos.Dapper.Mapper;
using Cosmos.Dapper.Operations;
using Cosmos.Data.Common;
using Cosmos.Data.Statements;
using Cosmos.Models;
using Dapper;

namespace Cosmos.Dapper.Core
{
    /// <summary>
    /// Interface for Dapper connector
    /// </summary>
    public interface IDapperConnector : IDapperEntityOperator, IDisposable
    {
        /// <summary>
        /// Has active transaction
        /// </summary>
        bool HasActiveTransaction { get; }

        /// <summary>
        /// Gets connection
        /// </summary>
        DbConnection Connection { get; }

        /// <summary>
        /// Begin transaction
        /// </summary>
        /// <param name="il"></param>
        void BeginTransaction(IsolationLevel il = IsolationLevel.ReadCommitted);

        /// <summary>
        /// Gets transaction wrapper
        /// </summary>
        ITransactionWrapper TransactionWrapper { get; }

        /// <summary>
        /// Commit
        /// </summary>
        void Commit();

        /// <summary>
        /// Rollback
        /// </summary>
        void Rollback();

        /// <summary>
        /// RUn in tranaction
        /// </summary>
        /// <param name="action"></param>
        void RunInTransaction(Action action);

        /// <summary>
        /// Get multiple
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        IMultipleResultReader GetMultiple(SQLMultiplePredicate predicate, IDbTransaction transaction);

        /// <summary>
        /// Get multiple async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IMultipleResultReader> GetMultipleAsync(SQLMultiplePredicate predicate, IDbTransaction transaction, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get multiple
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IMultipleResultReader GetMultiple(SQLMultiplePredicate predicate);

        /// <summary>
        /// Get multiple async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IMultipleResultReader> GetMultipleAsync(SQLMultiplePredicate predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Clear cache
        /// </summary>
        void ClearCache();

        /// <summary>
        /// Get next guid
        /// </summary>
        /// <returns></returns>
        Guid GetNextGuid();

        /// <summary>
        /// Get map
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IClassMap GetMap<T>() where T : class;

        /// <summary>
        /// Get DapperAction entry
        /// </summary>
        /// <param name="contextParams"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        ISQLActionEntry GetActionEntry(IDapperContextParams contextParams, ISQLPredicate[] filters);

        /// <summary>
        /// Get Entity DapperAction entry
        /// </summary>
        /// <param name="contextParams"></param>
        /// <param name="filters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        ISQLActionEntry<T> GetActionEntry<T>(IDapperContextParams contextParams, ISQLPredicate[] filters) where T : class, IEntity, new();

        /// <summary>
        /// Get Asynchronous DapperAction entry
        /// </summary>
        /// <param name="contextParams"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        ISQLActionAsyncEntry GetAsynchronousActionEntry(IDapperContextParams contextParams, ISQLPredicate[] filters);

        /// <summary>
        /// Get Asynchronous Entity DapperAction entry
        /// </summary>
        /// <param name="contextParams"></param>
        /// <param name="filters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        ISQLActionAsyncEntry<T> GetAsynchronousActionEntry<T>(IDapperContextParams contextParams, ISQLPredicate[] filters) where T : class, IEntity, new();
    }
}