using System;
using Cosmos.Dapper.Conventions;
using Cosmos.Dapper.FluentMap;
using Cosmos.Dapper.Mapper;

namespace Cosmos.Dapper
{
    /// <summary>
    /// Interface for fluent dapper mapping config
    /// </summary>
    public interface IFluentDapperMappingConfig
    {
        /// <summary>
        /// Add map
        /// </summary>
        /// <param name="classMap"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IFluentDapperMappingConfig AddMap<TEntity>(ClassMap<TEntity> classMap) where TEntity : class;

        /// <summary>
        /// Add convention
        /// </summary>
        /// <param name="configureConvention"></param>
        /// <typeparam name="TConvention"></typeparam>
        /// <returns></returns>
        IFluentDapperMappingConfig AddConvention<TConvention>(Action<FluentConventionConfiguration> configureConvention)
            where TConvention : ConventionBase, new();

        /// <summary>
        /// Configure options
        /// </summary>
        /// <param name="configure"></param>
        /// <returns></returns>
        IFluentDapperMappingConfig ConfigureOptions(Action<DapperOptions> configure);
    }
}