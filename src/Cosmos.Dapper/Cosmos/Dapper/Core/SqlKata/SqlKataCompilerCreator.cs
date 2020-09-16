using SqlKata.Compilers;

namespace Cosmos.Dapper.Core.SqlKata
{
    /// <summary>
    /// SqlKata compiler creator
    /// </summary>
    /// <typeparam name="TCompiler"></typeparam>
    public class SqlKataCompilerCreator<TCompiler> : ISqlKataCompilerCreator where TCompiler : Compiler, new()
    {
        /// <summary>
        /// Create
        /// </summary>
        /// <returns></returns>
        public Compiler Create() => new TCompiler();
    }
}