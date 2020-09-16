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
    /// Interface for property map
    /// </summary>
    public interface IPropertyMap
    {
        /// <summary>
        /// Name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Column name
        /// </summary>
        string ColumnName { get; }

        /// <summary>
        /// Ignore
        /// </summary>
        bool Ignored { get; }

        /// <summary>
        /// Is readonly
        /// </summary>
        bool IsReadOnly { get; }

        /// <summary>
        /// Is case sensitive
        /// </summary>
        bool IsCaseSensitive { get; }

        /// <summary>
        /// Key type
        /// </summary>
        KeyType KeyType { get; }

        /// <summary>
        /// Property info
        /// </summary>
        PropertyInfo PropertyInfo { get; }
    }
}