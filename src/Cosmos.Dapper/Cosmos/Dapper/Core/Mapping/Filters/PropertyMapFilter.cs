using System;
using System.Reflection;
using Cosmos.Dapper.EntityMapping;

namespace Cosmos.Dapper.Core.Mapping.Filters
{
    internal static class PropertyMapFilter
    {
        public static bool Filter(Type typeOfClass, PropertyInfo propertyInfo)
        {
            return !propertyInfo.IsDefined(typeof(IgnoreMapAttribute), true);
        }
    }
}