using System.Data;
using Cosmos.Data.Core.Transaction;

namespace Cosmos.Dapper.Core.Helpers
{
    internal static class CompatibilityExtensions
    {
        public static IDbTransaction Compatibility(this IDbTransaction transaction)
            => transaction is NullDbTransaction ? null : transaction;
    }
}