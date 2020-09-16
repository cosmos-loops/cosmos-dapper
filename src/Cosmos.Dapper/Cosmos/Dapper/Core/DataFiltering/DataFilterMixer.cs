using System;
using Cosmos.Data.Statements;

namespace Cosmos.Dapper.Core.DataFiltering
{
    /// <summary>
    /// Data filter mixer
    /// </summary>
    public static class DataFilterMixer
    {
        /// <summary>
        /// Mix
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static ISQLPredicate[] Mix(ISQLPredicate left, ISQLPredicate[] right)
        {
            if (left is null)
                return right;

            var ret = new ISQLPredicate[right.Length + 1];
            ret[0] = left;
            Array.Copy(right, 0, ret, 1, right.Length);
            return ret;
        }
    }
}