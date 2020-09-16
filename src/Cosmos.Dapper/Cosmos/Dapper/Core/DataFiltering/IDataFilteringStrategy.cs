using System;
using Cosmos.Data.Statements;

namespace Cosmos.Dapper.Core.DataFiltering
{
    /// <summary>
    /// Interface for data filter strategy
    /// </summary>
    public interface IDataFilteringStrategy
    {
        /// <summary>
        /// Get filtering predicate
        /// </summary>
        /// <returns></returns>
        ISQLPredicate GetFilteringPredicate();

        /// <summary>
        /// Get signature
        /// </summary>
        /// <returns></returns>
        (Type, Type) GetSignature();
    }
}