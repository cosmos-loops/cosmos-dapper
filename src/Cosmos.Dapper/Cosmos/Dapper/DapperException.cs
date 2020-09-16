using System;
using System.Data;

namespace Cosmos.Dapper
{
    /// <summary>
    /// Dapper exception
    /// </summary>
    public class DapperException : CosmosException
    {
        /// <summary>
        /// Defalt db ctx flag
        /// </summary>
        // ReSharper disable once InconsistentNaming
        protected const string DEFAULT_DBCTX_FLAG = "__DAPPER_DBCTX_FLG";

        /// <summary>
        /// Default db ctx error message
        /// </summary>
        // ReSharper disable once InconsistentNaming
        protected const string DEFAULT_DBCTX_ERROR_MESSAGE = "_DEFAULT_DAPPER_DBCONTEXT_ERROR";

        /// <summary>
        /// Default db ctx error code
        /// </summary>
        // ReSharper disable once InconsistentNaming
        protected const long DEFAULT_DBCTX_ERROR_CODE = 200101;

        /// <summary>
        /// Create a new instance of <see cref="DapperException" />
        /// </summary>
        public DapperException()
            : this(null, DEFAULT_DBCTX_ERROR_CODE, DEFAULT_DBCTX_ERROR_MESSAGE, DEFAULT_DBCTX_FLAG) { }

        /// <summary>
        /// Create a new instance of <see cref="DapperException" />
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="innerException"></param>
        public DapperException(string errorMessage, Exception innerException = null)
            : this(null, DEFAULT_DBCTX_ERROR_CODE, errorMessage, DEFAULT_DBCTX_FLAG, innerException) { }

        /// <summary>
        /// Create a new instance of <see cref="DapperException" />
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="flag"></param>
        /// <param name="innerException"></param>
        public DapperException(string errorMessage, string flag, Exception innerException = null)
            : this(null, DEFAULT_DBCTX_ERROR_CODE, errorMessage, flag, innerException) { }

        /// <summary>
        /// Create a new instance of <see cref="DapperException" />
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="errorMessage"></param>
        /// <param name="innerException"></param>
        public DapperException(long errorCode, string errorMessage, Exception innerException = null)
            : this(null, errorCode, errorMessage, DEFAULT_DBCTX_FLAG, innerException) { }

        /// <summary>
        /// Create a new instance of <see cref="DapperException" />
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="errorMessage"></param>
        /// <param name="flag"></param>
        /// <param name="innerException"></param>
        public DapperException(long errorCode, string errorMessage, string flag, Exception innerException)
            : this(null, errorCode, errorMessage, flag, innerException) { }

        /// <summary>
        /// Create a new instance of <see cref="DapperException" />
        /// </summary>
        /// <param name="connection"></param>
        public DapperException(IDbConnection connection)
            : this(connection, DEFAULT_DBCTX_ERROR_CODE, DEFAULT_DBCTX_ERROR_MESSAGE, DEFAULT_DBCTX_FLAG) { }

        /// <summary>
        /// Create a new instance of <see cref="DapperException" />
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="errorMessage"></param>
        /// <param name="innerException"></param>
        public DapperException(IDbConnection connection, string errorMessage, Exception innerException = null)
            : this(connection, DEFAULT_DBCTX_ERROR_CODE, errorMessage, DEFAULT_DBCTX_FLAG, innerException) { }

        /// <summary>
        /// Create a new instance of <see cref="DapperException" />
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="errorMessage"></param>
        /// <param name="flag"></param>
        /// <param name="innerException"></param>
        public DapperException(IDbConnection connection, string errorMessage, string flag,
            Exception innerException = null)
            : this(connection, DEFAULT_DBCTX_ERROR_CODE, errorMessage, flag, innerException) { }

        /// <summary>
        /// Create a new instance of <see cref="DapperException" />
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMessage"></param>
        /// <param name="innerException"></param>
        public DapperException(IDbConnection connection, long errorCode, string errorMessage,
            Exception innerException = null)
            : this(connection, errorCode, errorMessage, DEFAULT_DBCTX_FLAG, innerException) { }

        /// <summary>
        /// Create a new instance of <see cref="DapperException" />
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMessage"></param>
        /// <param name="flag"></param>
        /// <param name="innerException"></param>
        public DapperException(IDbConnection connection, long errorCode, string errorMessage, string flag,
            Exception innerException = null)
            : base(errorCode, errorMessage, flag, innerException)
        {
            if (connection != null)
            {
                Database = connection.Database;
                ConnectionString = connection.ConnectionString;
                ConnectionState = connection.State;
            }
        }

        /// <summary>
        /// Gets database
        /// </summary>
        public string Database { get; }

        /// <summary>
        /// Gets connection string
        /// </summary>
        public string ConnectionString { get; }

        /// <summary>
        /// Gets connection state
        /// </summary>
        public ConnectionState ConnectionState { get; }
    }
}