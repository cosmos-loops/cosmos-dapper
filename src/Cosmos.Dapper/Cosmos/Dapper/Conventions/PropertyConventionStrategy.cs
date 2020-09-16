using System;

namespace Cosmos.Dapper.Conventions
{
    /// <summary>
    /// Property convention strategy
    /// </summary>
    public class PropertyConventionStrategy
    {
        #region ColumnName

        internal string ColumnNameValue { get; private set; }

        /// <summary>
        /// Has column name
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public PropertyConventionStrategy HasColumnName(string columnName)
        {
            ColumnNameValue = columnName;
            return this;
        }

        #endregion

        #region Prefix

        internal string PrefixValue { get; private set; }

        /// <summary>
        /// Has prefix
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public PropertyConventionStrategy HasPrefix(string prefix)
        {
            PrefixValue = prefix;
            return this;
        }

        #endregion

        #region CaseSensitive

        internal bool CaseSensitiveValue { get; private set; } = true;

        /// <summary>
        /// Sets case sensitive
        /// </summary>
        /// <returns></returns>
        public PropertyConventionStrategy CaseSensitive()
        {
            CaseSensitiveValue = true;
            return this;
        }

        /// <summary>
        /// Sets case insensitive
        /// </summary>
        /// <returns></returns>
        public PropertyConventionStrategy CaseInsensitive()
        {
            CaseSensitiveValue = false;
            return this;
        }

        #endregion

        #region Transform

        internal Func<string, string> PropertyTransformer { get; private set; }

        /// <summary>
        /// Transform
        /// </summary>
        /// <param name="transformer"></param>
        /// <returns></returns>
        public PropertyConventionStrategy Transform(Func<string, string> transformer)
        {
            PropertyTransformer = transformer;
            return this;
        }

        #endregion
    }
}