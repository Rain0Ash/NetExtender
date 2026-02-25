using System;
using System.Runtime.CompilerServices;
using Mono.Cecil;

namespace NetExtender.Cecil
{
    internal readonly struct CecilModule : IEqualityStruct<CecilModule>
    {
        public static implicit operator CecilModule(ModuleDefinition? value)
        {
            return value is not null ? new CecilModule(value) : default;
        }

        public static implicit operator CecilModule((ModuleDefinition? Module, String? Location) value)
        {
            return value.Module is not null ? new CecilModule(value.Module) { Location = value.Location } : default;
        }

        public String Name { get; }

        private readonly String? _location = null;
        public String? Location
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _location;
            }
            init
            {
                _location = !String.IsNullOrWhiteSpace(value) ? value : null;
            }
        }

        public MetadataToken MetadataToken { get; }
        public MetadataScopeType MetadataScopeType { get; }
        public CecilAssemblyWrapper? Assembly { get; }

        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Name is null && MetadataToken.ToInt32() == 0;
            }
        }

        private CecilModule(ModuleDefinition value)
            : this(value, value.Assembly)
        {
        }

        internal CecilModule(ModuleDefinition value, CecilAssemblyWrapper? assembly)
        {
            Name = MonoCecilType.TypeKey.ModulePool.GetOrAdd(value.Name);
            MetadataToken = value.MetadataToken;
            MetadataScopeType = value.MetadataScopeType;
            Assembly = assembly;
        }

        public CecilModule? Make()
        {
            try
            {
                return !String.IsNullOrEmpty(Location) ? ModuleDefinition.ReadModule(Location) : null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Int32 CompareTo(CecilModule other)
        {
            return String.Compare(Name, other.Name, StringComparison.Ordinal);
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Name, MetadataToken);
        }

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                null => IsEmpty,
                CecilModule module => Equals(module),
                _ => false
            };
        }

        public Boolean Equals(CecilModule other)
        {
            return Name == other.Name && MetadataToken == other.MetadataToken && MetadataScopeType == other.MetadataScopeType;
        }

        public override String ToString()
        {
            return Name ?? String.Empty;
        }
    }
}