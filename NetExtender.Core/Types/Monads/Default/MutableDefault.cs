// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using NetExtender.Interfaces;
using NetExtender.Interfaces.Notify;
using NetExtender.Newtonsoft.Types.Monads;
using NetExtender.Types.Monads.Interfaces;
using NetExtender.Utilities.Serialization;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;

namespace NetExtender.Types.Monads
{
    [Serializable]
    [JsonConverter(typeof(NotifyMutableValueDefaultJsonConverter<>))]
    [System.Text.Json.Serialization.JsonConverter(typeof(Serialization.Json.Monads.NotifyMutableValueDefaultJsonConverter<>))]
    public class NotifyMutableValueDefault<T> : MutableValueDefault<T>, ICloneable<NotifyMutableValueDefault<T>>
    {
        public static implicit operator NotifyMutableValueDefault<T>(T value)
        {
            return new NotifyMutableValueDefault<T>(value);
        }
        
        public override T Value
        {
            get
            {
                return base.Value;
            }
            protected set
            {
                Boolean has = HasValue;
                Boolean empty = IsEmpty;
                Boolean @default = IsDefault;

                this.RaiseAndSetIfChanged(ref _value, value);

                if (has != HasValue)
                {
                    this.RaiseProperty(nameof(HasValue));
                }

                if (empty != IsEmpty)
                {
                    this.RaiseProperty(nameof(IsEmpty));
                }
                
                if (@default != IsDefault)
                {
                    this.RaiseProperty(nameof(IsDefault));
                }
            }
        }
        
        public override T Default
        {
            get
            {
                return _default;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _default, value);

                if (IsEmpty)
                {
                    this.RaisePropertyChanged(nameof(Value));
                }
            }
        }
        
        public NotifyMutableValueDefault(T @default)
            : base(@default)
        {
        }
        
        public NotifyMutableValueDefault(T @default, T value)
            : base(@default, value)
        {
        }

        private protected NotifyMutableValueDefault(T @default, Maybe<T> value)
            : base(@default, value)
        {
        }

        protected NotifyMutableValueDefault(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
        
        public override Boolean Reset()
        {
            if (IsEmpty)
            {
                return false;
            }

            Boolean has = HasValue;
            Boolean empty = IsEmpty;
            Boolean @default = IsDefault;

            this.RaiseAndSetIfChanged(ref _value, default(Maybe<T>), nameof(Value));

            if (has != HasValue)
            {
                this.RaisePropertyChanged(nameof(HasValue));
            }

            if (empty != IsEmpty)
            {
                this.RaisePropertyChanged(nameof(IsEmpty));
            }

            if (@default != IsDefault)
            {
                this.RaisePropertyChanged(nameof(IsDefault));
            }
            
            return true;
        }

        public override NotifyMutableValueDefault<T> Clone()
        {
            return new NotifyMutableValueDefault<T>(_default, _value);
        }
    }
    
    [Serializable]
    [JsonConverter(typeof(MutableValueDefaultJsonConverter<>))]
    [System.Text.Json.Serialization.JsonConverter(typeof(Serialization.Json.Monads.MutableValueDefaultJsonConverter<>))]
    public class MutableValueDefault<T> : MutableDefault<T>, ICloneable<MutableValueDefault<T>>
    {
        public static implicit operator MutableValueDefault<T>(T value)
        {
            return new MutableValueDefault<T>(value);
        }

        protected T _default;
        public new virtual T Default
        {
            get
            {
                return _default;
            }
            set
            {
                _default = value;
            }
        }

        public sealed override Boolean IsDefault
        {
            get
            {
                return _value.IsEmpty || EqualityComparer<T>.Default.Equals(Default, Value);
            }
        }
        
        public MutableValueDefault(T @default)
        {
            _default = @default;
        }
        
        public MutableValueDefault(T @default, T value)
            : base(value)
        {
            _default = @default;
        }

