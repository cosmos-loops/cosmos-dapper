using System;
using System.Collections.Generic;
using System.Reflection;
using Cosmos.Dapper;
using Cosmos.Dapper.Core;
using Cosmos.Dapper.Core.Binders;
using Cosmos.Dapper.Core.Configs;
using Cosmos.Dapper.Core.Contextual;
using Cosmos.Dapper.Core.SqlKata;
using Cosmos.Dapper.Mapper;
using Cosmos.Dapper.Operations;
using Cosmos.Data.Common;
using Cosmos.Data.Core.Registrars;
using Cosmos.Data.Statements.Dialects;
using Cosmos.Reflection;
using Microsoft.Extensions.Configuration;
using SqlKata.Compilers;

namespace Cosmos.Dependency
{
    /// <summary>
    /// Extensions for DbContext Config
    /// </summary>
    public static class DbContextDependencyExtensions
    {
        #region UseMySqlDapper

        /// <summary>
        /// Use Cosmos Dapper for MySql
        /// </summary>
        /// <param name="config"></param>
        /// <param name="name"></param>
        /// <param name="connectionString"></param>
        /// <param name="configureConvention"></param>
        /// <param name="mappingAssemblies"></param>
        /// <returns></returns>
        public static IDbContextConfig UseDapperWithMySql(
            this IDbContextConfig config,
            string name,
            string connectionString,
            Action<FluentMapConfiguration> configureConvention = null,
            IList<Assembly> mappingAssemblies = null)
        {
            var options = DapperOptions.Create(name, connectionString);
            return CoreRegistrar.AsNormal(config, options, configureConvention, mappingAssemblies);
        }

        /// <summary>
        /// Use Cosmos Dapper for MySql
        /// </summary>
        /// <param name="config"></param>
        /// <param name="name"></param>
        /// <param name="configuration"></param>
        /// <param name="configureConvention"></param>
        /// <param name="mappingAssemblies"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IDbContextConfig UseDapperWithMySql(
            this IDbContextConfig config,
            string name,
            IConfiguration configuration,
            Action<FluentMapConfiguration> configureConvention = null,
            IList<Assembly> mappingAssemblies = null)
        {
            configuration.CheckNull(nameof(configuration));

            if (string.IsNullOrWhiteSpace(name))
                name = "default";

            var conf = CoreRegistrar.LoadOptionFromConfiguration(configuration, name);

            if (name == "default")
                name = conf.ConnectionString.GetHashCode().ToString();

            return CoreRegistrar.AsNormal(config, conf.Mark(name), configureConvention, mappingAssemblies);
        }

        /// <summary>
        /// Use Cosmos Dapper for MySql
        /// </summary>
        /// <param name="config"></param>
        /// <param name="connectionString"></param>
        /// <param name="configureConvention"></param>
        /// <param name="mappingAssemblies"></param>
        /// <returns></returns>
        public static IDbContextConfig UseDapperWithMySql(
            this IDbContextConfig config,
            string connectionString,
            Action<FluentMapConfiguration> configureConvention = null,
            IList<Assembly> mappingAssemblies = null)
        {
            var options = DapperOptions.Create(connectionString.GetHashCode().ToString(), connectionString);
            return CoreRegistrar.AsNormal(config, options, configureConvention, mappingAssemblies);
        }

        /// <summary>
        /// Use Cosmos Dapper for MySql
        /// </summary>
        /// <param name="config"></param>
        /// <param name="optionAct"></param>
        /// <param name="configureConvention"></param>
        /// <param name="mappingAssemblies"></param>
        /// <returns></returns>
        public static IDbContextConfig UseDapperWithMySql(
            this IDbContextConfig config,
            Action<DapperOptions> optionAct,
            Action<FluentMapConfiguration> configureConvention = null,
            IList<Assembly> mappingAssemblies = null)
        {
            var options = DapperOptions.Create(optionAct);
            return CoreRegistrar.AsNormal(config, options, configureConvention, mappingAssemblies);
        }

