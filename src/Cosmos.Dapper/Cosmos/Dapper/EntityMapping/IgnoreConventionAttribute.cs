using System;

namespace Cosmos.Dapper.EntityMapping
{
    /// <summary>
    /// Ignore convention attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class IgnoreConventionAttribute : Attribute { }
}