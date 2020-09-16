using System.Collections.Generic;
using Dapper;

namespace Cosmos.Dapper
{
    /// <summary>
    /// Sequence reader result reader
    /// </summary>
    public class SequenceReaderResultReader : IMultipleResultReader
    {
        private readonly Queue<SqlMapper.GridReader> _items;

        /// <summary>
        /// Create a new instance of <see cref="SequenceReaderResultReader" />
        /// </summary>
        /// <param name="items"></param>
        public SequenceReaderResultReader(IEnumerable<SqlMapper.GridReader> items)
        {
            _items = new Queue<SqlMapper.GridReader>(items);
        }

        /// <summary>
        /// Read
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> Read<T>()
        {
            var reader = _items.Dequeue();
            return reader.Read<T>();
        }
    }
}