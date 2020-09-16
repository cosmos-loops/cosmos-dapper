using System;
using Cosmos.Dapper.Core;
using Cosmos.Dapper.Core.Configs;
using Cosmos.Dapper.Map;
using Oracle.ManagedDataAccess.Client;

namespace Cosmos.Dapper
{
    /// <summary>
    /// Oracle Context
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public abstract class OracleContext<TContext> : DapperContext<TContext, OracleConnection>
        where TContext : DapperContext<TContext, OracleConnection>, IDapperContext
    {
        /// <summary>
        /// Oracle Context
        /// </summary>
        /// <param name="options"></param>
        protected OracleContext(DapperOptions options) : base(options.ToConn(),
            new OracleContextParams(DapperConfigAccessor.Cache(options.ConnectionString))) { }

        /// <summary>
        /// Type of EntityMap
        /// </summary>
        protected override Type EntityMapType => typeof(IOracleEntityMap);
    }
}