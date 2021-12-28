using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;

namespace NetExtender.Initializer
{
    public static partial class NetExtenderFrameworkInitializer
    {
        private readonly struct AssemblyHash
        {
            public Byte[] Hash { get; }
            public System.Configuration.Assemblies.AssemblyHashAlgorithm Algorithm { get; }
            
            public AssemblyHash(Byte[] hash, System.Configuration.Assemblies.AssemblyHashAlgorithm algorithm)
            {
                Hash = hash ?? throw new ArgumentNullException(nameof(hash));
                Algorithm = algorithm;
            }
        }
        
        //TODO: Autocreate binary hashes
        // ReSharper disable once CollectionNeverUpdated.Local
        private static IDictionary<String, AssemblyHash> Hashes { get; } = new Dictionary<String, AssemblyHash>();

        private static Assembly LoadAssembly(String assembly, AssemblyHashInitialization initialization)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (initialization == AssemblyHashInitialization.Ignore)
            {
                return Assembly.LoadFrom(assembly);
            }
            
            if (!Hashes.TryGetValue(assembly, out AssemblyHash hash))
            {
                if (initialization == AssemblyHashInitialization.Equals)
                {
                    return Assembly.LoadFrom(assembly);
                }
                
                throw new CryptographicException($"Can't find hash for assembly: '{assembly}'");
            }

            try
            {
                return Assembly.LoadFrom(assembly, hash.Hash, hash.Algorithm);
            }
            catch (Exception exception)
            {
                throw new CryptographicException($"Invalid cryptography hash for assembly '{assembly}'", exception);
            }
        }
    }
}