using System;
using Cosmos.Dapper.Core;
using Cosmos.Dapper.Core.Configs;
using Cosmos.Dapper.Map;
using Microsoft.Data.SqlClient;

namespace Cosmos.Dapper
{
    /// <summary>
    /// SqlServer Context
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public abstract class SqlServerContext<TContext> : DapperContext<TContext, SqlConnection>
        where TContext : DapperContext<TContext, SqlConnection>, IDapperContext
    {
        /// <summary>
        /// SqlServer Context
        /// </summary>
        /// <param name="options"></param>
        protected SqlServerContext(DapperOptions options) : base(options.ToConn(),
            new SqlServerContextParams(DapperConfigAccessor.Cache(options.ConnectionString))) { }

        /// <summary>
        /// Type of EntityMap
        /// </summary>
        protected override Type EntityMapType => typeof(ISqlServerEntityMap);
    }
}