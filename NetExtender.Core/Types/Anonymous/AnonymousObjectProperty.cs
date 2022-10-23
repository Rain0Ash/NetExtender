// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Anonymous.Interfaces;
using NetExtender.Types.Monads;

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
                    return AnonymousObjectProperties.Type(Anonymous, Property);
                }

                return Index is Int32 index ? AnonymousObjectProperties.Type(Anonymous, index) : throw new InvalidOperationException();
            }
        }

        private Object? Value
        {
            get
            {
                if (Property is not null)
                {
                    return AnonymousObjectProperties.Get(Anonymous, Property).Value;
                }

                return Index is Int32 index ? AnonymousObjectProperties.Get(Anonymous, index).Value : throw new InvalidOperationException();
            }
            set
            {
                if (Property is not null)
                {
                    AnonymousObjectProperties.Set(Anonymous, Property, value);
                    return;
                }

                if (Index is Int32 index)
                {
                    AnonymousObjectProperties.Set(Anonymous, index, value);
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

        public AnonymousObjectProperty(IAnonymousObject dynamic, Int32 index)
        {
            Anonymous = dynamic ?? throw new ArgumentNullException(nameof(dynamic));
            Index = index;
            Property = null;
        }
        
        public AnonymousObjectProperty(IAnonymousObject dynamic, String property)
        {
            Anonymous = dynamic ?? throw new ArgumentNullException(nameof(dynamic));
            Property = property ?? throw new ArgumentNullException(nameof(property));
            Index = null;
        }

        public Object? Get()
        {
            return Value;
        }

        public Boolean Get<T>(out T? result)
        {
            try
            {
                result = (T?) Get();
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
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
}