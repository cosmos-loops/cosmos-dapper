using System;

namespace Cosmos.Dapper.EntityMapping
{
    /// <summary>
    /// Schema attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class SchemaAttribute : Attribute
    {
        /// <summary>
        /// Create a new instance of <see cref="SchemaAttribute" />
        /// </summary>
        /// <param name="name"></param>
        /// <param name="caseSensitive"></param>
        public SchemaAttribute(string name, bool caseSensitive = true)
        {
            Name = name;
            CaseSensitive = caseSensitive;
        }

        /// <summary>
        /// Gets name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets sensitive
        /// </summary>
        public bool CaseSensitive { get; }
    }
}