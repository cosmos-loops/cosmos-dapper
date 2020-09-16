using Cosmos.Data.SqlKata;
using SqlKata;

namespace Cosmos.Dapper.Core.SqlKata
{
    internal static class QueryBuilderExtensions
    {
        public static QueryBuilder WhereRawSafety(this QueryBuilder query, string sql, params object[] bindings)
        {
            return string.IsNullOrWhiteSpace(sql)
                ? query
                : query.WhereRaw(sql, bindings).AsQ();
        }

        public static QueryBuilder AsQ(this Query query) => (QueryBuilder) query;

        public static EntityQueryBuilder AsE(this Query query) => (EntityQueryBuilder) query;
    }
}