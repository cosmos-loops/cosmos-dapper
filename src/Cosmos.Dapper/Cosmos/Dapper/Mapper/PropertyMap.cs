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
using System.Reflection;

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
    /// Property map
    /// </summary>
    public class PropertyMap : IPropertyMap
    {
        /// <summary>
        /// Create a new instance of <see cref="PropertyMap" />
        /// </summary>
        /// <param name="property"></param>
        public PropertyMap(PropertyInfo property)
        {
            PropertyInfo = property;
            ColumnName = property.Name;

            var (name, caseSensitive) = PropertyMapperHelper.GetColumnName(property);
            if (!string.IsNullOrWhiteSpace(name))
                ToColumn(name, caseSensitive);

            if (PropertyMapperHelper.IsReadOnly(property))
                ReadOnly();

            if (PropertyMapperHelper.IsIgnore(property))
                Ignore();

            var keyType = PropertyMapperHelper.GetPracticeKey(property);
            if (keyType != KeyType.NotAKey)
                PrimaryKey(keyType);

            if (PropertyMapperHelper.IsIgnoreConvention(property))
                IgnoreConvention();
        }

        /// <summary>
        /// Name of property
        /// </summary>
        public string Name => PropertyInfo.Name;

        /// <summary>
        /// Column name
        /// </summary>
        public string ColumnName { get; private set; }

        /// <summary>
        /// Ignored
        /// </summary>
        public bool Ignored { get; private set; }

        /// <summary>
        /// Is ignore convertion
        /// </summary>
        public bool IsIgnoreConvention { get; private set; }

        /// <summary>
        /// Is readonly
        /// </summary>
        public bool IsReadOnly { get; private set; }

        /// <summary>
        /// Is case sensitive
        /// </summary>
        public bool IsCaseSensitive { get; private set; }

        /// <summary>
        /// Key type
        /// </summary>
        public KeyType KeyType { get; private set; }

        /// <summary>
        /// Property info
        /// </summary>
        public PropertyInfo PropertyInfo { get; }

        /// <summary>
        /// To column
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="caseSensitive"></param>
        /// <returns></returns>
        public PropertyMap ToColumn(string columnName, bool caseSensitive = true)
        {
            ColumnName = columnName;
            IsCaseSensitive = caseSensitive;
            return this;
        }

        /// <summary>
        /// Primary key
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public PropertyMap PrimaryKey(KeyType type)
        {
            if (Ignored)
                throw new ArgumentException($"'{Name}' is ignored and cannot be made a key field.");

            if (IsReadOnly)
                throw new ArgumentException($"'{Name}' is readonly and cannot be made a key field.");

            KeyType = type;
            return this;
        }

        /// <summary>
        /// Ignore
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public PropertyMap Ignore()
        {
            if (KeyType != KeyType.NotAKey)
                throw new ArgumentException($"'{Name}' is a key field and cannot be ignored.");

            Ignored = true;
            return this;
        }

        /// <summary>
        /// Ignore convention
        /// </summary>
        /// <returns></returns>
        public PropertyMap IgnoreConvention()
        {
            IsIgnoreConvention = true;
            return this;
        }

        /// <summary>
        /// Readonly
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public PropertyMap ReadOnly()
        {
            if (KeyType != KeyType.NotAKey)
                throw new ArgumentException($"'{Name}' is a key field and cannot be ignored.");

            IsReadOnly = true;
            return this;
        }

        /// <summary>
        /// Case sensitive
        /// </summary>
        /// <returns></returns>
        public PropertyMap CaseSensitive()
        {
            IsCaseSensitive = true;
            return this;
        }

        /// <summary>
        /// Case insensitive
        /// </summary>
        /// <returns></returns>
        public PropertyMap CaseInsensitive()
        {
            IsCaseSensitive = false;
            return this;
        }
    }
}