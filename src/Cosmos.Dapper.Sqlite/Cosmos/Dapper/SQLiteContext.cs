using System;
using System.Data.SQLite;
using Cosmos.Dapper.Core;
using Cosmos.Dapper.Core.Configs;
using Cosmos.Dapper.Map;

namespace Cosmos.Dapper
{
    /// <summary>
    /// SQLite Context
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    // ReSharper disable once InconsistentNaming
    public abstract class SQLiteContext<TContext> : DapperContext<TContext, SQLiteConnection>
        where TContext : DapperContext<TContext, SQLiteConnection>, IDapperContext
    {
        /// <summary>
        /// SQLite Context
        /// </summary>
        /// <param name="options"></param>
        protected SQLiteContext(DapperOptions options) : base(options.ToConn(),
            new SQLiteContextParams(DapperConfigAccessor.Cache(options.ConnectionString))) { }

        /// <summary>
        /// Type of EntityMap
        /// </summary>
        protected override Type EntityMapType => typeof(ISQLiteEntityMap);
    }
}