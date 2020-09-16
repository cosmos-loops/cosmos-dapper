using Cosmos.Dapper;
using Cosmos.Dapper.Actions;
using Cosmos.Dapper.Core;
using Cosmos.Dapper.Core.SqlKata;
using Cosmos.Data.SqlKata;
using Cosmos.Data.Statements;
using Cosmos.Data.Statements.Dialects;
using Cosmos.Models;
using MySql.Data.MySqlClient;
using SqlKata.Compilers;

namespace Cosmos.Data
{
    /// <summary>
    /// MySQL static helper with Dapper
    /// </summary>
    public static class MySqlDapper
    {
        /// <summary>
        /// Get dapper client
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static IDapperConnector GetClient(string connectionString, int? timeout = null)
        {
            var options = CreateOptions(connectionString, timeout);
            ISqlKataCompilerCreator sqlKataCompiler = new SqlKataCompilerCreator<MySqlCompiler>();
            var mappingConfig = new DapperConfig(new MySqlDialect(), sqlKataCompiler, options, false);
            return new DapperConnector(options.ToConn(), mappingConfig, new SQLGenerator(mappingConfig));
        }

        /// <summary>
        /// Create a new dapper options
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static DapperOptions CreateOptions(string connectionString, int? timeout = null)
        {
            return new DapperOptions
            {
                Name = connectionString.GetHashCode().ToString(),
                ConnectionString = connectionString,
                Timeout = timeout
            };
        }

        private static DapperConfig CreateMappingConfig(DapperOptions options)
        {
            ISqlKataCompilerCreator sqlKataCompiler = new SqlKataCompilerCreator<MySqlCompiler>();
            return new DapperConfig(new MySqlDialect(), sqlKataCompiler, options, false);
        }

        /// <summary>
        /// Gets dapper action
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static ISQLActionEntry GetDapperAction(string connectionString, int? timeout = null)
        {
            var options = CreateOptions(connectionString, timeout);
            var mappingConfig = CreateMappingConfig(options);
            var connector = new DapperConnector<MySqlConnection>(options.ToConn(), mappingConfig, new SQLGenerator(mappingConfig));
            var contextParams = new MySqlContextParams(mappingConfig);
            return connector.GetActionEntry(contextParams, null);
        }

        /// <summary>
        /// Gets dapper action
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="timeout"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public static ISQLActionEntry<TEntity> GetDapperAction<TEntity>(string connectionString, int? timeout = null) where TEntity : class, IEntity, new()
        {
            var options = CreateOptions(connectionString, timeout);
            var mappingConfig = CreateMappingConfig(options);
            var connector = new DapperConnector<MySqlConnection>(options.ToConn(), mappingConfig, new SQLGenerator(mappingConfig));
            var contextParams = new MySqlContextParams(mappingConfig);
            return connector.GetActionEntry<TEntity>(contextParams, null);
        }

        /// <summary>
        /// Gets asynchronous dapper action
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static ISQLActionAsyncEntry GetDapperAsynchronousAction(string connectionString, int? timeout = null)
        {
            var options = CreateOptions(connectionString, timeout);
            var mappingConfig = CreateMappingConfig(options);
            var connector = new DapperConnector<MySqlConnection>(options.ToConn(), mappingConfig, new SQLGenerator(mappingConfig));
            var contextParams = new MySqlContextParams(mappingConfig);
            return connector.GetAsynchronousActionEntry(contextParams, null);
        }

        /// <summary>
        /// Gets asynchronous dapper action
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="timeout"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public static ISQLActionAsyncEntry<TEntity> GetDapperAsynchronousAction<TEntity>(string connectionString, int? timeout = null) where TEntity : class, IEntity, new()
        {
            var options = CreateOptions(connectionString, timeout);
            var mappingConfig = CreateMappingConfig(options);
            var connector = new DapperConnector<MySqlConnection>(options.ToConn(), mappingConfig, new SQLGenerator(mappingConfig));
            var contextParams = new MySqlContextParams(mappingConfig);
            return connector.GetAsynchronousActionEntry<TEntity>(contextParams, null);
        }

        /// <summary>
        /// Get SqlKata Query Builder
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static QueryBuilder GetSqlKataQueryBuilder(string connectionString, int? timeout = null)
        {
            var options = CreateOptions(connectionString, timeout);
            var mappingConfig = CreateMappingConfig(options);
            var connector = new DapperConnector<MySqlConnection>(options.ToConn(), mappingConfig, new SQLGenerator(mappingConfig));
            return new QueryBuilder(connector, mappingConfig.GetCompiler(), options);
        }

        /// <summary>
        /// Get SqlKata Entity Query Builder
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static EntityQueryBuilder GetSqlKataEntityQueryBuilder(string connectionString, int? timeout = null)
        {
            var options = CreateOptions(connectionString, timeout);
            var mappingConfig = CreateMappingConfig(options);
            var connector = new DapperConnector<MySqlConnection>(options.ToConn(), mappingConfig, new SQLGenerator(mappingConfig));
            return new EntityQueryBuilder(connector, mappingConfig.GetCompiler(), options);
        }

        /// <summary>
        /// Get SqlKata Multiple Query Builder
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static MultipleQueryBuilder GetSqlKataMultipleQueryBuilder(string connectionString, int? timeout = null)
        {
            var options = CreateOptions(connectionString, timeout);
            var mappingConfig = CreateMappingConfig(options);
            var connector = new DapperConnector<MySqlConnection>(options.ToConn(), mappingConfig, new SQLGenerator(mappingConfig));
            return new MultipleQueryBuilder(connector, mappingConfig.GetCompiler());
        }
    }
}