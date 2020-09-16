using System.Collections.Generic;
using Dapper;

namespace Cosmos.Dapper
{
    /// <summary>
    /// Grid reader result reader
    /// </summary>
    public class GridReaderResultReader : IMultipleResultReader
    {
        private readonly SqlMapper.GridReader _reader;

        /// <summary>
        /// Create a new instance of <see cref="GridReaderResultReader" />
        /// </summary>
        /// <param name="reader"></param>
        public GridReaderResultReader(SqlMapper.GridReader reader)
        {
            _reader = reader;
        }

        /// <summary>
        /// Read
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> Read<T>()
        {
            return _reader.Read<T>();
        }
    }
}