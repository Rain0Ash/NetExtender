// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Converters.Interfaces;
using NetExtender.Types.Storages;
using NetExtender.Types.Storages.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Converters
{
    public abstract class TwoWayConverter<TInput> : TwoWayConverter<TInput, String?>
    {
        public static TwoWayConverter<TInput, String?> String()
        {
            return StringConverter.Default;
        }
        
        public static TwoWayConverter<TInput, String?> String(EscapeType escape)
        {
            return new StringConverter(escape, null, null);
        }
        
        public static TwoWayConverter<TInput, String?> String(IFormatProvider? provider)
        {
            return new StringConverter(ConvertUtilities.DefaultEscapeType, null, provider);
        }
        
        public static TwoWayConverter<TInput, String?> String(EscapeType escape, IFormatProvider? provider)
        {
            return new StringConverter(escape, null, provider);
        }
        
        public static TwoWayConverter<TInput, String?> String(String? format, IFormatProvider? provider)
        {
            return new StringConverter(ConvertUtilities.DefaultEscapeType, format, provider);
        }
        
        public static TwoWayConverter<TInput, String?> String(EscapeType escape, String? format, IFormatProvider? provider)
        {
            return new StringConverter(escape, format, provider);
        }
        
        public static TwoWayConverter<TInput, String?> String(TryConverter<String?, TInput> converter)
        {
            return new StringConverter(converter, ConvertUtilities.DefaultEscapeType, null, null);
        }
        
        public static TwoWayConverter<TInput, String?> String(TryConverter<String?, TInput> converter, EscapeType escape)
        {
            return new StringConverter(converter, escape, null, null);
        }
        
        public static TwoWayConverter<TInput, String?> String(TryConverter<String?, TInput> converter, IFormatProvider? provider)
        {
            return new StringConverter(converter, ConvertUtilities.DefaultEscapeType, null, provider);
        }
        
        public static TwoWayConverter<TInput, String?> String(TryConverter<String?, TInput> converter, EscapeType escape, IFormatProvider? provider)
        {
            return new StringConverter(converter, escape, null, provider);
        }
        
        public static TwoWayConverter<TInput, String?> String(TryConverter<String?, TInput> converter, String? format, IFormatProvider? provider)
        {
            return new StringConverter(converter, ConvertUtilities.DefaultEscapeType, format, provider);
        }
        
        public static TwoWayConverter<TInput, String?> String(TryConverter<String?, TInput> converter, EscapeType escape, String? format, IFormatProvider? provider)
        {
            return new StringConverter(converter, escape, format, provider);
        }
        
        private sealed class StringConverter : TwoWayConverter<TInput>
        {
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public new static StringConverter Default { get; } = new StringConverter();
            
            public TryConverter<String?, TInput> Converter { get; }
            private EscapeType EscapeType { get; }
            private String? Format { get; }
            private IFormatProvider? Provider { get; }
            
            public StringConverter()
                : this(ConvertUtilities.DefaultEscapeType, null, null)
            {
            }
            
            public StringConverter(EscapeType escape, String? format, IFormatProvider? provider)
                : this(ConvertUtilities.TryConvert, escape, format, provider)
            {
            }
            
            public StringConverter(TryConverter<String?, TInput> converter)
                : this(converter, ConvertUtilities.DefaultEscapeType, null, null)
            {
            }
            
            public StringConverter(TryConverter<String?, TInput> converter, EscapeType escape, String? format, IFormatProvider? provider)
            {
                Converter = converter ?? throw new ArgumentNullException(nameof(converter));
                EscapeType = escape;
                Format = format;
                Provider = provider;
            }
            
            public override String? Convert(TInput input)
            {
                return input.GetString(EscapeType, Format, Provider);
            }

            public override Boolean TryConvert(TInput input, out String? output)
            {
                output = input.GetString(EscapeType, Format, Provider);
                return true;
            }

            public override TInput ConvertBack(String? input)
            {
                return Converter.Invoke(input, out TInput? output) ? output : throw new InvalidCastException();
            }

            public override Boolean TryConvertBack(String? input, [MaybeNullWhen(false)] out TInput output)
            {
                return Converter.Invoke(input, out output);
            }
        }
    }

    public abstract class TwoWayConverter<TInput, TOutput> : ITwoWayConverter<TInput, TOutput>
    {
        private static IStorage<ITwoWayConverter<TInput, TOutput>, ITwoWayConverter<TOutput, TInput>> Storage { get; } = new WeakStorage<ITwoWayConverter<TInput, TOutput>, ITwoWayConverter<TOutput, TInput>>();
        public static TwoWayConverter<TInput, TOutput> Default { get; } = new TwoWayConverterWrapper<TInput, TOutput>(ConvertUtilities.TryConvert, ConvertUtilities.TryConvert);

        public static TwoWayConverter<TInput, TOutput> Create(TryConverter<TInput, TOutput> converter)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            return new TwoWayConverterWrapper<TInput, TOutput>(converter, ConvertUtilities.TryConvert);
        }
        
        public static TwoWayConverter<TInput, TOutput> Create(IOneWayConverter<TInput, TOutput> converter)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            return Combine(converter, ConvertUtilities.TryConvert);
        }

        public static TwoWayConverter<TInput, TOutput> Combine(TryConverter<TInput, TOutput> converter, TryConverter<TOutput, TInput> reverse)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            if (reverse is null)
            {
                throw new ArgumentNullException(nameof(reverse));
            }

            return new TwoWayConverterWrapper<TInput, TOutput>(converter, reverse);
        }

        public static TwoWayConverter<TInput, TOutput> Combine(TryConverter<TInput, TOutput> converter, IOneWayConverter<TOutput, TInput> reverse)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            if (reverse is null)
            {
                throw new ArgumentNullException(nameof(reverse));
            }

            return new CombineConverter(new OneWayConverterWrapper<TInput, TOutput>(converter), reverse);
        }
        
        public static TwoWayConverter<TInput, TOutput> Combine(IOneWayConverter<TInput, TOutput> converter, TryConverter<TOutput, TInput> reverse)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            if (reverse is null)
            {
                throw new ArgumentNullException(nameof(reverse));
            }

            return new CombineConverter(converter, new OneWayConverterWrapper<TOutput, TInput>(reverse));
        }
        
        public static TwoWayConverter<TInput, TOutput> Combine(IOneWayConverter<TInput, TOutput> converter, IOneWayConverter<TOutput, TInput> reverse)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            if (reverse is null)
            {
                throw new ArgumentNullException(nameof(reverse));
            }

            return new CombineConverter(converter, reverse);
        }
        
        public ITwoWayConverter<TOutput, TInput> Reverse()
        {
            return Storage.GetOrAdd(this, static converter => new ReverseConverter(converter));
        }

        public abstract TOutput Convert(TInput input);
        public abstract Boolean TryConvert(TInput input, [MaybeNullWhen(false)] out TOutput output);
        public abstract TInput ConvertBack(TOutput input);
        public abstract Boolean TryConvertBack(TOutput input, [MaybeNullWhen(false)] out TInput output);

        private sealed class CombineConverter : TwoWayConverter<TInput, TOutput>
        {
            public IOneWayConverter<TInput, TOutput> Converter { get; }
            public new IOneWayConverter<TOutput, TInput> Reverse { get; }

            public CombineConverter(IOneWayConverter<TInput, TOutput> converter, IOneWayConverter<TOutput, TInput> reverse)
            {
                Converter = converter ?? throw new ArgumentNullException(nameof(converter));
                Reverse = reverse ?? throw new ArgumentNullException(nameof(reverse));
            }

            public override TOutput Convert(TInput input)
            {
                return Converter.Convert(input);
            }

            public override Boolean TryConvert(TInput input, [MaybeNullWhen(false)] out TOutput output)
            {
                return Converter.TryConvert(input, out output);
            }

            public override TInput ConvertBack(TOutput input)
            {
                return Reverse.Convert(input);
            }

            public override Boolean TryConvertBack(TOutput input, [MaybeNullWhen(false)] out TInput output)
            {
                return Reverse.TryConvert(input, out output);
            }
        }

        private sealed class ReverseConverter : TwoWayConverter<TOutput, TInput>
        {
            private ITwoWayConverter<TInput, TOutput> Converter { get; }

            public ReverseConverter(ITwoWayConverter<TInput, TOutput> converter)
            {
                Converter = converter ?? throw new ArgumentNullException(nameof(converter));
            }

            public override TInput Convert(TOutput input)
            {
                return Converter.ConvertBack(input);
            }

            public override Boolean TryConvert(TOutput input, [MaybeNullWhen(false)] out TInput output)
            {
                return Converter.TryConvertBack(input, out output);
            }

            public override TOutput ConvertBack(TInput input)
            {
                return Converter.Convert(input);
            }

            public override Boolean TryConvertBack(TInput input, [MaybeNullWhen(false)] out TOutput output)
            {
                return Converter.TryConvert(input, out output);
            }
        }
    }

    public sealed class TwoWayConverterWrapper<TInput, TOutput> : TwoWayConverter<TInput, TOutput>
    {
        public TryConverter<TInput, TOutput> Converter { get; }
        public new TryConverter<TOutput, TInput> Reverse { get; }

        public TwoWayConverterWrapper(TryConverter<TInput, TOutput> converter, TryConverter<TOutput, TInput> reverse)
        {
            Converter = converter ?? throw new ArgumentNullException(nameof(converter));
            Reverse = reverse ?? throw new ArgumentNullException(nameof(reverse));
        }

        public override TOutput Convert(TInput input)
        {
            return Converter.Invoke(input, out TOutput? output) ? output : throw new InvalidCastException();
        }

        public override Boolean TryConvert(TInput input, [MaybeNullWhen(false)] out TOutput output)
        {
            return Converter.Invoke(input, out output);
        }

        public override TInput ConvertBack(TOutput input)
        {
            return Reverse.Invoke(input, out TInput? output) ? output : throw new InvalidCastException();
        }

        public override Boolean TryConvertBack(TOutput input, [MaybeNullWhen(false)] out TInput output)
        {
            return Reverse.Invoke(input, out output);
        }
    }
    
    //TODO:
    public abstract class EnumConverter<TInput, TOutput> : TwoWayConverter<TInput, TOutput> where TOutput : unmanaged, Enum
    {
    }
}