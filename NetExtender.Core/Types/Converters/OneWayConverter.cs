// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Converters.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Converters
{
    public abstract class OneWayConverter<TInput, TOutput> : IOneWayConverter<TInput, TOutput>
    {
        public static OneWayConverter<TInput, TOutput> Default { get; } = new OneWayConverterWrapper<TInput, TOutput>(ConvertUtilities.TryConvert);
        
        public abstract TOutput Convert(TInput input);
        public abstract Boolean TryConvert(TInput input, [MaybeNullWhen(false)] out TOutput output);
    }

    public sealed class OneWayConverterWrapper<TInput, TOutput> : OneWayConverter<TInput, TOutput>
    {
        [return: NotNullIfNotNull("converter")]
        public static implicit operator TryConverter<TInput, TOutput>?(OneWayConverterWrapper<TInput, TOutput>? converter)
        {
            return converter?.Converter;
        }

        [return: NotNullIfNotNull("converter")]
        public static implicit operator OneWayConverterWrapper<TInput, TOutput>?(TryConverter<TInput, TOutput>? converter)
        {
            return converter is not null ? new OneWayConverterWrapper<TInput, TOutput>(converter) : null;
        }

        public TryConverter<TInput, TOutput> Converter { get; }

        public OneWayConverterWrapper(TryConverter<TInput, TOutput> converter)
        {
            Converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        public override TOutput Convert(TInput input)
        {
            return Converter.Invoke(input, out TOutput? output) ? output : throw new InvalidCastException();
        }

        public override Boolean TryConvert(TInput input, [MaybeNullWhen(false)] out TOutput output)
        {
            return Converter.Invoke(input, out output);
        }
    }
}