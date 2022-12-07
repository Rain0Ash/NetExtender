// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Anonymous.Interfaces;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Anonymous
{
    public readonly ref struct AnonymousObjectProperty
    {
        public static implicit operator Maybe<Object?>(AnonymousObjectProperty property)
        {
            return property.Maybe;
        }

        private IAnonymousObject Anonymous { get; }
        public Int32? Index { get; }
        public String? Property { get; }

        public Type Type
        {
            get
            {
                if (Property is not null)
                {
                    return AnonymousTypeUtilities.Type(Anonymous, Property);
                }

                return Index is Int32 index ? AnonymousTypeUtilities.Type(Anonymous, index) : throw new InvalidOperationException();
            }
        }

        private Object? Value
        {
            get
            {
                if (Property is not null)
                {
                    return AnonymousTypeUtilities.Get(Anonymous, Property).Value;
                }

                return Index is Int32 index ? AnonymousTypeUtilities.Get(Anonymous, index).Value : throw new InvalidOperationException();
            }
            set
            {
                if (Property is not null)
                {
                    AnonymousTypeUtilities.Set(Anonymous, Property, value);
                    return;
                }

                if (Index is Int32 index)
                {
                    AnonymousTypeUtilities.Set(Anonymous, index, value);
                    return;
                }

                throw new InvalidOperationException();
            }
        }

        public Maybe<Object?> Maybe
        {
            get
            {
                try
                {
                    return Get();
                }
                catch (Exception)
                {
                    return default;
                }
            }
        }

        public AnonymousObjectProperty(IAnonymousObject anonymous, Int32 index)
        {
            Anonymous = anonymous ?? throw new ArgumentNullException(nameof(anonymous));
            Index = index;
            Property = null;
        }

        public AnonymousObjectProperty(IAnonymousObject anonymous, String property)
        {
            Anonymous = anonymous ?? throw new ArgumentNullException(nameof(anonymous));
            Property = property ?? throw new ArgumentNullException(nameof(property));
            Index = null;
        }

        public AnonymousObjectProperty<T> To<T>()
        {
            return new AnonymousObjectProperty<T>(this);
        }

        public Object? Get()
        {
            return Value;
        }

        public T? Get<T>()
        {
            Object? value = Get();

            if (value is null)
            {
                return default;
            }

            return value.TryConvert(out T? result) ? result : throw new InvalidCastException($"Can't cast object of type {value.GetType()} to {typeof(T).Name}");
        }

        public Boolean Get<T>(out T? result)
        {
            return Get().TryConvert(out result);
        }

        public Boolean Set<T>(T value)
        {
            try
            {
                Value = value;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Boolean Set<T, TResult>(Func<T?, TResult> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            try
            {
                return Get(out T? result) && Set(selector(result));
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
    
    public readonly ref struct AnonymousObjectProperty<T>
    {
        public static implicit operator AnonymousObjectProperty(AnonymousObjectProperty<T> property)
        {
            return property.Anonymous;
        }
        
        public static implicit operator Maybe<T?>(AnonymousObjectProperty<T> property)
        {
            return property.Maybe;
        }

        private AnonymousObjectProperty Anonymous { get; }

        public Int32? Index
        {
            get
            {
                return Anonymous.Index;
            }
        }

        public String? Property
        {
            get
            {
                return Anonymous.Property;
            }
        }

        public Type Type
        {
            get
            {
                return Anonymous.Type;
            }
        }

        private T? Value
        {
            get
            {
                return Anonymous.Get<T>();
            }
            set
            {
                Anonymous.Set(value);
            }
        }

        public Maybe<T?> Maybe
        {
            get
            {
                try
                {
                    return Get();
                }
                catch (Exception)
                {
                    return default;
                }
            }
        }

        public AnonymousObjectProperty(IAnonymousObject anonymous, Int32 index)
        {
            Anonymous = anonymous is not null ? new AnonymousObjectProperty(anonymous, index) : throw new ArgumentNullException(nameof(anonymous));
        }

        public AnonymousObjectProperty(IAnonymousObject anonymous, String property)
        {
            Anonymous = anonymous is not null ? new AnonymousObjectProperty(anonymous, property) : throw new ArgumentNullException(nameof(anonymous));
        }

        public AnonymousObjectProperty(AnonymousObjectProperty anonymous)
        {
            Anonymous = anonymous;
        }
        
        public AnonymousObjectProperty<TConvert> To<TConvert>()
        {
            return new AnonymousObjectProperty<TConvert>(Anonymous);
        }

        public T? Get()
        {
            return Value;
        }

        public TConvert? Get<TConvert>()
        {
            return Anonymous.Get<TConvert>();
        }

        public Boolean Get<TConvert>(out TConvert? result)
        {
            return Anonymous.Get(out result);
        }

        public Boolean Set(T value)
        {
            return Anonymous.Set(value);
        }

        public Boolean Set(Func<T?, T> selector)
        {
            return Anonymous.Set(selector);
        }
    }
}