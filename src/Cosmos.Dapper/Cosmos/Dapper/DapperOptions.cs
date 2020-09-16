using System;

namespace Cosmos.Dapper
{
    /// <summary>
    /// Dapper configuration from user
    /// </summary>
    public class DapperConf
    {
        /// <summary>
        /// Connection string
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Using connection pool mode
        /// </summary>
        public bool? ConnectionPoolMode { get; set; }

        /// <summary>
        /// Timeout
        /// </summary>
        public int? Timeout { get; set; }

        /// <summary>
        /// Batch size
        /// </summary>
        public int? BatchSize { get; set; }

        /// <summary>
        /// Secure fle priv
        /// </summary>
        public string SecureFilePriv { get; set; }
    }

    /// <summary>
    /// Dapper options
    /// </summary>
    public class DapperOptions
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Connection string
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Using connection pool mode
        /// </summary>
        public bool? ConnectionPoolMode { get; set; } = false;

        /// <summary>
        /// Timeout
        /// </summary>
        public int? Timeout { get; set; }

        /// <summary>
        /// Batch size
        /// </summary>
        public int BatchSize { get; set; } = 500;

        /// <summary>
        /// Secure fle priv
        /// </summary>
        public string SecureFilePriv { get; set; }

        /// <summary>
        /// Merge options from...
        /// </summary>
        /// <param name="options"></param>
        public void MergeOptionsFrom(DapperOptions options)
        {
            if (options is null)
                return;

            if (!string.IsNullOrWhiteSpace(options.ConnectionString))
                ConnectionString = options.ConnectionString;

            if (options.ConnectionPoolMode.HasValue)
                ConnectionPoolMode = options.ConnectionPoolMode;

            if (options.Timeout.HasValue && options.Timeout.Value > 0)
                Timeout = options.Timeout.Value;

            if (options.BatchSize >= 0)
                BatchSize = options.BatchSize;

            SecureFilePriv = options.SecureFilePriv;
        }

        /// <summary>
        /// Merge options from...
        /// </summary>
        /// <param name="conf"></param>
        public void MergeOptionsFrom(DapperConf conf)
        {
            if (conf is null)
                return;

            if (string.IsNullOrWhiteSpace(ConnectionString))
                ConnectionString = conf.ConnectionString;

            if (!ConnectionPoolMode.HasValue && conf.ConnectionPoolMode.HasValue)
                ConnectionPoolMode = conf.ConnectionPoolMode;

            if (!Timeout.HasValue && conf.Timeout.HasValue)
                Timeout = conf.Timeout.Value;

            if (conf.BatchSize.HasValue && conf.BatchSize.Value > 0)
                BatchSize = conf.BatchSize.Value;

            if (string.IsNullOrWhiteSpace(SecureFilePriv))
                SecureFilePriv = conf.SecureFilePriv;
        }

        /// <summary>
        /// Create Dapper options
        /// </summary>
        /// <param name="name"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static DapperOptions Create(string name, string connectionString)
        {
            return new DapperOptions {Name = name, ConnectionString = connectionString};
        }

        /// <summary>
        /// Create Dapper options
        /// </summary>
        /// <param name="optionAct"></param>
        /// <returns></returns>
        public static DapperOptions Create(Action<DapperOptions> optionAct)
        {
            var options = new DapperOptions();
            optionAct?.Invoke(options);
            return options;
        }

        /// <summary>
        /// Create Dapper options
        /// </summary>
        /// <param name="name"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static DapperOptions<TContext> Create<TContext>(string name, string connectionString)
            where TContext : class, IDapperContext
        {
            return new DapperOptions<TContext> {Name = name, ConnectionString = connectionString};
        }

        /// <summary>
        /// Create Dapper options
        /// </summary>
        /// <param name="optionAct"></param>
        /// <returns></returns>
        public static DapperOptions<TContext> Create<TContext>(Action<DapperOptions<TContext>> optionAct)
            where TContext : class, IDapperContext
        {
            var options = new DapperOptions<TContext>();
            optionAct?.Invoke(options);
            return options;
        }
    }

    /// <summary>
    /// Dapper options
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public class DapperOptions<TContext> : DapperOptions where TContext : class, IDapperContext { }

    /// <summary>
    /// Extensions for dapper options
    /// </summary>
    public static class DapperOptionsExtensions
    {
        /// <summary>
        /// Mark for context
        /// </summary>
        /// <param name="originalOptions"></param>
        /// <param name="name"></param>
        /// <typeparam name="TContext"></typeparam>
        /// <returns></returns>
        public static DapperOptions<TContext> MarkFor<TContext>(this DapperConf originalOptions, string name) where TContext : class, IDapperContext
        {
            var ret = new DapperOptions<TContext>
            {
                Name = name
            };

            ret.MergeOptionsFrom(originalOptions);

            return ret;
        }

        /// <summary>
        /// Mark
        /// </summary>
        /// <param name="originalOptions"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DapperOptions Mark(this DapperConf originalOptions, string name)
        {
            var ret = new DapperOptions
            {
                Name = name
            };

            ret.MergeOptionsFrom(originalOptions);

            return ret;
        }
    }
}