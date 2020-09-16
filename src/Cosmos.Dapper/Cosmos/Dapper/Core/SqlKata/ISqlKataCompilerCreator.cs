using SqlKata.Compilers;

namespace Cosmos.Dapper.Core.SqlKata
{
    /// <summary>
    /// Interface for SqlKata compiler creator
    /// </summary>
    public interface ISqlKataCompilerCreator
    {
        /// <summary>
        /// Create
        /// </summary>
        /// <returns></returns>
        Compiler Create();
    }
}