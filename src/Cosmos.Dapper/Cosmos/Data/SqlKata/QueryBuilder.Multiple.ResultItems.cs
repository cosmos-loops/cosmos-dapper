namespace Cosmos.Data.SqlKata
{
    /// <summary>
    /// Result items
    /// </summary>
    public class ResultItems : IResultItems
    {
        /// <summary>
        /// Gets or sets value
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Result type
        /// </summary>
        public ResultType ResultType { get; set; }

        /// <summary>
        /// Get value...
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetValue<T>()
        {
            return (T) Value;
        }
    }
}