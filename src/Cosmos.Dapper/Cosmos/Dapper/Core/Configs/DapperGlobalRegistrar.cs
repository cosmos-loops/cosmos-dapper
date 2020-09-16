using Cosmos.Data.Core;
using Cosmos.Data.Core.Registrars;
using Cosmos.Dependency;

namespace Cosmos.Dapper.Core.Configs
{
    /// <summary>
    /// Dapper global registrar
    /// </summary>
    public static class DapperGlobalRegistrar
    {
        /// <summary>
        /// Register for Cosmos Dapper
        /// </summary>
        public static void RegisterForCosmosDapper()
        {
            SystemSupportRegistrar.AddDescriptorOnce("CosmosDapper", config =>
            {
                var optAccessorDescriptor = DependencyProxyDescriptor.CreateForInstanceSelf<DapperOptionsAccessor>(DependencyLifetimeType.Singleton);
                config.Configure(bag => bag.Register(optAccessorDescriptor));
            });
        }
    }
}