        /// <summary>
        /// Use Cosmos Dapper for MySql
        /// </summary>
        /// <param name="config"></param>
        /// <param name="configuration"></param>
        /// <param name="optionAct"></param>
        /// <param name="configureConvention"></param>
        /// <param name="mappingAssemblies"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IDbContextConfig UseDapperWithMySql(
            this IDbContextConfig config,
            IConfiguration configuration,
            Action<DapperOptions> optionAct,
            Action<FluentMapConfiguration> configureConvention = null,
            IList<Assembly> mappingAssemblies = null)
        {
            configuration.CheckNull(nameof(configuration));

            var options = DapperOptions.Create(optionAct);

            if (string.IsNullOrWhiteSpace(options.Name))
                options.Name = "default";

            var conf = CoreRegistrar.LoadOptionFromConfiguration(configuration, options.Name);

            options.MergeOptionsFrom(conf);

            if (options.Name == "default")
                options.Name = options.ConnectionString.GetHashCode().ToString();

            return CoreRegistrar.AsNormal(config, options, configureConvention, mappingAssemblies);
        }

        #endregion

        #region UseMySqlDapperStrictly

        /// <summary>
        /// Use Cosmos Dapper for MySql Strictly
        /// </summary>
        /// <param name="config"></param>
        /// <param name="name"></param>
        /// <param name="connectionString"></param>
        /// <param name="configureConvention"></param>
        /// <param name="mappingAssemblies"></param>
        /// <typeparam name="TContext"></typeparam>
        /// <returns></returns>
        public static IDbContextConfig UseDapperWithMySql<TContext>(
            this IDbContextConfig config,
            string name,
            string connectionString,
            Action<FluentMapConfiguration> configureConvention = null,
            IList<Assembly> mappingAssemblies = null)
            where TContext : class, IDapperContext, IDapperQueryOperator, IStoreContext
        {
            var options = DapperOptions.Create<TContext>(name, connectionString);
            return CoreRegistrar.AsStrictly(config, options, configureConvention, mappingAssemblies);
        }

        /// <summary>
        /// Use Cosmos Dapper for MySql Strictly
        /// </summary>
        /// <param name="config"></param>
        /// <param name="name"></param>
        /// <param name="configuration"></param>
        /// <param name="configureConvention"></param>
        /// <param name="mappingAssemblies"></param>
        /// <typeparam name="TContext"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IDbContextConfig UseDapperWithMySql<TContext>(
            this IDbContextConfig config,
            string name,
            IConfiguration configuration,
            Action<FluentMapConfiguration> configureConvention = null,
            IList<Assembly> mappingAssemblies = null)
            where TContext : class, IDapperContext, IDapperQueryOperator, IStoreContext
        {
            configuration.CheckNull(nameof(configuration));

            if (string.IsNullOrWhiteSpace(name))
                name = "default";

            var conf = CoreRegistrar.LoadOptionFromConfiguration(configuration, name);

            if (name == "default")
                name = typeof(TContext).FullName;

            return CoreRegistrar.AsStrictly(config, conf.MarkFor<TContext>(name), configureConvention, mappingAssemblies);
        }

        /// <summary>
        /// Use Cosmos Dapper for MySql Strictly
        /// </summary>
        /// <param name="config"></param>
        /// <param name="connectionString"></param>
        /// <param name="configureConvention"></param>
        /// <param name="mappingAssemblies"></param>
        /// <typeparam name="TContext"></typeparam>
        /// <returns></returns>
        public static IDbContextConfig UseDapperWithMySql<TContext>(
            this IDbContextConfig config,
            string connectionString,
            Action<FluentMapConfiguration> configureConvention = null,
            IList<Assembly> mappingAssemblies = null)
            where TContext : class, IDapperContext, IDapperQueryOperator, IStoreContext
        {
            var options = DapperOptions.Create<TContext>(typeof(TContext).FullName, connectionString);
            return CoreRegistrar.AsStrictly(config, options, configureConvention, mappingAssemblies);
        }

        /// <summary>
        /// Use Cosmos Dapper for MySql Strictly
        /// </summary>
        /// <param name="config"></param>
        /// <param name="optionAct"></param>
        /// <param name="configureConvention"></param>
        /// <param name="mappingAssemblies"></param>
        /// <typeparam name="TContext"></typeparam>
        /// <returns></returns>
        public static IDbContextConfig UseDapperWithMySql<TContext>(
            this IDbContextConfig config,
            Action<DapperOptions<TContext>> optionAct,
            Action<FluentMapConfiguration> configureConvention = null,
            IList<Assembly> mappingAssemblies = null)
            where TContext : class, IDapperContext, IDapperQueryOperator, IStoreContext
        {
            var options = DapperOptions.Create(optionAct);
            return CoreRegistrar.AsStrictly(config, options, configureConvention, mappingAssemblies);
        }

        /// <summary>
        /// Use Cosmos Dapper for MySql Strictly
        /// </summary>
        /// <param name="config"></param>
        /// <param name="configuration"></param>
        /// <param name="optionAct"></param>
        /// <param name="configureConvention"></param>
        /// <param name="mappingAssemblies"></param>
        /// <typeparam name="TContext"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IDbContextConfig UseDapperWithMySql<TContext>(
            this IDbContextConfig config,
            IConfiguration configuration,
            Action<DapperOptions<TContext>> optionAct,
            Action<FluentMapConfiguration> configureConvention = null,
            IList<Assembly> mappingAssemblies = null)
            where TContext : class, IDapperContext, IDapperQueryOperator, IStoreContext
        {
            configuration.CheckNull(nameof(configuration));

            var options = DapperOptions.Create(optionAct);

            if (string.IsNullOrWhiteSpace(options.Name))
                options.Name = "default";

            var conf = CoreRegistrar.LoadOptionFromConfiguration(configuration, options.Name);

            options.MergeOptionsFrom(conf);

            if (options.Name == "default")
                options.Name = options.ConnectionString.GetHashCode().ToString();

            return CoreRegistrar.AsStrictly(config, options, configureConvention, mappingAssemblies);
        }

        #endregion

        private static class CoreRegistrar
        {
            public static IDbContextConfig AsNormal(IDbContextConfig config, DapperOptions options,
                Action<FluentMapConfiguration> configureConvention = null,
                IList<Assembly> mappingAssemblies = null)
            {
                Register(options, false, configureConvention, mappingAssemblies);

                var optDescriptor = DependencyProxyDescriptor.CreateForService<DapperOptions>(options, DependencyLifetimeType.Singleton);

                config.Configure(bag => { bag.Register(optDescriptor); });

                DapperOptionManager.Set(options);

                return config;
            }

            public static IDbContextConfig AsStrictly<TContext>(IDbContextConfig config, DapperOptions<TContext> options,
                Action<FluentMapConfiguration> configureConvention = null,
                IList<Assembly> mappingAssemblies = null)
                where TContext : class, IDapperContext, IDapperQueryOperator, IStoreContext
            {
                var mappingConfig = Register(options, true, configureConvention, mappingAssemblies);

                var context = BuildContextInstanceOnce(options);

                DapperContextualManager.ModelCreatingCall(context, mappingConfig);

                var ctxDescriptor = DependencyProxyDescriptor.CreateForServiceDelegate<TContext, TContext>(() => BuildContextInstanceOnce(options), DependencyLifetimeType.Scoped);
                var optDescriptor = DependencyProxyDescriptor.CreateForService<DapperOptions<TContext>>(options, DependencyLifetimeType.Singleton);

                config.Configure(bag =>
                {
                    bag.Register(ctxDescriptor);
                    bag.Register(optDescriptor);
                });

                DapperOptionManager.Set(options);

                return config;
            }

            private static DapperConfig Register(DapperOptions options, bool strictMode,
                Action<FluentMapConfiguration> configureConvention = null,
                IList<Assembly> mappingAssemblies = null)
            {
                InternalDapperRegistrar.GuardDapperOptions(options);

                var dialect = new MySqlDialect();
                var defaultMapper = typeof(AutoClassMap<>);
                ISqlKataCompilerCreator sqlKataCompiler = new SqlKataCompilerCreator<MySqlCompiler>();
                var dapperConfig = new DapperConfig(defaultMapper, mappingAssemblies, dialect, sqlKataCompiler, options, strictMode);

                if (configureConvention != null)
                {
                    var conventionConfig = new FluentMapConfiguration(dapperConfig);
                    configureConvention(conventionConfig);
                }

                SyncBindingManager.Sync(dapperConfig);

                DapperConfigAccessor.RefreshCache(options.ConnectionString, dapperConfig);

                DapperGlobalRegistrar.RegisterForCosmosDapper();

                return dapperConfig;
            }

            private static TContext BuildContextInstanceOnce<TContext>(DapperOptions<TContext> options)
                where TContext : class, IDapperContext, IDapperQueryOperator, IStoreContext
            {
                return Types.CreateInstance<TContext>(typeof(TContext), options);
            }

            public static DapperConf LoadOptionFromConfiguration(IConfiguration configuration, string sectionName)
            {
                var conf = configuration.GetValue<DapperConf>($"Cosmos:Data:{sectionName}");
                if (conf.ConnectionString.IsNullOrWhiteSpace())
                    conf.ConnectionString = configuration.GetConnectionString(sectionName);
                return conf;
            }
        }
    }
}