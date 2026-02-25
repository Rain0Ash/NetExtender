using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using Mono.Cecil;
using Mono.Collections.Generic;
using NetExtender.Types.Storages;

namespace NetExtender.Cecil
{
    internal sealed class CecilAssemblyWrapper : IEquality<CecilAssembly>, IEquality<CecilAssemblyWrapper>
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator CecilAssemblyWrapper?(AssemblyDefinition? value)
        {
            if (value is null)
            {
                return null;
            }

            CecilAssembly assembly = (CecilAssembly) value;
            if (Storage.TryGetValue(assembly, out CecilAssemblyWrapper? result))
            {
                return result;
            }

            result = new CecilAssemblyWrapper(assembly, value);
            Storage.AddOrUpdate(result);
            return result.Self(value);
        }

        public static implicit operator CecilAssemblyWrapper?((AssemblyDefinition? Assembly, String? Location) value)
        {
            if (value.Assembly is null)
            {
                return null;
            }

            CecilAssembly assembly = (CecilAssembly) value.Assembly;
            if (Storage.TryGetValue(assembly, out CecilAssemblyWrapper? result))
            {
                return result;
            }

            result = new CecilAssemblyWrapper(assembly, value.Assembly);
            Storage.AddOrUpdate(result);
            return result.Initialize(value.Assembly.CustomAttributes);
        }

        public static implicit operator CecilAssembly(CecilAssemblyWrapper? value)
        {
            return value?.Assembly ?? default;
        }

        private static KeyWeakStorage<CecilAssembly, CecilAssemblyWrapper> Storage { get; } = new KeyWeakStorage<CecilAssembly, CecilAssemblyWrapper>(static wrapper => wrapper.Assembly);

        private readonly CecilAssembly Assembly;

        public String? Name
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Assembly.Name;
            }
        }

        public String FullName
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Assembly.FullName;
            }
        }

        public Version? Version
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Assembly.Version;
            }
        }

        public String? Location
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Assembly.Location;
            }
        }

        public MetadataToken? MetadataToken
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Assembly.MetadataToken;
            }
        }

        public CecilModule MainModule { get; }
        public ImmutableArray<CecilModule> Modules { get; }

        public Boolean HasCustomAttributes
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return CustomAttributes.Length > 0;
            }
        }

        public ImmutableArray<CecilAttributeWrapper> CustomAttributes { get; private set; }

        private CecilAssemblyWrapper(CecilAssembly assembly, AssemblyDefinition value)
        {
            Assembly = assembly;
            MainModule = new CecilModule(value.MainModule, this);

            Collection<ModuleDefinition> modules = value.Modules;
            ImmutableArray<CecilModule>.Builder builder = ImmutableArray.CreateBuilder<CecilModule>(modules.Count);

            foreach (ModuleDefinition module in modules)
            {
                builder.Add(new CecilModule(module, this));
            }

            Modules = builder.MoveToImmutable();
        }

        private CecilAssemblyWrapper Self(AssemblyDefinition value)
        {
            return Initialize(value.CustomAttributes);
        }

        private CecilAssemblyWrapper Initialize(Collection<CustomAttribute> attributes)
        {
            ImmutableArray<CecilAttributeWrapper>.Builder builder = ImmutableArray.CreateBuilder<CecilAttributeWrapper>(attributes.Count);

            foreach (CustomAttribute attribute in attributes)
            {
                builder.Add(attribute);
            }

            CustomAttributes = builder.MoveToImmutable();
            return this;
        }

        public AssemblyDefinition? Make()
        {
            return Assembly.Make();
        }

        public Int32 CompareTo(CecilAssembly other)
        {
            return Assembly.CompareTo(other);
        }

        public Int32 CompareTo(CecilAssemblyWrapper? other)
        {
            return other is not null ? Assembly.CompareTo(other.Assembly) : 1;
        }

        public override Int32 GetHashCode()
        {
            return Assembly.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                null => false,
                CecilAssembly value => Equals(value),
                CecilAssemblyWrapper value => Equals(value),
                _ => false
            };
        }

        public Boolean Equals(CecilAssembly other)
        {
            return Assembly.Equals(other);
        }

        public Boolean Equals(CecilAssemblyWrapper? other)
        {
            return other is not null && Assembly.Equals(other.Assembly);
        }

        public override String ToString()
        {
            return Assembly.ToString();
        }
    }

    internal readonly struct CecilAssembly : IEqualityStruct<CecilAssembly>
    {
        public static implicit operator CecilAssembly(Assembly? value)
        {
            return value is not null ? new CecilAssembly(value) : default;
        }

        public static implicit operator CecilAssembly(AssemblyDefinition? value)
        {
            return value is not null ? new CecilAssembly(value) : default;
        }

        public static implicit operator CecilAssembly((AssemblyDefinition? Assembly, String? Location) value)
        {
            return value.Assembly is not null ? new CecilAssembly(value.Assembly) { Location = value.Location } : default;
        }

        private readonly CecilAssemblyIdentifier Identifier;

        public String? Name
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Identifier.Name;
            }
        }

        public String FullName
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Identifier.FullName;
            }
        }

        public Version? Version
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Identifier.Version;
            }
        }

        public String? CultureName
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Identifier.CultureName;
            }
        }

        public CultureInfo? CultureInfo
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Identifier.CultureInfo;
            }
        }

        private readonly String? _location = null;
        public String? Location
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _location;
            }
            private init
            {
                _location = !String.IsNullOrWhiteSpace(value) ? value : null;
            }
        }

        public MetadataToken? MetadataToken { get; }
        public Boolean HasCustomAttributes { get; }

        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Name is null;
            }
        }

        private CecilAssembly(Assembly value)
        {
            Identifier = value.GetName();

            try
            {
                String location = value.Location;
                _location = !String.IsNullOrWhiteSpace(location) ? location : null;
            }
            catch (Exception)
            {
            }

            MetadataToken = null;
            HasCustomAttributes = value.GetCustomAttributesData().Count > 0;
        }

        private CecilAssembly(AssemblyDefinition value)
        {
            Identifier = value.Name;
            MetadataToken = value.MetadataToken;
            HasCustomAttributes = value.HasCustomAttributes;
        }

        public AssemblyDefinition? Make()
        {
            try
            {
                return !String.IsNullOrEmpty(Location) ? AssemblyDefinition.ReadAssembly(Location) : null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Int32 CompareTo(CecilAssembly other)
        {
            return Identifier.CompareTo(other.Identifier);
        }

        public override Int32 GetHashCode()
        {
            return MetadataToken.HasValue ? HashCode.Combine(Identifier, MetadataToken.Value) : Identifier.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                null => IsEmpty,
                CecilAssembly value => Equals(value),
                _ => false
            };
        }

        public Boolean Equals(CecilAssembly other)
        {
            return Identifier.Equals(other.Identifier) && (MetadataToken is null || other.MetadataToken is null || MetadataToken == other.MetadataToken);
        }

        public override String ToString()
        {
            return Identifier.ToString();
        }
    }
}