using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using NetExtender.Types.Parsers.Interfaces;

namespace NetExtender.Types.Parsers
{
    public abstract class Parser<T, TResult> : IParser<T, TResult>
    {
        public virtual TResult Parse(T value)
        {
            return Parse(value, null);
        }
        
        Object? IParser<T>.Parse(T value)
        {
            return Parse(value);
        }
        
        Object? IParser.Parse(Object? value)
        {
            if (value is not T convert)
            {
                convert = value is not null ? (T) Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture) : default!;
            }
            
            return Parse(convert);
        }
        
        public virtual TResult Parse(T value, NumberStyles style)
        {
            return Parse(value, style, null);
        }
        
        Object? IParser<T>.Parse(T value, NumberStyles style)
        {
            return Parse(value, style);
        }
        
        Object? IParser.Parse(Object? value, NumberStyles style)
        {
            if (value is not T convert)
            {
                convert = value is not null ? (T) Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture) : default!;
            }
            
            return Parse(convert, style);
        }
        
        public virtual TResult Parse(T value, IFormatProvider? provider)
        {
            return Parse(value, NumberStyles.Any, provider);
        }
        
        Object? IParser<T>.Parse(T value, IFormatProvider? provider)
        {
            return Parse(value, provider);
        }
        
        Object? IParser.Parse(Object? value, IFormatProvider? provider)
        {
            if (value is not T convert)
            {
                convert = value is not null ? (T) Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture) : default!;
            }
            
            return Parse(convert, provider);
        }
        
        public abstract TResult Parse(T value, NumberStyles style, IFormatProvider? provider);
        
        Object? IParser<T>.Parse(T value, NumberStyles style, IFormatProvider? provider)
        {
            return Parse(value, style, provider);
        }
        
        Object? IParser.Parse(Object? value, NumberStyles style, IFormatProvider? provider)
        {
            if (value is not T convert)
            {
                convert = value is not null ? (T) Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture) : default!;
            }
            
            return Parse(convert, style, provider);
        }
        
        public virtual Boolean TryParse(T? value, [MaybeNullWhen(false)] out TResult result)
        {
            return TryParse(value, null, out result);
        }
        
        Boolean IParser<T>.TryParse(T? value, out Object? result)
        {
            if (TryParse(value, out TResult? convert))
            {
                result = convert;
                return true;
            }
            
            result = default;
            return false;
        }
        
        Boolean IParser.TryParse(Object? value, out Object? result)
        {
            if (value is not T convert)
            {
                convert = value is not null ? (T) Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture) : default!;
            }
            
            return ((IParser<T>) this).TryParse(convert, out result);
        }
        
        public virtual Boolean TryParse(T? value, NumberStyles style, [MaybeNullWhen(false)] out TResult result)
        {
            return TryParse(value, style, null, out result);
        }
        
        Boolean IParser<T>.TryParse(T? value, NumberStyles style, out Object? result)
        {
            if (TryParse(value, style, out TResult? convert))
            {
                result = convert;
                return true;
            }
            
            result = default;
            return false;
        }
        
        Boolean IParser.TryParse(Object? value, NumberStyles style, out Object? result)
        {
            if (value is not T convert)
            {
                convert = value is not null ? (T) Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture) : default!;
            }
            
            return ((IParser<T>) this).TryParse(convert, style, out result);
        }
        
        public virtual Boolean TryParse(T? value, IFormatProvider? provider, [MaybeNullWhen(false)] out TResult result)
        {
            return TryParse(value, NumberStyles.Any, provider, out result);
        }
        
        Boolean IParser<T>.TryParse(T? value, IFormatProvider? provider, out Object? result)
        {
            if (TryParse(value, provider, out TResult? convert))
            {
                result = convert;
                return true;
            }
            
            result = default;
            return false;
        }
        
        Boolean IParser.TryParse(Object? value, IFormatProvider? provider, out Object? result)
        {
            if (value is not T convert)
            {
                convert = value is not null ? (T) Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture) : default!;
            }
            
            return ((IParser<T>) this).TryParse(convert, provider, out result);
        }
        
        public virtual Boolean TryParse(T? value, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out TResult result)
        {
            try
            {
                result = Parse(value!, style, provider);
                return true;
            }
            catch (ParserException)
            {
                throw;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }
        
        Boolean IParser<T>.TryParse(T? value, NumberStyles style, IFormatProvider? provider, out Object? result)
        {
            if (TryParse(value, style, provider, out TResult? convert))
            {
                result = convert;
                return true;
            }
            
            result = default;
            return false;
        }
        
        Boolean IParser.TryParse(Object? value, NumberStyles style, IFormatProvider? provider, out Object? result)
        {
            if (value is not T convert)
            {
                convert = value is not null ? (T) Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture) : default!;
            }
            
            return ((IParser<T>) this).TryParse(convert, style, provider, out result);
        }
    }
}