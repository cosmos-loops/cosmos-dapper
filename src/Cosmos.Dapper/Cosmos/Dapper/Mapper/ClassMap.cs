/*
 * Copyright 2011 Thad Smith, Page Brooks and contributors
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Cosmos.Dapper.Core.Helpers;
using Cosmos.Dapper.Core.Mapping;

/*
 * Reference to:
 *      tmsmith/Dapper-Extensions
 *      Author: Thad Smith
 *      Url: https://github.com/tmsmith/Dapper-Extensions
 *      Apache License 2.0
 *          http://www.apache.org/licenses/LICENSE-2.0
 *
 * Changed and updated by Alex Lewis
 */

namespace Cosmos.Dapper.Mapper
{
    /// <summary>
    /// Class map
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ClassMap<T> : IClassMap<T>, IInternalClassMapper<T> where T : class
    {
        /// <summary>
        /// Create a new instance of <see cref="ClassMap{T}" />
        /// </summary>
        public ClassMap()
        {
            PropertyMaps = new List<IPropertyMap>();

            var schemaInfo = ClassMapperHelper.GetSchemaInfo(this);
            if (!string.IsNullOrWhiteSpace(schemaInfo.SchemaName))
                SchemaName = schemaInfo.SchemaName;

            var tableInfo = ClassMapperHelper.GetTableInfo(this);
            TableName = string.IsNullOrWhiteSpace(tableInfo.TableName)
                ? EntityType.Name
                : tableInfo.TableName;

            SchemaNameCaseSensitive = schemaInfo.CaseSensitive;
            TableNameCaseSensitive = tableInfo.CaseSensitive;
        }

        /// <summary>
        /// Schema name
        /// </summary>
        public string SchemaName { get; protected set; }

        /// <summary>
        /// Schema name case sensitive
        /// </summary>
        public bool SchemaNameCaseSensitive { get; protected set; } = true;

        /// <summary>
        /// Table name
        /// </summary>
        public string TableName { get; protected set; }

        /// <summary>
        /// Table name case sensitive
        /// </summary>
        public bool TableNameCaseSensitive { get; protected set; } = true;

        /// <summary>
        /// Property maps
        /// </summary>
        public IList<IPropertyMap> PropertyMaps { get; protected set; }

        /// <summary>
        /// Gets entity type
        /// </summary>
        public Type EntityType => typeof(T);

        /// <summary>
        /// Property type key type mapping
        /// </summary>
        protected Dictionary<Type, KeyType> PropertyTypeKeyTypeMapping { get; } = ClassMapperHelper.GetKeyTypeMapping();

        /// <summary>
        /// Sets schema
        /// </summary>
        /// <param name="schemaName"></param>
        /// <param name="caseSensitive"></param>
        public virtual void Schema(string schemaName, bool caseSensitive = true)
        {
            SchemaName = schemaName;
            SchemaNameCaseSensitive = caseSensitive;
        }

        /// <summary>
        /// Sets table name
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="caseSensitive"></param>
        public virtual void Table(string tableName, bool caseSensitive = true)
        {
            TableName = tableName;
            TableNameCaseSensitive = caseSensitive;
        }

        #region Table-level mapping

        /// <summary>
        /// Auto map
        /// </summary>
        protected virtual void AutoMap() => AutoMap(null);

        /// <summary>
        /// Auto map
        /// </summary>
        /// <param name="canMap"></param>
        protected virtual void AutoMap(Func<Type, PropertyInfo, bool> canMap)
        {
            var type = typeof(T);
            var hasDefinedKey = PropertyMaps.Any(x => x.KeyType != KeyType.NotAKey);
            PropertyMap keyMap = null;

            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (PropertyMaps.Any(x => x.Name.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase)))
                    continue;

                if (canMap != null && !canMap(type, property))
                    continue;

                var map = GetPropertyMap(property);

                if (!hasDefinedKey)
                {
                    if (string.Equals(map.PropertyInfo.Name, "id", StringComparison.InvariantCultureIgnoreCase))
                        keyMap = map;

                    if (keyMap == null && map.PropertyInfo.Name.EndsWith("id", true, CultureInfo.InvariantCulture))
                        keyMap = map;
                }
            }

            keyMap?.PrimaryKey(PropertyTypeKeyTypeMapping.ContainsKey(keyMap.PropertyInfo.PropertyType)
                ? PropertyTypeKeyTypeMapping[keyMap.PropertyInfo.PropertyType]
                : KeyType.Assigned);
        }

        #endregion

        #region Property-level mapping

        /// <summary>
        /// Gets property map
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        protected PropertyMap GetPropertyMap(Expression<Func<T, object>> expression)
        {
            var property = ReflectionHelper.GetProperty(expression) as PropertyInfo;
            return GetPropertyMap(property);
        }

        /// <summary>
        /// Gets property map
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        protected PropertyMap GetPropertyMap(PropertyInfo property)
        {
            var ret = new PropertyMap(property);
            GuardForDuplicatePropertyMap(ret);
            PropertyMaps.Add(ret);
            return ret;
        }

        /// <summary>
        /// Remove property map
        /// </summary>
        /// <param name="expression"></param>
        /// <exception cref="ApplicationException"></exception>
        protected void RemovePropertyMap(Expression<Func<T, object>> expression)
        {
            var property = ReflectionHelper.GetProperty(expression) as PropertyInfo;
            var mapping = PropertyMaps.SingleOrDefault(x => x.Name == property?.Name);

            if (mapping == null)
                throw new ApplicationException("Unable to UnMap because mapping does not exist.");

            PropertyMaps.Remove(mapping);
        }

        #endregion

        #region Private Guard

        private void GuardForDuplicatePropertyMap(PropertyMap map)
        {
            if (PropertyMaps.Any(x => x.Name.Equals(map.Name)))
                throw new ArgumentException($"Duplicate mapping detected. Property '{map.Name}' is already to column '{map.ColumnName}'.");
        }

        #endregion

        #region Actions before and after save

        /// <summary>
        /// Before save active
        /// </summary>
        public Action<T> BeforeSaveAction { get; set; }

        /// <summary>
        /// After save action
        /// </summary>
        public Action<T> AfterSaveAction { get; set; }

        /// <summary>
        /// Before save
        /// </summary>
        /// <param name="entity"></param>
        public void BeforeSave(T entity) => BeforeSaveAction?.Invoke(entity);

        /// <summary>
        /// After save
        /// </summary>
        /// <param name="entity"></param>
        public void AfterSave(T entity) => AfterSaveAction?.Invoke(entity);

        #endregion

        #region Internal methods

        /// <summary>
        /// Internal get property map
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public PropertyMap InternalGetPropertyMap(PropertyInfo propertyInfo) => GetPropertyMap(propertyInfo);

        /// <summary>
        /// Internal schema
        /// </summary>
        /// <param name="schemaName"></param>
        /// <param name="caseSensitive"></param>
        public void InternalSchema(string schemaName, bool caseSensitive = true) => Schema(schemaName, caseSensitive);

        /// <summary>
        /// Internal table
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="caseSensitive"></param>
        public void InternalTable(string tableName, bool caseSensitive = true) => Table(tableName, caseSensitive);

        #endregion

    }
}