using System;
using System.Collections.Generic;
using System.Reflection;
using Cosmos.Dapper.Mapper;
using Cosmos.Data.Statements;

namespace Cosmos.Dapper
{
    /// <summary>
    /// Interface for internal dapper mapping config
    /// </summary>
    internal interface IInternalDapperMappingConfig
    {
        Type DefaultMapper { get; }

        IList<Assembly> MappingAssemblies { get; }

        ISQLDialect Dialect { get; }

        IReadOnlyDictionary<Type, IClassMap> ClassMappers { get; }

        IClassMap GetMap(Type entityType);

        IClassMap<T> GetMap<T>() where T : class;

        void SetMap(IClassMap classMap);

        bool IsStrictMode { get; }
    }
}