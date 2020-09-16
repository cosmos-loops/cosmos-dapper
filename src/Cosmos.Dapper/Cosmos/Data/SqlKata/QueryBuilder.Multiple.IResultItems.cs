namespace Cosmos.Data.SqlKata
{
    /// <summary>
    /// Interface for result items
    /// </summary>
    public interface IResultItems : IResultBase
    {
        /// <summary>
        /// Gets or sets value
        /// </summary>
        object Value { get; set; }

        /// <summary>
        /// Get value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetValue<T>();
    }
}