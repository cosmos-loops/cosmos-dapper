using System.Reflection;
using Cosmos.Dapper.EntityMapping;

namespace Cosmos.Dapper.Mapper
{
    /// <summary>
    /// Property mapper helper
    /// </summary>
    internal static class PropertyMapperHelper
    {
        public static bool IsIgnore(PropertyInfo propertyInfo)
            => propertyInfo.IsDefined(typeof(IgnoreMapAttribute), true);

        public static bool IsReadOnly(PropertyInfo propertyInfo)
            => propertyInfo.IsDefined(typeof(ReadOnlyAttribute), true);

        public static KeyType GetPracticeKey(PropertyInfo propertyInfo)
            => propertyInfo.GetCustomAttribute<PrimaryKeyAttribute>()?.KeyType ?? KeyType.NotAKey;

        public static (string, bool) GetColumnName(PropertyInfo propertyInfo)
        {
            var attr = propertyInfo.GetCustomAttribute<ColumnAttribute>();
            return attr != null ? (attr.Name, attr.CaseSensitive) : (string.Empty, true);
        }

        public static bool IsIgnoreConvention(PropertyInfo propertyInfo)
            => propertyInfo.IsDefined(typeof(IgnoreConventionAttribute), true);
    }
}