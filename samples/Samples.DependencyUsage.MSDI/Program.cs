using System;
using Cosmos.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Samples.DependencyUsage.MSDI
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddCosmosDataSupport(config => config.UseDapperWithSqlServer<ResearchContext>(o =>
            {
                o.Name = "DependencyMsdiUsage";
                o.ConnectionString = "server=<ip_address>;database=<db_name>;uid=<uid>;pwd=<pwd>;";
            }));

            var provider = services.BuildServiceProvider();

            using (var scope = provider.CreateScope())
            {
                var ctx = scope.ServiceProvider.GetService(typeof(ResearchContext)) as ResearchContext;

                if (ctx is null)
                {
                    Console.WriteLine("ResearchContext is null");
                }
                else
                {
                    var model0 = ctx.ResearchModels.First(x => x.IsValid, null);
                    var model1 = ctx.ResearchModels.FirstOrDefault(@"SELECT * FROM Research WHERE IsValid = 1");
                    var model2 = ctx.QueryOperators.QueryFirst<ResearchModel>(@"SELECT * FROM Research WHERE IsValid = 1");
                    Console.WriteLine(model0);
                    Console.WriteLine(model1);
                    Console.WriteLine(model2);

                }
            }

            Console.WriteLine("Hello World!");
        }
    }
}