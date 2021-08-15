// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Types.Numerics
{
    /// <summary>
    /// Represents a fraction
    /// </summary>
    public readonly struct Fraction
    {
        /// <summary>
        /// Converts the fraction to a decimal
        /// </summary>
        /// <param name="fraction">Fraction</param>
        /// <returns>The fraction as a decimal</returns>
        public static implicit operator Decimal(Fraction fraction)
        {
            return fraction.Numerator / (Decimal) fraction.Denominator;
        }

        /// <summary>
        /// Converts the fraction to a double
        /// </summary>
        /// <param name="fraction">Fraction</param>
        /// <returns>The fraction as a double</returns>
        public static implicit operator Double(Fraction fraction)
        {
            return fraction.Numerator / (Double) fraction.Denominator;
        }

        /// <summary>
        /// Converts the fraction to a float
        /// </summary>
        /// <param name="fraction">Fraction</param>
        /// <returns>The fraction as a float</returns>
        public static implicit operator Single(Fraction fraction)
        {
            return fraction.Numerator / (Single) fraction.Denominator;
        }

        /// <summary>
        /// Converts the double to a fraction
        /// </summary>
        /// <param name="fraction">Fraction</param>
        /// <returns>The double as a fraction</returns>
        public static implicit operator Fraction(Double fraction)
        {
            return new Fraction(fraction, 1.0);
        }

        /// <summary>
        /// Converts the decimal to a fraction
        /// </summary>
        /// <param name="fraction">Fraction</param>
        /// <returns>The decimal as a fraction</returns>
        public static implicit operator Fraction(Decimal fraction)
        {
            return new Fraction(fraction, 1.0m);
        }

        /// <summary>
        /// Converts the float to a fraction
        /// </summary>
        /// <param name="fraction">Fraction</param>
        /// <returns>The float as a fraction</returns>
        public static implicit operator Fraction(Single fraction)
        {
            return new Fraction(fraction, 1.0);
        }

        /// <summary>
        /// Converts the int to a fraction
        /// </summary>
        /// <param name="fraction">Fraction</param>
        /// <returns>The int as a fraction</returns>
        public static implicit operator Fraction(Int32 fraction)
        {
            return new Fraction(fraction, 1);
        }

        /// <summary>
        /// Converts the uint to a fraction
        /// </summary>
        /// <param name="fraction">Fraction</param>
        /// <returns>The uint as a fraction</returns>
        public static implicit operator Fraction(UInt32 fraction)
        {
            return new Fraction((Int32) fraction, 1);
        }

        /// <summary>
        /// Converts the fraction to a string
        /// </summary>
        /// <param name="fraction">Fraction</param>
        /// <returns>The fraction as a string</returns>
        public static implicit operator String(Fraction fraction)
        {
            return fraction.ToString();
        }

        /// <summary>
        /// Subtraction
        /// </summary>
        /// <param name="first">First fraction</param>
        /// <param name="second">Second fraction</param>
        /// <returns>The subtracted fraction</returns>
        public static Fraction operator -(Fraction first, Fraction second)
        {
            Fraction value1 = new Fraction(first.Numerator * second.Denominator, first.Denominator * second.Denominator);
            Fraction value2 = new Fraction(second.Numerator * first.Denominator, second.Denominator * first.Denominator);
            Fraction result = new Fraction(value1.Numerator - value2.Numerator, value1.Denominator);
            result.Reduce();
            return result;
        }

        /// <summary>
        /// Negation of the fraction
        /// </summary>
        /// <param name="first">Fraction to negate</param>
        /// <returns>The negated fraction</returns>
        public static Fraction operator -(Fraction first)
        {
            return new Fraction(-first.Numerator, first.Denominator);
        }

        /// <summary>
        /// Not equals operator
        /// </summary>
        /// <param name="first">First item</param>
        /// <param name="second">Second item</param>
        /// <returns>True if they are, false otherwise</returns>
        public static Boolean operator !=(Fraction first, Fraction second)
        {
            return !(first == second);
        }

        /// <summary>
        /// Not equals operator
        /// </summary>
        /// <param name="first">First item</param>
        /// <param name="second">Second item</param>
        /// <returns>True if they are, false otherwise</returns>
        public static Boolean operator !=(Fraction first, Double second)
        {
            return !(first == second);
        }

        /// <summary>
        /// Not equals operator
        /// </summary>
        /// <param name="first">First item</param>
        /// <param name="second">Second item</param>
        /// <returns>True if they are, false otherwise</returns>
        public static Boolean operator !=(Double first, Fraction second)
        {
            return !(first == second);
        }

        /// <summary>
        /// Multiplication
        /// </summary>
        /// <param name="first">First fraction</param>
        /// <param name="second">Second fraction</param>
        /// <returns>The resulting fraction</returns>
        public static Fraction operator *(Fraction first, Fraction second)
        {
            Fraction result = new Fraction(first.Numerator * second.Numerator, first.Denominator * second.Denominator);
            result.Reduce();
            return result;
        }

        /// <summary>
        /// Division
        /// </summary>
        /// <param name="first">First item</param>
        /// <param name="second">Second item</param>
        /// <returns>The divided fraction</returns>
        public static Fraction operator /(Fraction first, Fraction second)
        {
            return first * second.Inverse();
        }

        /// <summary>
        /// Addition
        /// </summary>
        /// <param name="first">First fraction</param>
        /// <param name="second">Second fraction</param>
        /// <returns>The added fraction</returns>
        public static Fraction operator +(Fraction first, Fraction second)
        {
            Fraction value1 = new Fraction(first.Numerator * second.Denominator, first.Denominator * second.Denominator);
            Fraction value2 = new Fraction(second.Numerator * first.Denominator, second.Denominator * first.Denominator);
            Fraction result = new Fraction(value1.Numerator + value2.Numerator, value1.Denominator);
            result.Reduce();
            return result;
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="first">First item</param>
        /// <param name="second">Second item</param>
        /// <returns>True if they are, false otherwise</returns>
        public static Boolean operator ==(Fraction first, Fraction second)
        {
            Decimal value1 = first;
            Decimal value2 = second;
            return value1 == value2;
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="first">First item</param>
        /// <param name="second">Second item</param>
        /// <returns>True if they are, false otherwise</returns>
        public static Boolean operator ==(Fraction first, Double second)
        {
            return Math.Abs((Double) first - second) < Double.Epsilon;
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="first">First item</param>
        /// <param name="second">Second item</param>
        /// <returns>True if they are, false otherwise</returns>
        public static Boolean operator ==(Double first, Fraction second)
        {
            return Math.Abs((Double) second - first) < Double.Epsilon;
        }

        /// <summary>
        /// Denominator of the fraction
        /// </summary>
        public Int32 Denominator { get; }

        /// <summary>
        /// Numerator of the faction
        /// </summary>
        public Int32 Numerator { get; }

        /// <summary>
        /// Gets the epsilon.
        /// </summary>
        /// <value>The epsilon.</value>
        private const Double Epsilon = 0.001d;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="numerator">Numerator</param>
        /// <param name="denominator">Denominator</param>
        public Fraction(Int32 numerator, Int32 denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="numerator">Numerator</param>
        /// <param name="denominator">Denominator</param>
        public Fraction(Double numerator, Double denominator)
        {
            while (Math.Abs(numerator - Math.Round(numerator, MidpointRounding.AwayFromZero)) > Epsilon
                   || Math.Abs(denominator - Math.Round(denominator, MidpointRounding.AwayFromZero)) > Epsilon)
            {
                numerator *= 10;
                denominator *= 10;
            }

            Numerator = (Int32) numerator;
            Denominator = (Int32) denominator;
            if (Denominator == Int32.MinValue)
            {
                return;
            }

            Reduce();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="numerator">Numerator</param>
        /// <param name="denominator">Denominator</param>
        public Fraction(Decimal numerator, Decimal denominator)
        {
            while (numerator != Math.Round(numerator, MidpointRounding.AwayFromZero)
                   || denominator != Math.Round(denominator, MidpointRounding.AwayFromZero))
            {
                numerator *= 10;
                denominator *= 10;
            }

            Numerator = (Int32) numerator;
            Denominator = (Int32) denominator;
            Reduce();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="numerator">Numerator</param>
        /// <param name="denominator">Denominator</param>
        /// <exception cref="ArgumentException">denominator</exception>
        public Fraction(Single numerator, Single denominator)
        {
            if (Math.Abs(denominator - Int32.MinValue) < Epsilon)
            {
                throw new ArgumentException(@"Denominator integer can't be minimum", nameof(denominator));
            }

            while (Math.Abs(numerator - Math.Round(numerator, MidpointRounding.AwayFromZero)) > Epsilon
                   || Math.Abs(denominator - Math.Round(denominator, MidpointRounding.AwayFromZero)) > Epsilon)
            {
                numerator *= 10;
                denominator *= 10;
            }

            Numerator = (Int32) numerator;
            Denominator = (Int32) denominator;
            if (Denominator == Int32.MinValue)
            {
                return;
            }

            Reduce();
        }

        /// <summary>
        /// Adds the specified values.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns>The result</returns>
        public static Fraction Add(Fraction first, Fraction second)
        {
            return first + second;
        }

        /// <summary>
        /// Divides the specified values.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns>The result</returns>
        public static Fraction Divide(Fraction first, Fraction second)
        {
            return first / second;
        }

        /// <summary>
        /// Multiplies the specified values.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns>The result</returns>
        public static Fraction Multiply(Fraction first, Fraction second)
        {
            return first * second;
        }

        /// <summary>
        /// Negates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The result</returns>
        public static Fraction Negate(Fraction item)
        {
            return -item;
        }

        /// <summary>
        /// Subtracts the specified values.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns>The result</returns>
        public static Fraction Subtract(Fraction first, Fraction second)
        {
            return first - second;
        }

        /// <summary>
        /// Converts to fraction.
        /// </summary>
        /// <returns>The value as a fraction.</returns>
        public static Fraction ToFraction(Double value)
        {
            return value;
        }

        /// <summary>
        /// Converts to fraction.
        /// </summary>
        /// <returns>The value as a fraction.</returns>
        public static Fraction ToFraction(Single value)
        {
            return value;
        }

        /// <summary>
        /// Converts to fraction.
        /// </summary>
        /// <returns>The value as a fraction.</returns>
        public static Fraction ToFraction(Decimal value)
        {
            return value;
        }

        /// <summary>
        /// Converts to fraction.
        /// </summary>
        /// <returns>The value as a fraction.</returns>
        public static Fraction ToFraction(Int32 value)
        {
            return value;
        }

        /// <summary>
        /// Converts to fraction.
        /// </summary>
        /// <returns>The value as a fraction.</returns>
        public static Fraction ToFraction(UInt32 value)
        {
            return value;
        }

        /// <summary>
        /// Determines if the fractions are equal
        /// </summary>
        /// <param name="obj">object to check</param>
        /// <returns>True if they are, false otherwise</returns>
        public override Boolean Equals(Object? obj)
        {
            if (obj is not Fraction other)
            {
                return false;
            }

            Decimal value1 = this;
            Decimal value2 = other;
            return value1 == value2;
        }

        /// <summary>
        /// Gets the hash code of the fraction
        /// </summary>
        /// <returns>The hash code of the fraction</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override Int32 GetHashCode()
        {
            return Numerator.GetHashCode() % Denominator.GetHashCode();
        }

        /// <summary>
        /// Returns the inverse of the fraction
        /// </summary>
        /// <returns>The inverse</returns>
        public Fraction Inverse()
        {
            return new Fraction(Denominator, Numerator);
        }

        /// <summary>
        /// Reduces the fraction (finds the greatest common denominator and divides the
        /// numerator/denominator by it).
        /// </summary>
        public Fraction Reduce()
        {
            Int32 numerator = Numerator;

            if (numerator == Int32.MinValue)
            {
                numerator = Int32.MinValue + 1;
            }

            Int32 denominator = Denominator;

            if (denominator == Int32.MinValue)
            {
                denominator = Int32.MinValue + 1;
            }

            Int32 gcd = numerator.Gcd(denominator);

            return gcd == 0 ? this : new Fraction(numerator / gcd, denominator / gcd);
        }

        /// <summary>
        /// Converts to decimal.
        /// </summary>
        /// <returns>The decimal value.</returns>
        public Decimal ToDecimal()
        {
            return this;
        }

        /// <summary>
        /// Converts to double.
        /// </summary>
        /// <returns>The value as a double</returns>
        public Double ToDouble()
        {
            return this;
        }

        /// <summary>
        /// Converts to single.
        /// </summary>
        /// <returns>The value as a single.</returns>
        public Single ToSingle()
        {
            return this;
        }

        /// <summary>
        /// Displays the fraction as a string
        /// </summary>
        /// <returns>The fraction as a string</returns>
        public override String ToString()
        {
            return $"{Numerator}/{Denominator}";
        }
    }
}