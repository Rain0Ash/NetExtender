using System;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Numerics.Physics
{
    public static class TemperatureUtilities
    {
        public const Char Kelvin = 'K';
        public const Char Celsius = '℃';
        public const Char Fahrenheit = '℉';
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single KelvinToCelsius(Single value)
        {
            return value - 273.15F;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double KelvinToCelsius(Double value)
        {
            return value - 273.15;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal KelvinToCelsius(Decimal value)
        {
            return value - 273.15M;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single KelvinToFahrenheit(Single value)
        {
            Single celsius = KelvinToCelsius(value);
            return CelsiusToFahrenheit(celsius);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double KelvinToFahrenheit(Double value)
        {
            Double celsius = KelvinToCelsius(value);
            return CelsiusToFahrenheit(celsius);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal KelvinToFahrenheit(Decimal value)
        {
            Decimal celsius = KelvinToCelsius(value);
            return CelsiusToFahrenheit(celsius);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single CelsiusToKelvin(Single value)
        {
            return value + 273.15F;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double CelsiusToKelvin(Double value)
        {
            return value + 273.15;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal CelsiusToKelvin(Decimal value)
        {
            return value + 273.15M;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single CelsiusToFahrenheit(Single value)
        {
            return value * 9F / 5F + 32F;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double CelsiusToFahrenheit(Double value)
        {
            return value * 9.0 / 5.0 + 32.0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal CelsiusToFahrenheit(Decimal value)
        {
            return value * 9M / 5M + 32M;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single FahrenheitToKelvin(Single value)
        {
            Single celsius = FahrenheitToCelsius(value);
            return CelsiusToKelvin(celsius);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double FahrenheitToKelvin(Double value)
        {
            Double celsius = FahrenheitToCelsius(value);
            return CelsiusToKelvin(celsius);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal FahrenheitToKelvin(Decimal value)
        {
            Decimal celsius = FahrenheitToCelsius(value);
            return CelsiusToKelvin(celsius);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single FahrenheitToCelsius(Single value)
        {
            return (value - 32F) * 5F / 9F;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double FahrenheitToCelsius(Double value)
        {
            return (value - 32.0) * 5.0 / 9.0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal FahrenheitToCelsius(Decimal value)
        {
            return (value - 32M) * 5M / 9M;
        }
    }
}