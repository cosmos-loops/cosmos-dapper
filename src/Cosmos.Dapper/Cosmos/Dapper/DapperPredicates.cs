using System;
using System.Reflection;
using System.Linq.Expressions;
using Cosmos.Dapper.Core.Helpers;
using Cosmos.Data.Statements;
using Cosmos.Models;

namespace Cosmos.Dapper
{
    /// <summary>
    /// Dapper predicates
    /// </summary>
    public static class DapperPredicates
    {
        /// <summary>
        /// Field...
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="op"></param>
        /// <param name="value"></param>
        /// <param name="not"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static ISQLFieldPredicate Field<TEntity>(
            Expression<Func<TEntity, object>> expression, SQLOperatorSlim op, object value, bool not = false)
            where TEntity : class, IEntity, new()
        {
            var property = ReflectionHelper.GetProperty(expression) as PropertyInfo;
            if (property is null)
                throw new ArgumentException("No property can be found by 'expression'", nameof(expression));

            return new SQLFieldPredicate<TEntity>
            {
                PropertyName = property.Name,
                Operator = op,
                Value = value,
                Not = not
            };
        }

        /// <summary>
        /// Property...
        /// </summary>
        /// <param name="expression1"></param>
        /// <param name="op"></param>
        /// <param name="expression2"></param>
        /// <param name="not"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TEntity2"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static ISQLPropertyPredicate Property<TEntity, TEntity2>(
            Expression<Func<TEntity, object>> expression1, SQLOperatorSlim op,
            Expression<Func<TEntity2, object>> expression2, bool not = false)
            where TEntity : class, IEntity, new()
            where TEntity2 : class, IEntity, new()
        {
            var property1 = ReflectionHelper.GetProperty(expression1) as PropertyInfo;
            var property2 = ReflectionHelper.GetProperty(expression2) as PropertyInfo;
            if (property1 is null)
                throw new ArgumentException("No property can be found by 'expression1'", nameof(expression1));
            if (property2 is null)
                throw new ArgumentException("No property can be found by 'expression2'", nameof(expression2));
            return new SQLPropertyPredicate<TEntity, TEntity2>
            {
                PropertyName = property1.Name,
                PropertyName2 = property2.Name,
                Operator = op,
                Not = not
            };
        }

        /// <summary>
        /// Group...
        /// </summary>
        /// <param name="op"></param>
        /// <param name="predicates"></param>
        /// <returns></returns>
        public static ISQLPredicateGroup Group(SQLGroupOperator op, params ISQLPredicate[] predicates)
        {
            return new SQLPredicateGroup
            {
                Operator = op,
                Predicates = predicates
            };
        }

        /// <summary>
        /// Exists...
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="not"></param>
        /// <typeparam name="TSub"></typeparam>
        /// <returns></returns>
        public static ISQLExistsPredicate Exists<TSub>(ISQLPredicate predicate, bool not = false)
            where TSub : class
        {
            return new SQLExistsPredicate<TSub>
            {
                Not = not,
                Predicate = predicate
            };
        }

        /// <summary>
        /// Between...
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="values"></param>
        /// <param name="not"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static ISQLBetweenPredicate Between<TEntity>(Expression<Func<TEntity, object>> expression, SQLBetweenValues values, bool not = false)
            where TEntity : class, IEntity, new()
        {
            var property = ReflectionHelper.GetProperty(expression) as PropertyInfo;
            if (property is null)
                throw new ArgumentException("No property can be found by 'expression'", nameof(expression));
            return new SQLBetweenPredicate<TEntity>
            {
                Not = not,
                PropertyName = property.Name,
                Value = values
            };
        }

        /// <summary>
        /// Sort...
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="ascending"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static ISQLSort Sort<TEntity>(Expression<Func<TEntity>> expression, bool ascending = true)
        {
            var property = ReflectionHelper.GetProperty(expression) as PropertyInfo;
            if (property is null)
                throw new ArgumentException("No property can be found by 'expression'", nameof(expression));
            return new SQLSort(0, property.Name, ascending ? SQLSortType.ASC : SQLSortType.DESC);
        }
    }
}