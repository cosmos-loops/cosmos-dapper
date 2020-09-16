using System;

namespace Cosmos.Dapper.EntityMapping
{
    /// <summary>
    /// Column attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ColumnAttribute : Attribute
    {
        /// <summary>
        /// Create a new instance of <see cref="ColumnAttribute" />
        /// </summary>
        /// <param name="name"></param>
        /// <param name="caseSensitive"></param>
        public ColumnAttribute(string name, bool caseSensitive = true)
        {
            Name = name;
            CaseSensitive = caseSensitive;
        }

        /// <summary>
        /// Gets name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets case sensitive
        /// </summary>
        public bool CaseSensitive { get; }
    }
}