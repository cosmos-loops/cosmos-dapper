using System.Data;
using System.Threading;
using Dapper;

namespace Cosmos.Dapper
{
    /// <summary>
    /// Override Sql convert result
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class OverrideSQLConvertResult : SQLConvertResult
    {
        /// <summary>
        /// Create a new instance of <see cref="OverrideSQLConvertResult" />
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="objectParameter"></param>
        /// <param name="enableNullParameter"></param>
        public OverrideSQLConvertResult(SQLConvertResult sql, object objectParameter, bool enableNullParameter = false) : base(sql)
        {
            ObjectParameter = objectParameter;
            EnableNullParameter = enableNullParameter;
        }

        /// <summary>
        /// Object parameter
        /// </summary>
        public object ObjectParameter { get; set; }

        /// <summary>
        /// Enable null parameter
        /// </summary>
        public bool EnableNullParameter { get; set; }

        /// <summary>
        /// To sql command
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="timeout"></param>
        /// <param name="commandType"></param>
        /// <param name="commandFlags"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override CommandDefinition ToSQLCommand(
            IDbTransaction transaction = null,
            int? timeout = null,
            CommandType? commandType = null,
            CommandFlags commandFlags = CommandFlags.Buffered,
            CancellationToken cancellationToken = default)
        {
            if (EnableNullParameter)
                return new CommandDefinition(Sql, ObjectParameter, transaction, timeout, commandType, commandFlags, cancellationToken);
            return ObjectParameter == null
                ? base.ToSQLCommand(transaction, timeout, commandType, commandFlags, cancellationToken)
                : new CommandDefinition(Sql, ObjectParameter, transaction, timeout, commandType, commandFlags, cancellationToken);
        }
    }
}