using System;
using Cosmos.Dapper.Core;
using Cosmos.Dapper.Core.Configs;
using Cosmos.Dapper.Map;
using Npgsql;

namespace Cosmos.Dapper
{
    /// <summary>
    /// PostgreSql Context
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public abstract class PostgreSqlContext<TContext> : DapperContext<TContext, NpgsqlConnection>
        where TContext : DapperContext<TContext, NpgsqlConnection>, IDapperContext
    {
        /// <summary>
        /// PostgreSql Context
        /// </summary>
        /// <param name="options"></param>
        protected PostgreSqlContext(DapperOptions options) : base(options.ToConn(),
            new PostgreSqlContextParams(DapperConfigAccessor.Cache(options.ConnectionString))) { }

        /// <summary>
        /// Type of EntityMap
        /// </summary>
        protected override Type EntityMapType => typeof(IPostgreSqlEntityMap);
    }
}