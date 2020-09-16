using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AspectCore.Extensions.Reflection;
using Cosmos.Dapper.Mapper;
using Dapper;

namespace Cosmos.Dapper.Core
{
    public partial class DapperImplementor
    {
        #region Insert for single entity

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public dynamic Insert<T>(IDbConnection connection, T entity, IDbTransaction transaction) where T : class
        {
            var classMap = GetClassMap<T>();
            ClassMapperHelper.CallForEntity(entity, classMap.BeforeSave);

            var nonIdentityKeyProperties = classMap.PropertyMaps.Where(p => p.KeyType == KeyType.Guid || p.KeyType == KeyType.Assigned).ToList();
            var identityColMap = classMap.PropertyMaps.SingleOrDefault(p => p.KeyType == KeyType.Identity);
            var triggerIdentityColMap = classMap.PropertyMaps.SingleOrDefault(p => p.KeyType == KeyType.TriggerIdentity);

            UpdateGuidColumnValuePreInsert(entity, nonIdentityKeyProperties);

            IDictionary<string, object> keyValues = new ExpandoObject();
            var sql = SQLGenerator.Insert(classMap);
            var flag = false;

            if (identityColMap != null)
            {
                if (SQLGenerator.SupportsMultipleStatements())
                {
                    sql.AppendSql(SQLGenerator.Configuration.Dialect.BatchSeperator);
                    sql.AppendSql(SQLGenerator.IdentitySql(classMap));
                }
                else
                {
                    connection.Execute(sql.Sql, entity, transaction, Options.Timeout, CommandType.Text);
                    sql.ResetSql(SQLGenerator.IdentitySql(classMap));
                }

                var cmd = sql.ToSQLCommand(entity, transaction, Options.Timeout, commandFlags: CommandFlags.None);
                var result = connection.Query<long>(cmd);

                var identityInt = Convert.ToInt32(result.FirstOrDefault());
                SetExpandoObjectPostInserted(keyValues, identityColMap, identityInt);
                SetPropertyMapPostInserted(entity, identityColMap, identityInt);
                flag = identityInt > 0;
            }
            else if (triggerIdentityColMap != null)
            {
                var dynamicParameters = GetInsertParameters(entity, triggerIdentityColMap);

                var cmd = sql.ToSQLCommand(dynamicParameters, transaction, Options.Timeout);

                flag = connection.Execute(cmd) > 0;

                var value = GetTriggerIdentityValuePostInserted(dynamicParameters);
                SetExpandoObjectPostInserted(keyValues, triggerIdentityColMap, value);
                SetPropertyMapPostInserted(entity, triggerIdentityColMap, value);
            }
            else
            {
                var cmd = sql.ToSQLCommand(entity, transaction, Options.Timeout);
                flag = connection.Execute(cmd) > 0;
            }

            SetExpandoObjectPostInserted(entity, keyValues, nonIdentityKeyProperties);

            ClassMapperHelper.CallForEntity(entity, classMap.AfterSave, flag);

            return keyValues.Count == 1
                ? keyValues.First().Value
                : keyValues;
        }

