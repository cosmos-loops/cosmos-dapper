using Dapper;
using System;
using System.Data;
using SqlKata;
using System.Threading.Tasks;
using System.Collections.Generic;
using SqlKata.Compilers;
using static Dapper.SqlMapper;
using System.Linq;
using Cosmos.Dapper.Core;

namespace Cosmos.Data.SqlKata
{
    /// <summary>
    /// Multiple query builder
    /// </summary>
    public class MultipleQueryBuilder :
        IQueryBuilderMultipleSelectDapper,
        IQueryBuilderMultipleUpdateDapper,
        IQueryBuilderMultipleDeleteDapper,
        IQueryBuilderMultipleInsertDapper,
        IDisposable
    {
        private IList<Tuple<Type, Query, ResultType>> Queries { get; set; }
        private IDapperConnector Connector { get; set; }
        private Compiler Compiler { get; set; }
        private ResultType ResultTypeGlobal { get; set; } = ResultType.None;

        /// <summary>
        /// Create a new instance of <see cref="MultipleQueryBuilder" />
        /// </summary>
        /// <param name="connector"></param>
        /// <param name="compiler"></param>
        public MultipleQueryBuilder(IDapperConnector connector, Compiler compiler)
        {
            Clear();
            Connector = connector;
            Compiler = compiler;
        }

        #region Add

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="query"></param>
        /// <param name="resultType"></param>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        protected MultipleQueryBuilder Add<TReturn>(Query query, ResultType resultType)
        {
            if (ResultTypeGlobal == ResultType.None)
            {
                ResultTypeGlobal = resultType;
            }

            Queries.Add(new Tuple<Type, Query, ResultType>(typeof(TReturn), query, resultType));
            return this;
        }

        #endregion Add

        #region AddSelect

        /// <summary>
        /// Add select
        /// </summary>
        /// <param name="query"></param>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public IQueryBuilderMultipleSelectDapper AddSelect<TReturn>(Query query)
        {
            return Add<TReturn>(query, ResultType.Select);
        }

        /// <summary>
        /// Add select
        /// </summary>
        /// <param name="query"></param>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public IQueryBuilderMultipleSelectDapper AddSelect<TReturn>(Func<Query, Query> query)
        {
            return AddSelect<TReturn>(query(new Query()));
        }

        #endregion AddSelect

        #region AddUpdate

        /// <summary>
        /// Add update
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IQueryBuilderMultipleUpdateDapper AddUpdate(Query query)
        {
            return Add<bool>(query, ResultType.Update);
        }

        /// <summary>
        /// Add update
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IQueryBuilderMultipleUpdateDapper AddUpdate(Func<Query, Query> query)
        {
            return AddUpdate(query(new Query()));
        }

        #endregion AddUpdate

        #region AddDelete

        /// <summary>
        /// Add delete
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IQueryBuilderMultipleDeleteDapper AddDelete(Query query)
        {
            return Add<bool>(query, ResultType.Delete);
        }

        /// <summary>
        /// Add delete
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IQueryBuilderMultipleDeleteDapper AddDelete(Func<Query, Query> query)
        {
            return AddDelete(query(new Query()));
        }

        #endregion AddDelete

        #region AddInsert

        /// <summary>
        /// Add insert
        /// </summary>
        /// <param name="query"></param>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public IQueryBuilderMultipleInsertDapper AddInsert<TReturn>(Query query)
        {
            return Add<TReturn>(query, ResultType.Insert);
        }

        /// <summary>
        /// Add insert
        /// </summary>
        /// <param name="query"></param>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public IQueryBuilderMultipleInsertDapper AddInsert<TReturn>(Func<Query, Query> query)
        {
            return AddInsert<TReturn>(query(new Query()));
        }

        #endregion AddInsert

        #region Execute

        IEnumerable<IResultItems> IQueryBuilderMultipleExecuteDapper<IResultItems>.ExecuteMultiple()
        {
            return ExecuteMultiple<IResultItems>();
        }

        async Task<IEnumerable<IResultItems>> IQueryBuilderMultipleExecuteDapper<IResultItems>.ExecuteMultipleAsync()
        {
            return await ExecuteMultipleAsync<IResultItems>();
        }

        IEnumerable<IResultAffectedRows> IQueryBuilderMultipleExecuteDapper<IResultAffectedRows>.ExecuteMultiple()
        {
            return ExecuteMultiple<IResultAffectedRows>();
        }

        async Task<IEnumerable<IResultAffectedRows>> IQueryBuilderMultipleExecuteDapper<IResultAffectedRows>.ExecuteMultipleAsync()
        {
            return await ExecuteMultipleAsync<IResultAffectedRows>();
        }

        /// <summary>
        /// Execute multiple
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        protected IEnumerable<TResult> ExecuteMultiple<TResult>()
        {
            IEnumerable<TResult> items = null;
            if (ResultTypeGlobal == ResultType.Insert || ResultTypeGlobal == ResultType.Select)
            {
                GridReader result = GridReaderResults();
                items = GetGridReaderResultsMultiple<TResult>(result);
            }

            if (ResultTypeGlobal == ResultType.Delete || ResultTypeGlobal == ResultType.Update)
            {
                items = GetAffectedRowsResultsMultiple<TResult>();
            }

            return items;
        }

        /// <summary>
        /// Execute multiple async
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        protected async Task<IEnumerable<TResult>> ExecuteMultipleAsync<TResult>()
        {
            IEnumerable<TResult> items = null;
            if (ResultTypeGlobal == ResultType.Insert || ResultTypeGlobal == ResultType.Select)
            {
                GridReader result = await GridReaderResultsAsync();
                items = GetGridReaderResultsMultiple<TResult>(result);
            }

            if (ResultTypeGlobal == ResultType.Delete || ResultTypeGlobal == ResultType.Update)
            {
                items = await GetAffectedRowsResultsMultipleAsync<TResult>();
            }

            return items;
        }

        #endregion

        #region GetResultsMultiple

        /// <summary>
        /// Gets grid reader results multiple
        /// </summary>
        /// <param name="gridReader"></param>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        protected IEnumerable<TResult> GetGridReaderResultsMultiple<TResult>(GridReader gridReader)
        {
            foreach (Tuple<Type, Query, ResultType> item in Queries)
            {
                bool ReturnId = item.Item2
                   .Clauses
                   .OfType<InsertClause>()
                   .Select(_ => _.ReturnId)
                   .FirstOrDefault();

                ResultItems result = new ResultItems
                {
                    ResultType = item.Item3,
                    Value = item.Item3 == ResultType.Insert
                        ? !gridReader.IsConsumed && ReturnId ? gridReader.ReadFirstOrDefault(item.Item1) : null
                        : !gridReader.IsConsumed
                            ? gridReader.Read(item.Item1)
                            : null
                };
                yield return ChangeTypeToType<TResult>(result);
            }

            Clear();
        }

        /// <summary>
        /// Gets affected rows results multiple
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        protected IEnumerable<TResult> GetAffectedRowsResultsMultiple<TResult>(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            foreach (Query query in GetQueries())
            {
                SqlResult compiler = Compiler.Compile(query);
                int affectedRows = Connector.Connection.Execute(compiler.Sql, compiler.NamedBindings, transaction, commandTimeout, commandType);
                ResultAffectedRows result = new ResultAffectedRows
                {
                    AffectedRows = affectedRows,
                    ResultType = ResultTypeGlobal
                };
                yield return ChangeTypeToType<TResult>(result);
            }
        }

        /// <summary>
        /// Gets affected rows results multiple async
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        protected async Task<IEnumerable<TResult>> GetAffectedRowsResultsMultipleAsync<TResult>(IDbTransaction transaction = null, int? commandTimeout =
            null, CommandType? commandType = null)
        {
            IList<TResult> results = new List<TResult>();
            foreach (Query query in GetQueries())
            {
                SqlResult compiler = Compiler.Compile(query);
                int affectedRows = await Connector.Connection.ExecuteAsync(compiler.Sql, compiler.NamedBindings, transaction, commandTimeout, commandType);
                ResultAffectedRows result = new ResultAffectedRows
                {
                    AffectedRows = affectedRows,
                    ResultType = ResultTypeGlobal
                };
                results.Add(ChangeTypeToType<TResult>(result));
            }

            return results;
        }

        #endregion GetResultsMultiple

        #region GetReaderResults

        /// <summary>
        /// Grid reader results
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        protected GridReader GridReaderResults(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = GetSqlResult();
            return Connector.Connection.QueryMultiple(result.Sql, result.NamedBindings, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Grid reader results async
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        protected async Task<GridReader> GridReaderResultsAsync(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = GetSqlResult();
            return await Connector.Connection.QueryMultipleAsync(result.Sql, result.NamedBindings, transaction, commandTimeout, commandType);
        }

        #endregion GetReaderResults

        /// <summary>
        /// Change type to type
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="TType"></typeparam>
        /// <returns></returns>
        protected TType ChangeTypeToType<TType>(object value)
        {
            return (TType) value;
        }

        /// <summary>
        /// Get sql result
        /// </summary>
        /// <returns></returns>
        protected SqlResult GetSqlResult()
        {
            return Compiler.Compile(GetQueries());
        }

        /// <summary>
        /// Get queries
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<Query> GetQueries()
        {
            return Queries.Select(x => x.Item2);
        }

        /// <summary>
        /// Clear
        /// </summary>
        public void Clear()
        {
            if (Queries == null)
            {
                Queries = new List<Tuple<Type, Query, ResultType>>();
            }

            else
            {
                Queries.Clear();
            }

            ResultTypeGlobal = ResultType.None;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Connector?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}