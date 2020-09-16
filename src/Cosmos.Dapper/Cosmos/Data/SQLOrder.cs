using System;
using System.Linq.Expressions;
using Cosmos.Data.Statements;

namespace Cosmos.Data
{
    /// <summary>
    /// Sql Order by
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static class SQLOrder
    {
        /// <summary>
        /// Get default sort
        /// </summary>
        public static SQLSortSet Default => null;

        /// <summary>
        /// Order by
        /// </summary>
        /// <param name="memberGetter"></param>
        /// <param name="type"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TMember"></typeparam>
        /// <returns></returns>
        public static SQLOrderBuilder<TEntity> By<TEntity, TMember>(
            Expression<Func<TEntity, TMember>> memberGetter, SQLSortType type = SQLSortType.ASC)
            where TEntity : class
        {
            return SQLOrder<TEntity>.By(memberGetter, type);
        }

        /// <summary>
        /// Then order by
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="memberGetter"></param>
        /// <param name="type"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TMember"></typeparam>
        /// <returns></returns>
        public static SQLOrderBuilder<TEntity> ThenBy<TEntity, TMember>(SQLOrderBuilder<TEntity> builder,
            Expression<Func<TEntity, TMember>> memberGetter, SQLSortType type = SQLSortType.ASC)
            where TEntity : class
        {
            return SQLOrder<TEntity>.ThenBy(builder, memberGetter, type);
        }
    }

    /// <summary>
    /// Sql order by
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    // ReSharper disable once InconsistentNaming
    public static class SQLOrder<TEntity> where TEntity : class
    {
        /// <summary>
        /// Order by
        /// </summary>
        /// <param name="memberGetter"></param>
        /// <param name="type"></param>
        /// <typeparam name="TMember"></typeparam>
        /// <returns></returns>
        public static SQLOrderBuilder<TEntity> By<TMember>(
            Expression<Func<TEntity, TMember>> memberGetter, SQLSortType type = SQLSortType.ASC)
        {
            var builder = SQLOrderBuilder<TEntity>.Create();
            return builder.AppendOrderRule(memberGetter, type);
        }

        /// <summary>
        /// Then order by
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="memberGetter"></param>
        /// <param name="type"></param>
        /// <typeparam name="TMember"></typeparam>
        /// <returns></returns>
        public static SQLOrderBuilder<TEntity> ThenBy<TMember>(SQLOrderBuilder<TEntity> builder,
            Expression<Func<TEntity, TMember>> memberGetter, SQLSortType type = SQLSortType.ASC)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            return builder.AppendOrderRule(memberGetter, type);
        }
    }
}