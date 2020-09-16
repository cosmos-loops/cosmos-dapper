using System;
using Cosmos.Collections;
using Cosmos.Data;
using Cosmos.Data.Statements;

namespace Samples.StaticUsage
{
    class Program
    {
        static void Main(string[] args)
        {
            var connector = SqlServerDapper.GetClient("server=<ip_address>;database=<db_name>;uid=<uid>;pwd=<pwd>;");

            var models = connector.GetList<ResearchModel>(
                x => x.IsValid,
                SQLOrder<ResearchModel>.By(x => x.Id, SQLSortType.DESC));

            models?.ForEach(Console.WriteLine);

            Console.ReadLine();
        }
    }
}