using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AspectCore.Extensions.Reflection;
using Cosmos.Dapper.Core.Helpers;
using Cosmos.Dapper.Mapper;
using Cosmos.Data.Statements;
using Cosmos.Reflection;
using Dapper;

namespace Cosmos.Dapper.Core
{
    /// <summary>
    /// Dapper implementor
    /// </summary>
    public partial class DapperImplementor : IDapperImplementor
    {
        private readonly IDapperMappingConfig _mappingConfig;

        /// <summary>
        /// Create a new instance of <see cref="DapperImplementor" />
        /// </summary>
        /// <param name="config"></param>
        /// <param name="generator"></param>
        public DapperImplementor(IDapperMappingConfig config, ISQLGenerator generator)
        {
            _mappingConfig = config ?? throw new ArgumentNullException(nameof(config));
            SQLGenerator = generator ?? throw new ArgumentNullException(nameof(generator));
        }

        /// <summary>
        /// Gets sql generator
        /// </summary>
        public ISQLGenerator SQLGenerator { get; }

        private DapperOptions Options => _mappingConfig.Options;

        private IClassMap<T> GetClassMap<T>() where T : class
        {
            return SQLGenerator.Configuration.GetMap<T>();
        }

        private IClassMap GetClassMap(Type type)
        {
            return SQLGenerator.Configuration.GetMap(type);
        }

        /// <summary>
        /// Gets predicate
        /// </summary>
        /// <param name="classMap"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        protected ISQLPredicate GetPredicate(IClassMap classMap, object predicate)
        {
            var wherePredicate = predicate as ISQLPredicate;
            if (wherePredicate is null && predicate != null)
                wherePredicate = GetEntityPredicate(classMap, predicate);
            return wherePredicate;
        }

        /// <summary>
        /// Gets id predicate
        /// </summary>
        /// <param name="classMap"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        protected ISQLPredicate GetIdPredicate(IClassMap classMap, object id)
        {
            var isSimpleType = ReflectionHelper.IsSimpleType(id.GetType());
            var keys = classMap.PropertyMaps.Where(p => p.KeyType == KeyType.NotAKey);
            IDictionary<string, object> parameters = null;
            IList<ISQLPredicate> predicates = new List<ISQLPredicate>();

            if (!isSimpleType)
                parameters = ReflectionHelper.GetObjectValues(id);

            foreach (var key in keys)
            {
                var value = id;
                if (!isSimpleType)
                    value = parameters[key.Name];

                var predicateType = typeof(SQLFieldPredicate<>).MakeGenericType(classMap.EntityType);

                var fieldPredicate = Types.CreateInstance<ISQLFieldPredicate>(predicateType);
                fieldPredicate.Not = false;
                fieldPredicate.Operator = SQLOperatorSlim.EQ;
                fieldPredicate.PropertyName = key.Name;
                fieldPredicate.Value = value;
                predicates.Add(fieldPredicate);
            }

            return predicates.Count == 1
                ? predicates[0]
                : new SQLPredicateGroup {Operator = SQLGroupOperator.AND, Predicates = predicates};
        }

        /// <summary>
        /// Gets key predicate
        /// </summary>
        /// <param name="classMap"></param>
        /// <param name="entity"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        protected ISQLPredicate GetKeyPredicate<T>(IClassMap classMap, T entity) where T : class
        {
            var whereFields = classMap.PropertyMaps.Where(p => p.KeyType != KeyType.NotAKey);

            if (!whereFields.Any())
                throw new ArgumentException("At least noe key column must be defined.");

            IList<ISQLPredicate> predicates =
                (from field in whereFields
                 select new SQLFieldPredicate<T>
                 {
                     Not = false,
                     Operator = SQLOperatorSlim.EQ,
                     PropertyName = field.Name,
                     Value = field.PropertyInfo.GetReflector().GetValue(entity)
                 })
               .Cast<ISQLPredicate>().ToList();

            return predicates.Count == 1
                ? predicates[0]
                : new SQLPredicateGroup {Operator = SQLGroupOperator.AND, Predicates = predicates};
        }

        /// <summary>
        /// Gets key predicate
        /// </summary>
        /// <param name="classMap"></param>
        /// <param name="entities"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        protected ISQLPredicate GetKeyPredicate<T>(IClassMap classMap, IEnumerable<T> entities) where T : class
        {
            var whereFields = classMap.PropertyMaps.Where(p => p.KeyType != KeyType.NotAKey);

            if (!whereFields.Any())
                throw new ArgumentException("At least noe key column must be defined.");

            var predicates = new List<ISQLPredicate>();

            foreach (var entity in entities)
            {
                var currentPredicates = (from field in whereFields
                                         select new SQLFieldPredicate<T>
                                         {
                                             Not = false,
                                             Operator = SQLOperatorSlim.EQ,
                                             PropertyName = field.Name,
                                             Value = field.PropertyInfo.GetReflector().GetValue(entity)
                                         }).Cast<ISQLPredicate>().ToList();

                if (currentPredicates.Count == 1)
                    predicates.Add(currentPredicates[0]);
                else if (currentPredicates.Count > 0)
                    predicates.AddRange(currentPredicates);
            }

            return new SQLPredicateGroup {Operator = SQLGroupOperator.AND, Predicates = predicates};
        }

        /// <summary>
        /// Gets entity predicate
        /// </summary>
        /// <param name="classMap"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected ISQLPredicate GetEntityPredicate(IClassMap classMap, object entity)
        {
            var predicateType = typeof(SQLFieldPredicate<>).MakeGenericType(classMap.EntityType);
            IList<ISQLPredicate> predicates = new List<ISQLPredicate>();

            foreach (var pair in ReflectionHelper.GetObjectValues(entity))
            {
                var fieldPredicate = Types.CreateInstance<ISQLFieldPredicate>(predicateType);
                fieldPredicate.Not = false;
                fieldPredicate.Operator = SQLOperatorSlim.EQ;
                fieldPredicate.PropertyName = pair.Key;
                fieldPredicate.Value = pair.Value;
                predicates.Add(fieldPredicate);
            }

            return predicates.Count == 1
                ? predicates[0]
                : new SQLPredicateGroup {Operator = SQLGroupOperator.AND, Predicates = predicates};
        }

        /// <summary>
        /// Convert to predicate
        /// </summary>
        /// <param name="expression"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected ISQLPredicate ConvertToPredicate<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return DynamicQuery.DynamicExpressionResolver.ResolveExprTree(expression);
        }
    }
}