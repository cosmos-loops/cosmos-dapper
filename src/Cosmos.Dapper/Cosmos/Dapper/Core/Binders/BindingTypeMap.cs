using System;
using System.Linq;
using Cosmos.Dapper.Mapper;
using Dapper;

namespace Cosmos.Dapper.Core.Binders
{
    /// <summary>
    /// Binding type map
    /// </summary>
    public class BindingTypeMap : MultiTypeMap
    {
        /// <summary>
        /// Create a new instance of <see cref="BindingTypeMap" />
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="classMap"></param>
        public BindingTypeMap(Type entityType, IClassMap classMap)
            : base(
                new CustomPropertyTypeMap(
                    entityType,
                    (type, columnName) => classMap.PropertyMaps.FirstOrDefault(x => x.ColumnName == columnName)?.PropertyInfo),
                new DefaultTypeMap(entityType)) { }
    }
}