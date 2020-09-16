using System.Reflection;
using Cosmos.Dapper.Mapper;

namespace Cosmos.Dapper.Core.Mapping
{
    internal interface IInternalClassMapper
    {
        PropertyMap InternalGetPropertyMap(PropertyInfo propertyInfo);

        void InternalSchema(string schemaName, bool caseSensitive = true);

        void InternalTable(string tableName, bool caseSensitive = true);
    }
}