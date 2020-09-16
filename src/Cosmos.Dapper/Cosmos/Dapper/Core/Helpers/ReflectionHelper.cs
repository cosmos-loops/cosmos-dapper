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
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using AspectCore.Extensions.Reflection;
using Cosmos.Dapper.Mapper;

/*
 * Reference to:
 *      tmsmith/Dapper-Extensions
 *      Author: Thad Smith
 *      Url: https://github.com/tmsmith/Dapper-Extensions
 *      Apache License 2.0
 *          http://www.apache.org/licenses/LICENSE-2.0
 *
 * Updated and changed by Alex LEWIS
 */

namespace Cosmos.Dapper.Core.Helpers
{
    /// <summary>
    /// Reflection helper
    /// </summary>
    internal static class ReflectionHelper
    {
        private static List<Type> _simpleTypes
            = new List<Type>
            {
                typeof(byte),
                typeof(sbyte),
                typeof(short),
                typeof(ushort),
                typeof(int),
                typeof(uint),
                typeof(long),
                typeof(ulong),
                typeof(float),
                typeof(double),
                typeof(decimal),
                typeof(bool),
                typeof(string),
                typeof(char),
                typeof(Guid),
                typeof(DateTime),
                typeof(DateTimeOffset),
                typeof(byte[])
            };

        public static MemberInfo GetProperty(LambdaExpression lambda)
        {
            Expression expr = lambda;
            for (;;)
            {
                switch (expr.NodeType)
                {
                    case ExpressionType.Lambda:
                        expr = ((LambdaExpression) expr).Body;
                        break;
                    case ExpressionType.Convert:
                        expr = ((UnaryExpression) expr).Operand;
                        break;
                    case ExpressionType.MemberAccess:
                        var memberExpression = (MemberExpression) expr;
                        var mi = memberExpression.Member;
                        return mi;
                    default:
                        return null;
                }
            }
        }

        public static IDictionary<string, object> GetObjectValues(object instance, IEnumerable<IPropertyMap> limitedPropertyMaps = null)
        {
            IDictionary<string, object> result = new Dictionary<string, object>();

            if (instance == null)
                return result;

            foreach (var property in instance.GetType().GetTypeInfo().GetProperties())
            {
                if (limitedPropertyMaps != null && !limitedPropertyMaps.Any(p => p.Name == property.Name))
                    continue;

                var reflector = property.GetReflector();
                result[reflector.Name] = reflector.GetValue(instance);
            }

            return result;
        }

        public static string AppendStrings(this IEnumerable<string> list, string seperator = ", ")
        {
            if (list == null)
                return string.Empty;
            
            return list.Aggregate(
                new StringBuilder(),
                (sb, s) => (sb.Length == 0 ? sb : sb.Append(seperator)).Append(s),
                sb => sb.ToString());
        }

        public static bool IsSimpleType(Type type)
        {
            var actualType = type;

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                actualType = type.GetGenericArguments()[0];

            return _simpleTypes.Contains(actualType);
        }

        public static string GetParameterName(this IDictionary<string, object> parameters, string parameterName, char parameterPrefix)
        {
            return $"{parameterPrefix}{parameterName}_{parameters.Count}";
        }

        public static string SetParameterName(this IDictionary<string, object> parameters, string parameterName, object value, char parameterPrefix)
        {
            var name = parameters.GetParameterName(parameterName, parameterPrefix);
            parameters.Add(name, value);
            return name;
        }
    }
}