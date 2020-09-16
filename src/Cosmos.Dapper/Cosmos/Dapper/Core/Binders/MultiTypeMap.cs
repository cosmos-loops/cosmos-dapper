using System;
using System.Reflection;
using Dapper;

namespace Cosmos.Dapper.Core.Binders
{
    /// <summary>
    /// Multi type map
    /// </summary>
    public abstract class MultiTypeMap : SqlMapper.ITypeMap
    {
        private readonly SqlMapper.ITypeMap[] _mappers;

        /// <summary>
        /// Create a new instance of <see cref="MultiTypeMap" />
        /// </summary>
        /// <param name="mappers"></param>
        protected MultiTypeMap(params SqlMapper.ITypeMap[] mappers)
        {
            _mappers = mappers;
        }

        /// <summary>
        /// Find constructor by names and parameters' type
        /// </summary>
        /// <param name="names"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public ConstructorInfo FindConstructor(string[] names, Type[] types)
        {
            foreach (var map in _mappers)
            {
                try
                {
                    var ret = map.FindConstructor(names, types);
                    if (ret != null)
                        return ret;
                }
                catch
                {
                    //Ignore
                }
            }

            return null;
        }

        /// <summary>
        /// Find explicit constructor
        /// </summary>
        /// <returns></returns>
        public ConstructorInfo FindExplicitConstructor()
        {
            foreach (var map in _mappers)
            {
                try
                {
                    var ret = map.FindExplicitConstructor();
                    if (ret != null)
                        return ret;
                }
                catch
                {
                    //Ignore
                }
            }

            return null;
        }

        /// <summary>
        /// Gets constructor parameter by given constructor and column name
        /// </summary>
        /// <param name="constructor"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public SqlMapper.IMemberMap GetConstructorParameter(ConstructorInfo constructor, string columnName)
        {
            foreach (var map in _mappers)
            {
                try
                {
                    var ret = map.GetConstructorParameter(constructor, columnName);
                    if (ret != null)
                        return ret;
                }
                catch
                {
                    //Ignore
                }
            }

            return null;
        }

        /// <summary>
        /// Gets member by given column name
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public SqlMapper.IMemberMap GetMember(string columnName)
        {
            foreach (var map in _mappers)
            {
                try
                {
                    var ret = map.GetMember(columnName);
                    if (ret != null)
                        return ret;
                }
                catch
                {
                    //Ignore
                }
            }

            return null;
        }
    }
}