using System;

namespace Cosmos.Dapper.EntityMapping
{
    /// <summary>
    /// Readonly attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ReadOnlyAttribute : Attribute { }
}