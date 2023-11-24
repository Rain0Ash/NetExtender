// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;
using NetExtender.NewtonSoft.Types.Enums;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;

namespace NetExtender.Types.Enums
{
    [JsonConverter(typeof(EnumJsonConverter))]
    public abstract class Enum<T, TEnum> : Enum<T>, IEquatable<Enum<T, TEnum>>, IComparable<Enum<T, TEnum>> where T : unmanaged, Enum where TEnum : Enum<T, TEnum>, new()
    {
        public static implicit operator Enum<T, TEnum>(T value)
        {
            return TryParse<TEnum>(value, out TEnum? result) ? result : Enum<T>.Create<TEnum>(value).As<TEnum>();
        }

        [return: NotNullIfNotNull("value")]
        public static explicit operator Enum<T, TEnum>?(String? value)
        {
            return value is not null ? TryParse<TEnum>(value, out TEnum? result) ? result : throw new InvalidCastException($"Can't cast specified value '{value}' to enum '{typeof(T)}'.") : null;
        }
        
        public sealed override Boolean IsIntern
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            get
            {
                return EnumUtilities.CacheEnum<T>.IsIntern((TEnum) this);
            }
        }
        
        protected Enum()
        {
        }

        protected Enum(T value)
            : base(value)
        {
        }

        protected Enum(T value, String title)
            : base(value, title)
        {
        }
        
        protected override TEnum Initialize()
        {
            return (TEnum) this;
        }
        
