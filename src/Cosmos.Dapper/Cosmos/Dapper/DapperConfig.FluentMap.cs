using System;
using Cosmos.Dapper.Conventions;
using Cosmos.Dapper.FluentMap;
using Cosmos.Dapper.Mapper;

namespace Cosmos.Dapper
{
    /// <summary>
    /// Fluent map configuretion
    /// </summary>
    public class FluentMapConfiguration : IFluentDapperMappingConfig
    {
        private readonly IDapperMappingConfig _mappingConfig;
        private readonly IInternalDapperMappingConfig _internalMappingConfig;

        /// <summary>
        /// Create a new instance of <see cref="FluentMapConfiguration" />
        /// </summary>
        /// <param name="mappingConfig"></param>
        public FluentMapConfiguration(IDapperMappingConfig mappingConfig)
        {
            _mappingConfig = mappingConfig;
            _internalMappingConfig = mappingConfig as IInternalDapperMappingConfig;
        }

        /// <summary>
        /// Add map
        /// </summary>
        /// <param name="classMap"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public IFluentDapperMappingConfig AddMap<TEntity>(ClassMap<TEntity> classMap) where TEntity : class
        {
            _internalMappingConfig.SetMap(classMap);
            return this;
        }

        /// <summary>
        /// Add convention
        /// </summary>
        /// <param name="configureConvention"></param>
        /// <typeparam name="TConvention"></typeparam>
        /// <returns></returns>
        public IFluentDapperMappingConfig AddConvention<TConvention>(Action<FluentConventionConfiguration> configureConvention)
            where TConvention : ConventionBase, new()
        {
            var conventionConfig = new FluentConventionConfiguration(new TConvention(), _mappingConfig);
            configureConvention?.Invoke(conventionConfig);
            return this;
        }

        /// <summary>
        /// Configure operations
        /// </summary>
        /// <param name="configure"></param>
        /// <returns></returns>
        public IFluentDapperMappingConfig ConfigureOptions(Action<DapperOptions> configure)
        {
            configure?.Invoke(_mappingConfig.Options);
            return this;
        }
    }
}