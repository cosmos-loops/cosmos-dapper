using System.Collections.Generic;
using Cosmos.Reflection;

namespace Cosmos.Dapper.Conventions
{
    /// <summary>
    /// Convention base
    /// </summary>
    public abstract class ConventionBase
    {
        private readonly List<ConventionConfig> _conventionConfigs = new List<ConventionConfig>();

        /// <summary>
        /// Gets convention configs
        /// </summary>
        public List<ConventionConfig> ConventionConfigs => _conventionConfigs;

        /// <summary>
        /// Gets properties
        /// </summary>
        /// <returns></returns>
        protected ConventionConfig Properties()
        {
            var config = new ConventionConfig();
            _conventionConfigs.Add(config);
            return config;
        }

        /// <summary>
        /// Gets properties
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <returns></returns>
        protected ConventionConfig Properties<TProperty>()
        {
            var underlyingType = Types.Of<TProperty>();
            var config = new ConventionConfig().Filter(p => p.PropertyType == underlyingType);
            _conventionConfigs.Add(config);
            return config;
        }
    }
}