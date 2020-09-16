using System;
using System.Collections.Concurrent;
using System.Data;
using System.Data.Common;
using System.Threading;
using Cosmos.Dapper.Actions;
using Cosmos.Dapper.Core;
using Cosmos.Dapper.Core.Contextual;
using Cosmos.Dapper.Core.Helpers;
using Cosmos.Dapper.Core.Mapping;
using Cosmos.Dapper.EntityMapping;
using Cosmos.Dapper.Operations;
using Cosmos.Data.SqlKata;
using Cosmos.Data.Statements;
using Cosmos.Models;
using Dapper;

namespace Cosmos.Dapper
{
    /// <summary>
    /// Dapper context
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <typeparam name="TConnection"></typeparam>
    public abstract partial class DapperContext<TContext, TConnection> : IDapperContext, IDapperQueryOperator, IWithConnection<TConnection>, IWithSQLGenerator
        where TContext : DapperContext<TContext, TConnection>, IDapperContext, IWithConnection<TConnection>, IWithSQLGenerator
        where TConnection : DbConnection
    {
        private readonly IDapperConnector<TConnection> _connector;
        private readonly DapperConfig _mappingConfig;
        private readonly IDapperContextParams _contextParams;

        /// <summary>
        /// Create a new instance of <see cref="DapperContext{TContext, TConnection}" />
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="params"></param>
        protected DapperContext(TConnection connection, IDapperContextParams @params)
        {
            var eap = LazyThreadSafetyMode.ExecutionAndPublication;
            ((IWithSQLGenerator) this).SqlGenerator = @params.GetSqlGenerator();
            _contextParams = @params;
            _mappingConfig = @params.GetConfig();
            _connector = new DapperConnector<TConnection>(connection, _mappingConfig, ((IWithSQLGenerator) this).SqlGenerator);
            _lazyQueryOperators = new Lazy<IDapperQueryOperator>(() => new DapperQueryOperator(_connector, _mappingConfig, InjectTransaction), eap);
            _lazyCommandOperators = new Lazy<IDapperCommandOperator>(() => new DapperCommandOperator(_connector, _mappingConfig, InjectTransaction), eap);
            _lazyBulkInsertOperators = new Lazy<IDapperBulkInsertOperator>(() => @params.GetBulkInsertOperator(_connector), eap);
            _dapperSetCache = new ConcurrentDictionary<int, object>();

            SafeConnectionString = connection.ConnectionString;
            OriginalConnectionString = @params.GetOptions().ConnectionString;

            OnContextCreatingScoped();
        }

        #region TSQLGenerator

        ISQLGenerator IWithSQLGenerator.SqlGenerator { get; set; }

        #endregion

        #region Connection String

        /// <inheritdoc />
        public string SafeConnectionString { get; }

        /// <inheritdoc />
        public string OriginalConnectionString { get; }

        #endregion

        #region transaction

        /// <summary>
        /// Gets transation
        /// </summary>
        public IDbTransaction Transaction => _connector.TransactionWrapper.GetOrBegin(false).Compatibility();

        /// <summary>
        /// Commit
        /// </summary>
        public void Commit()
        {
            Commit(null);
        }

        /// <summary>
        /// Commit
        /// </summary>
        /// <param name="callback"></param>
        public void Commit(Action callback)
        {
            try
            {
                _connector.TransactionWrapper.Commit();
            }
            catch
            {
                Rollback();
                throw;
            }
            finally
            {
                callback?.Invoke();
            }
        }

        /// <summary>
        /// Rollback
        /// </summary>
        public void Rollback()
        {
            _connector.TransactionWrapper.Rollback();
        }

        private CommandDefinition InjectTransaction(CommandDefinition commandDefinition)
        {
            if (Transaction is null)
            {
                return commandDefinition;
            }

            var command = new CommandDefinition(
                commandDefinition.CommandText,
                commandDefinition.Parameters,
                Transaction,
                commandDefinition.CommandTimeout,
                commandDefinition.CommandType,
                commandDefinition.Flags,
                commandDefinition.CancellationToken);

            return command;
        }

        #endregion

        #region operation proxy

        private readonly Lazy<IDapperQueryOperator> _lazyQueryOperators;
        private readonly Lazy<IDapperCommandOperator> _lazyCommandOperators;
        private readonly Lazy<IDapperBulkInsertOperator> _lazyBulkInsertOperators;

        /// <summary>
        /// Gets query operators
        /// </summary>
        public IDapperQueryOperator QueryOperators => _lazyQueryOperators.Value;

        /// <summary>
        /// Gets command operators
        /// </summary>
        public IDapperCommandOperator CommandOperators => _lazyCommandOperators.Value;

        /// <summary>
        /// Gets entity operators
        /// </summary>
        public IDapperEntityOperator EntityOperators => _connector;

        /// <summary>
        /// Gets bulk insert operators
        /// </summary>
        public IDapperBulkInsertOperator BulkInsertOperators => _lazyBulkInsertOperators.Value;

        #endregion

        #region dapper set entry

        private readonly ConcurrentDictionary<int, object> _dapperSetCache;

        /// <summary>
        /// Gets lazy DapperSet
        /// </summary>
        /// <param name="bindingPropertyName"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public Lazy<IDapperSet<TEntity>> DapperSetLazy<TEntity>(string bindingPropertyName) where TEntity : class, IEntity, new()
        {
            var hash = bindingPropertyName.GetHashCode();
            return _dapperSetCache.GetOrAdd(hash, DapperSet.LazyEntity<TContext, TEntity, TConnection>(this as TContext, bindingPropertyName)) as Lazy<IDapperSet<TEntity>>;
        }

        #endregion

        #region action entry

        /// <summary>
        /// Gets DapperAction entry
        /// </summary>
        /// <param name="dataFilterPredicates"></param>
        /// <returns></returns>
        public ISQLActionEntry GetActionEntry(ISQLPredicate[] dataFilterPredicates = null)
            => _connector.GetActionEntry(_contextParams, dataFilterPredicates);

        /// <summary>
        /// Gets DapperAction entry
        /// </summary>
        /// <param name="dataFilterPredicates"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public ISQLActionEntry<TEntity> GetActionEntry<TEntity>(ISQLPredicate[] dataFilterPredicates = null) where TEntity : class, IEntity, new()
            => _connector.GetActionEntry<TEntity>(_contextParams, dataFilterPredicates);

        /// <summary>
        /// Gets asynchronous DapperAction entry
        /// </summary>
        /// <param name="dataFilterPredicates"></param>
        /// <returns></returns>
        public ISQLActionAsyncEntry GetAsynchronousActionEntry(ISQLPredicate[] dataFilterPredicates = null)
            => _connector.GetAsynchronousActionEntry(_contextParams, dataFilterPredicates);

        /// <summary>
        /// Gets asynchronous DapperAction entry
        /// </summary>
        /// <param name="dataFilterPredicates"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public ISQLActionAsyncEntry<TEntity> GetAsynchronousActionEntry<TEntity>(ISQLPredicate[] dataFilterPredicates = null) where TEntity : class, IEntity, new()
            => _connector.GetAsynchronousActionEntry<TEntity>(_contextParams, dataFilterPredicates);

        #endregion

        #region sqlkata entry

        /// <summary>
        /// Gets SqlKata QueryBuilder
        /// </summary>
        /// <returns></returns>
        public QueryBuilder GetSqlKataQueryBuilder() => new QueryBuilder(_connector, _mappingConfig.GetCompiler(), _mappingConfig.Options, false);

        /// <summary>
        /// Gets SqlKata Entity QueryBuilder
        /// </summary>
        /// <returns></returns>
        public EntityQueryBuilder GetSqlKataEntityQueryBuilder() => new EntityQueryBuilder(_connector, _mappingConfig.GetCompiler(), _mappingConfig.Options, false);

        /// <summary>
        /// Gets SqlKata Multiple QueryBuilder
        /// </summary>
        /// <returns></returns>
        public MultipleQueryBuilder GetSqlKataMultipleQueryBuilder() => new MultipleQueryBuilder(_connector, _mappingConfig.GetCompiler());

        /// <summary>
        /// Gets function for SqlKata Query Builder
        /// </summary>
        /// <returns></returns>
        public Func<QueryBuilder> SqlKataQueryBuilderFunc()
        {
            return GetSqlKataQueryBuilder;
        }

        #endregion

        #region on context creating

        /// <summary>
        /// On context creating scoped
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        internal void OnContextCreatingScoped()
        {
            DapperSetsManager.RuntimeInit<TContext, TConnection>(this);
        }

        #endregion

        #region on model creating

        /// <summary>
        /// On model creating
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected virtual void OnModelCreating(DapperClassBuilder modelBuilder)
        {
            using var scanner = new EntityMapScanner<TContext>(EntityMapType);
            foreach (var map in scanner.ScanAndReturnInstances())
            {
                map?.Map(modelBuilder);
            }
        }

        /// <summary>
        /// Gets type of entity map
        /// </summary>
        protected abstract Type EntityMapType { get; }

        #endregion

        #region internal handler

        IDapperConnector<TConnection> IWithConnection<TConnection>.Connector => _connector;

        /// <summary>
        /// Gets mapping config
        /// </summary>
        internal DapperConfig MappingConfig => _mappingConfig;

        #endregion

        #region dispose

        private bool _disposed;

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _connector.Dispose();
            }

            _disposed = true;
        }

        #endregion
    }
}