using System;

namespace Cosmos.Dapper.EntityMapping
{
    /// <summary>
    /// Table attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : Attribute
    {
        /// <summary>
        /// Create a new instance of <see cref="TableAttribute" />
        /// </summary>
        /// <param name="name"></param>
        /// <param name="caseSensitive"></param>
        public TableAttribute(string name, bool caseSensitive = true)
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