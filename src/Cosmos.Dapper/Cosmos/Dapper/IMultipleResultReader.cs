using System.Collections.Generic;

namespace Cosmos.Dapper
{
    /// <summary>
    /// Interface for Multiple result reader
    /// </summary>
    public interface IMultipleResultReader
    {
        /// <summary>
        /// Read
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> Read<T>();
    }
}