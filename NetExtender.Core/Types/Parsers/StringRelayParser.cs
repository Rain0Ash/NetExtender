// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace NetExtender.Types.Parsers
{
    public class StringRelayParser<TResult> : StringParser<TResult>
    {
        public ParseHandler<String, TResult>? ParseHandler { get; init; }
        public ParseHandler<TResult>? ParseSpanHandler { get; init; }
        public StyleParseHandler<String, TResult>? StyleParseHandler { get; init; }
        public StyleParseHandler<TResult>? StyleParseSpanHandler { get; init; }
        public FormatParseHandler<String, TResult>? FormatParseHandler { get; init; }
        public FormatParseHandler<TResult>? FormatParseSpanHandler { get; init; }
        public NumericParseHandler<String, TResult>? NumericParseHandler { get; init; }
        public NumericParseHandler<TResult>? NumericParseSpanHandler { get; init; }
        public TryParseHandler<String?, TResult>? TryParseHandler { get; init; }
        public TryParseHandler<TResult>? TryParseSpanHandler { get; init; }
        public StyleTryParseHandler<String?, TResult>? StyleTryParseHandler { get; init; }
        public StyleTryParseHandler<TResult>? StyleTryParseSpanHandler { get; init; }
        public FormatTryParseHandler<String?, TResult>? FormatTryParseHandler { get; init; }
        public FormatTryParseHandler<TResult>? FormatTryParseSpanHandler { get; init; }
        public NumericTryParseHandler<String?, TResult>? NumericTryParseHandler { get; init; }
        public NumericTryParseHandler<TResult>? NumericTryParseSpanHandler { get; init; }

        public sealed override TResult Parse(String value)
        {
            return ParseHandler is not null ? ParseHandler(value) : base.Parse(value);
        }
        
        public sealed override TResult Parse(ReadOnlySpan<Char> value)
        {
            return ParseSpanHandler is not null ? ParseSpanHandler.Invoke(value) : base.Parse(value);
        }
        
        public sealed override TResult Parse(String value, NumberStyles style)
        {
            return StyleParseHandler is not null ? StyleParseHandler(value, style) : base.Parse(value, style);
        }
        
        public sealed override TResult Parse(ReadOnlySpan<Char> value, NumberStyles style)
        {
            return StyleParseSpanHandler is not null ? StyleParseSpanHandler(value, style) : base.Parse(value, style);
        }
        
        public sealed override TResult Parse(String value, IFormatProvider? provider)
        {
            return FormatParseHandler is not null ? FormatParseHandler(value, provider) : base.Parse(value, provider);
        }
        
        public sealed override TResult Parse(ReadOnlySpan<Char> value, IFormatProvider? provider)
        {
            return FormatParseSpanHandler is not null ? FormatParseSpanHandler(value, provider) : base.Parse(value, provider);
        }
        
        // ReSharper disable once CognitiveComplexity
        public sealed override TResult Parse(String value, NumberStyles style, IFormatProvider? provider)
        {
            if (NumericParseHandler is not null)
            {
                return NumericParseHandler(value, style, provider);
            }
            
            if (NumericParseSpanHandler is not null)
            {
                return NumericParseSpanHandler(value, style, provider);
            }
            
            if (NumericTryParseHandler is not null)
            {
                return NumericTryParseHandler(value, style, provider, out TResult? result) ? result : throw new InvalidOperationException();
            }
            
            if (NumericTryParseSpanHandler is not null)
            {
                return NumericTryParseSpanHandler(value, style, provider, out TResult? result) ? result : throw new InvalidOperationException();
            }
            
            if (FormatParseHandler is not null)
            {
                return FormatParseHandler(value, provider);
            }
            
            if (FormatParseSpanHandler is not null)
            {
                return FormatParseSpanHandler(value, provider);
            }
            
            if (FormatTryParseHandler is not null)
            {
                return FormatTryParseHandler(value, provider, out TResult? result) ? result : throw new InvalidOperationException();
            }
            
            if (FormatTryParseSpanHandler is not null)
            {
                return FormatTryParseSpanHandler(value, provider, out TResult? result) ? result : throw new InvalidOperationException();
            }
            
            if (StyleParseHandler is not null)
            {
                return StyleParseHandler(value, style);
            }
            
            if (StyleParseSpanHandler is not null)
            {
                return StyleParseSpanHandler(value, style);
            }
            
            if (StyleTryParseHandler is not null)
            {
                return StyleTryParseHandler(value, style, out TResult? result) ? result : throw new InvalidOperationException();
            }
            
            if (StyleTryParseSpanHandler is not null)
            {
                return StyleTryParseSpanHandler(value, style, out TResult? result) ? result : throw new InvalidOperationException();
            }
            
            if (ParseHandler is not null)
            {
                return ParseHandler(value);
            }
            
            if (ParseSpanHandler is not null)
            {
                return ParseSpanHandler(value);
            }
            
            if (TryParseHandler is not null)
            {
                return TryParseHandler(value, out TResult? result) ? result : throw new InvalidOperationException();
            }
            
            if (TryParseSpanHandler is not null)
            {
                return TryParseSpanHandler(value, out TResult? result) ? result : throw new InvalidOperationException();
            }
            
            throw new ParserException();
        }
        
        // ReSharper disable once CognitiveComplexity
        public sealed override TResult Parse(ReadOnlySpan<Char> value, NumberStyles style, IFormatProvider? provider)
        {
            if (NumericParseSpanHandler is not null)
            {
                return NumericParseSpanHandler(value, style, provider);
            }
            
            if (NumericTryParseSpanHandler is not null)
            {
                return NumericTryParseSpanHandler(value, style, provider, out TResult? result) ? result : throw new InvalidOperationException();
            }
            
            if (NumericParseHandler is not null)
            {
                return NumericParseHandler(new String(value), style, provider);
            }
            
            if (NumericTryParseHandler is not null)
            {
                return NumericTryParseHandler(new String(value), style, provider, out TResult? result) ? result : throw new InvalidOperationException();
            }
            
            if (FormatParseSpanHandler is not null)
            {
                return FormatParseSpanHandler(value, provider);
            }
            
            if (FormatTryParseSpanHandler is not null)
            {
                return FormatTryParseSpanHandler(value, provider, out TResult? result) ? result : throw new InvalidOperationException();
            }
            
            if (FormatParseHandler is not null)
            {
                return FormatParseHandler(new String(value), provider);
            }
            
            if (FormatTryParseHandler is not null)
            {
                return FormatTryParseHandler(new String(value), provider, out TResult? result) ? result : throw new InvalidOperationException();
            }
            
            if (StyleParseSpanHandler is not null)
            {
                return StyleParseSpanHandler(value, style);
            }
            
            if (StyleTryParseSpanHandler is not null)
            {
                return StyleTryParseSpanHandler(value, style, out TResult? result) ? result : throw new InvalidOperationException();
            }
            
            if (StyleParseHandler is not null)
            {
                return StyleParseHandler(new String(value), style);
            }
            
            if (StyleTryParseHandler is not null)
            {
                return StyleTryParseHandler(new String(value), style, out TResult? result) ? result : throw new InvalidOperationException();
            }
            
            if (ParseSpanHandler is not null)
            {
                return ParseSpanHandler(value);
            }
            
            if (TryParseSpanHandler is not null)
            {
                return TryParseSpanHandler(value, out TResult? result) ? result : throw new InvalidOperationException();
            }
            
            if (ParseHandler is not null)
            {
                return ParseHandler(new String(value));
            }
            
            if (TryParseHandler is not null)
            {
                return TryParseHandler(new String(value), out TResult? result) ? result : throw new InvalidOperationException();
            }
            
            throw new ParserException();
        }
        
        public sealed override Boolean TryParse(String? value, [MaybeNullWhen(false)] out TResult result)
        {
            return TryParseHandler is not null ? TryParseHandler(value, out result) : base.TryParse(value, out result);
        }
        
        public sealed override Boolean TryParse(ReadOnlySpan<Char> value, [MaybeNullWhen(false)] out TResult result)
        {
            return TryParseSpanHandler is not null ? TryParseSpanHandler(value, out result) : base.TryParse(value, out result);
        }
        
        public sealed override Boolean TryParse(String? value, NumberStyles style, [MaybeNullWhen(false)] out TResult result)
        {
            return StyleTryParseHandler is not null ? StyleTryParseHandler(value, style, out result) : base.TryParse(value, style, out result);
        }
        
        public sealed override Boolean TryParse(ReadOnlySpan<Char> value, NumberStyles style, [MaybeNullWhen(false)] out TResult result)
        {
            return StyleTryParseSpanHandler is not null ? StyleTryParseSpanHandler(value, style, out result) : base.TryParse(value, style, out result);
        }
        
        public sealed override Boolean TryParse(String? value, IFormatProvider? provider, [MaybeNullWhen(false)] out TResult result)
        {
            return FormatTryParseHandler is not null ? FormatTryParseHandler(value, provider, out result) : base.TryParse(value, provider, out result);
        }
        
        public sealed override Boolean TryParse(ReadOnlySpan<Char> value, IFormatProvider? provider, [MaybeNullWhen(false)] out TResult result)
        {
            return FormatTryParseSpanHandler is not null ? FormatTryParseSpanHandler(value, provider, out result) : base.TryParse(value, provider, out result);
        }
        
        // ReSharper disable once CognitiveComplexity
        public sealed override Boolean TryParse(String? value, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out TResult result)
        {
            try
            {
                if (NumericTryParseHandler is not null)
                {
                    return NumericTryParseHandler(value, style, provider, out result);
                }
                
                if (NumericTryParseSpanHandler is not null)
                {
                    return NumericTryParseSpanHandler(value, style, provider, out result);
                }

                if (NumericParseHandler is not null)
                {
                    result = NumericParseHandler(value!, style, provider);
                    return true;
                }

                if (NumericParseSpanHandler is not null)
                {
                    result = NumericParseSpanHandler(value, style, provider);
                    return true;
                }

                if (FormatTryParseHandler is not null)
                {
                    return FormatTryParseHandler(value, provider, out result);
                }

                if (FormatTryParseSpanHandler is not null)
                {
                    return FormatTryParseSpanHandler(value, provider, out result);
                }

                if (FormatParseHandler is not null)
                {
                    result = FormatParseHandler(value!, provider);
                    return true;
                }

                if (FormatParseSpanHandler is not null)
                {
                    result = FormatParseSpanHandler(value, provider);
                    return true;
                }

                if (StyleTryParseHandler is not null)
                {
                    return StyleTryParseHandler(value, style, out result);
                }

                if (StyleTryParseSpanHandler is not null)
                {
                    return StyleTryParseSpanHandler(value, style, out result);
                }

                if (StyleParseHandler is not null)
                {
                    result = StyleParseHandler(value!, style);
                    return true;
                }

                if (StyleParseSpanHandler is not null)
                {
                    result = StyleParseSpanHandler(value, style);
                    return true;
                }

                if (TryParseHandler is not null)
                {
                    return TryParseHandler(value, out result);
                }

                if (TryParseSpanHandler is not null)
                {
                    return TryParseSpanHandler(value, out result);
                }

                if (ParseHandler is not null)
                {
                    result = ParseHandler(value!);
                    return true;
                }

                if (ParseSpanHandler is not null)
                {
                    result = ParseSpanHandler(value);
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
        
        // ReSharper disable once CognitiveComplexity
        public sealed override Boolean TryParse(ReadOnlySpan<Char> value, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out TResult result)
        {
            try
            {
                if (NumericTryParseSpanHandler is not null)
                {
                    return NumericTryParseSpanHandler(value, style, provider, out result);
                }
                
                if (NumericParseSpanHandler is not null)
                {
                    result = NumericParseSpanHandler(value, style, provider);
                    return true;
                }
                
                if (NumericTryParseHandler is not null)
                {
                    return NumericTryParseHandler(new String(value), style, provider, out result);
                }
                
                if (NumericParseHandler is not null)
                {
                    result = NumericParseHandler(new String(value), style, provider);
                    return true;
                }
                
                if (FormatTryParseSpanHandler is not null)
                {
                    return FormatTryParseSpanHandler(value, provider, out result);
                }
                
                if (FormatParseSpanHandler is not null)
                {
                    result = FormatParseSpanHandler(value, provider);
                    return true;
                }
                
                if (FormatTryParseHandler is not null)
                {
                    return FormatTryParseHandler(new String(value), provider, out result);
                }
                
                if (FormatParseHandler is not null)
                {
                    result = FormatParseHandler(new String(value), provider);
                    return true;
                }
                
                if (StyleTryParseSpanHandler is not null)
                {
                    return StyleTryParseSpanHandler(value, style, out result);
                }
                
                if (StyleParseSpanHandler is not null)
                {
                    result = StyleParseSpanHandler(value, style);
                    return true;
                }
                
                if (StyleTryParseHandler is not null)
                {
                    return StyleTryParseHandler(new String(value), style, out result);
                }
                
                if (StyleParseHandler is not null)
                {
                    result = StyleParseHandler(new String(value), style);
                    return true;
                }
                
                if (TryParseSpanHandler is not null)
                {
                    return TryParseSpanHandler(value, out result);
                }
                
                if (ParseSpanHandler is not null)
                {
                    result = ParseSpanHandler(value);
                    return true;
                }
                
                if (TryParseHandler is not null)
                {
                    return TryParseHandler(new String(value), out result);
                }
                
                if (ParseHandler is not null)
                {
                    result = ParseHandler(new String(value));
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