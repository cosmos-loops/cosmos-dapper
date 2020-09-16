using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cosmos.Data.Statements;
using Cosmos.Reflection;

namespace Cosmos.Dapper.Core.DynamicQuery
{
    /// <summary>
    /// Dynamic expression resolver
    /// </summary>
    public static class DynamicExpressionResolver
    {
        /// <summary>
        /// Resolve expression to sql predicate
        /// </summary>
        /// <param name="expression"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public static ISQLPredicate ResolveExprTree<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class
        {
            if (expression is null)
                return null;

            var body = expression.Body;
            var bodyType = body.GetType();

            return expression.Body switch
            {
                MemberExpression memberExpression => ResolveExprTree<TEntity>(memberExpression, false),
                UnaryExpression unaryExpression   => ResolveExprTree<TEntity>(unaryExpression.Operand, true),
                _                                 => ResolveExprTree<TEntity>((BinaryExpression) expression.Body, false)
            };
        }

        private static ISQLPredicate ResolveExprTree<TEntity>(Expression expression, bool not) where TEntity : class
        {
            return expression switch
            {
                null                              => null,
                MemberExpression memberExpression => ResolveExprTree<TEntity>(memberExpression, not),
                UnaryExpression unaryExpression   => ResolveExprTree<TEntity>((MemberExpression) unaryExpression.Operand, !not),
                _                                 => ResolveExprTree<TEntity>((BinaryExpression) expression, not)
            };
        }

        private static ISQLPredicate ResolveExprTree<TEntity>(BinaryExpression body, bool not) where TEntity : class
        {
            ISQLPredicate predicate;

            if (body.NodeType != ExpressionType.AndAlso && body.NodeType != ExpressionType.OrElse)
            {
                var propertyName = GetPropertyName(body);
                var propertyValue = GetPropertyValue(body.Right);
                var @operator = GetSQLOperator(body.NodeType);

                var fPredicate = new SQLFieldPredicate<TEntity>
                {
                    EntityType = typeof(TEntity),
                    PropertyName = propertyName,
                    Value = propertyValue,
                    Operator = @operator.@operator,
                    Not = @operator.not
                };

                predicate = fPredicate;
            }
            else
            {
                var group = new SQLPredicateGroup
                {
                    Operator = GetSQLGroupOperator(body.NodeType),
                    Predicates = new List<ISQLPredicate>()
                };

                var lPredicate = ResolveExprTree<TEntity>(body.Left, not);
                var rPredicate = ResolveExprTree<TEntity>(body.Right, not);

                group.Predicates.Add(lPredicate);
                group.Predicates.Add(rPredicate);

                predicate = group;
            }

            return predicate;
        }

        private static ISQLPredicate ResolveExprTree<TEntity>(MemberExpression body, bool not) where TEntity : class
        {
            var isBoolean = TypeConv.ToSafeNonNullableType(body.Type) == TypeClass.BooleanClazz;

            if (body.NodeType == ExpressionType.MemberAccess && isBoolean)
            {
                return new SQLFieldPredicate<TEntity>
                {
                    EntityType = typeof(TEntity),
                    PropertyName = GetPropertyName(body),
                    Value = true,
                    Operator = SQLOperatorSlim.EQ,
                    Not = not
                };
            }

            return null;
        }

        private static object GetPropertyValue(Expression source)
        {
            if (source is ConstantExpression constantExpression)
                return constantExpression.Value;

            var evalExpr = Expression.Lambda<Func<object>>(Expression.Convert(source, typeof(object)));
            var evalFunc = evalExpr.Compile();
            var value = evalFunc();
            return value;
        }

        private static string GetPropertyName(BinaryExpression body)
        {
            var propertyName = body.Left.ToString().Split('.')[1];

            if (body.Left.NodeType == ExpressionType.Convert)
            {
                //hack to remove the trailing ')' when converting.
                propertyName = propertyName.Replace(")", string.Empty);
            }

            return propertyName;
        }

        private static string GetPropertyName(MemberExpression body)
        {
            var propertyName = body.ToString().Split('.')[1];

            return propertyName;
        }

        // ReSharper disable once InconsistentNaming
        private static SQLGroupOperator GetSQLGroupOperator(ExpressionType nodeType)
        {
            return nodeType == ExpressionType.AndAlso
                ? SQLGroupOperator.AND
                : SQLGroupOperator.OR;
        }

        // ReSharper disable once InconsistentNaming
        private static (SQLOperatorSlim @operator, bool not) GetSQLOperator(ExpressionType type)
        {
            switch (type)
            {
                case ExpressionType.Equal:
                    return (SQLOperatorSlim.EQ, false);

                case ExpressionType.NotEqual:
                    return (SQLOperatorSlim.EQ, true);

                case ExpressionType.LessThan:
                    return (SQLOperatorSlim.LT, false);

                case ExpressionType.GreaterThan:
                    return (SQLOperatorSlim.GT, false);

                case ExpressionType.GreaterThanOrEqual:
                    return (SQLOperatorSlim.GE, false);

                case ExpressionType.LessThanOrEqual:
                    return (SQLOperatorSlim.LE, false);

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}