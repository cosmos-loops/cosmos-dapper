using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cosmos.Data.Statements;

namespace Cosmos.Data
{
    /// <summary>
    /// SQL order builder
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class SQLOrderBuilder<TEntity> where TEntity : class
    {
        private readonly List<SQLSort> _sorts;

        private SQLOrderBuilder()
        {
            _sorts = new List<SQLSort>();
        }

        internal SQLOrderBuilder<TEntity> AppendOrderRule<TMember>(Expression<Func<TEntity, TMember>> memberGetter, SQLSortType type)
        {
            if (memberGetter is null)
            {
                return this;
            }

            var max = _sorts.Count;
            _sorts.Add(new SQLSort(max + 1, GetPropertyName(memberGetter), type));

            return this;
        }

        /// <summary>
        /// Then by
        /// </summary>
        /// <param name="memberGetter"></param>
        /// <param name="type"></param>
        /// <typeparam name="TMember"></typeparam>
        /// <returns></returns>
        public SQLOrderBuilder<TEntity> ThenBy<TMember>(Expression<Func<TEntity, TMember>> memberGetter, SQLSortType type)
        {
            return AppendOrderRule(memberGetter, type);
        }

        internal static SQLOrderBuilder<TEntity> Create() => new SQLOrderBuilder<TEntity>();

        private static string GetPropertyName<TMember>(Expression<Func<TEntity, TMember>> memberGetter)
        {
            if (memberGetter is null)
                return string.Empty;

            return memberGetter.Body switch
            {
                MemberExpression memberExpression => GetFromMemberExp(memberExpression),
                _                                 => string.Empty
            };
        }

        private static string GetFromMemberExp(MemberExpression memberExpression)
        {
            return memberExpression.ToString().Split('.')[1];
        }

        /// <summary>
        /// Sql sort set
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static implicit operator SQLSortSet(SQLOrderBuilder<TEntity> builder)
        {
            var ret = new SQLSortSet();

            ret.AddRange(builder._sorts);

            return ret;
        }
    }
}