using System;
using SqlKata;

namespace Cosmos.Data.SqlKata
{
    /// <summary>
    /// Interface for query builder multiple insert dapper
    /// </summary>
    public interface IQueryBuilderMultipleInsertDapper : IQueryBuilderMultipleExecuteDapper<IResultItems>
    {
        /// <summary>
        /// Add insert
        /// </summary>
        /// <param name="query"></param>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        IQueryBuilderMultipleInsertDapper AddInsert<TReturn>(Query query);

        /// <summary>
        /// Add insert
        /// </summary>
        /// <param name="query"></param>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        IQueryBuilderMultipleInsertDapper AddInsert<TReturn>(Func<Query, Query> query);
    }
}