        private protected MutableValueDefault(T @default, Maybe<T> value)
            : base(value)
        {
            _default = @default;
        }

        protected MutableValueDefault(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _default = info.GetValue<T>(nameof(Default));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(Default), _default);
        }

        protected sealed override T GetDefault()
        {
            return Default;
        }
        
        protected sealed override void SetDefault(T value)
        {
            Default = value;
        }

        public override MutableValueDefault<T> Clone()
        {
            return new MutableValueDefault<T>(_default, _value);
        }
    }

    [JsonConverter(typeof(NotifyMutableDynamicDefaultJsonConverter<>))]
    [System.Text.Json.Serialization.JsonConverter(typeof(Serialization.Json.Monads.NotifyMutableDynamicDefaultJsonConverter<>))]
    public class NotifyMutableDynamicDefault<T> : MutableDynamicDefault<T>, ICloneable<NotifyMutableDynamicDefault<T>>
    {
        public override T Value
        {
            get
            {
                return base.Value;
            }
            protected set
            {
                Boolean has = HasValue;
                Boolean empty = IsEmpty;
                Boolean @default = IsDefault;
                
                this.RaiseAndSetIfChanged(ref _value, value);

                if (has != HasValue)
                {
                    this.RaisePropertyChanged(nameof(HasValue));
                }

                if (empty != IsEmpty)
                {
                    this.RaisePropertyChanged(nameof(IsEmpty));
                }

                if (@default != IsDefault)
                {
                    this.RaisePropertyChanged(nameof(IsDefault));
                }
            }
        }
        
        public override Func<T> Default
        {
            get
            {
                return base.Default;
            }
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                
                this.RaiseAndSetIfChanged(ref _default, value);

                if (IsEmpty)
                {
                    this.RaisePropertyChanged(nameof(Value));
                }
            }
        }

        public NotifyMutableDynamicDefault(Func<T> @default)
            : base(@default)
        {
        }
        
        public NotifyMutableDynamicDefault(Func<T> @default, T value)
            : base(@default, value)
        {
        }

        private protected NotifyMutableDynamicDefault(Func<T> @default, Maybe<T> value)
            : base(@default, value)
        {
        }

        protected NotifyMutableDynamicDefault(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
        
        public override Boolean Reset()
        {
            if (IsEmpty)
            {
                return false;
            }

            Boolean has = HasValue;
            Boolean empty = IsEmpty;
            Boolean @default = IsDefault;

            this.RaiseAndSetIfChanged(ref _value, default(Maybe<T>), nameof(Value));

            if (has != HasValue)
            {
                this.RaiseProperty(nameof(HasValue));
            }

            if (empty != IsEmpty)
            {
                this.RaiseProperty(nameof(IsEmpty));
            }

            if (@default != IsDefault)
            {
                this.RaiseProperty(nameof(IsDefault));
            }
            
            return true;
        }

        public override NotifyMutableDynamicDefault<T> Clone()
        {
            return new NotifyMutableDynamicDefault<T>(Default, Maybe);
        }
    }
    
    [JsonConverter(typeof(MutableDynamicDefaultJsonConverter<>))]
    [System.Text.Json.Serialization.JsonConverter(typeof(Serialization.Json.Monads.MutableDynamicDefaultJsonConverter<>))]
    public class MutableDynamicDefault<T> : MutableDefault<T>, ICloneable<MutableDynamicDefault<T>>
    {
        protected Func<T> _default;
        public new virtual Func<T> Default
        {
            get
            {
                return _default;
            }
            set
            {
                _default = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public sealed override Boolean IsDefault
        {
            get
            {
                return _value.IsEmpty || EqualityComparer<T>.Default.Equals(Default(), Value);
            }
        }

        public MutableDynamicDefault(Func<T> @default)
        {
            _default = @default ?? throw new ArgumentNullException(nameof(@default));
        }
        
        public MutableDynamicDefault(Func<T> @default, T value)
            : base(value)
        {
            _default = @default ?? throw new ArgumentNullException(nameof(@default));
        }

        private protected MutableDynamicDefault(Func<T> @default, Maybe<T> value)
            : base(value)
        {
            _default = @default ?? throw new ArgumentNullException(nameof(@default));
        }

        protected MutableDynamicDefault(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            T @default = info.GetValue<T>(nameof(Default));
            _default = () => @default;
        }
        
        protected sealed override T GetDefault()
        {
            return Default();
        }
        
        protected sealed override void SetDefault(T value)
        {
            Default = () => value;
        }

        public override MutableDynamicDefault<T> Clone()
        {
            return new MutableDynamicDefault<T>(Default, _value);
        }

        MutableDynamicDefault<T> ICloneable<MutableDynamicDefault<T>>.Clone()
        {
            return Clone();
        }
    }
    
    [Serializable]
    [JsonConverter(typeof(MutableDefaultJsonConverter<>))]
    [System.Text.Json.Serialization.JsonConverter(typeof(Serialization.Json.Monads.MutableDefaultJsonConverter<>))]
    public abstract class MutableDefault<T> : IDefault<T>, IDefaultEquality<T, MutableDefault<T>>, ICloneable<MutableDefault<T>>, ISerializable, INotifyProperty
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator T?(MutableDefault<T>? value)
        {
            return value is not null ? value.Value : default;
        }
        
        public static Boolean operator ==(T? first, MutableDefault<T>? second)
        {
            return second == first;
        }

        public static Boolean operator !=(T? first, MutableDefault<T>? second)
        {
            return !(first == second);
        }

        public static Boolean operator ==(MutableDefault<T>? first, T? second)
        {
            return first is not null && first.Equals(second);
        }

        public static Boolean operator !=(MutableDefault<T>? first, T? second)
        {
            return !(first == second);
        }
        
        public static Boolean operator ==(MutableDefault<T>? first, MutableDefault<T>? second)
        {
            return ReferenceEquals(first, second) || first is not null && first.Equals(second);
        }

        public static Boolean operator !=(MutableDefault<T>? first, MutableDefault<T>? second)
        {
            return !(first == second);
        }

        public static Boolean operator >(T? first, MutableDefault<T>? second)
        {
            return second < first;
        }

        public static Boolean operator >=(T? first, MutableDefault<T>? second)
        {
            return second <= first;
        }

        public static Boolean operator <(T? first, MutableDefault<T>? second)
        {
            return second > first;
        }

        public static Boolean operator <=(T? first, MutableDefault<T>? second)
        {
            return second >= first;
        }

        public static Boolean operator >(MutableDefault<T>? first, T? second)
        {
            return first is not null && first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(MutableDefault<T>? first, T? second)
        {
            return first is null && second is null || first is not null && first.CompareTo(second) >= 0;
        }

        public static Boolean operator <(MutableDefault<T>? first, T? second)
        {
            return first is not null && first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(MutableDefault<T>? first, T? second)
        {
            return first is null && second is null || first is not null && first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(MutableDefault<T>? first, MutableDefault<T>? second)
        {
            return first is not null && (second is null || first.CompareTo(second) > 0);
        }

        public static Boolean operator >=(MutableDefault<T>? first, MutableDefault<T>? second)
        {
            return ReferenceEquals(first, second) || first is not null && (second is null || first.CompareTo(second) >= 0);
        }

        public static Boolean operator <(MutableDefault<T>? first, MutableDefault<T>? second)
        {
            return first is null && second is not null || first is not null && first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(MutableDefault<T>? first, MutableDefault<T>? second)
        {
            return ReferenceEquals(first, second) || first is null && second is not null || first is not null && first.CompareTo(second) <= 0;
        }
        
        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;
        
        private protected Maybe<T> _value;
        protected internal Maybe<T> Maybe
        {
            get
            {
                return _value;
            }
        }

        public T Default
        {
            get
            {
                return GetDefault();
            }
            protected set
            {
                SetDefault(value);
            }
        }
        
        public virtual T Value
        {
            get
            {
                return _value.Internal;
            }
            protected set
            {
                _value = value;
            }
        }

        public Boolean HasValue
        {
            get
            {
                return !_value.IsEmpty;
            }
        }

        public abstract Boolean IsDefault { get; }

        public Boolean IsEmpty
        {
            get
            {
                return _value.IsEmpty;
            }
        }
        
        protected MutableDefault()
        {
        }
        
        protected MutableDefault(T value)
        {
            _value = value;
        }
        
        private protected MutableDefault(Maybe<T> value)
        {
            _value = value;
        }

        protected MutableDefault(SerializationInfo info, StreamingContext context)
        {
            _value = info.GetBoolean(nameof(_value.HasValue)) ? info.GetValue<T>(nameof(_value.Value))! : default(Maybe<T>);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MutableDefault<T> Create(T @default)
        {
            return new MutableValueDefault<T>(@default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MutableDefault<T> CreateNotify(T @default)
        {
            return new NotifyMutableValueDefault<T>(@default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MutableDefault<T> Create(T @default, Boolean notify)
        {
            return notify ? CreateNotify(@default) : Create(@default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MutableDefault<T> Create(T @default, T value)
        {
            return new MutableValueDefault<T>(@default, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MutableDefault<T> CreateNotify(T @default, T value)
        {
            return new NotifyMutableValueDefault<T>(@default, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MutableDefault<T> Create(T @default, T value, Boolean notify)
        {
            return notify ? CreateNotify(@default, value) : Create(@default, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MutableDefault<T> Create(Func<T> @default)
        {
            return new MutableDynamicDefault<T>(@default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MutableDefault<T> CreateNotify(Func<T> @default)
        {
            return new NotifyMutableDynamicDefault<T>(@default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MutableDefault<T> Create(Func<T> @default, Boolean notify)
        {
            return notify ? CreateNotify(@default) : Create(@default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MutableDefault<T> Create(Func<T> @default, T value)
        {
            return new MutableDynamicDefault<T>(@default, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MutableDefault<T> CreateNotify(Func<T> @default, T value)
        {
            return new NotifyMutableDynamicDefault<T>(@default, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MutableDefault<T> Create(Func<T> @default, T value, Boolean notify)
        {
            return notify ? CreateNotify(@default, value) : Create(@default, value);
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(_value.HasValue), _value.HasValue);

            if (_value.HasValue)
            {
                info.AddValue(nameof(Value), _value.Value);
            }
        }
        
        protected abstract T GetDefault();
        protected abstract void SetDefault(T value);
        
        public Boolean Set(T value)
        {
            if (_value == value)
            {
                return false;
            }
            
            Value = value;
            return true;
        }
        
        IDefault<T> IDefault<T>.Set(T value)
        {
            Set(value);
            return this;
        }
        
        Boolean IDefault<T>.Set(T value, [MaybeNullWhen(false)] out IDefault<T> result)
        {
            result = this;
            return Set(value);
        }
        
        public virtual Boolean Swap()
        {
            Maybe<T> value = _value;
            
            if (!value.HasValue)
            {
                return false;
            }
            
            _value = GetDefault();
            SetDefault(value.Unwrap()!);
            return true;
        }
        
        IDefault<T> IDefault<T>.Swap()
        {
            Swap();
            return this;
        }
        
        IDefault IDefault.Swap()
        {
            Swap();
            return this;
        }
        
        public virtual Boolean Reset()
        {
            if (IsEmpty)
            {
                return false;
            }
            
            _value = default;
            return true;
        }
        
        IDefault<T> IDefault<T>.Reset()
        {
            Reset();
            return this;
        }
        
        IDefault IDefault.Reset()
        {
            Reset();
            return this;
        }
        
        public Int32 CompareTo(T? other)
        {
            return CompareTo(other, null);
        }
        
        public Int32 CompareTo(T? other, IComparer<T>? comparer)
        {
            return comparer.SafeCompare(Value, other) ?? 0;
        }
        
        public Int32 CompareTo(MutableDefault<T>? other)
        {
            return CompareTo(other, null);
        }
        
        public Int32 CompareTo(MutableDefault<T>? other, IComparer<T>? comparer)
        {
            return other is not null ? CompareTo(other.Value, comparer) : 1;
        }
        
        public Int32 CompareTo(IDefault<T>? other)
        {
            return CompareTo(other, null);
        }
        
        public Int32 CompareTo(IDefault<T>? other, IComparer<T>? comparer)
        {
            return other is not null ? CompareTo(other.Value, comparer) : 1;
        }

        public sealed override Int32 GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return _value.GetHashCode();
        }

        public sealed override Boolean Equals(Object? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(Object? other, IEqualityComparer<T>? comparer)
        {
            return other switch
            {
                T value => Equals(value, comparer),
                MutableDefault<T> value => Equals(value, comparer),
                IDefault<T> value => Equals(value, comparer),
                _ => false
            };
        }
        
        public Boolean Equals(T? other)
        {
            return Equals(other, null);
        }
        
        public Boolean Equals(T? other, IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;
            return comparer.Equals(Value, other);
        }

        public Boolean Equals(MutableDefault<T>? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(MutableDefault<T>? other, IEqualityComparer<T>? comparer)
        {
            return other is not null && Equals(other.Value, comparer);
        }

        public Boolean Equals(IDefault<T>? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(IDefault<T>? other, IEqualityComparer<T>? comparer)
        {
            return other is not null && Equals(other.Value, comparer);
        }

        public abstract MutableDefault<T> Clone();

        IDefault<T> IDefault<T>.Clone()
        {
            return Clone();
        }

        IDefault<T> ICloneable<IDefault<T>>.Clone()
        {
            return Clone();
        }

        IDefault IDefault.Clone()
        {
            return Clone();
        }

        IDefault ICloneable<IDefault>.Clone()
        {
            return Clone();
        }

        IMonad<T> IMonad<T>.Clone()
        {
            return Clone();
        }

        IMonad<T> ICloneable<IMonad<T>>.Clone()
        {
            return Clone();
        }

        IMonad IMonad.Clone()
        {
            return Clone();
        }

        IMonad ICloneable<IMonad>.Clone()
        {
            return Clone();
        }

        Object ICloneable.Clone()
        {
            return Clone();
        }

        public sealed override String? ToString()
        {
            return Value is not null ? Value.ToString() : null;
        }

        public String ToString(String? format)
        {
            return ToString(format, null);
        }

        public String ToString(IFormatProvider? provider)
        {
            return ToString(null, provider);
        }

        public String ToString(String? format, IFormatProvider? provider)
        {
            return Value is { } value ? StringUtilities.ToString(in value, format, provider) : String.Empty;
        }

        public String? GetString()
        {
            return _value.GetString();
        }

        public String? GetString(EscapeType escape)
        {
            return _value.GetString(escape);
        }

        public String? GetString(String? format)
        {
            return _value.GetString(format);
        }

        public String? GetString(EscapeType escape, String? format)
        {
            return _value.GetString(escape, format);
        }

        public String? GetString(IFormatProvider? provider)
        {
            return _value.GetString(provider);
        }

        public String? GetString(EscapeType escape, IFormatProvider? provider)
        {
            return _value.GetString(escape, provider);
        }

        public String? GetString(String? format, IFormatProvider? provider)
        {
            return _value.GetString(format, provider);
        }

        public String? GetString(EscapeType escape, String? format, IFormatProvider? provider)
        {
            return _value.GetString(escape, format, provider);
        }
    }
}