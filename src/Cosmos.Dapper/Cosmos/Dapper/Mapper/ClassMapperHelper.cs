using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using AspectCore.Extensions.Reflection;
using Cosmos.Dapper.Core.Mapping;
using Cosmos.Dapper.EntityMapping;
using Cosmos.Reflection;

namespace Cosmos.Dapper.Mapper
{
    /// <summary>
    /// Class mapper helper
    /// </summary>
    internal static class ClassMapperHelper
    {
        private static IList<Assembly> _mapperAssemblies;

        private static readonly Dictionary<Type, KeyType> _keyTypeMappings;

        static ClassMapperHelper()
        {
            _keyTypeMappings = new Dictionary<Type, KeyType>
            {
                [TypeClass.ByteClazz] = KeyType.Identity,
                [TypeClass.ByteNullableClazz] = KeyType.Identity,
                [TypeClass.SByteClazz] = KeyType.Identity,
                [TypeClass.SByteNullableClazz] = KeyType.Identity,
                [TypeClass.ShortClazz] = KeyType.Identity,
                [TypeClass.ShortNullableClazz] = KeyType.Identity,
                [TypeClass.UShortClazz] = KeyType.Identity,
                [TypeClass.UShortNullableClazz] = KeyType.Identity,
                [TypeClass.IntClazz] = KeyType.Identity,
                [TypeClass.IntNullableClazz] = KeyType.Identity,
                [TypeClass.UIntClazz] = KeyType.Identity,
                [TypeClass.UIntNullableClazz] = KeyType.Identity,
                [TypeClass.LongClazz] = KeyType.Identity,
                [TypeClass.LongNullableClazz] = KeyType.Identity,
                [TypeClass.ULongClazz] = KeyType.Identity,
                [TypeClass.ULongNullableClazz] = KeyType.Identity,
                [typeof(BigInteger)] = KeyType.Identity,
                [typeof(BigInteger?)] = KeyType.Identity,
                [TypeClass.GuidClazz] = KeyType.Identity,
                [TypeClass.GuidNullableClazz] = KeyType.Identity
            };
        }

        public static IList<Assembly> GetMapperAssemblies()
        {
            if (_mapperAssemblies != null)
                return _mapperAssemblies;

            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Concat(GetAllUnlinkedAssemblies());

            _mapperAssemblies = AssemblyFilter(assemblies).ToList();

            return _mapperAssemblies;
        }

        private static IEnumerable<Assembly> AssemblyFilter(IEnumerable<Assembly> assemblies)
            => assemblies.Where(assembly =>
                assembly.FullName != typeof(IClassMap<>).Assembly.FullName &&
                (
                    assembly.GetTypes().Any(m => m.GetInterface(typeof(IClassMap<>).FullName) != null) ||
                    assembly.GetTypes().Any(m => m.GetInterface(typeof(IMap).FullName) != null)
                )
            );

        private static IEnumerable<Assembly> GetAllUnlinkedAssemblies()
        {
            var directoryRoot = new DirectoryInfo(Directory.GetCurrentDirectory());
            var files = directoryRoot.GetFiles("*.dll", SearchOption.AllDirectories);
            foreach (var file in files)
                yield return Assembly.LoadFrom(file.FullName);
        }

        public static void CallForEntity<T>(T entity, Action<T> caller, bool flag = true) where T : class
        {
            if (flag)
            {
                caller?.Invoke(entity);
            }
        }

        public static void CallForEntity<T>(IEnumerable<T> entities, Action<T> caller, bool flag = true) where T : class
        {
            if (caller == null || !flag)
                return;

            foreach (var entity in entities)
                CallForEntity(entity, caller);
        }

        public static Dictionary<Type, KeyType> GetKeyTypeMapping() => _keyTypeMappings;

        public static (string SchemaName, bool CaseSensitive) GetSchemaInfo(IClassMap classMap) => GetSchemaInfo(classMap.EntityType);

        public static (string, bool) GetSchemaInfo(Type type)
        {
            var attr = type.GetReflector().GetCustomAttribute<SchemaAttribute>();
            return (attr?.Name ?? string.Empty, attr?.CaseSensitive ?? true);
        }

        public static (string TableName, bool CaseSensitive) GetTableInfo(IClassMap classMap) => GetTableInfo(classMap.EntityType);

        public static (string, bool) GetTableInfo(Type type)
        {
            var attr = type.GetReflector().GetCustomAttribute<TableAttribute>();
            return (attr?.Name ?? string.Empty, attr?.CaseSensitive ?? true);
        }
    }
}