        public override TEnum Intern()
        {
            return !IsIntern && TryParse(Id, out TEnum? @enum) && Equals(@enum) ? @enum : (TEnum) this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TEnum Create(T value)
        {
            return Create<TEnum>(value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TEnum Create(T value, String title)
        {
            return Create<TEnum>(value, title);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static ImmutableSortedSet<TEnum> Get()
        {
            return Get<TEnum>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(T value, [MaybeNullWhen(false)] out TEnum result)
        {
            return TryParse<TEnum>(value, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(String value, [MaybeNullWhen(false)] out TEnum result)
        {
            return TryParse<TEnum>(value, out result);
        }
        
        public sealed override Int32 CompareTo(Object? obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return 0;
            }
            
            return obj switch
            {
                Enum<T, TEnum> other => CompareTo(other),
                Enum<T> other => CompareTo(other),
                T other => CompareTo(other),
                _ => 1
            };
        }

        public sealed override Int32 CompareTo(Enum<T>? other)
        {
            return other is Enum<T, TEnum> @enum ? CompareTo(@enum) : base.CompareTo(other);
        }

        public virtual Int32 CompareTo(Enum<T, TEnum>? other)
        {
            return ReferenceEquals(this, other) ? 0 : ReferenceEquals(null, other) ? 1 : base.CompareTo(other.Id);
        }

        public override Int32 GetHashCode()
        {
            return base.GetHashCode();
        }

        public sealed override Boolean Equals(Object? obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj switch
            {
                Enum<T, TEnum> other => Equals(other),
                Enum<T> other => Equals(other),
                T other => Equals(other),
                _ => false
            };
        }
        
        public sealed override Boolean Equals(Enum<T>? other)
        {
            return other is Enum<T, TEnum> @enum ? Equals(@enum) : base.Equals(other);
        }

        public virtual Boolean Equals(Enum<T, TEnum>? other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return EqualityComparer<T>.Default.Equals(Id, other.Id) && String.Equals(Title, other.Title, StringComparison.Ordinal);
        }
    }
    
    [JsonConverter(typeof(EnumJsonConverter))]
    public class Enum<T> : IEquatable<T>, IEquatable<Enum<T>>, IComparable, IComparable<T>, IComparable<Enum<T>>, IFormattable where T : unmanaged, Enum
    {
        public static implicit operator Enum<T>(T value)
        {
            return TryParse(value, out Enum<T>? result) ? result : new Enum<T>(value);
        }

        [return: NotNullIfNotNull("value")]
        public static implicit operator Enum?(Enum<T>? value)
        {
            return value?.Id;
        }

        public static implicit operator T(Enum<T>? value)
        {
            return value?.Id ?? default;
        }

        public static implicit operator T?(Enum<T>? value)
        {
            return value?.Id;
        }

        [return: NotNullIfNotNull("value")]
        public static explicit operator Enum<T>?(String? value)
        {
            return value is not null ? TryParse(value, out Enum<T>? result) ? result : throw new InvalidCastException($"Can't cast specified value '{value}' to enum '{typeof(T)}'.") : null;
        }

        [return: NotNullIfNotNull("value")]
        public static implicit operator String?(Enum<T>? value)
        {
            return value?.ToString();
        }

        public static Boolean operator ==(Enum<T> first, Enum<T> second)
        {
            return Equals(first, second);
        }

        public static Boolean operator !=(Enum<T> first, Enum<T> second)
        {
            return !Equals(first, second);
        }

        public static Boolean operator <(Enum<T> first, Enum<T> second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator >(Enum<T> first, Enum<T> second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator <=(Enum<T> first, Enum<T> second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >=(Enum<T> first, Enum<T> second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator ==(Enum<T> first, T second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(Enum<T> first, T second)
        {
            return !first.Equals(second);
        }

        public static Boolean operator <(Enum<T> first, T second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator >(Enum<T> first, T second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator <=(Enum<T> first, T second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >=(Enum<T> first, T second)
        {
            return first.CompareTo(second) >= 0;
        }

        [JsonIgnore]
        public Type Type
        {
            get
            {
                return typeof(T);
            }
        }

        public T Id { get; private init; }
        public String Title { get; private init; }

        [JsonIgnore]
        public virtual Boolean IsIntern
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return EnumUtilities.CacheEnum<T>.IsIntern(this);
            }
        }

        protected Enum()
        {
            Id = default;
            Title = String.Empty;
        }

        protected Enum(T value)
        {
            Id = value;
            Title = ToTitle(value);
        }

        protected Enum(T value, String title)
        {
            Id = value;
            Title = title ?? throw new ArgumentNullException(nameof(title));
        }

        protected virtual Enum<T> Initialize()
        {
            return this;
        }

        public virtual Enum<T> Intern()
        {
            return !IsIntern && TryParse(Id, out Enum<T>? @enum) && Equals(@enum) ? @enum : this;
        }

        public virtual TEnum As<TEnum>() where TEnum : Enum<T>, new()
        {
            return this as TEnum ?? (TryParse<TEnum>(Id, out TEnum? result) && Title == result.Title ? result : new TEnum { Id = Id, Title = Title });
        }

        public Boolean HasFlag(T value)
        {
            return Id.HasFlag(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static String ToTitle(T value)
        {
            return value.GetDescriptionOrName();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Enum<T> Create(T value)
        {
            return new Enum<T> { Id = value, Title = ToTitle(value) }.Initialize();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Enum<T> Create(T value, String title)
        {
            if (title is null)
            {
                throw new ArgumentNullException(nameof(title));
            }

            return new Enum<T> { Id = value, Title = title }.Initialize();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TEnum Create<TEnum>(T value) where TEnum : Enum<T>, new()
        {
            return (TEnum) new TEnum { Id = value, Title = ToTitle(value) }.Initialize();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TEnum Create<TEnum>(T value, String title) where TEnum : Enum<T>, new()
        {
            if (title is null)
            {
                throw new ArgumentNullException(nameof(title));
            }

            return (TEnum) new TEnum { Id = value, Title = title }.Initialize();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedSet<Enum<T>> Get()
        {
            return EnumUtilities.CacheEnum<T>.Get();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedSet<TEnum> Get<TEnum>() where TEnum : Enum<T>, new()
        {
            return EnumUtilities.CacheEnum<T>.Get<TEnum>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(T value, [MaybeNullWhen(false)] out Enum<T> result)
        {
            return EnumUtilities.CacheEnum<T>.TryParse(value, out result);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(String value, [MaybeNullWhen(false)] out Enum<T> result)
        {
            return EnumUtilities.CacheEnum<T>.TryParse(value, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse<TEnum>(T value, [MaybeNullWhen(false)] out TEnum result) where TEnum : Enum<T>, new()
        {
            return EnumUtilities.CacheEnum<T>.TryParse(value, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse<TEnum>(String value, [MaybeNullWhen(false)] out TEnum result) where TEnum : Enum<T>, new()
        {
            return EnumUtilities.CacheEnum<T>.TryParse(value, out result);
        }
        
        public virtual Int32 CompareTo(Object? obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return 0;
            }
            
            return obj switch
            {
                Enum<T> other => CompareTo(other),
                T other => CompareTo(other),
                _ => 1
            };
        }

        public Int32 CompareTo(T other)
        {
            return Id.CompareTo(other);
        }

        public virtual Int32 CompareTo(Enum<T>? other)
        {
            return ReferenceEquals(this, other) ? 0 : ReferenceEquals(null, other) ? 1 : CompareTo(other.Id);
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Id, Title);
        }

        public override Boolean Equals(Object? obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj switch
            {
                Enum<T> other => Equals(other),
                T other => Equals(other),
                _ => false
            };
        }

        public Boolean Equals(T other)
        {
            return EqualityComparer<T>.Default.Equals(Id, other);
        }

        public virtual Boolean Equals(Enum<T>? other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return EqualityComparer<T>.Default.Equals(Id, other.Id) && String.Equals(Title, other.Title, StringComparison.Ordinal);
        }

        public override String ToString()
        {
            return Title;
        }
        
        public String ToString(String? format)
        {
            return ToString(format, null);
        }

        public virtual String ToString(String? format, IFormatProvider? provider)
        {
            if (String.IsNullOrEmpty(format))
            {
                return ToString();
            }

            switch (format)
            {
                case "r":
                case "rid":
                case "r-id":
                case "R":
                case "RID":
                case "R-ID":
                    return $"{GetType().Name} {{ Id = {Id.ToString()}, Title = {Title} }}";
                case "rn":
                case "RN":
                case "rnumber":
                case "RNUMBER":
                case "r-number":
                case "R-NUMBER":
                    return $"{GetType().Name} {{ Id = {Id.ToDecimal().ToString(provider)}, Title = {Title} }}";
                case "rt":
                case "RT":
                case "rtitle":
                case "RTITLE":
                case "r-title":
                case "R-TITLE":
                    return $"{GetType().Name} {{ Id = {ToTitle(Id)}, Title = {Title} }}";
                case "id":
                case "ID":
                    return Id.ToString();
                case "n":
                case "N":
                case "number":
                case "NUMBER":
                    return Id.ToDecimal().ToString(provider);
                case "t":
                case "T":
                case "title":
                case "TITLE":
                    return Title;
                case "idt":
                case "IDT":
                case "idtitle":
                case "IDTITLE":
                    return ToTitle(Id);
                default:
                    return new StringBuilder(format).Replace("{TYPE}", GetType().Name).Replace("{ID}", Id.ToString()).Replace("{NUMBER}", Id.ToDecimal().ToString(provider)).Replace("{TITLE}", Title).Replace("{IDTITLE}", ToTitle(Id)).ToString();
            }
        }
    }
}