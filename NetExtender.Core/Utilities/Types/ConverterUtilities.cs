// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Converters;
using NetExtender.Types.Converters.Interfaces;

namespace NetExtender.Utilities.Types
{
    public static class ConverterUtilities
    {
        public static IOneWayConverter<TInput, TOutput> Converter<TInput, TOutput>(this TryConverter<TInput, TOutput> converter)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            return new OneWayConverterWrapper<TInput, TOutput>(converter);
        }
        
        public static ITwoWayConverter<TInput, TOutput> AsTwoWay<TInput, TOutput>(this TryConverter<TInput, TOutput> converter)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            return TwoWayConverter<TInput, TOutput>.Create(converter);
        }
        
        public static ITwoWayConverter<TInput, TOutput> AsTwoWay<TInput, TOutput>(this IOneWayConverter<TInput, TOutput> converter)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            return converter as ITwoWayConverter<TInput, TOutput> ?? TwoWayConverter<TInput, TOutput>.Create(converter);
        }
        
        public static ITwoWayConverter<TInput, TOutput> AsTwoWay<TInput, TOutput>(this TryConverter<TInput, TOutput> converter, TryConverter<TOutput, TInput> reverse)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            if (reverse is null)
            {
                throw new ArgumentNullException(nameof(reverse));
            }

            return TwoWayConverter<TInput, TOutput>.Combine(converter, reverse);
        }
        
        public static ITwoWayConverter<TInput, TOutput> AsTwoWay<TInput, TOutput>(this IOneWayConverter<TInput, TOutput> converter, TryConverter<TOutput, TInput> reverse)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            if (reverse is null)
            {
                throw new ArgumentNullException(nameof(reverse));
            }
            
            return converter as ITwoWayConverter<TInput, TOutput> ?? TwoWayConverter<TInput, TOutput>.Combine(converter, reverse);
        }
        
        public static ITwoWayConverter<TInput, TOutput> AsTwoWay<TInput, TOutput>(this TryConverter<TInput, TOutput> converter, IOneWayConverter<TOutput, TInput> reverse)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            if (reverse is null)
            {
                throw new ArgumentNullException(nameof(reverse));
            }

            return TwoWayConverter<TInput, TOutput>.Combine(converter, reverse);
        }
        
        public static ITwoWayConverter<TInput, TOutput> AsTwoWay<TInput, TOutput>(this IOneWayConverter<TInput, TOutput> converter, IOneWayConverter<TOutput, TInput> reverse)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            if (reverse is null)
            {
                throw new ArgumentNullException(nameof(reverse));
            }
            
            return converter as ITwoWayConverter<TInput, TOutput> ?? TwoWayConverter<TInput, TOutput>.Combine(converter, reverse);
        }
        
        public static ITwoWayConverter<TInput, TOutput> ToTwoWay<TInput, TOutput>(this TryConverter<TInput, TOutput> converter)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            return TwoWayConverter<TInput, TOutput>.Create(converter);
        }
        
        public static ITwoWayConverter<TInput, TOutput> ToTwoWay<TInput, TOutput>(this IOneWayConverter<TInput, TOutput> converter)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            return TwoWayConverter<TInput, TOutput>.Create(converter);
        }
        
        public static ITwoWayConverter<TInput, TOutput> ToTwoWay<TInput, TOutput>(this TryConverter<TInput, TOutput> converter, TryConverter<TOutput, TInput> reverse)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            if (reverse is null)
            {
                throw new ArgumentNullException(nameof(reverse));
            }

            return TwoWayConverter<TInput, TOutput>.Combine(converter, reverse);
        }
        
        public static ITwoWayConverter<TInput, TOutput> ToTwoWay<TInput, TOutput>(this IOneWayConverter<TInput, TOutput> converter, TryConverter<TOutput, TInput> reverse)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            if (reverse is null)
            {
                throw new ArgumentNullException(nameof(reverse));
            }
            
            return TwoWayConverter<TInput, TOutput>.Combine(converter, reverse);
        }
        
        public static ITwoWayConverter<TInput, TOutput> ToTwoWay<TInput, TOutput>(this TryConverter<TInput, TOutput> converter, IOneWayConverter<TOutput, TInput> reverse)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            if (reverse is null)
            {
                throw new ArgumentNullException(nameof(reverse));
            }

            return TwoWayConverter<TInput, TOutput>.Combine(converter, reverse);
        }
        
        public static ITwoWayConverter<TInput, TOutput> ToTwoWay<TInput, TOutput>(this IOneWayConverter<TInput, TOutput> converter, IOneWayConverter<TOutput, TInput> reverse)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            if (reverse is null)
            {
                throw new ArgumentNullException(nameof(reverse));
            }
            
            return TwoWayConverter<TInput, TOutput>.Combine(converter, reverse);
        }
    }
}