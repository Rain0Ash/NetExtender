// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using NetExtender.Types.Exceptions;

namespace NetExtender.Initializer
{
    public record AssemblyVerifyInfo
    {
        public const String NetExtenderPublicKeyToken = "fcac122b9de889d5";
        public String? Sign { get; }
        public Byte[]? Hash { get; }
        public System.Configuration.Assemblies.AssemblyHashAlgorithm Algorithm { get; }

        public AssemblyVerifyInfo(String? sign, Byte[]? hash, System.Configuration.Assemblies.AssemblyHashAlgorithm algorithm)
        {
            Sign = sign;
            Hash = hash;
            Algorithm = algorithm;
        }
    }

    public static partial class NetExtenderFrameworkInitializer
    {
        //TODO: Autocreate binary hashes
        // ReSharper disable once CollectionNeverUpdated.Local

        private static Assembly? Load(String assembly, Boolean isThrow)
        {
            return Load(assembly, null, default, isThrow);
        }

        private static Assembly? Load(String assembly, Byte[]? hash, System.Configuration.Assemblies.AssemblyHashAlgorithm algorithm, Boolean isThrow)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            try
            {
                return hash is not null ? Assembly.LoadFrom(assembly, hash, algorithm) : Assembly.LoadFrom(assembly);
            }
            catch (FileNotFoundException)
            {
                throw;
            }
            catch (Exception exception)
            {
                if (isThrow)
                {
                    throw new CryptographicException($"Invalid cryptography for assembly '{assembly}'", exception);
                }

                return null;
            }
        }

        // ReSharper disable once CognitiveComplexity
        private static Assembly LoadAssemblySign(String assembly, Boolean? strong)
        {
            Byte[]? bytes = AssemblyName.GetAssemblyName(assembly).GetPublicKeyToken();

            if (bytes is null)
            {
                return strong == false ? Load(assembly, true)! : throw new CryptographicException($"Invalid cryptography for assembly '{assembly}'");
            }

            String token = Convert.ToHexString(bytes);
            if (Assemblies.TryGetValue(assembly, out AssemblyVerifyInfo? info) && (info is null || !String.IsNullOrEmpty(info.Sign)))
            {
                if (info is null || String.Equals(token, info.Sign, StringComparison.OrdinalIgnoreCase))
                {
                    return Load(assembly, true)!;
                }

                if (strong == true)
                {
                    throw new CryptographicException($"Invalid cryptography for assembly '{assembly}'");
                }
            }

            if (strong == true)
            {
                throw new CryptographicException($"Can't find cryptography for assembly: '{assembly}'");
            }

            if (String.Equals(token, AssemblyVerifyInfo.NetExtenderPublicKeyToken, StringComparison.OrdinalIgnoreCase))
            {
                return Load(assembly, true)!;
            }

            if (strong != false)
            {
                throw new CryptographicException($"Invalid cryptography for assembly '{assembly}'");
            }

            return Load(assembly, true)!;
        }

        private static Assembly LoadAssemblyHash(String assembly, Boolean? strong)
        {
            if (Assemblies.TryGetValue(assembly, out AssemblyVerifyInfo? info) && (info is null || info.Hash is not null))
            {
                Assembly? result = Load(assembly, info?.Hash, info?.Algorithm ?? System.Configuration.Assemblies.AssemblyHashAlgorithm.None, strong != false);

                if (result is not null)
                {
                    return result;
                }
            }

            if (strong != false)
            {
                throw new CryptographicException($"Can't find cryptography for assembly: '{assembly}'");
            }

            return Load(assembly, true)!;
        }

        private static Assembly LoadAssembly(String assembly, AssemblySignInitialization initialization)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return initialization switch
            {
                AssemblySignInitialization.None => Assembly.LoadFrom(assembly),
                AssemblySignInitialization.Weak => Assembly.LoadFrom(assembly),
                AssemblySignInitialization.Strong => Assembly.LoadFrom(assembly),
                AssemblySignInitialization.Sign => LoadAssemblySign(assembly, null),
                AssemblySignInitialization.WeakSign => LoadAssemblySign(assembly, false),
                AssemblySignInitialization.StrongSign => LoadAssemblySign(assembly, true),
                AssemblySignInitialization.Hash => LoadAssemblyHash(assembly, null),
                AssemblySignInitialization.WeakHash => LoadAssemblyHash(assembly, false),
                AssemblySignInitialization.StrongHash => LoadAssemblyHash(assembly, true),
                _ => throw new AssemblySignInitializationNotSupportedException()
            };
        }
    }
}