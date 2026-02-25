using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Mono.Cecil;
using Mono.Collections.Generic;
using NetExtender.Types.Storages;

namespace NetExtender.Cecil
{
    internal sealed class CecilAttributeWrapper : IEquatable<CecilAttribute>, IEquatable<CecilAttributeWrapper>
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator CecilAttributeWrapper?(CustomAttribute? value)
        {
            if (value is null)
            {
                return null;
            }

            CecilAttribute @interface = (CecilAttribute) value;
            if (Storage.TryGetValue(@interface, out CecilAttributeWrapper? result))
            {
                return result;
            }

            result = new CecilAttributeWrapper(@interface, value);
            Storage.AddOrUpdate(result);
            return result.Self(value);
        }

        public static implicit operator CecilAttribute(CecilAttributeWrapper? value)
        {
            return value?.Attribute ?? default;
        }

        private static KeyWeakStorage<CecilAttribute, CecilAttributeWrapper> Storage { get; } = new KeyWeakStorage<CecilAttribute, CecilAttributeWrapper>(static wrapper => wrapper.Attribute);

        private CecilAttribute Attribute { get; }

        public MonoCecilType.TypeKey Identifier
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Attribute.Identifier;
            }
        }

        public CecilReference AttributeType { get; private set; } = null!;

        public Boolean HasConstructorArguments
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ConstructorArguments.Length > 0;
            }
        }

        public ImmutableArray<CecilAttributeArgument> ConstructorArguments { get; private set; }

        public Boolean HasFields
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Fields.Length > 0;
            }
        }

        public ImmutableArray<CecilAttributeNamedArgument> Fields { get; private set; }

        public Boolean HasProperties
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Properties.Length > 0;
            }
        }

        public ImmutableArray<CecilAttributeNamedArgument> Properties { get; private set; }

        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        private CecilAttributeWrapper(CecilAttribute attribute, CustomAttribute value)
        {
            Attribute = attribute;
        }

        private CecilAttributeWrapper Self(CustomAttribute value)
        {
            return Initialize(value.AttributeType).InitializeArguments(value.ConstructorArguments).InitializeFields(value.Fields).InitializeProperties(value.Properties);
        }

        private CecilAttributeWrapper Initialize(TypeReference @interface)
        {
            AttributeType = @interface;
            return this;
        }

        private CecilAttributeWrapper InitializeArguments(Collection<CustomAttributeArgument> arguments)
        {
            ImmutableArray<CecilAttributeArgument>.Builder builder = ImmutableArray.CreateBuilder<CecilAttributeArgument>(arguments.Count);

            foreach (CustomAttributeArgument argument in arguments)
            {
                builder.Add(new CecilAttributeArgument(argument.Type, argument.Value));
            }

            ConstructorArguments = builder.MoveToImmutable();
            return this;
        }

        private CecilAttributeWrapper InitializeFields(Collection<CustomAttributeNamedArgument> arguments)
        {
            ImmutableArray<CecilAttributeNamedArgument>.Builder builder = ImmutableArray.CreateBuilder<CecilAttributeNamedArgument>(arguments.Count);

            foreach (CustomAttributeNamedArgument argument in arguments)
            {
                builder.Add(new CecilAttributeNamedArgument(argument.Name, argument.Argument.Type, argument.Argument.Value));
            }

            Fields = builder.MoveToImmutable();
            return this;
        }

        private CecilAttributeWrapper InitializeProperties(Collection<CustomAttributeNamedArgument> arguments)
        {
            ImmutableArray<CecilAttributeNamedArgument>.Builder builder = ImmutableArray.CreateBuilder<CecilAttributeNamedArgument>(arguments.Count);

            foreach (CustomAttributeNamedArgument argument in arguments)
            {
                builder.Add(new CecilAttributeNamedArgument(argument.Name, argument.Argument.Type, argument.Argument.Value));
            }

            Properties = builder.MoveToImmutable();
            return this;
        }

        public override Int32 GetHashCode()
        {
            return Attribute.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                null => false,
                CecilAttribute value => Equals(value),
                CecilAttributeWrapper value => Equals(value),
                _ => false
            };
        }

        public Boolean Equals(CecilAttribute other)
        {
            return Attribute.Equals(other);
        }

        public Boolean Equals(CecilAttributeWrapper? other)
        {
            return other is not null && Attribute.Equals(other.Attribute);
        }

        public override String ToString()
        {
            return Attribute.ToString();
        }
    }

    internal readonly struct CecilAttribute : IEquatableStruct<CecilAttribute>
    {
        public static implicit operator CecilAttribute(CustomAttribute? value)
        {
            return value is not null ? new CecilAttribute(value) : default;
        }

        public MonoCecilType.TypeKey Identifier { get; }
        public Boolean HasConstructorArguments { get; }
        public Boolean HasFields { get; }
        public Boolean HasProperties { get; }

        public Boolean IsEmpty
        {
            get
            {
                return Identifier.IsEmpty;
            }
        }

        private CecilAttribute(CustomAttribute value)
        {
            Identifier = new MonoCecilType.TypeKey(value.AttributeType);
            HasConstructorArguments = value.HasConstructorArguments;
            HasFields = value.HasFields;
            HasProperties = value.HasProperties;
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
                CustomAttribute value => Equals(value),
                CecilAttribute value => Equals(value),
                _ => false
            };
        }

        public Boolean Equals(CecilAttribute other)
        {
            return Identifier.Equals(other.Identifier);
        }

        public override String ToString()
        {
            return Identifier.ToString();
        }
    }

    internal readonly struct CecilAttributeNamedArgument : IEquatableStruct<CecilAttributeNamedArgument>, IEquatable<CecilAttributeArgument>
    {
        public static implicit operator CecilAttributeArgument(CecilAttributeNamedArgument value)
        {
            return value._argument;
        }

        private readonly CecilAttributeArgument _argument;
        public String? Name { get; }

        public CecilReference Type
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _argument.Type;
            }
        }

        public Object? Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _argument.Value;
            }
        }

        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _argument.IsEmpty && Name is null;
            }
        }

        public CecilAttributeNamedArgument(String? name, CecilAttributeArgument argument)
        {
            Name = name;
            _argument = argument;
        }

        public CecilAttributeNamedArgument(String? name, CecilReference type, Object? value)
        {
            Name = name;
            _argument = new CecilAttributeArgument(type, value);
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Name, Type, Value);
        }

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                null => IsEmpty,
                CecilAttributeArgument value => Equals(value),
                CecilAttributeNamedArgument value => Equals(value),
                _ => false
            };
        }

        public Boolean Equals(CecilAttributeArgument other)
        {
            return _argument.Equals(other);
        }

        public Boolean Equals(CecilAttributeNamedArgument other)
        {
            return Name == other.Name && _argument.Equals(other._argument);
        }

        public override String ToString()
        {
            return $"{{ {nameof(Name)} = {Name}, {nameof(Type)} = {Type}, {nameof(Value)} = {Value} }}";
        }
    }

    internal readonly struct CecilAttributeArgument : IEquatableStruct<CecilAttributeArgument>
    {
        public CecilReference Type { get; }
        public Object? Value { get; }

        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Type is null && Value is null;
            }
        }

        public CecilAttributeArgument(CecilReference type, Object? value)
        {
            Type = type;
            Value = Handle(value);
        }

        private static Object? Handle(Object? value)
        {
            switch (value)
            {
                case TypeReference type:
                {
                    return (CecilReference) type;
                }
                case CustomAttributeArgument[] arguments:
                {
                    CecilAttributeArgument[] result = new CecilAttributeArgument[arguments.Length];

                    for (Int32 i = 0; i < arguments.Length; i++)
                    {
                        result[i] = new CecilAttributeArgument(arguments[i].Type, arguments[i].Value);
                    }

                    return result;
                }
                default:
                {
                    return value;
                }
            }
        }

        public override Int32 GetHashCode()
        {
            if (Value is not CecilAttributeArgument[] array)
            {
                return HashCode.Combine(Type, Value);
            }

            HashCode hash = new HashCode();
            hash.Add(Type);

            foreach (CecilAttributeArgument item in array)
            {
                hash.Add(item);
            }

            return hash.ToHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                null => IsEmpty,
                CecilAttributeArgument value => Equals(value),
                CecilAttributeNamedArgument value => Equals(value),
                _ => false
            };
        }

        public Boolean Equals(CecilAttributeArgument other)
        {
            if (!Equals(Type, other.Type))
            {
                return false;
            }

            if (Equals(Value, other.Value))
            {
                return true;
            }

            if (Value is not CecilAttributeArgument[] first || other.Value is not CecilAttributeArgument[] second)
            {
                return false;
            }

            if (first.Length != second.Length)
            {
                return false;
            }

            // ReSharper disable once LoopCanBeConvertedToQuery
            for (Int32 i = 0; i < first.Length; i++)
            {
                if (!first[i].Equals(second[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public override String ToString()
        {
            String content = Value is CecilAttributeArgument[] array ? $"{{ {String.Join(", ", array)} }}" : Value?.ToString() ?? String.Empty;
            return $"{{ {nameof(Type)} = {Type}, {nameof(Value)} = {content} }}";
        }
    }
}