using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cosmos.Dapper.Core.Helpers;
using Cosmos.Dapper.Mapper;
using Cosmos.Data.Statements;
using Dapper;

namespace Cosmos.Dapper.Core
{
    public partial class DapperImplementor
    {
        #region Update for single entity

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Update<T>(IDbConnection connection, T entity, IDbTransaction transaction,
            ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false)
            where T : class
        {
            var classMap = GetClassMap<T>();
            ClassMapperHelper.CallForEntity(entity, classMap.BeforeSave);

            var predicate = GetKeyPredicate(classMap, entity).Join(filters);
            var sql = GetUpdateSql(entity, classMap, predicate, ignoreAllKeyProperties);
            var cmd = sql.ToSQLCommand(transaction, Options.Timeout);
            var flag = connection.Execute(cmd) > 0;

            ClassMapperHelper.CallForEntity(entity, classMap.AfterSave, flag);

            return flag;
        }

        /// <summary>
        /// Update async
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<bool> UpdateAsync<T>(IDbConnection connection, T entity, IDbTransaction transaction,
            ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false, CancellationToken cancellationToken = default)
            where T : class
        {
            var classMap = GetClassMap<T>();
            ClassMapperHelper.CallForEntity(entity, classMap.BeforeSave);

            var predicate = GetKeyPredicate(classMap, entity).Join(filters);
            var sql = GetUpdateSql(entity, classMap, predicate, ignoreAllKeyProperties);
            var cmd = sql.ToSQLCommand(transaction, Options.Timeout, cancellationToken: cancellationToken);
            var flag = await connection.ExecuteAsync(cmd).ConfigureAwait(false) > 0;

            ClassMapperHelper.CallForEntity(entity, classMap.AfterSave, flag);

            return flag;
        }

        #endregion

        #region Update for multi entities

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="entities"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Update<T>(IDbConnection connection, IEnumerable<T> entities, IDbTransaction transaction,
            ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false)
            where T : class
        {
            var classMap = GetClassMap<T>();
            ClassMapperHelper.CallForEntity(entities, classMap.BeforeSave);

            var predicate = GetKeyPredicate(classMap, entities).Join(filters);
            var sql = GetUpdateSql(entities, classMap, predicate, ignoreAllKeyProperties, out var expectCount);
            var cmd = sql.ToSQLCommand(transaction, Options.Timeout);
            var flag = connection.Execute(cmd) == expectCount;

            ClassMapperHelper.CallForEntity(entities, classMap.AfterSave, flag);

            return flag;
        }

        /// <summary>
        /// Update async
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="entities"></param>
        /// <param name="transaction"></param>
        /// <param name="filters"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<bool> UpdateAsync<T>(IDbConnection connection, IEnumerable<T> entities, IDbTransaction transaction,
            ISQLPredicate[] filters = null, bool ignoreAllKeyProperties = false, CancellationToken cancellationToken = default)
            where T : class
        {
            var classMap = GetClassMap<T>();
            ClassMapperHelper.CallForEntity(entities, classMap.BeforeSave);

            var predicate = GetKeyPredicate(classMap, entities).Join(filters);
            var sql = GetUpdateSql(entities, classMap, predicate, ignoreAllKeyProperties, out var expectCount);
            var cmd = sql.ToSQLCommand(transaction, Options.Timeout, cancellationToken: cancellationToken);
            var flag = await connection.ExecuteAsync(cmd).ConfigureAwait(false) == expectCount;

            ClassMapperHelper.CallForEntity(entities, classMap.AfterSave, flag);

            return flag;
        }

        #endregion

        #region internal helpers

        private SQLConvertResult GetUpdateSql<T>(T entity, IClassMap classMap, ISQLPredicate predicate, bool ignoreAllKeyProperties)
        {
            var sql = SQLGenerator.Update(classMap, predicate, new Dictionary<string, object>(), ignoreAllKeyProperties);
            var columns = GetUpdateProperties(classMap, ignoreAllKeyProperties);
            foreach (var property in ReflectionHelper.GetObjectValues(entity, columns))
                sql.Parameters.Add(property.Key, property.Value);
            return sql;
        }

        private SQLConvertResult GetUpdateSql<T>(IEnumerable<T> entities, IClassMap classMap,
            ISQLPredicate predicate, bool ignoreAllKeyProperties, out int expectCount)
        {
            var sql = SQLGenerator.Update(classMap, predicate, new Dictionary<string, object>(), ignoreAllKeyProperties);
            var parameters = new List<DynamicParameters>();
            var counter = 0;

            var columns = GetUpdateProperties(classMap, ignoreAllKeyProperties).ToList();
            foreach (var entity in entities)
            {
                var currentParameters = ReflectionHelper.GetObjectValues(entity, columns)
                   .ToDictionary(property => property.Key, property => property.Value);
                parameters.Add(currentParameters.ToDynamicParameters());
                counter++;
            }

            expectCount = counter;
            return new OverrideSQLConvertResult(sql, parameters, true);
        }

        private IEnumerable<IPropertyMap> GetUpdateProperties(IClassMap classMap, bool ignoreAllKeyProperties)
        {
            return ignoreAllKeyProperties
                ? classMap.PropertyMaps.Where(p => !(p.Ignored || p.IsReadOnly) && p.KeyType == KeyType.NotAKey)
                : classMap.PropertyMaps.Where(p => !(p.Ignored || p.IsReadOnly || p.KeyType == KeyType.Identity || p.KeyType == KeyType.Assigned));
        }

        #endregion
    }
}