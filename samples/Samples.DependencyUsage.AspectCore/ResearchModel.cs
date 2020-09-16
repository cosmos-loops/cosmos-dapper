using Cosmos.Dapper.EntityMapping;
using Cosmos.Domain.Core;

namespace Samples.DependencyUsage.AspectCore
{
    [Table("Research")]
    public class ResearchModel : IEntity
    {
        public string Id { get; set; }

        [Column("Name")]
        public string ModelName { get; set; }

        public bool IsValid { get; set; }
        public long Version { get; set; }

        public override string ToString()
        {
            return $"{Id}-{ModelName},v={Version}";
        }

        public void Init() { }
    }
}