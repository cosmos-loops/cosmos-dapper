/*
 * Copyright 2018 Arjen Post
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

/*
 * Reference to:
 *     dotarj/Dapper.Mapper
 *     Author: Arjen Post
 *     URL: https://github.com/dotarj/Dapper.Mapper
 *     Apache License 2.0
 */

namespace Cosmos.Dapper.Core.Mapping.Cache
{
    internal static class MappingCache
    {
        internal static Expression GetSetExpression(ParameterExpression sourceExpression, params ParameterExpression[] destinationExpressions)
        {
            var destination = destinationExpressions
               .Select(parameter => new
                {
                    Parameter = parameter,
                    Property = parameter.Type
                       .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                       .FirstOrDefault(property => IsWritable(property) && IsOfType(property, sourceExpression.Type))
                })
               .FirstOrDefault(parameter => parameter.Property != null);

            if (destination is null)
                throw new InvalidOperationException($"No writable property of type '{sourceExpression.Type}' found in type {destinationExpressions.Select(p => p.Type.FullName)}.");

            return Expression.IfThen(
                Expression.Not(Expression.Equal(destination.Parameter, Expression.Constant(null))),
                Expression.Call(destination.Parameter, destination.Property.GetSetMethod(), sourceExpression));
        }

        private static bool IsWritable(PropertyInfo propertyInfo)
        {
            return propertyInfo.CanWrite && !propertyInfo.GetIndexParameters().Any();
        }

        private static bool IsOfType(PropertyInfo propertyInfo, Type type)
        {
            return propertyInfo.PropertyType == type
                || IsSubclassOf(type, propertyInfo.PropertyType)
                || propertyInfo.PropertyType.IsAssignableFrom(type);
        }

        private static bool IsSubclassOf(Type type, Type otherType)
        {
            return type.GetTypeInfo().IsSubclassOf(otherType);
        }
    }
}