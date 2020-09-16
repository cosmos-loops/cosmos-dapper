using System;
using System.Collections.Generic;
using System.Reflection;

namespace Cosmos.Dapper.Conventions
{
    /// <summary>
    /// Convention config
    /// </summary>
    public class ConventionConfig
    {
        private readonly List<Func<PropertyInfo, bool>> _propertyPredicates = new List<Func<PropertyInfo, bool>>();

        /// <summary>
        /// Gets property predicates
        /// </summary>
        public IReadOnlyCollection<Func<PropertyInfo, bool>> PropertyPredicates => _propertyPredicates;

        /// <summary>
        /// Gets property convention
        /// </summary>
        public PropertyConventionStrategy PropertyConvention { get; private set; }

        /// <summary>
        /// Sets flter
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public ConventionConfig Filter(Func<PropertyInfo, bool> predicate)
        {
            _propertyPredicates.Add(predicate);
            return this;
        }

        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="configure"></param>
        public void Configure(Action<PropertyConventionStrategy> configure)
        {
            var strategy = new PropertyConventionStrategy();
            PropertyConvention = strategy;
            configure?.Invoke(strategy);
        }
    }
}