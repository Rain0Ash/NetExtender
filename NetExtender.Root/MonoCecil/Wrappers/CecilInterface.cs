using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Mono.Cecil;
using Mono.Collections.Generic;
using NetExtender.Types.Storages;

namespace NetExtender.Cecil
{
    internal sealed class CecilInterfaceWrapper : IEquatable<CecilInterface>, IEquatable<CecilInterfaceWrapper>
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator CecilInterfaceWrapper?(InterfaceImplementation? value)
        {
            if (value is null)
            {
                return null;
            }

            CecilInterface @interface = (CecilInterface) value;
            if (Storage.TryGetValue(@interface, out CecilInterfaceWrapper? result))
            {
                return result;
            }

            result = new CecilInterfaceWrapper(@interface, value);
            Storage.AddOrUpdate(result);
            return result.Self(value);
        }

        public static implicit operator CecilInterface(CecilInterfaceWrapper? value)
        {
            return value?.Interface ?? default;
        }

        private static KeyWeakStorage<CecilInterface, CecilInterfaceWrapper> Storage { get; } = new KeyWeakStorage<CecilInterface, CecilInterfaceWrapper>(static wrapper => wrapper.Interface);

        private CecilInterface Interface { get; }

        public MonoCecilType.TypeKey Identifier
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Interface.Identifier;
            }
        }

        public CecilReference InterfaceType { get; private set; } = null!;

        public MetadataToken MetadataToken
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Interface.MetadataToken;
            }
        }

        public Boolean HasCustomAttributes
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return CustomAttributes.Length > 0;
            }
        }

        public ImmutableArray<CecilAttribute> CustomAttributes { get; private set; }

        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        private CecilInterfaceWrapper(CecilInterface @interface, InterfaceImplementation value)
        {
            Interface = @interface;
        }

        private CecilInterfaceWrapper Self(InterfaceImplementation value)
        {
            return Initialize(value.InterfaceType).Initialize(value.CustomAttributes);
        }

        private CecilInterfaceWrapper Initialize(TypeReference @interface)
        {
            InterfaceType = @interface;
            return this;
        }

        private CecilInterfaceWrapper Initialize(Collection<CustomAttribute> attributes)
        {
            ImmutableArray<CecilAttribute>.Builder builder = ImmutableArray.CreateBuilder<CecilAttribute>(attributes.Count);

            foreach (CustomAttribute attribute in attributes)
            {
                builder.Add(attribute);
            }

            CustomAttributes = builder.MoveToImmutable();
            return this;
        }

        public override Int32 GetHashCode()
        {
            return Interface.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                null => false,
                CecilInterface value => Equals(value),
                CecilInterfaceWrapper value => Equals(value),
                _ => false
            };
        }

        public Boolean Equals(CecilInterface other)
        {
            return Interface.Equals(other);
        }

        public Boolean Equals(CecilInterfaceWrapper? other)
        {
            return other is not null && Interface.Equals(other.Interface);
        }

        public override String ToString()
        {
            return Interface.ToString();
        }
    }

    internal readonly struct CecilInterface : IEquatableStruct<CecilInterface>
    {
        public static implicit operator CecilInterface(InterfaceImplementation? value)
        {
            return value is not null ? new CecilInterface(value) : default;
        }

        public MonoCecilType.TypeKey Identifier { get; }
        public MetadataToken MetadataToken { get; }
        public Boolean HasCustomAttributes { get; }

        public Boolean IsEmpty
        {
            get
            {
                return Identifier.IsEmpty;
            }
        }

        private CecilInterface(InterfaceImplementation value)
        {
            Identifier = new MonoCecilType.TypeKey(value.InterfaceType);
            MetadataToken = value.MetadataToken;
            HasCustomAttributes = value.HasCustomAttributes;
        }

        public override Int32 GetHashCode()
        {
            return Identifier.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                null => IsEmpty,
                InterfaceImplementation value => Equals(value),
                CecilInterface value => Equals(value),
                _ => false
            };
        }

        public Boolean Equals(CecilInterface other)
        {
            return Identifier.Equals(other.Identifier);
        }

        public override String ToString()
        {
            return Identifier.ToString();
        }
    }
}