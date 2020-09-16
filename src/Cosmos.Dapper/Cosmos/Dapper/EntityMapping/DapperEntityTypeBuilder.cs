using System;
using System.Linq.Expressions;
using Cosmos.Dapper.Core.Helpers;
using Cosmos.Dapper.Core.Mapping;
using Cosmos.Dapper.Mapper;
using Cosmos.Models;

namespace Cosmos.Dapper.EntityMapping
{
    /// <summary>
    /// Dapper entity type builder
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class DapperEntityTypeBuilder<TEntity> where TEntity : class, IEntity
    {
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly DapperConfig _mappingConfig;

        /// <summary>
        /// Create a new instance of <see cref="DapperEntityTypeBuilder{TEntity}" />
        /// </summary>
        /// <param name="config"></param>
        public DapperEntityTypeBuilder(DapperConfig config)
        {
            _mappingConfig = config ?? throw new ArgumentNullException(nameof(config));
            ClassMapper = _mappingConfig.GetInternalMap<TEntity>();
        }

        /// <summary>
        /// Class mapper
        /// </summary>
        private IInternalClassMapper<TEntity> ClassMapper { get; }

        /// <summary>
        /// Tp schema
        /// </summary>
        /// <param name="schemaName"></param>
        /// <returns></returns>
        public DapperEntityTypeBuilder<TEntity> ToSchema(string schemaName)
        {
            ClassMapper.InternalSchema(schemaName);
            return this;
        }

        /// <summary>
        /// To table name
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DapperEntityTypeBuilder<TEntity> ToTable(string tableName)
        {
            ClassMapper.InternalTable(tableName);
            return this;
        }

        /// <summary>
        /// For property
        /// </summary>
        /// <param name="expression"></param>
        /// <typeparam name="TProperty"></typeparam>
        /// <returns></returns>
        public PropertyMap ForProperty<TProperty>(Expression<Func<TEntity, TProperty>> expression)
        {
            var property = LambdaHelper.GetProperty(expression);
            return ClassMapper.InternalGetPropertyMap(property);
        }
    }
}