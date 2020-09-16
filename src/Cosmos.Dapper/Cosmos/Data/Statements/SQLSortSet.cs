using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cosmos.Dapper.Mapper;

namespace Cosmos.Data.Statements
{
    /// <summary>
    /// Sql sort set
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class SQLSortSet : List<SQLSort>
    {
        internal SQLSortSet() { }

        /// <summary>
        /// To flag this sql sort set is func mode or not
        /// </summary>
        public virtual bool IsFactoryMode => false;

        /// <summary>
        /// To string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            Sort(new SQLSortComparer());
            var sb = new StringBuilder();

            for (var i = 0; i < Count; i++)
            {
                sb.AppendFormat("{0} {1}", this[i].FieldName, this[i].SortType);

                if (i < Count - 1)
                    sb.Append(", ");
            }

            return sb.ToString();
        }

        /// <summary>
        /// To strings
        /// </summary>
        /// <param name="classMap"></param>
        /// <param name="columnNameFunc"></param>
        /// <returns></returns>
        public virtual IEnumerable<string> ToStrings(IClassMap classMap, Func<IClassMap, string, bool, string> columnNameFunc)
        {
            return this.Select(s => $"{columnNameFunc(classMap, s.FieldName, false)} {s.SortType}");
        }
    }
}