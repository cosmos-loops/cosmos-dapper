using System;
using SqlKata;

namespace Cosmos.Data.SqlKata
{
    /// <summary>
    /// Interface for query builder multiple update dapper
    /// </summary>
    public interface IQueryBuilderMultipleUpdateDapper : IQueryBuilderMultipleExecuteDapper<IResultAffectedRows>
    {
        /// <summary>
        /// Add update
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IQueryBuilderMultipleUpdateDapper AddUpdate(Query query);

        /// <summary>
        /// Add update
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IQueryBuilderMultipleUpdateDapper AddUpdate(Func<Query, Query> query);
    }
}