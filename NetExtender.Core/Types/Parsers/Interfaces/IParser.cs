using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace NetExtender.Types.Parsers.Interfaces
{
    public interface IParser<in T, TResult> : IParser<T>
    {
        public new TResult Parse(T value);
        public new TResult Parse(T value, NumberStyles style);
        public new TResult Parse(T value, IFormatProvider? provider);
        public new TResult Parse(T value, NumberStyles style, IFormatProvider? provider);
        public Boolean TryParse(T? value, [MaybeNullWhen(false)] out TResult result);
        public Boolean TryParse(T? value, NumberStyles style, [MaybeNullWhen(false)] out TResult result);
        public Boolean TryParse(T? value, IFormatProvider? provider, [MaybeNullWhen(false)] out TResult result);
        public Boolean TryParse(T? value, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out TResult result);
    }
    
    public interface IParser<in T> : IParser
    {
        public Object? Parse(T value);
        public Object? Parse(T value, NumberStyles style);
        public Object? Parse(T value, IFormatProvider? provider);
        public Object? Parse(T value, NumberStyles style, IFormatProvider? provider);
        public Boolean TryParse(T? value, out Object? result);
        public Boolean TryParse(T? value, NumberStyles style, out Object? result);
        public Boolean TryParse(T? value, IFormatProvider? provider, out Object? result);
        public Boolean TryParse(T? value, NumberStyles style, IFormatProvider? provider, out Object? result);
    }
    
    public interface IParser
    {
        public Object? Parse(Object? value);
        public Object? Parse(Object? value, NumberStyles style);
        public Object? Parse(Object? value, IFormatProvider? provider);
        public Object? Parse(Object? value, NumberStyles style, IFormatProvider? provider);
        public Boolean TryParse(Object? value, out Object? result);
        public Boolean TryParse(Object? value, NumberStyles style, out Object? result);
        public Boolean TryParse(Object? value, IFormatProvider? provider, out Object? result);
        public Boolean TryParse(Object? value, NumberStyles style, IFormatProvider? provider, out Object? result);
    }
}