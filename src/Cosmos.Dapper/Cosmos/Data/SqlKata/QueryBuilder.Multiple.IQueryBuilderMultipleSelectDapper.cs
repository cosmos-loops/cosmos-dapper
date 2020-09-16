using System;
using SqlKata;

namespace Cosmos.Data.SqlKata
{
    /// <summary>
    /// Interface for query builder multiple select dapper
    /// </summary>
    public interface IQueryBuilderMultipleSelectDapper : IQueryBuilderMultipleExecuteDapper<IResultItems>
    {
        /// <summary>
        /// Add select
        /// </summary>
        /// <param name="query"></param>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        IQueryBuilderMultipleSelectDapper AddSelect<TReturn>(Query query);

        /// <summary>
        /// Add select
        /// </summary>
        /// <param name="query"></param>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        IQueryBuilderMultipleSelectDapper AddSelect<TReturn>(Func<Query, Query> query);
    }
}