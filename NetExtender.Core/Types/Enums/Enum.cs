// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;
using NetExtender.NewtonSoft.Types.Enums;
using NetExtender.Types.Culture;
using NetExtender.Types.Enums.Interfaces;
using NetExtender.Types.Lists.Interfaces;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;

namespace NetExtender.Types.Enums
{
    [JsonConverter(typeof(EnumJsonConverter))]
    public abstract partial class Enum<T, TEnum> : Enum<T>, IEnum<T, TEnum>, IEquality<Enum<T, TEnum>> where T : unmanaged, Enum where TEnum : Enum<T, TEnum>, new()
    {
        public static implicit operator Enum<T, TEnum>(T value)
        {
            return TryParse<TEnum>(value, out TEnum? result) ? result : Enum<T>.Create<TEnum>(value).As<TEnum>();
        }

        [return: NotNullIfNotNull("value")]
        public static explicit operator Enum<T, TEnum>?(String? value)
        {
            return value is not null ? TryParse<TEnum>(value, out TEnum? result) ? result : throw new InvalidCastException($"Can't cast specified value '{value}' to enum '{typeof(T).Name}'.") : null;
        }

        private TEnum This
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return (TEnum) this;
            }
        }
        
        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public sealed override Boolean IsFlags
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            get
            {
                return EnumUtilities.EnumStorage<T>.IsFlags(This);
            }
        }

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public new FlagsEnumerator Flags
        {
            get
            {
                return new FlagsEnumerator(This);
            }
        }

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        IReadOnlySortedList<TEnum> IEnum<T, TEnum>.Flags
        {
            get
            {
                return Flags.This;
            }
        }

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        IReadOnlySortedList<IEnum<T>> IEnum<T>.Flags
        {
            get
            {
                return Flags;
            }
        }

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        IReadOnlySortedList<IEnum> IEnum.Flags
        {
            get
            {
                return Flags.Interface;
            }
        }
        
        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public sealed override Boolean IsIntern
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            get
            {
                return EnumUtilities.EnumStorage<T>.IsIntern(This);
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
            return This;
        }
        
        public override TEnum Intern()
        {
            TryIntern(out TEnum result);
            return result;
        }

        public virtual Boolean TryIntern(out TEnum result)
        {
            if (IsIntern)
            {
                result = This;
                return true;
            }

            if (Identifier is { } identifier ? !TryParse(identifier, Id, out TEnum? @enum) : !TryParse(Id, out @enum))
            {
                result = This;
                return false;
            }

            if (Equals(@enum))
            {
                result = @enum;
                return true;
            }

            result = This;
            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TEnum Create(T value)
        {
            return Create<TEnum>(value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TEnum Create(T value, LocalizationIdentifier identifier)
        {
            return Create<TEnum>(value, identifier);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TEnum Create(T value, String title)
        {
            return Create<TEnum>(value, title);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TEnum Create(T value, String title, LocalizationIdentifier identifier)
        {
            return Create<TEnum>(value, title, identifier);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static ImmutableSortedSet<TEnum> Get()
        {
            return Get<TEnum>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static ImmutableSortedSet<TEnum>? Get(LocalizationIdentifier identifier)
        {
            return Get<TEnum>(identifier);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(T value, [MaybeNullWhen(false)] out TEnum result)
        {
            return TryParse<TEnum>(value, out result);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(LocalizationIdentifier identifier, T value, [MaybeNullWhen(false)] out TEnum result)
        {
            return Enum<T>.TryParse(identifier, value, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(String value, [MaybeNullWhen(false)] out TEnum result)
        {
            return TryParse<TEnum>(value, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(LocalizationIdentifier identifier, String value, [MaybeNullWhen(false)] out TEnum result)
        {
            return Enum<T>.TryParse(identifier, value, out result);
        }
        
        public sealed override Int32 CompareTo(Object? other)
        {
            if (ReferenceEquals(this, other))
            {
                return 0;
            }
            
            return other switch
            {
                TEnum value => CompareTo(value),
                Enum<T, TEnum> value => CompareTo(value),
                IEnum<T, TEnum> value => CompareTo(value),
                Enum<T> value => CompareTo(value),
                IEnum<T> value => CompareTo(value),
                T value => CompareTo(value),
                _ => 1
            };
        }

        public sealed override Int32 CompareTo(IEnum<T>? other)
        {
            return other is Enum<T, TEnum> @enum ? CompareTo(@enum) : base.CompareTo(other);
        }

        public virtual Int32 CompareTo(TEnum? other)
        {
            return CompareTo((Enum<T, TEnum>?) other);
        }

        public Int32 CompareTo(Enum<T, TEnum>? other)
        {
            return CompareTo((IEnum<T, TEnum>?) other);
        }

        public virtual Int32 CompareTo(IEnum<T, TEnum>? other)
        {
            return ReferenceEquals(this, other) ? 0 : other is null ? 1 : base.CompareTo(other.Id);
        }

        public override Int32 GetHashCode()
        {
            return base.GetHashCode();
        }

        public sealed override Boolean Equals(Object? other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return other switch
            {
                null => false,
                TEnum value => Equals(value),
                Enum<T, TEnum> value => Equals(value),
                IEnum<T, TEnum> value => Equals(value),
                Enum<T> value => Equals(value),
                IEnum<T> value => Equals(value),
                T value => Equals(value),
                _ => false
            };
        }
        
        public sealed override Boolean Equals(IEnum<T>? other)
        {
            return other is Enum<T, TEnum> @enum ? Equals(@enum) : base.Equals(other);
        }

        public virtual Boolean Equals(TEnum? other)
        {
            return Equals((Enum<T, TEnum>?) other);
        }

        public Boolean Equals(Enum<T, TEnum>? other)
        {
            return Equals((IEnum<T, TEnum>?) other);
        }

        public virtual Boolean Equals(IEnum<T, TEnum>? other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (other is null)
            {
                return false;
            }

            return EqualityComparer<T>.Default.Equals(Id, other.Id) && Identifier == other.Identifier && String.Equals(Title, other.Title, StringComparison.Ordinal);
        }
    }
    
    [JsonConverter(typeof(EnumJsonConverter))]
    public partial class Enum<T> : IEnum<T>, IEquality<Enum<T>> where T : unmanaged, Enum
    {
        public static implicit operator Enum<T>(T value)
        {
            return TryParse(value, out Enum<T>? result) ? result : Create(value);
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
            return value is not null ? TryParse(value, out Enum<T>? result) ? result : throw new InvalidCastException($"Can't cast specified value '{value}' to enum '{typeof(T).Name}'.") : null;
        }

        [return: NotNullIfNotNull("value")]
        public static implicit operator String?(Enum<T>? value)
        {
            return value?.ToString();
        }

        public static Boolean operator ==(Enum<T>? first, Enum<T>? second)
        {
            return ReferenceEquals(first, second) || first is not null && first.Equals(second);
        }

        public static Boolean operator !=(Enum<T>? first, Enum<T>? second)
        {
            return !(first == second);
        }

        public static Boolean operator <(Enum<T>? first, Enum<T>? second)
        {
            return !ReferenceEquals(first, second) && first is not null && first.CompareTo(second) < 0;
        }

        public static Boolean operator >(Enum<T>? first, Enum<T>? second)
        {
            return !ReferenceEquals(first, second) && first is not null && first.CompareTo(second) > 0;
        }

        public static Boolean operator <=(Enum<T>? first, Enum<T>? second)
        {
            return ReferenceEquals(first, second) || first is not null && first.CompareTo(second) <= 0;
        }

        public static Boolean operator >=(Enum<T>? first, Enum<T>? second)
        {
            return ReferenceEquals(first, second) || first is not null && first.CompareTo(second) >= 0;
        }

        public static Boolean operator ==(Enum<T>? first, T second)
        {
            return first is not null && first.Equals(second);
        }

        public static Boolean operator !=(Enum<T>? first, T second)
        {
            return !(first == second);
        }

        public static Boolean operator <(Enum<T>? first, T second)
        {
            return first is null || first.CompareTo(second) < 0;
        }

        public static Boolean operator >(Enum<T>? first, T second)
        {
            return first is not null && first.CompareTo(second) > 0;
        }

        public static Boolean operator <=(Enum<T>? first, T second)
        {
            return first is null || first.CompareTo(second) <= 0;
        }

        public static Boolean operator >=(Enum<T>? first, T second)
        {
            return first is not null && first.CompareTo(second) >= 0;
        }

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public Type Underlying
        {
            get
            {
                return typeof(T);
            }
        }

        public T Id { get; private init; }

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        Enum IEnum.Id
        {
            get
            {
                return Id;
            }
        }
        
        public String Title { get; private init; }

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public Boolean HasIdentifier
        {
            get
            {
                return Identifier is not null && Identifier != LocalizationIdentifier.Invariant;
            }
        }
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault)]
        public LocalizationIdentifier? Identifier { get; private init; }

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Boolean IsFlags
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return EnumUtilities.EnumStorage<T>.IsFlags(this);
            }
        }

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public FlagsEnumerator Flags
        {
            get
            {
                return new FlagsEnumerator(this);
            }
        }

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        IReadOnlySortedList<IEnum<T>> IEnum<T>.Flags
        {
            get
            {
                return Flags;
            }
        }

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        IReadOnlySortedList<IEnum> IEnum.Flags
        {
            get
            {
                return Flags.Interface;
            }
        }

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Boolean IsIntern
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return EnumUtilities.EnumStorage<T>.IsIntern(this);
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

        protected Enum(T value, LocalizationIdentifier identifier)
        {
            Id = value;
            Title = ToTitle(value, identifier);
            Identifier = identifier;
        }

        protected Enum(T value, String title, LocalizationIdentifier identifier)
        {
            Id = value;
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Identifier = identifier;
        }

        protected virtual Enum<T> Initialize()
        {
            return this;
        }

        public virtual Enum<T> Intern()
        {
            TryIntern(out Enum<T> result);
            return result;
        }

        public virtual Boolean TryIntern(out Enum<T> result)
        {
            if (IsIntern)
            {
                result = this;
                return true;
            }

            if (Identifier is { } identifier ? !TryParse(identifier, Id, out Enum<T>? @enum) : !TryParse(Id, out @enum))
            {
                result = this;
                return false;
            }

            if (Equals(@enum))
            {
                result = @enum;
                return true;
            }

            result = this;
            return false;
        }

        public virtual TEnum As<TEnum>() where TEnum : Enum<T, TEnum>, new()
        {
            if (this is TEnum @enum)
            {
                return @enum;
            }

            if ((Identifier is { } identifier ? TryParse<TEnum>(identifier, Id, out TEnum? result) : TryParse<TEnum>(Id, out result)) && Title == result.Title)
            {
                return result;
            }

            return new TEnum { Id = Id, Title = Title, Identifier = Identifier };
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
        protected static String ToTitle(T value, LocalizationIdentifier identifier)
        {
            return value.GetDescriptionOrName(identifier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Enum<T> Create(T value)
        {
            return new Enum<T> { Id = value, Title = ToTitle(value) }.Initialize();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Enum<T> Create(T value, LocalizationIdentifier identifier)
        {
            return new Enum<T> { Id = value, Title = ToTitle(value, identifier), Identifier = identifier }.Initialize();
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
        public static Enum<T> Create(T value, String title, LocalizationIdentifier identifier)
        {
            if (title is null)
            {
                throw new ArgumentNullException(nameof(title));
            }

            return new Enum<T> { Id = value, Title = title, Identifier = identifier }.Initialize();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TEnum Create<TEnum>(T value) where TEnum : Enum<T>, new()
        {
            return (TEnum) new TEnum { Id = value, Title = ToTitle(value) }.Initialize();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TEnum Create<TEnum>(T value, LocalizationIdentifier identifier) where TEnum : Enum<T>, new()
        {
            return (TEnum) new TEnum { Id = value, Title = ToTitle(value, identifier), Identifier = identifier }.Initialize();
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
        public static TEnum Create<TEnum>(T value, String title, LocalizationIdentifier identifier) where TEnum : Enum<T>, new()
        {
            if (title is null)
            {
                throw new ArgumentNullException(nameof(title));
            }

            return (TEnum) new TEnum { Id = value, Title = title, Identifier = identifier }.Initialize();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedSet<Enum<T>> Get()
        {
            return EnumUtilities.EnumStorage<T>.Get();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedSet<Enum<T>>? Get(LocalizationIdentifier identifier)
        {
            return EnumUtilities.EnumStorage<T>.Get(identifier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedSet<TEnum> Get<TEnum>() where TEnum : Enum<T, TEnum>, new()
        {
            return EnumUtilities.EnumStorage<T>.Get<TEnum>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedSet<TEnum>? Get<TEnum>(LocalizationIdentifier identifier) where TEnum : Enum<T, TEnum>, new()
        {
            return EnumUtilities.EnumStorage<T>.Get<TEnum>(identifier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(T value, [MaybeNullWhen(false)] out Enum<T> result)
        {
            return EnumUtilities.EnumStorage<T>.TryParse(value, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(LocalizationIdentifier identifier, T value, [MaybeNullWhen(false)] out Enum<T> result)
        {
            return EnumUtilities.EnumStorage<T>.TryParse(identifier, value, out result);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(String value, [MaybeNullWhen(false)] out Enum<T> result)
        {
            return EnumUtilities.EnumStorage<T>.TryParse(value, out result);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(LocalizationIdentifier identifier, String value, [MaybeNullWhen(false)] out Enum<T> result)
        {
            return EnumUtilities.EnumStorage<T>.TryParse(identifier, value, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse<TEnum>(T value, [MaybeNullWhen(false)] out TEnum result) where TEnum : Enum<T, TEnum>, new()
        {
            return EnumUtilities.EnumStorage<T>.TryParse(value, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse<TEnum>(LocalizationIdentifier identifier, T value, [MaybeNullWhen(false)] out TEnum result) where TEnum : Enum<T, TEnum>, new()
        {
            return EnumUtilities.EnumStorage<T>.TryParse(identifier, value, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse<TEnum>(String value, [MaybeNullWhen(false)] out TEnum result) where TEnum : Enum<T, TEnum>, new()
        {
            return EnumUtilities.EnumStorage<T>.TryParse(value, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse<TEnum>(LocalizationIdentifier identifier, String value, [MaybeNullWhen(false)] out TEnum result) where TEnum : Enum<T, TEnum>, new()
        {
            return EnumUtilities.EnumStorage<T>.TryParse(identifier, value, out result);
        }
        
        public virtual Int32 CompareTo(Object? other)
        {
            if (ReferenceEquals(this, other))
            {
                return 0;
            }
            
            return other switch
            {
                Enum<T> value => CompareTo(value),
                IEnum<T> value => CompareTo(value),
                T value => CompareTo(value),
                _ => 1
            };
        }

        public Int32 CompareTo(T other)
        {
            return Id.CompareTo(other);
        }

        public Int32 CompareTo(Enum<T>? other)
        {
            return CompareTo((IEnum<T>?) other);
        }

        public virtual Int32 CompareTo(IEnum<T>? other)
        {
            return ReferenceEquals(this, other) ? 0 : other is null ? 1 : CompareTo(other.Id);
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Id, Title, Identifier);
        }

        public override Boolean Equals(Object? other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return other switch
            {
                null => false,
                Enum<T> value => Equals(value),
                IEnum<T> value => Equals(value),
                T value => Equals(value),
                _ => false
            };
        }

        public Boolean Equals(T other)
        {
            return EqualityComparer<T>.Default.Equals(Id, other);
        }

        public Boolean Equals(Enum<T>? other)
        {
            return Equals((IEnum<T>?) other);
        }

        public virtual Boolean Equals(IEnum<T>? other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (other is null)
            {
                return false;
            }

            return EqualityComparer<T>.Default.Equals(Id, other.Id) && Identifier == other.Identifier && String.Equals(Title, other.Title, StringComparison.Ordinal);
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
                    return $"{GetType().Name} {{ {nameof(Id)} = {Id.ToString()}, {nameof(Title)} = {Title} }}";
                case "rn":
                case "RN":
                case "rnumber":
                case "RNUMBER":
                case "r-number":
                case "R-NUMBER":
                    return $"{GetType().Name} {{ {nameof(Id)} = {Id.ToDecimal().ToString(provider)}, Title = {Title} }}";
                case "rt":
                case "RT":
                case "rtitle":
                case "RTITLE":
                case "r-title":
                case "R-TITLE":
                    return $"{GetType().Name} {{ {nameof(Id)} = {ToTitle(Id)}, Title = {Title} }}";
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
                case "id-title":
                case "ID-TITLE":
                    return ToTitle(Id);
                default:
                    return new StringBuilder(format).Replace("{TYPE}", GetType().Name).Replace("{ID}", Id.ToString()).Replace("{NUMBER}", Id.ToDecimal().ToString(provider)).Replace("{TITLE}", Title).Replace("{ID-TITLE}", ToTitle(Id)).ToString();
            }
        }
    }
}