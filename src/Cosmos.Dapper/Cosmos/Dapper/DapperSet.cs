using System;
using System.Data;
using System.Threading;
using Cosmos.Dapper.Core.Contextual;
using Cosmos.Data.Statements;
using Cosmos.Models;

namespace Cosmos.Dapper
{
    /// <summary>
    /// Dapper set
    /// </summary>
    public abstract class DapperSet : IDapperSet
    {
        #region Repo level data filter

        /// <summary>
        /// Repository level data filter
        /// </summary>
        protected ISQLPredicate[] RepoLevelDataFilter { get; private set; }

        internal void SetRepoLevelDataFilter(ISQLPredicate[] filteringPredicates) => RepoLevelDataFilter = filteringPredicates;

        #endregion

        #region Lazy get entity(DapperSet)

        internal static Lazy<DapperSet<TEntityRef>> LazyEntity<TContextRef, TEntityRef, TConnectionRef>(TContextRef context, string dapperDbSetName)
            where TContextRef : IDapperContext, IWithConnection<TConnectionRef>
            where TEntityRef : class, IEntity, new()
            where TConnectionRef : class, IDbConnection
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            return new Lazy<DapperSet<TEntityRef>>(
                () => DapperContextualManager.GetOrUpdateEntity<TContextRef, TEntityRef, TConnectionRef>(BuildDapperSet, dapperDbSetName),
                LazyThreadSafetyMode.ExecutionAndPublication);
            
            DapperSet<TEntityRef> BuildDapperSet()
            {
                return new DapperSet<TEntityRef>(
                    context.Connector,
                    context.QueryOperators,
                    context.CommandOperators,
                    context.EntityOperators,
                    context.BulkInsertOperators,
                    context.SqlKataQueryBuilderFunc());
            }
        }

        #endregion
    }
}