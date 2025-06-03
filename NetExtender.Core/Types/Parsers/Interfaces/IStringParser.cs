// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace NetExtender.Types.Parsers.Interfaces
{
    public interface IStringParser<TResult> : IStringParser, IParser<String, TResult>
    {
        public new TResult Parse(ReadOnlySpan<Char> value);
        public new TResult Parse(ReadOnlySpan<Char> value, NumberStyles style);
        public new TResult Parse(ReadOnlySpan<Char> value, IFormatProvider? provider);
        public new TResult Parse(ReadOnlySpan<Char> value, NumberStyles style, IFormatProvider? provider);
        public Boolean TryParse(ReadOnlySpan<Char> value, [MaybeNullWhen(false)] out TResult result);
        public Boolean TryParse(ReadOnlySpan<Char> value, NumberStyles style, [MaybeNullWhen(false)] out TResult result);
        public Boolean TryParse(ReadOnlySpan<Char> value, IFormatProvider? provider, [MaybeNullWhen(false)] out TResult result);
        public Boolean TryParse(ReadOnlySpan<Char> value, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out TResult result);
    }
    
    public interface IStringParser : IParser<String>
    {
        public Object? Parse(ReadOnlySpan<Char> value);
        public Object? Parse(ReadOnlySpan<Char> value, NumberStyles style);
        public Object? Parse(ReadOnlySpan<Char> value, IFormatProvider? provider);
        public Object? Parse(ReadOnlySpan<Char> value, NumberStyles style, IFormatProvider? provider);
        public Boolean TryParse(ReadOnlySpan<Char> value, out Object? result);
        public Boolean TryParse(ReadOnlySpan<Char> value, NumberStyles style, out Object? result);
        public Boolean TryParse(ReadOnlySpan<Char> value, IFormatProvider? provider, out Object? result);
        public Boolean TryParse(ReadOnlySpan<Char> value, NumberStyles style, IFormatProvider? provider, out Object? result);
    }
}