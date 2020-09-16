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
using System.Linq.Expressions;

/*
 * Reference to:
 *     dotarj/Dapper.Mapper
 *     Author: Arjen Post
 *     URL: https://github.com/dotarj/Dapper.Mapper
 *     Apache License 2.0
 */

namespace Cosmos.Dapper.Core.Mapping.Cache
{
    internal static class MappingCache<T1, T2, T3, T4, T5, T6, T7>
    {
        static MappingCache()
        {
            var first = Expression.Parameter(typeof(T1), "first");
            var second = Expression.Parameter(typeof(T2), "second");
            var third = Expression.Parameter(typeof(T3), "third");
            var fourth = Expression.Parameter(typeof(T4), "fourth");
            var fifth = Expression.Parameter(typeof(T5), "fifth");
            var sixth = Expression.Parameter(typeof(T6), "sixth");
            var seventh = Expression.Parameter(typeof(T7), "seventh");

            var secondSetExpression = MappingCache.GetSetExpression(second, first);
            var thirdSetExpression = MappingCache.GetSetExpression(third, first, second);
            var fourthSetExpression = MappingCache.GetSetExpression(fourth, first, second, third);
            var fifthSetExpression = MappingCache.GetSetExpression(fifth, first, second, third, fourth);
            var sixthSetExpression = MappingCache.GetSetExpression(sixth, first, second, third, fourth, fifth);
            var seventhSetExpression = MappingCache.GetSetExpression(seventh, first, second, third, fourth, fifth, sixth);

            var blockExpression = Expression.Block(first, second, third, fourth, fifth, sixth, seventh,
                secondSetExpression, thirdSetExpression, fourthSetExpression, fifthSetExpression, sixthSetExpression, seventhSetExpression,
                first);

            Map = Expression.Lambda<Func<T1, T2, T3, T4, T5, T6, T7, T1>>(blockExpression, first, second, third, fourth, fifth, sixth, seventh).Compile();
        }

        internal static Func<T1, T2, T3, T4, T5, T6, T7, T1> Map { get; }
    }
}