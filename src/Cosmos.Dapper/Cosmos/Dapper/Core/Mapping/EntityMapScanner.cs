using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cosmos.Dapper.EntityMapping;
using Cosmos.Models;
using Cosmos.Reflection;

namespace Cosmos.Dapper.Core.Mapping
{
    /// <summary>
    /// Entity map scanner
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public sealed class EntityMapScanner<TContext> : InstanceScanner<IMap>
        where TContext : class, IDapperContext
    {
        // ReSharper disable once InconsistentNaming
        private const string SKIP_ASSEMBLIES =
            "^System|^Mscorlib|^Netstandard|^Microsoft|^Autofac|^AutoMapper|^EntityFramework|^Newtonsoft|^Castle|^NLog|^Pomelo|^AspectCore|^Xunit|^Nito|^Npgsql|^Exceptionless|^MySqlConnector|^Anonymously Hosted";

        /// <summary>
        /// Create a new instance of <see cref="EntityMapScanner{TContext}" />
        /// </summary>
        /// <param name="entityMapType"></param>
        public EntityMapScanner(Type entityMapType) : this(entityMapType, string.Empty) { }

        /// <summary>
        /// Create a new instance of <see cref="EntityMapScanner{TContext}" />
        /// </summary>
        /// <param name="entityMapType"></param>
        /// <param name="limitedAssemblies"></param>
        public EntityMapScanner(Type entityMapType, string limitedAssemblies) : base(entityMapType)
        {
            LimitedInAssemblies = limitedAssemblies;
            DbContextType = typeof(TContext);
            BindingEntityTypes = GetDbSetBodyTypes();
        }

        private Type DbContextType { get; }

        private List<Type> BindingEntityTypes { get; }

        private string LimitedInAssemblies { get; }

        /// <summary>
        /// Gets skip assemblies namespaces
        /// </summary>
        /// <returns></returns>
        protected override string GetSkipAssembliesNamespaces() => SKIP_ASSEMBLIES;

        /// <summary>
        /// Gets limited assemblies namespaces
        /// </summary>
        /// <returns></returns>
        protected override string GetLimitedAssembliesNamespaces() => LimitedInAssemblies;

        private List<Type> GetDbSetBodyTypes()
        {
            return DbContextType.GetProperties()
               .Select(t => t.PropertyType)
               .Where(t => t.IsGenericType)
               .Select(s => s.GetGenericArguments()[0])
               .ToList();
        }

        /// <summary>
        /// Type filter
        /// </summary>
        /// <returns></returns>
        protected override Func<Type, bool> TypeFilter() =>
            t => t.IsClass && t.IsPublic && !t.IsAbstract &&
                 BaseType.IsAssignableFrom(t) &&
                 !t.IsDefined<IgnoreMapAttribute>() &&
                 !t.IsDefined<EntityMapIgnoreScanningAttribute>() &&
                 t.IsMatchedEntityMappingRule(BindingEntityTypes);
    }
}