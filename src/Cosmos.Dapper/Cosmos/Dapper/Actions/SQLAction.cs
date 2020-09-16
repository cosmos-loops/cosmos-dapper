// ReSharper disable InconsistentNaming

using System;
using Cosmos.Dapper.Core;
using Cosmos.Dapper.Core.DataFiltering;
using Cosmos.Data.Common;
using Cosmos.Data.Statements;
using Cosmos.Models;

namespace Cosmos.Dapper.Actions
{
    /// <summary>
    /// SqlAction
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public abstract class SQLAction : ISQLAction, IHasRootActionBank, IHasDataFilter, IHasBulkOpt
    {
        SQLActionSetBase IHasRootActionBank.RootActionBank { get; set; }

        IDapperContextParams IHasBulkOpt.ContextParams { get; set; }

        ISQLPredicate[] IHasDataFilter.Filters { get; set; }

        /// <summary>
        /// Gets SqlAction bank getter
        /// </summary>
        protected IHasRootActionBank ActionBankGetter => this;

        /// <summary>
        /// Dapper mapping config
        /// </summary>
        protected readonly IDapperMappingConfig _mappingConfig;

        /// <summary>
        /// Dapper connector
        /// </summary>
        protected readonly IDapperConnector _connector;

        /// <summary>
        /// Create a new instance of <see cref="SQLAction" />
        /// </summary>
        /// <param name="rootActionBank"></param>
        /// <param name="kind"></param>
        /// <param name="callingMode"></param>
        /// <param name="contextParams"></param>
        /// <param name="filters"></param>
        protected SQLAction(
            SQLActionSetBase rootActionBank,
            ActionKind kind,
            ActionCallingMode callingMode,
            IDapperContextParams contextParams,
            ISQLPredicate[] filters)
        {
            var _ = (IHasRootActionBank) this;
            _.RootActionBank = rootActionBank ?? throw new ArgumentNullException(nameof(rootActionBank));

            _mappingConfig = _.RootActionBank.InternalMappingConfig;
            _connector = _.RootActionBank.InternalConnector;

            var __ = (IHasDataFilter) this;
            __.Filters = filters;

            var ___ = (IHasBulkOpt) this;
            ___.ContextParams = contextParams ?? throw new ArgumentNullException(nameof(contextParams));

            Kind = kind;
            CallingMode = callingMode;
            Options = _mappingConfig.Options;
        }

        /// <summary>
        /// Gets sql dialect
        /// </summary>
        public string Dialect => _mappingConfig.Dialect.DialectName;

        /// <summary>
        /// Get DapperAction kink
        /// </summary>
        public ActionKind Kind { get; }

        /// <summary>
        /// Gets SqlAction calling mode
        /// </summary>
        public ActionCallingMode CallingMode { get; }

        /// <summary>
        /// Get Dapper options
        /// </summary>
        public DapperOptions Options { get; }

        /// <summary>
        /// Is executed
        /// </summary>
        public bool IsExecuted { get; set; }

        /// <summary>
        /// Gets transaction wrapper
        /// </summary>
        protected ITransactionWrapper TransactionWrapper => _connector.TransactionWrapper;

        #region Add new action

        /// <summary>
        /// Store SqlAction into bank...
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        protected ISQLAction StoreActionToBank(ISQLAction action)
        {
            if (action != null)
            {
                ActionBankGetter.RootActionBank.AddSQLAction(action);
            }

            return action;
        }

        #endregion

        #region Merge filters with global

        /// <summary>
        /// Mixed Sql DataFilter
        /// </summary>
        /// <param name="filters"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        protected ISQLPredicate[] MixedDataFilter<TEntity>(ISQLPredicate[] filters) where TEntity : class, IEntity, new()
        {
            var globalFilter = GlobalDataFilterManager.GetFilter((typeof(TEntity), typeof(TEntity)));
            return DataFilterMixer.Mix(globalFilter, filters);
        }

        #endregion
    }
}