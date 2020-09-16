using System;
using System.Collections.Concurrent;

namespace Cosmos.Dapper.Core.Configs
{
    internal class DapperOptionCollection
    {
        private readonly ConcurrentDictionary<string, DapperOptions> _dapperOptionsCache;
        private readonly ConcurrentDictionary<Type, DapperOptions> _typedDapperOptionsCache;

            public DapperOptionCollection()
            {
                _dapperOptionsCache = new ConcurrentDictionary<string, DapperOptions>();
                _typedDapperOptionsCache = new ConcurrentDictionary<Type, DapperOptions>();
            }

            public DapperOptions<TContext> GetOptions<TContext>() where TContext : class, IDapperContext
            {
                return _typedDapperOptionsCache.TryGetValue(typeof(TContext), out var options) ? options as DapperOptions<TContext> : null;
            }

            public DapperOptions<TContext> GetOptions<TContext>(string name) where TContext : class, IDapperContext
            {
                return _dapperOptionsCache.TryGetValue(name, out var options) ? options as DapperOptions<TContext> : null;
            }

            public DapperOptions GetOptions(string name)
            {
                return _dapperOptionsCache.TryGetValue(name, out var options) ? options : null;
            }

            public void Add(string name, DapperOptions options)
            {
                if (_dapperOptionsCache.ContainsKey(name))
                    throw new ArgumentException($"DapperOption '{name}' exists.");

                _dapperOptionsCache.TryAdd(name, options);
            }

            public void Add<TContext>(string name, DapperOptions<TContext> options) where TContext : class, IDapperContext
            {
                if (_dapperOptionsCache.ContainsKey(name))
                    throw new ArgumentException($"DapperOption '{name}' exists.");

                var type = typeof(TContext);
                if (_typedDapperOptionsCache.ContainsKey(type))
                    throw new ArgumentException($"DapperOption '{type}' exists.");

                _dapperOptionsCache.TryAdd(name, options);
                _typedDapperOptionsCache.TryAdd(type, options);
            }
    }
}