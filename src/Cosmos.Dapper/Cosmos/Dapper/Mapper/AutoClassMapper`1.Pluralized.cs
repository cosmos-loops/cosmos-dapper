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

using System.Collections.Generic;
using System.Text.RegularExpressions;

/*
 * Reference to:
 *      tmsmith/Dapper-Extensions
 *      Author: Thad Smith
 *      Url: https://github.com/tmsmith/Dapper-Extensions
 *      Apache License 2.0
 *          http://www.apache.org/licenses/LICENSE-2.0
 */

//TODO 本模块将基于 Cosmos.I18N 进行重写

namespace Cosmos.Dapper.Mapper
{
    /// <summary>
    /// Pluralized auto class map
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PluralizedAutoClassMap<T> : AutoClassMap<T> where T : class
    {
        /// <summary>
        /// Sets table name
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="caseSensitive"></param>
        public override void Table(string tableName, bool caseSensitive = true)
        {
            base.Table(Formatting.Pluralize(tableName), caseSensitive);
        }

        /// <summary>
        /// Formatting
        /// </summary>
        public static class Formatting
        {
            // ReSharper disable once StaticMemberInGenericType
            private static readonly IList<string> Unpluralizables
                = new List<string>
                {
                    "equipment",
                    "information",
                    "rice",
                    "money",
                    "species",
                    "series",
                    "fish",
                    "sheep",
                    "deer"
                };

            // ReSharper disable once StaticMemberInGenericType
            private static readonly IDictionary<string, string> Pluralizations
                = new Dictionary<string, string>
                {
                    // Start with the rarest cases, and move to the most common
                    {"person", "people"},
                    {"ox", "oxen"},
                    {"child", "children"},
                    {"foot", "feet"},
                    {"tooth", "teeth"},
                    {"goose", "geese"},
                    // And now the more standard rules.
                    {"(.*)fe?$", "$1ves"}, // ie, wolf, wife
                    {"(.*)man$", "$1men"},
                    {"(.+[aeiou]y)$", "$1s"},
                    {"(.+[^aeiou])y$", "$1ies"},
                    {"(.+z)$", "$1zes"},
                    {"([m|l])ouse$", "$1ice"},
                    {"(.+)(e|i)x$", @"$1ices"}, // ie, Matrix, Index
                    {"(octop|vir)us$", "$1i"},
                    {"(.+(s|x|sh|ch))$", @"$1es"},
                    {"(.+)", @"$1s"}
                };

            /// <summary>
            /// Pluralize
            /// </summary>
            /// <param name="singular"></param>
            /// <returns></returns>
            public static string Pluralize(string singular)
            {
                if (Unpluralizables.Contains(singular))
                    return singular;

                var plural = string.Empty;

                foreach (var pluralization in Pluralizations)
                {
                    if (Regex.IsMatch(singular, pluralization.Key))
                    {
                        plural = Regex.Replace(singular, pluralization.Key, pluralization.Value);
                        break;
                    }
                }

                return plural;
            }
        }
    }
}