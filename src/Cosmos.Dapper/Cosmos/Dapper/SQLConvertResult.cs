using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using Cosmos.Dapper.Core.Helpers;
using Cosmos.Reflection;
using Dapper;

namespace Cosmos.Dapper
{
    /// <summary>
    /// Sql convert result
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class SQLConvertResult
    {
        private StringBuilder _builder;
        private readonly IDictionary<string, object> _parameters;

        /// <summary>
        /// Create a new instance of <see cref="SQLConvertResult" />
        /// </summary>
        public SQLConvertResult()
        {
            _builder = new StringBuilder();
            _parameters = new Dictionary<string, object>();
        }

        /// <summary>
        /// Create a new instance of <see cref="SQLConvertResult" />
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        public SQLConvertResult(string sql, IDictionary<string, object> parameters)
        {
            _builder = new StringBuilder(sql);
            _parameters = parameters;
        }

        /// <summary>
        /// Create a new instance of <see cref="SQLConvertResult" />
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="parameters"></param>
        public SQLConvertResult(StringBuilder builder, IDictionary<string, object> parameters)
        {
            _builder = builder;
            _parameters = parameters;
        }

        /// <summary>
        /// Create a new instance of <see cref="SQLConvertResult" />
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="deepCopy"></param>
        protected SQLConvertResult(SQLConvertResult sql, bool deepCopy = false)
        {
            if (deepCopy)
            {
                _builder = new StringBuilder(sql.Sql);
                _parameters = sql._parameters.DeepCopy();
            }
            else
            {
                _builder = sql._builder;
                _parameters = sql._parameters;
            }
        }

        /// <summary>
        /// To get final sql
        /// </summary>
        public string Sql => _builder.ToString();

        /// <summary>
        /// Append sql.
        /// </summary>
        /// <param name="sql"></param>
        public void AppendSql(string sql)
        {
            _builder.Append(sql);
        }

        /// <summary>
        /// Append sql at new line.
        /// </summary>
        /// <param name="sql"></param>
        public void AppendSqlLine(string sql)
        {
            _builder.AppendLine(sql);
        }

        /// <summary>
        /// Clear sql and append new sql.
        /// </summary>
        /// <param name="sql"></param>
        public void ResetSql(string sql)
        {
            _builder.Clear().Append(sql);
        }

        /// <summary>
        /// Dapper dynamic parameters
        /// </summary>
        public DynamicParameters Parameters => _parameters.ToDynamicParameters();

        /// <summary>
        /// Writable parameters
        /// </summary>
        public IDictionary<string, object> WritableParameters => _parameters;

        /// <summary>
        /// Copy instance
        /// </summary>
        /// <returns></returns>
        public SQLConvertResult CopyInstance()
        {
            return new SQLConvertResult(Sql, _parameters.DeepCopy());
        }

        /// <summary>
        /// Convert <see cref="SQLConvertResult"/> to a <see cref="CommandDefinition"/> object.
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="timeout"></param>
        /// <param name="commandType"></param>
        /// <param name="commandFlags"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        // ReSharper disable once InconsistentNaming
        public virtual CommandDefinition ToSQLCommand(
            IDbTransaction transaction = null,
            int? timeout = null,
            CommandType? commandType = null,
            CommandFlags commandFlags = CommandFlags.Buffered,
            CancellationToken cancellationToken = default)
        {
            return new CommandDefinition(Sql, Parameters, transaction, timeout, commandType, commandFlags, cancellationToken);
        }

        /// <summary>
        /// Convert <see cref="SQLConvertResult"/> to a <see cref="CommandDefinition"/> object.
        /// </summary>
        /// <param name="explicitParams"></param>
        /// <param name="transaction"></param>
        /// <param name="timeout"></param>
        /// <param name="commandType"></param>
        /// <param name="commandFlags"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        // ReSharper disable once InconsistentNaming
        public virtual CommandDefinition ToSQLCommand(
            object explicitParams,
            IDbTransaction transaction = null,
            int? timeout = null,
            CommandType? commandType = null,
            CommandFlags commandFlags = CommandFlags.Buffered,
            CancellationToken cancellationToken = default)
        {
            return new CommandDefinition(Sql, explicitParams, transaction, timeout, commandType, commandFlags, cancellationToken);
        }
    }
}