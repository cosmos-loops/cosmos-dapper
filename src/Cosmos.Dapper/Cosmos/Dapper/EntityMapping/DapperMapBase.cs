using System;
using System.Linq.Expressions;
using Cosmos.Dapper.Core.DataFiltering;
using Cosmos.Dapper.Core.Mapping;
using Cosmos.Models;

namespace Cosmos.Dapper.EntityMapping
{
    /// <summary>
    /// Dapper map base
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class DapperMapBase<TEntity> : IMap where TEntity : class, IEntity
    {
        /// <summary>
        /// Class builder
        /// </summary>
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        protected DapperClassBuilder ClassBuilder { get; private set; }

        /// <summary>
        /// Map
        /// </summary>
        /// <param name="modelBuilder"></param>
        public void Map(DapperClassBuilder modelBuilder)
        {
            ClassBuilder = modelBuilder;
            var builder = modelBuilder.Entity<TEntity>();
            MapTable(builder);
            MapProperties(builder);
            MapAssociations(builder);

            //config global data filtering strategy
            var filter = new GlobalLevelDataFilteringStrategy<TEntity>(HasQueryFilter);
            GlobalDataFilterManager.Register(filter);
        }

        /// <summary>
        /// 映射表
        /// </summary>
        /// <param name="builder"></param>
        protected virtual void MapTable(DapperEntityTypeBuilder<TEntity> builder) { }

        /// <summary>
        /// 映射属性
        /// </summary>
        /// <param name="builder"></param>
        protected virtual void MapProperties(DapperEntityTypeBuilder<TEntity> builder) { }

        /// <summary>
        /// 映射导航属性
        /// </summary>
        /// <param name="builder"></param>
        protected virtual void MapAssociations(DapperEntityTypeBuilder<TEntity> builder) { }

        /// <summary>
        /// To configure global data filtering strategy for such entity
        /// </summary>
        /// <returns></returns>
        protected virtual Expression<Func<TEntity, bool>> HasQueryFilter() => null;
    }
}