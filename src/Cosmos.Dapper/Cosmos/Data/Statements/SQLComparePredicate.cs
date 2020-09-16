namespace Cosmos.Data.Statements
{
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// Sql compare predicate
    /// </summary>
    public abstract class SQLComparePredicate : SQLBasePredicate, ISQLComparePredicate
    {
        /// <summary>
        /// Operator
        /// </summary>
        public SQLOperatorSlim Operator { get; set; }

        /// <summary>
        /// Not
        /// </summary>
        public bool Not { get; set; }

        /// <summary>
        /// Get operator string
        /// </summary>
        /// <returns></returns>
        public virtual string GetOperatorString()
        {
            switch (Operator)
            {
                case SQLOperatorSlim.GT: return Not ? "<=" : ">";
                case SQLOperatorSlim.GE: return Not ? "<" : ">=";
                case SQLOperatorSlim.LT: return Not ? ">=" : "<";
                case SQLOperatorSlim.LE: return Not ? ">" : "<=";
                case SQLOperatorSlim.LIKE: return Not ? "NOT LIKE" : "LIKE";
                default: return Not ? "<>" : "=";
            }
        }
    }
}