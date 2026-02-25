using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using Mono.Cecil;

namespace NetExtender.Cecil
{
    public readonly struct CecilAssemblyIdentifier : IEqualityStruct<CecilAssemblyIdentifier>
    {
        public static implicit operator CecilAssemblyIdentifier(AssemblyName? value)
        {
            return value is not null ? new CecilAssemblyIdentifier(value) : default;
        }

        public static implicit operator CecilAssemblyIdentifier(AssemblyNameDefinition? value)
        {
            return value is not null ? new CecilAssemblyIdentifier(value) : default;
        }

        public String? Name { get; }

        private readonly String? _name = null;
        public String FullName
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _name ?? Name ?? String.Empty;
            }
        }

        public Version? Version { get; }

        private readonly String? _culture = null;
        public String? CultureName
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _culture ?? CultureInfo?.Name;
            }
        }

        public CultureInfo? CultureInfo { get; } = null;
        public AssemblyNameFlags? Flags { get; } = null;
        public AssemblyContentType? ContentType { get; } = null;

#if !NET6_0_OR_GREATER
        public ProcessorArchitecture? ProcessorArchitecture { get; } = null;
        public System.Configuration.Assemblies.AssemblyVersionCompatibility? VersionCompatibility { get; } = null;
        public System.Reflection.AssemblyHashAlgorithm HashAlgorithm { get; }
#endif

        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return String.IsNullOrEmpty(FullName) && Version is null;
            }
        }

        private CecilAssemblyIdentifier(AssemblyName assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            Name = MonoCecilType.TypeKey.AssemblyPool.GetOrAdd(assembly.Name);
            _name = !String.IsNullOrEmpty(assembly.FullName) ? MonoCecilType.TypeKey.AssemblyPool.GetOrAdd(assembly.FullName) : null;
            Version = assembly.Version;

            if (assembly.CultureInfo is { } culture)
            {
                CultureInfo = culture;
            }
            else
            {
                _culture = assembly.CultureName;
            }

            Flags = assembly.Flags;
            ContentType = assembly.ContentType;

#if !NET6_0_OR_GREATER
            ProcessorArchitecture = assembly.ProcessorArchitecture;
            VersionCompatibility = assembly.VersionCompatibility;
            HashAlgorithm = Algorithm(assembly.HashAlgorithm);
#endif
        }

        private CecilAssemblyIdentifier(AssemblyNameDefinition assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            Name = MonoCecilType.TypeKey.AssemblyPool.GetOrAdd(assembly.Name);
            _name = !String.IsNullOrEmpty(assembly.FullName) ? MonoCecilType.TypeKey.AssemblyPool.GetOrAdd(assembly.FullName) : null;
            Version = assembly.Version;

            try
            {
                CultureInfo = CultureInfo.GetCultureInfo(assembly.Culture);
            }
            catch (Exception)
            {
                _culture = assembly.Culture;
            }

#if !NET6_0_OR_GREATER
            HashAlgorithm = Algorithm(assembly.HashAlgorithm);
#endif
        }

#if !NET6_0_OR_GREATER
        private static System.Reflection.AssemblyHashAlgorithm Algorithm(System.Configuration.Assemblies.AssemblyHashAlgorithm algorithm)
        {
            return algorithm switch
            {
                System.Configuration.Assemblies.AssemblyHashAlgorithm.None => System.Reflection.AssemblyHashAlgorithm.None,
                System.Configuration.Assemblies.AssemblyHashAlgorithm.MD5 => System.Reflection.AssemblyHashAlgorithm.MD5,
                System.Configuration.Assemblies.AssemblyHashAlgorithm.SHA1 => System.Reflection.AssemblyHashAlgorithm.Sha1,
                System.Configuration.Assemblies.AssemblyHashAlgorithm.SHA256 => System.Reflection.AssemblyHashAlgorithm.Sha256,
                System.Configuration.Assemblies.AssemblyHashAlgorithm.SHA384 => System.Reflection.AssemblyHashAlgorithm.Sha384,
                System.Configuration.Assemblies.AssemblyHashAlgorithm.SHA512 => System.Reflection.AssemblyHashAlgorithm.Sha512,
                _ => (System.Reflection.AssemblyHashAlgorithm) algorithm
            };
        }

        private static System.Reflection.AssemblyHashAlgorithm Algorithm(Mono.Cecil.AssemblyHashAlgorithm algorithm)
        {
            return algorithm switch
            {
                Mono.Cecil.AssemblyHashAlgorithm.None => System.Reflection.AssemblyHashAlgorithm.None,
                Mono.Cecil.AssemblyHashAlgorithm.MD5 => System.Reflection.AssemblyHashAlgorithm.MD5,
                Mono.Cecil.AssemblyHashAlgorithm.SHA1 => System.Reflection.AssemblyHashAlgorithm.Sha1,
                Mono.Cecil.AssemblyHashAlgorithm.SHA256 => System.Reflection.AssemblyHashAlgorithm.Sha256,
                Mono.Cecil.AssemblyHashAlgorithm.SHA384 => System.Reflection.AssemblyHashAlgorithm.Sha384,
                Mono.Cecil.AssemblyHashAlgorithm.SHA512 => System.Reflection.AssemblyHashAlgorithm.Sha512,
                _ => (System.Reflection.AssemblyHashAlgorithm) algorithm
            };
        }
#endif

        public Int32 CompareTo(CecilAssemblyIdentifier other)
        {
            if (IsEmpty)
            {
                return other.IsEmpty ? 0 : 1;
            }

            Int32 compare = StringComparer.Ordinal.Compare(FullName, other.FullName);
            return compare != 0 ? compare : Comparer<Version>.Default.Compare(Version, other.Version);
        }

        public override Int32 GetHashCode()
        {
            return !IsEmpty ? HashCode.Combine(FullName, Version) : 0;
        }

        public override Boolean Equals(Object? other)
        {
            return other is CecilAssemblyIdentifier value && Equals(value);
        }

        public Boolean Equals(CecilAssemblyIdentifier other)
        {
            return other.FullName == FullName && other.Version == Version;
        }

        public override String ToString()
        {
            return FullName;
        }
    }
}