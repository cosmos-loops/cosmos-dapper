using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Dapper.Core;
using Dapper;
using Humanizer;

// ReSharper disable InconsistentNaming
namespace Cosmos.Dapper.Actions
{
    /// <summary>
    /// SqlAction Sets base
    /// </summary>
    public abstract class SQLActionSetBase : ISQLActionSet
    {
        private readonly IDapperMappingConfig _mappingConfig;
        private readonly IDapperConnector _connector;

        /// <summary>
        /// Gets a list of SqlAction
        /// </summary>
        protected readonly List<ISQLAction> _sqlActions;

        /// <summary>
        /// Create a new instance of <see cref="SQLActionSetBase" />
        /// </summary>
        /// <param name="connector"></param>
        /// <param name="config"></param>
        protected SQLActionSetBase(IDapperConnector connector, IDapperMappingConfig config)
        {
            _mappingConfig = config ?? throw new ArgumentNullException(nameof(config));
            _connector = connector ?? throw new ArgumentNullException(nameof(connector));
            _sqlActions = new List<ISQLAction>();

            Options = _mappingConfig.Options;
        }

        /// <summary>
        /// Gets calling mode
        /// </summary>
        public ActionCallingMode CallingMode { get; private set; } = ActionCallingMode.SyncMode;

        private bool _hasChangedCallingMode = false;

        /// <summary>
        /// Gets dapper options
        /// </summary>
        public DapperOptions Options { get; }

        #region internal getters

        internal IDapperMappingConfig InternalMappingConfig => _mappingConfig;

        internal IDapperConnector InternalConnector => _connector;

        #endregion

        #region add sql action

        /// <summary>
        /// Add SqlAction
        /// </summary>
        /// <param name="action"></param>
        public void AddSQLAction(ISQLAction action)
        {
            GuardAction(action);
            AddDapperActionSafety(action);
            UpdateActionCallingModeIfNeed(action);
        }

        private void GuardAction(ISQLAction action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            if (_hasChangedCallingMode && action.CallingMode != CallingMode)
                throw new InvalidOperationException($"Current action calling mode is '{CallingMode.Humanize()}', not '{action.CallingMode.Humanize()}'");
        }

        private void AddDapperActionSafety(ISQLAction action)
        {
            _sqlActions.Add(action);
        }

        private void UpdateActionCallingModeIfNeed(ISQLAction action)
        {
            if (!_hasChangedCallingMode)
            {
                CallingMode = action.CallingMode;
                _hasChangedCallingMode = true;
            }
        }

        #endregion

        /// <summary>
        /// Execute
        /// </summary>
        public void Execute()
        {
            foreach (var action in _sqlActions)
            {
                if (action is IExecutableSQLAction executable)
                {
                    executable.ExecuteCalledFromBank();
                }
            }
        }

        /// <summary>
        /// Execute async
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            foreach (var action in _sqlActions)
            {
                if (action is IAsynchronousExecutableSQLAction executable)
                {
                    await executable.ExecuteCalledFromBankAsync(cancellationToken);
                }
            }
        }
    }
}