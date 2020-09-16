using System;
using Cosmos.Dapper.Mapper;

namespace Cosmos.Dapper.EntityMapping
{
    /// <summary>
    /// Primary key attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class PrimaryKeyAttribute : Attribute
    {
        /// <summary>
        /// Create a new instance of <see cref="PrimaryKeyAttribute" />
        /// </summary>
        /// <param name="keyType"></param>
        public PrimaryKeyAttribute(KeyType keyType = KeyType.Identity) => KeyType = keyType;

        /// <summary>
        /// Key type
        /// </summary>
        public KeyType KeyType { get; }
    }
}