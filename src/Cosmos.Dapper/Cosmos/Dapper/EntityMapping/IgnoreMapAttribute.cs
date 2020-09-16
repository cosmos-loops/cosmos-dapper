using System;

namespace Cosmos.Dapper.EntityMapping
{
    /// <summary>
    /// Ignore map attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class IgnoreMapAttribute : Attribute { }
}