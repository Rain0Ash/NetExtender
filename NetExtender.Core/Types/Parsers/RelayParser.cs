using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace NetExtender.Types.Parsers
{
    public class RelayParser<T, TResult> : Parser<T, TResult>
    {
        public ParseHandler<T, TResult>? ParseHandler { get; init; }
        public StyleParseHandler<T, TResult>? StyleParseHandler { get; init; }
        public FormatParseHandler<T, TResult>? FormatParseHandler { get; init; }
        public NumericParseHandler<T, TResult>? NumericParseHandler { get; init; }
        public TryParseHandler<T?, TResult>? TryParseHandler { get; init; }
        public StyleTryParseHandler<T?, TResult>? StyleTryParseHandler { get; init; }
        public FormatTryParseHandler<T?, TResult>? FormatTryParseHandler { get; init; }
        public NumericTryParseHandler<T?, TResult>? NumericTryParseHandler { get; init; }
        
        public sealed override TResult Parse(T value)
        {
            return ParseHandler is not null ? ParseHandler(value) : base.Parse(value);
        }
        
        public sealed override TResult Parse(T value, NumberStyles style)
        {
            return StyleParseHandler is not null ? StyleParseHandler(value, style) : base.Parse(value, style);
        }
        
        public sealed override TResult Parse(T value, IFormatProvider? provider)
        {
            return FormatParseHandler is not null ? FormatParseHandler(value, provider) : base.Parse(value, provider);
        }
        
        public sealed override TResult Parse(T value, NumberStyles style, IFormatProvider? provider)
        {
            if (NumericParseHandler is not null)
            {
                return NumericParseHandler(value, style, provider);
            }
            
            if (NumericTryParseHandler is not null)
            {
                return NumericTryParseHandler(value, style, provider, out TResult? result) ? result : throw new InvalidOperationException();
            }
            
            if (FormatParseHandler is not null)
            {
                return FormatParseHandler(value, provider);
            }
            
            if (FormatTryParseHandler is not null)
            {
                return FormatTryParseHandler(value, provider, out TResult? result) ? result : throw new InvalidOperationException();
            }
            
            if (StyleParseHandler is not null)
            {
                return StyleParseHandler(value, style);
            }
            
            if (StyleTryParseHandler is not null)
            {
                return StyleTryParseHandler(value, style, out TResult? result) ? result : throw new InvalidOperationException();
            }
            
            if (ParseHandler is not null)
            {
                return ParseHandler(value);
            }
            
            if (TryParseHandler is not null)
            {
                return TryParseHandler(value, out TResult? result) ? result : throw new InvalidOperationException();
            }

            throw new ParserException();
        }
        
        public sealed override Boolean TryParse(T? value, [MaybeNullWhen(false)] out TResult result)
        {
            return TryParseHandler is not null ? TryParseHandler(value, out result) : base.TryParse(value, out result);
        }
        
        public sealed override Boolean TryParse(T? value, NumberStyles style, [MaybeNullWhen(false)] out TResult result)
        {
            return StyleTryParseHandler is not null ? StyleTryParseHandler(value, style, out result) : base.TryParse(value, style, out result);
        }
        
        public sealed override Boolean TryParse(T? value, IFormatProvider? provider, [MaybeNullWhen(false)] out TResult result)
        {
            return FormatTryParseHandler is not null ? FormatTryParseHandler(value, provider, out result) : base.TryParse(value, provider, out result);
        }
        
        public sealed override Boolean TryParse(T? value, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out TResult result)
        {
            try
            {
                if (NumericTryParseHandler is not null)
                {
                    return NumericTryParseHandler(value, style, provider, out result);
                }

                if (NumericParseHandler is not null)
                {
                    result = NumericParseHandler(value!, style, provider);
                    return true;
                }
                
                if (FormatTryParseHandler is not null)
                {
                    return FormatTryParseHandler(value, provider, out result);
                }
                
                if (FormatParseHandler is not null)
                {
                    result = FormatParseHandler(value!, provider);
                    return true;
                }
                
                if (StyleTryParseHandler is not null)
                {
                    return StyleTryParseHandler(value, style, out result);
                }

                if (StyleParseHandler is not null)
                {
                    result = StyleParseHandler(value!, style);
                    return true;
                }

                if (TryParseHandler is not null)
                {
                    return TryParseHandler(value, out result);
                }
                
                if (ParseHandler is not null)
                {
                    result = ParseHandler(value!);
                    return true;
                }

                return base.TryParse(value, style, provider, out result);
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
    }
}