        /// <summary>
        /// Insert async
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<dynamic> InsertAsync<T>(IDbConnection connection, T entity,
            IDbTransaction transaction = null, CancellationToken cancellationToken = default) where T : class
        {
            var classMap = GetClassMap<T>();
            ClassMapperHelper.CallForEntity(entity, classMap.BeforeSave);

            var nonIdentityKeyProperties = classMap.PropertyMaps.Where(p => p.KeyType == KeyType.Guid || p.KeyType == KeyType.Assigned).ToList();
            var identityColMap = classMap.PropertyMaps.SingleOrDefault(p => p.KeyType == KeyType.Identity);
            var triggerIdentityColMap = classMap.PropertyMaps.SingleOrDefault(p => p.KeyType == KeyType.TriggerIdentity);

            UpdateGuidColumnValuePreInsert(entity, nonIdentityKeyProperties);

            IDictionary<string, object> keyValues = new ExpandoObject();
            var sql = SQLGenerator.Insert(classMap);
            var flag = false;

            if (identityColMap != null)
            {
                if (SQLGenerator.SupportsMultipleStatements())
                {
                    sql.AppendSql(SQLGenerator.Configuration.Dialect.BatchSeperator);
                    sql.AppendSql(SQLGenerator.IdentitySql(classMap));
                }
                else
                {
                    connection.Execute(sql.Sql, entity, transaction, Options.Timeout, CommandType.Text);
                    sql.ResetSql(SQLGenerator.IdentitySql(classMap));
                }

                var cmd = sql.ToSQLCommand(entity, transaction, Options.Timeout, CommandType.Text, CommandFlags.None, cancellationToken);
                var result = await connection.QueryAsync<long>(cmd).ConfigureAwait(false);

                var identityInt = Convert.ToInt32(result.FirstOrDefault());
                SetExpandoObjectPostInserted(keyValues, identityColMap, identityInt);
                SetPropertyMapPostInserted(entity, identityColMap, identityInt);
                flag = identityInt > 0;
            }
            else if (triggerIdentityColMap != null)
            {
                var parameters = GetInsertParameters(entity, triggerIdentityColMap);
                var cmd = sql.ToSQLCommand(parameters, transaction, Options.Timeout, cancellationToken: cancellationToken);

                flag = await connection.ExecuteAsync(cmd).ConfigureAwait(false) > 0;

                var value = GetTriggerIdentityValuePostInserted(parameters);
                SetExpandoObjectPostInserted(keyValues, triggerIdentityColMap, value);
                SetPropertyMapPostInserted(entity, triggerIdentityColMap, value);
            }
            else
            {
                var cmd = sql.ToSQLCommand(entity, transaction, Options.Timeout, cancellationToken: cancellationToken);
                flag = await connection.ExecuteAsync(cmd).ConfigureAwait(false) > 0;
            }

            SetExpandoObjectPostInserted(entity, keyValues, nonIdentityKeyProperties);

            ClassMapperHelper.CallForEntity(entity, classMap.AfterSave, flag);

            return keyValues.Count == 1
                ? keyValues.First().Value
                : keyValues;
        }

        #endregion

        #region Insert for multi entities

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="entities"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        public void Insert<T>(IDbConnection connection, IEnumerable<T> entities, IDbTransaction transaction) where T : class
        {
            var classMap = GetClassMap<T>();
            ClassMapperHelper.CallForEntity(entities, classMap.BeforeSave);

            var notKeyProperties = classMap.PropertyMaps.Where(p => p.KeyType != KeyType.NotAKey);
            var triggerIdentityColMap = classMap.PropertyMaps.SingleOrDefault(p => p.KeyType == KeyType.TriggerIdentity);

            var properties = triggerIdentityColMap == null
                ? null
                : GetInsertPropertyInfos(typeof(T), triggerIdentityColMap);

            var parameters = new List<DynamicParameters>();
            foreach (var entity in entities)
            {
                UpdateGuidColumnValuePreInsert(entity, notKeyProperties);

                if (triggerIdentityColMap == null)
                    continue;

                parameters.Add(GetInsertParameters(entity, triggerIdentityColMap, properties));
            }

            var cmd = triggerIdentityColMap == null
                ? SQLGenerator.Insert(classMap).ToSQLCommand(entities, transaction, Options.Timeout)
                : SQLGenerator.Insert(classMap).ToSQLCommand(parameters, transaction, Options.Timeout);
            var flag = connection.Execute(cmd) > 0;

            ClassMapperHelper.CallForEntity(entities, classMap.AfterSave, flag);
        }

