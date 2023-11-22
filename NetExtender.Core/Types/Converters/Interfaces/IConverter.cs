// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.Types.Converters.Interfaces
{
    public interface IOneWayConverter<in TInput, TOutput>
    {
        public TOutput Convert(TInput input);
        public Boolean TryConvert(TInput input, [MaybeNullWhen(false)] out TOutput output);
    }
    
    public interface ITwoWayConverter<TInput, TOutput> : IOneWayConverter<TInput, TOutput>
    {
        public TInput ConvertBack(TOutput input);
        public Boolean TryConvertBack(TOutput input, [MaybeNullWhen(false)] out TInput output);

        public ITwoWayConverter<TOutput, TInput> Reverse();
    }
}