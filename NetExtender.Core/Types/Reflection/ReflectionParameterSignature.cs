using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Utilities.Core;

namespace NetExtender.Types.Reflection
{
    public readonly struct ReflectionParameterSignature : IEquatable<ReflectionParameterSignature>, IEquatable<ParameterInfo>, IEquatable<Type>, IEquatable<String>
    {
        public static implicit operator ParameterDifferenceStrictMode(ReflectionParameterSignature value)
        {
            return value.Strict;
        }

        public static implicit operator ReflectionParameterSignature(String? value)
        {
            return new ReflectionParameterSignature(value);
        }
        
        public static implicit operator ReflectionParameterSignature(TypeSignature value)
        {
            return new ReflectionParameterSignature(value);
        }
        
        public static implicit operator ReflectionParameterSignature(Type? value)
        {
            return new ReflectionParameterSignature(value);
        }
        
        public static implicit operator ReflectionParameterSignature(ParameterInfo? value)
        {
            return new ReflectionParameterSignature(value);
        }
        
        public static explicit operator ReflectionParameterSignature(Predicate<String>? value)
        {
            return new ReflectionParameterSignature(value);
        }

        public static explicit operator ReflectionParameterSignature(Func<String, Boolean>? value)
        {
            return new ReflectionParameterSignature(value);
        }

        public static implicit operator ReflectionParameterSignature(Predicate<Type>? value)
        {
            return new ReflectionParameterSignature(value);
        }

        public static explicit operator ReflectionParameterSignature(Func<Type, Boolean>? value)
        {
            return new ReflectionParameterSignature(value);
        }

        public static explicit operator ReflectionParameterSignature(Predicate<ParameterInfo>? value)
        {
            return new ReflectionParameterSignature(value);
        }

        public static explicit operator ReflectionParameterSignature(Func<ParameterInfo, Boolean>? value)
        {
            return new ReflectionParameterSignature(value);
        }

        public static Boolean operator ==(ReflectionParameterSignature first, ReflectionParameterSignature second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(ReflectionParameterSignature first, ReflectionParameterSignature second)
        {
            return !(first == second);
        }

        public static ReflectionParameterSignature Default
        {
            get
            {
                return default;
            }
        }

        public static ReflectionParameterSignature[]? Any
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return null;
            }
        }

#pragma warning disable CA1825
        // ReSharper disable once UseArrayEmptyMethod
        public static ReflectionParameterSignature[] NotEmpty { get; } = new ReflectionParameterSignature[0];
#pragma warning restore CA1825

        private Object? Parameter { get; }
        public ParameterDifferenceStrictMode Strict { get; }

        public Boolean IsEmpty
        {
            get
            {
                return Parameter is null && Strict is default(ParameterDifferenceStrictMode);
            }
        }

        private ReflectionParameterSignature(Object? parameter)
        {
            Parameter = parameter;
            Strict = ParameterDifferenceStrictMode.Name;
        }

        public override Int32 GetHashCode()
        {
            return Parameter?.GetHashCode() ?? 0;
        }

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                null => Parameter is null,
                ReflectionParameterSignature value => Equals(value),
                ParameterInfo value => Equals(value),
                Type value => Equals(value),
                String value => Equals(value),
                _ => Equals(Parameter, other)
            };
        }

        public Boolean Equals(ReflectionParameterSignature other)
        {
            return other.Parameter switch
            {
                null => Parameter is null,
                ParameterInfo value => Equals(value),
                Type value => Equals(value),
                String value => Equals(value),
                _ => Equals(Parameter, other.Parameter)
            };
        }

        public Boolean Equals(ParameterInfo? other)
        {
            return Parameter switch
            {
                null => true,
                ParameterInfo value => other is not null && value.Equality(other, Strict),
                Predicate<ParameterInfo> value => other is not null && value(other),
                Func<ParameterInfo, Boolean> value => other is not null && value(other),
                _ => other is not null ? Equals(other.ParameterType) || Equals(other.Name) : Equals(default(Type))
            };
        }

        public Boolean Equals(Type? other)
        {
            return Parameter switch
            {
                null => true,
                ParameterInfo value => value.ParameterType == other,
                Type value when other is not null && other.IsGenericTypeDefinition => value.TryGetGenericTypeDefinition() == other,
                Type value => value == other,
                TypeSignature value => other is not null && other.HasSignature(value),
                Predicate<Type> value => other is not null && value(other),
                Func<Type, Boolean> value => other is not null && value(other),
                _ => other is not null ? Equals(other.AssemblyQualifiedName) || Equals(other.FullName) || Equals(other.Name) : Equals(default(String))
            };
        }

        public Boolean Equals(String? other)
        {
            StringComparison comparison = Strict.HasFlag(ParameterDifferenceStrictMode.Name) ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
            return Parameter switch
            {
                null => true,
                ParameterInfo value => String.Equals(value.Name, other, comparison) || String.Equals(value.ParameterType.FullName, other, comparison) || String.Equals(value.ParameterType.Name, other, comparison),
                Type value => String.Equals(value.FullName, other, comparison) || String.Equals(value.FullName, other, comparison),
                Predicate<String> value => other is not null && value(other),
                Func<String, Boolean> value => other is not null && value(other),
                _ => false
            };
        }

        public override String? ToString()
        {
            return Parameter?.ToString();
        }
    }
}