using System;
using AspectCore.DependencyInjection;
using AspectCore.Injector;
using Cosmos.Data;

namespace Samples.DependencyUsage.AspectCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ServiceContext();
            builder.AddCosmosDataSupport(config => config.UseDapperWithSqlServer<ResearchContext>(o =>
            {
                o.Name = "DependencyNccAspectCoreUsage";
                o.ConnectionString = "server=<ip_address>;database=<db_name>;uid=<uid>;pwd=<pwd>;";
            }));

            var resolver = builder.Build();

            Console.WriteLine("Hello World!");
        }
    }
}