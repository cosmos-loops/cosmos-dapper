using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using Cosmos.Dapper.Core.Mapping;
using Cosmos.Dapper.Core.Mapping.Filters;
using Cosmos.Dapper.Core.SqlKata;
using Cosmos.Dapper.EntityMapping;
using Cosmos.Dapper.Mapper;
using Cosmos.Data.Statements;
using Cosmos.IdUtils;
using Cosmos.Reflection;
using SqlKata.Compilers;

namespace Cosmos.Dapper
{
    /// <summary>
    /// Dapper config
    /// </summary>
    public class DapperConfig : IDapperMappingConfig, IInternalDapperMappingConfig, IClassMapGetter
    {
        private readonly ConcurrentDictionary<Type, IClassMap> _classMappers;
        private readonly ISqlKataCompilerCreator _compilerCreator;

        /// <summary>
        /// Create a new instance of <see cref="DapperConfig" />
        /// </summary>
        /// <param name="sqlDialect"></param>
        /// <param name="compilerCreator"></param>
        /// <param name="options"></param>
        /// <param name="strict"></param>
        public DapperConfig(ISQLDialect sqlDialect, ISqlKataCompilerCreator compilerCreator, DapperOptions options, bool strict)
            : this(typeof(AutoClassMap<>), null, sqlDialect, compilerCreator, options, strict) { }

        /// <summary>
        /// Create a new instance of <see cref="DapperConfig" />
        /// </summary>
        /// <param name="defaultMapper"></param>
        /// <param name="mappingAssemblies"></param>
        /// <param name="sqlDialect"></param>
        /// <param name="compilerCreator"></param>
        /// <param name="options"></param>
        /// <param name="strict"></param>
        public DapperConfig(
            Type defaultMapper,
            IList<Assembly> mappingAssemblies,
            ISQLDialect sqlDialect,
            ISqlKataCompilerCreator compilerCreator,
            DapperOptions options,
            bool strict)
        {
            _classMappers = new ConcurrentDictionary<Type, IClassMap>();
            _compilerCreator = compilerCreator ?? throw new ArgumentNullException(nameof(compilerCreator));
            Options = options ?? throw new ArgumentNullException(nameof(options));
            IsStrictMode = strict;

            DefaultMapper = defaultMapper;
            MappingAssemblies = mappingAssemblies ?? ClassMapperHelper.GetMapperAssemblies();
            Dialect = sqlDialect;
        }

        /// <summary>
        /// Gets class mappers
        /// </summary>
        public IReadOnlyDictionary<Type, IClassMap> ClassMappers => new ReadOnlyDictionary<Type, IClassMap>(_classMappers);

        /// <summary>
        /// Gets default mapper
        /// </summary>
        public Type DefaultMapper { get; }

        /// <summary>
        /// Gets mapping assemblies
        /// </summary>
        public IList<Assembly> MappingAssemblies { get; }

        /// <summary>
        /// Gets dialect
        /// </summary>
        public ISQLDialect Dialect { get; }

        #region GetMap

        /// <summary>
        /// Get map by given entity type
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public IClassMap GetMap(Type entityType)
        {
            if (!_classMappers.TryGetValue(entityType, out var map))
            {
                var mapType = GetMapType(entityType);
                if (mapType == null)
                    mapType = DefaultMapper.MakeGenericType(entityType);

                map = Types.CreateInstance<IClassMap>(mapType);

                UpdateSchemaName(map, entityType);
                UpdateTableName(map, entityType);

                _classMappers[entityType] = map;
            }

            return map;
        }

        /// <summary>
        /// Get map by given entity type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IClassMap<T> GetMap<T>() where T : class => GetMap(typeof(T)) as IClassMap<T>;

        /// <summary>
        /// Gets internal map
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        internal IInternalClassMapper GetInternalMap(Type entityType) => GetMap(entityType) as IInternalClassMapper;

        /// <summary>
        /// Gets internal map
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        internal IInternalClassMapper<T> GetInternalMap<T>() where T : class => GetMap<T>() as IInternalClassMapper<T>;

        /// <summary>
        /// Gets map type
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="fluentMapMode"></param>
        /// <returns></returns>
        protected virtual Type GetMapType(Type entityType, bool fluentMapMode = false)
        {
            var ret = ClassMapFilter.Filter(entityType, entityType.Assembly, fluentMapMode);
            if (ret != null) return ret;

            foreach (var mappingAssembly in MappingAssemblies)
            {
                ret = ClassMapFilter.Filter(entityType, mappingAssembly, fluentMapMode);
                if (ret != null) return ret;
            }

            return null;
        }

        #endregion

        #region SetMap

        /// <summary>
        /// Sets map
        /// </summary>
        /// <param name="classMap"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void SetMap(IClassMap classMap)
        {
            if (classMap is null)
                throw new ArgumentNullException(nameof(classMap));
            _classMappers.TryAdd(classMap.EntityType, classMap);
        }

        #endregion

        #region UpdateMap

        private void UpdateSchemaName(IClassMap map, Type entityType)
        {
            entityType.IsDefined<SchemaAttribute>().IfTrue(() =>
            {
                var schema = entityType.GetCustomAttribute<SchemaAttribute>();
                map.SetPropertyValue("SchemaName", schema?.Name);
            });
        }

        private void UpdateTableName(IClassMap map, Type entityType)
        {
            entityType.IsDefined<TableAttribute>().IfTrue(() =>
            {
                var table = entityType.GetCustomAttribute<TableAttribute>();
                map.SetPropertyValue("TableName", table?.Name);
            });
        }

        private void UpdateColumnNames(IClassMap map, Type entityType)
        {
            var properties = entityType.GetProperties();
            foreach (var property in properties)
            {
                map.PropertyMaps.Add(new PropertyMap(property));
            }
        }

        #endregion

        #region GetSqlKataCompiler

        /// <summary>
        /// Sets compiler
        /// </summary>
        /// <returns></returns>
        public Compiler GetCompiler() => _compilerCreator.Create();

        #endregion

        /// <summary>
        /// Clear cache
        /// </summary>
        public void ClearCache() => _classMappers.Clear();

        /// <summary>
        /// Gets next guid
        /// </summary>
        /// <returns></returns>
        public Guid GetNextGuid() => GuidProvider.Create(CombStyle.NormalStyle);

        /// <summary>
        /// Is strict mode
        /// </summary>
        public bool IsStrictMode { get; }

        /// <summary>
        /// Gets options
        /// </summary>
        public DapperOptions Options { get; }
    }
}