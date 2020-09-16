using System;
using Cosmos.Models;

namespace Cosmos.Dapper.EntityMapping
{
    /// <summary>
    /// Dapper class builder
    /// </summary>
    public class DapperClassBuilder
    {
        private readonly DapperConfig _mappingConfig;

        /// <summary>
        /// Create a new instance of <see cref="DapperClassBuilder" />
        /// </summary>
        /// <param name="config"></param>
        public DapperClassBuilder(DapperConfig config)
        {
            _mappingConfig = config ?? throw new ArgumentNullException(nameof(config));
        }

        /// <summary>
        /// Entity..
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public DapperEntityTypeBuilder<TEntity> Entity<TEntity>() where TEntity : class, IEntity
        {
            return new DapperEntityTypeBuilder<TEntity>(_mappingConfig);
        }
    }
}