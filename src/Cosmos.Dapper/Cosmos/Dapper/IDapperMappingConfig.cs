using System;
using System.Collections.Generic;
using System.Reflection;
using Cosmos.Dapper.Mapper;
using Cosmos.Data.Statements;

namespace Cosmos.Dapper
{
    /// <summary>
    /// Interface for dapper mapping config
    /// </summary>
    public interface IDapperMappingConfig
    {
        /// <summary>
        /// Type of default mapper
        /// </summary>
        Type DefaultMapper { get; }

        /// <summary>
        /// Mapping assemblies
        /// </summary>
        IList<Assembly> MappingAssemblies { get; }

        /// <summary>
        /// Dialect
        /// </summary>
        ISQLDialect Dialect { get; }

        /// <summary>
        /// Gets map for given entity type
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        IClassMap GetMap(Type entityType);

        /// <summary>
        /// Get map for given entity type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IClassMap<T> GetMap<T>() where T : class;

        /// <summary>
        /// Clear cache
        /// </summary>
        void ClearCache();

        /// <summary>
        /// Get next guid
        /// </summary>
        /// <returns></returns>
        Guid GetNextGuid();

        /// <summary>
        /// Opetions
        /// </summary>
        DapperOptions Options { get; }
    }
}