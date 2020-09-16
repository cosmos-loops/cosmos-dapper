using System;
using Cosmos.Dapper.Core;
using Cosmos.Dapper.Core.Configs;
using Cosmos.Dapper.Map;
using MySql.Data.MySqlClient;

namespace Cosmos.Dapper
{
    /// <summary>
    /// MySql Context
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public abstract class MySqlContext<TContext> : DapperContext<TContext, MySqlConnection>
        where TContext : DapperContext<TContext, MySqlConnection>, IDapperContext
    {
        /// <summary>
        /// MySql Context
        /// </summary>
        /// <param name="options"></param>
        protected MySqlContext(DapperOptions options)
            : base(options.ToConn(), new MySqlContextParams(DapperConfigAccessor.Cache(options.ConnectionString))) { }

        /// <summary>
        /// Type of EntityMap
        /// </summary>
        protected override Type EntityMapType => typeof(IMySqlEntityMap);
    }
}