        /// <summary>
        /// Insert async
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="entities"></param>
        /// <param name="transaction"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task InsertAsync<T>(IDbConnection connection, IEnumerable<T> entities,
            IDbTransaction transaction = null, CancellationToken cancellationToken = default) where T : class
        {
            var classMap = GetClassMap<T>();
            ClassMapperHelper.CallForEntity(entities, classMap.BeforeSave);

            var notKeyProperties = classMap.PropertyMaps.Where(p => p.KeyType != KeyType.NotAKey);
            var triggerIdentityColMap = classMap.PropertyMaps.SingleOrDefault(p => p.KeyType == KeyType.TriggerIdentity);

            var properties = triggerIdentityColMap == null
                ? null
                : GetInsertPropertyInfos(typeof(T), triggerIdentityColMap);

            var parameters = new List<DynamicParameters>();
            foreach (var entity in entities)
            {
                UpdateGuidColumnValuePreInsert(entity, notKeyProperties);

                if (triggerIdentityColMap == null)
                    continue;

                parameters.Add(GetInsertParameters(entity, triggerIdentityColMap, properties));
            }

            var cmd = triggerIdentityColMap == null
                ? SQLGenerator.Insert(classMap).ToSQLCommand(entities, transaction, Options.Timeout, cancellationToken: cancellationToken)
                : SQLGenerator.Insert(classMap).ToSQLCommand(transaction, Options.Timeout, cancellationToken: cancellationToken);
            var flag = await connection.ExecuteAsync(cmd).ConfigureAwait(false) > 0;

            ClassMapperHelper.CallForEntity(entities, classMap.AfterSave, flag);
        }

        #endregion

        #region internal helpers

        private DynamicParameters GetInsertParameters<T>(T entity, IPropertyMap triggerIdentityColumn, IEnumerable<PropertyInfo> properties = null) where T : class
        {
            if (triggerIdentityColumn == null)
                throw new ArgumentNullException(nameof(triggerIdentityColumn));

            var entityType = typeof(T);
            var parameters = new DynamicParameters();
            var triggerIdentityColumnName = triggerIdentityColumn.PropertyInfo.Name;

            foreach (var property in properties ?? GetInsertPropertyInfos(entityType, triggerIdentityColumn))
            {
                var reflector = property.GetReflector();
                parameters.Add(reflector.Name, reflector.GetValue(entity));
            }

            var idOutParamValue = entityType.GetProperty(triggerIdentityColumnName).GetReflector()?.GetValue(entity);
            parameters.Add("IdOutParam", direction: ParameterDirection.Output, value: idOutParamValue);

            return parameters;
        }

        private IEnumerable<PropertyInfo> GetInsertPropertyInfos(Type type, IPropertyMap propertyMap)
        {
            return type
               .GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public)
               .Where(p => p.Name != propertyMap.PropertyInfo.Name);
        }

        private void UpdateGuidColumnValuePreInsert<T>(T entity, IEnumerable<IPropertyMap> propertyMaps)
        {
            foreach (var map in propertyMaps)
            {
                if (map.KeyType != KeyType.Guid)
                    continue;

                var reflector = map.PropertyInfo.GetReflector();

                if (reflector.GetValue(entity) is Guid g && g == Guid.Empty)
                {
                    var comb = SQLGenerator.Configuration.GetNextGuid();
                    reflector.SetValue(entity, comb);
                }
            }
        }

        private object GetTriggerIdentityValuePostInserted(DynamicParameters parameters)
        {
            return parameters.Get<object>($"{SQLGenerator.Configuration.Dialect.ParameterPrefix}IdOutParam");
        }

        private void SetExpandoObjectPostInserted(IDictionary<string, object> keyValues, IPropertyMap propertyMap, object value)
        {
            keyValues.Add(propertyMap.Name, value);
        }

        private void SetExpandoObjectPostInserted<T>(T entity, IDictionary<string, object> keyValues, IEnumerable<IPropertyMap> propertyMaps)
        {
            foreach (var propertyMap in propertyMaps)
            {
                var reflector = propertyMap.PropertyInfo.GetReflector();
                SetExpandoObjectPostInserted(keyValues, propertyMap, reflector.GetValue(entity));
            }
        }

        private void SetPropertyMapPostInserted<T>(T targetEntity, IPropertyMap propertyMap, object value)
        {
            propertyMap.PropertyInfo.GetReflector().SetValue(targetEntity, value);
        }

        #endregion
    }
}