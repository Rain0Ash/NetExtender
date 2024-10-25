using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using NetExtender.Types.Parsers.Interfaces;

namespace NetExtender.Types.Parsers
{
    public abstract class StringParser<TResult> : Parser<String, TResult>, IStringParser<TResult>
    {
        public virtual TResult Parse(ReadOnlySpan<Char> value)
        {
            return Parse(value, null);
        }
        
        Object? IStringParser.Parse(ReadOnlySpan<Char> value)
        {
            return Parse(value);
        }
        
        public virtual TResult Parse(ReadOnlySpan<Char> value, NumberStyles style)
        {
            return Parse(value, style, null);
        }
        
        Object? IStringParser.Parse(ReadOnlySpan<Char> value, NumberStyles style)
        {
            return Parse(value, style);
        }
        
        public virtual TResult Parse(ReadOnlySpan<Char> value, IFormatProvider? provider)
        {
            return Parse(value, NumberStyles.Any, provider);
        }
        
        Object? IStringParser.Parse(ReadOnlySpan<Char> value, IFormatProvider? provider)
        {
            return Parse(value, provider);
        }
        
        public virtual TResult Parse(ReadOnlySpan<Char> value, NumberStyles style, IFormatProvider? provider)
        {
            return ((Parser<String, TResult>) this).Parse(new String(value), style, provider);
        }
        
        Object? IStringParser.Parse(ReadOnlySpan<Char> value, NumberStyles style, IFormatProvider? provider)
        {
            return Parse(value, style, provider);
        }
        
        public virtual Boolean TryParse(ReadOnlySpan<Char> value, [MaybeNullWhen(false)] out TResult result)
        {
            return TryParse(value, null, out result);
        }
        
        Boolean IStringParser.TryParse(ReadOnlySpan<Char> value, out Object? result)
        {
            if (TryParse(value, out TResult? convert))
            {
                result = convert;
                return true;
            }
            
            result = default;
            return false;
        }
        
        public virtual Boolean TryParse(ReadOnlySpan<Char> value, NumberStyles style, [MaybeNullWhen(false)] out TResult result)
        {
            return TryParse(value, style, null, out result);
        }
        
        Boolean IStringParser.TryParse(ReadOnlySpan<Char> value, NumberStyles style, out Object? result)
        {
            if (TryParse(value, style, out TResult? convert))
            {
                result = convert;
                return true;
            }
            
            result = default;
            return false;
        }
        
        public virtual Boolean TryParse(ReadOnlySpan<Char> value, IFormatProvider? provider, [MaybeNullWhen(false)] out TResult result)
        {
            return TryParse(value, NumberStyles.Any, provider, out result);
        }
        
        Boolean IStringParser.TryParse(ReadOnlySpan<Char> value, IFormatProvider? provider, out Object? result)
        {
            if (TryParse(value, provider, out TResult? convert))
            {
                result = convert;
                return true;
            }
            
            result = default;
            return false;
        }
        
        public virtual Boolean TryParse(ReadOnlySpan<Char> value, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out TResult result)
        {
            return ((Parser<String, TResult>) this).TryParse(new String(value), style, provider, out result);
        }
        
        Boolean IStringParser.TryParse(ReadOnlySpan<Char> value, NumberStyles style, IFormatProvider? provider, out Object? result)
        {
            if (TryParse(value, style, provider, out TResult? convert))
            {
                result = convert;
                return true;
            }
            
            result = default;
            return false;
        }
    }
}