using System;
using System.Collections.Generic;
using Cosmos.Dapper.Mapper;

namespace Cosmos.Dapper
{
    /// <summary>
    /// Interface for class map getter
    /// </summary>
    public interface IClassMapGetter
    {
        /// <summary>
        /// Class mappers
        /// </summary>
        IReadOnlyDictionary<Type, IClassMap> ClassMappers { get; }

        /// <summary>
        /// Get map for given entity type
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        IClassMap GetMap(Type entityType);
    }
}