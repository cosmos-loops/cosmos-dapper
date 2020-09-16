using System;
using Autofac;
using Cosmos.Data;

namespace Samples.DependencyUsage.Autofac
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.AddCosmosDataSupport(config => config.UseDapperWithSqlServer<ResearchContext>(o =>
            {
                o.Name = "DependencyAutofacUsage";
                o.ConnectionString = "server=<ip_address>;database=<db_name>;uid=<uid>;pwd=<pwd>;";
            }));

            var container = builder.Build();

            Console.WriteLine("Hello World!");
        }
    }
}