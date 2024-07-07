using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Interfaces.Notify;
using NetExtender.Types.Monads;
using NetExtender.Types.Monads.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Monads.Default
{
    public class NotifyMutableValueDefault<T> : MutableValueDefault<T>
    {
        public override T Value
        {
            get
            {
                return base.Value;
            }
            set
            {
                Boolean @default = IsDefault;
                this.RaiseAndSetIfChanged(ref _value, value);
                
                if (@default)
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
                
                if (IsDefault)
                {
                    this.RaiseProperty(nameof(Value));
                }
            }
        }
        
        public NotifyMutableValueDefault(T @default)
            : base(@default)
        {
        }
        
        public NotifyMutableValueDefault(T value, T @default)
            : base(value, @default)
        {
        }
        
        public override Boolean Reset()
        {
            if (IsDefault)
            {
                return false;
            }
            
            this.RaiseAndSetIfChanged(ref _value, default(Maybe<T>), nameof(Value));
            this.RaiseProperty(nameof(IsDefault));
            return true;
        }
    }
    
    public class MutableValueDefault<T> : MutableDefault<T>
    {
        protected T _default;
        public virtual T Default
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
        
        public MutableValueDefault(T @default)
        {
            _default = @default;
        }
        
        public MutableValueDefault(T value, T @default)
            : base(value)
        {
            _default = @default;
        }
        
        protected sealed override T GetDefault()
        {
            return Default;
        }
        
        protected sealed override void SetDefault(T value)
        {
            Default = value;
        }
    }
    
    public class NotifyMutableDynamicDefault<T> : MutableDynamicDefault<T>
    {
        public override T Value
        {
            get
            {
                return base.Value;
            }
            set
            {
                Boolean @default = IsDefault;
                this.RaiseAndSetIfChanged(ref _value, value);
                
                if (@default)
                {
                    this.RaiseProperty(nameof(IsDefault));
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
                
                if (IsDefault)
                {
                    this.RaiseProperty(nameof(Value));
                }
            }
        }
        
        public NotifyMutableDynamicDefault(Func<T> @default)
            : base(@default)
        {
        }
        
        public NotifyMutableDynamicDefault(T value, Func<T> @default)
            : base(value, @default)
        {
        }
        
        public override Boolean Reset()
        {
            if (IsDefault)
            {
                return false;
            }
            
            this.RaiseAndSetIfChanged(ref _value, default(Maybe<T>), nameof(Value));
            this.RaiseProperty(nameof(IsDefault));
            return true;
        }
    }
    
    public class MutableDynamicDefault<T> : MutableDefault<T>
    {
        protected Func<T> _default;
        public virtual Func<T> Default
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
        
        public MutableDynamicDefault(Func<T> @default)
        {
            _default = @default ?? throw new ArgumentNullException(nameof(@default));
        }
        
        public MutableDynamicDefault(T value, Func<T> @default)
            : base(value)
        {
            _default = @default ?? throw new ArgumentNullException(nameof(@default));
        }
        
        protected sealed override T GetDefault()
        {
            return Default();
        }
        
        protected sealed override void SetDefault(T value)
        {
            Default = () => value;
        }
    }
    
    public abstract class MutableDefault<T> : IDefault<T>, INotifyProperty
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator T?(MutableDefault<T>? value)
        {
            return value is not null ? value.Value : default;
        }
        
        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;
        
        protected Maybe<T> _value;
        public virtual T Value
        {
            get
            {
                return _value.Unwrap(GetDefault);
            }
            set
            {
                _value = value;
            }
        }
        
        public Boolean IsDefault
        {
            get
            {
                return !_value.HasValue;
            }
        }
        
        protected MutableDefault()
        {
        }
        
        protected MutableDefault(T value)
        {
            _value = value;
        }
        
        protected abstract T GetDefault();
        protected abstract void SetDefault(T value);
        
        public virtual void Set(T value)
        {
            Value = value;
        }
        
        IDefault<T> IDefault<T>.Set(T value)
        {
            Set(value);
            return this;
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
            if (IsDefault)
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
            try
            {
                comparer ??= Comparer<T>.Default;
                return comparer.Compare(Value, other);
            }
            catch (ArgumentException)
            {
                return 0;
            }
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

        public Boolean Equals(IDefault<T>? other)
        {
            return Equals(other, null);
        }

        public Boolean Equals(IDefault<T>? other, IEqualityComparer<T>? comparer)
        {
            return other is not null && Equals(other.Value, comparer);
        }

        public sealed override String? ToString()
        {
            return Value?.ToString();
        }
    }
}