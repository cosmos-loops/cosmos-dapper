using System;
using SqlKata;

namespace Cosmos.Data.SqlKata
{
    /// <summary>
    /// Interface for Quert Builder Multiple Delete Dapper
    /// </summary>
    public interface IQueryBuilderMultipleDeleteDapper : IQueryBuilderMultipleExecuteDapper<IResultAffectedRows>
    {
        /// <summary>
        /// Add delete
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IQueryBuilderMultipleDeleteDapper AddDelete(Query query);

        /// <summary>
        /// Add delete
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IQueryBuilderMultipleDeleteDapper AddDelete(Func<Query, Query> query);
    }
}