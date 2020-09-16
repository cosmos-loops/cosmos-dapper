using System.Collections.Generic;
using Dapper;

namespace Cosmos.Dapper.Core.Helpers
{
    /// <summary>
    /// Dynamic parameter converter
    /// </summary>
    internal static class DynamicParameterConverter
    {
        /// <summary>
        /// To dynamic parameters
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DynamicParameters ToDynamicParameters(this IDictionary<string, object> parameters)
        {
            var dynamicParameters = new DynamicParameters();
            foreach (var parameter in parameters)
                dynamicParameters.Add(parameter.Key, parameter.Value);
            return dynamicParameters;
        }
    }
}