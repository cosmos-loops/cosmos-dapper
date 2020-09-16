using System;
using System.Data;
using Cosmos.Dapper.Actions;
using Cosmos.Dapper.Operations;
using Cosmos.Data.Common;
using Cosmos.Data.SqlKata;
using Cosmos.Data.Statements;
using Cosmos.Models;

namespace Cosmos.Dapper
{
    /// <summary>
    /// Interface for dapper context
    /// </summary>
    public interface IDapperContext : IStoreContext, IDisposable
    {
        /// <summary>
        /// Gets safe connection string
        /// </summary>
        string SafeConnectionString { get; }
        
        /// <summary>
        /// Gets original connection string
        /// </summary>
        string OriginalConnectionString { get; }

        /// <summary>
        /// Gets current transaction
        /// </summary>
        IDbTransaction Transaction { get; }

        /// <summary>
        /// Query operators
        /// </summary>
        IDapperQueryOperator QueryOperators { get; }

        /// <summary>
        /// Command operators
        /// </summary>
        IDapperCommandOperator CommandOperators { get; }

        /// <summary>
        /// Entity operators
        /// </summary>
        IDapperEntityOperator EntityOperators { get; }

        /// <summary>
        /// Bulk insert operators
        /// </summary>
        IDapperBulkInsertOperator BulkInsertOperators { get; }

        /// <summary>
        /// Gets action entry
        /// </summary>
        /// <param name="dataFilterPredicates"></param>
        /// <returns></returns>
        ISQLActionEntry GetActionEntry(ISQLPredicate[] dataFilterPredicates = null);

        /// <summary>
        /// Gets action entry
        /// </summary>
        /// <param name="dataFilterPredicates"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        ISQLActionEntry<TEntity> GetActionEntry<TEntity>(ISQLPredicate[] dataFilterPredicates = null) where TEntity : class, IEntity, new();

        /// <summary>
        /// Gets asynchronous action entry
        /// </summary>
        /// <param name="dataFilterPredicates"></param>
        /// <returns></returns>
        ISQLActionAsyncEntry GetAsynchronousActionEntry(ISQLPredicate[] dataFilterPredicates = null);

        /// <summary>
        /// Gets asynchronous action entry
        /// </summary>
        /// <param name="dataFilterPredicates"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        ISQLActionAsyncEntry<TEntity> GetAsynchronousActionEntry<TEntity>(ISQLPredicate[] dataFilterPredicates = null) where TEntity : class, IEntity, new();

        /// <summary>
        /// Gets sql-kata query builder
        /// </summary>
        /// <returns></returns>
        QueryBuilder GetSqlKataQueryBuilder();

        /// <summary>
        /// Gets sql-kata entity query builder
        /// </summary>
        /// <returns></returns>
        EntityQueryBuilder GetSqlKataEntityQueryBuilder();

        /// <summary>
        /// Gets sql-kata multiple query builder
        /// </summary>
        /// <returns></returns>
        MultipleQueryBuilder GetSqlKataMultipleQueryBuilder();

        /// <summary>
        /// Gets sql-kata query builder function
        /// </summary>
        /// <returns></returns>
        Func<QueryBuilder> SqlKataQueryBuilderFunc();

        /// <summary>
        /// Dapper set lazy
        /// </summary>
        /// <param name="bindingPropertyName"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        Lazy<IDapperSet<TEntity>> DapperSetLazy<TEntity>(string bindingPropertyName) where TEntity : class, IEntity, new();
    }
}