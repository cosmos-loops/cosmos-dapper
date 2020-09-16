using Cosmos.Dapper;

namespace Samples.DependencyUsage.Autofac
{
    public class ResearchContext : SqlServerContext<ResearchContext>
    {
        public ResearchContext(DapperOptions options) : base(options) { }

        public virtual DapperSet<ResearchModel> ResearchModels { get; set; }
    }
}