// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.Contracts;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Types.Numerics
{
    [Serializable]
    public readonly struct AngleDegree : IEquality<AngleDegree>, IFormattable
    {
        public static implicit operator AngleDegree(Double degree)
        {
            return new AngleDegree(degree);
        }

        public static explicit operator Double(AngleDegree angle)
        {
            return angle.Degree;
        }
        
        /// <summary>
        /// Represents the zero <see cref="AngleDegree"/> value (0 degrees).
        /// </summary>
        public static AngleDegree Zero { get; } = new AngleDegree(AngleUtilities.Degree.Double.Zero);
        
        /// <summary>
        /// Represents the golden <see cref="AngleDegree"/> value (~137.508 degrees).
        /// </summary>
        public static AngleDegree Golden { get; } = AngleRadian.Golden.ToAngleDegree();

        /// <summary>
        /// Represents the quarter <see cref="AngleDegree"/> value (90 degrees).
        /// </summary>
        public static AngleDegree Right { get; } = new AngleDegree(AngleUtilities.Degree.Double.Quarter);

        /// <summary>
        /// Represents the straight <see cref="AngleDegree"/> value (180 degrees).
        /// </summary>
        public static AngleDegree Straight { get; } = new AngleDegree(AngleUtilities.Degree.Double.Straight);

        /// <summary>
        /// Represents the three quarter <see cref="AngleDegree"/> value (270 degrees).
        /// </summary>
        public static AngleDegree ThreeQuarter { get; } = new AngleDegree(AngleUtilities.Degree.Double.ThreeQuarter);

        /// <summary>
        /// Represents the full <see cref="AngleDegree"/> value (360 degrees).
        /// </summary>
        public static AngleDegree Full { get; } = new AngleDegree(AngleUtilities.Degree.Double.Full);
        
        /// <summary>
        /// Represents the smallest possible value of a <see cref="AngleDegree"/>.
        /// </summary>
        public static AngleDegree MinValue { get; } = new AngleDegree(Double.MinValue);

        /// <summary>
        /// Represents the largest possible value of a <see cref="AngleDegree"/>.
        /// </summary>
        public static AngleDegree MaxValue { get; } = new AngleDegree(Double.MaxValue);

        /// <summary>
        /// Indicates whether two <see cref="AngleDegree"/> instances are equal.
        /// </summary>
        /// <param name="first">The first angle to compare.</param>
        /// <param name="second">The second angle to compare.</param>
        /// <returns>true if the values of a1 and a2 are equal; otherwise, false.</returns>
        [Pure]
        public static Boolean operator ==(AngleDegree first, AngleDegree second)
        {
            return Math.Abs(first.Degree - second.Degree) < Double.Epsilon;
        }

        /// <summary>
        /// Indicates whether two <see cref="AngleDegree"/> instances are equal.
        /// </summary>
        /// <param name="first">The first angle to compare.</param>
        /// <param name="second">The second angle to compare.</param>
        /// <returns>true if the values of a1 and a2 are equal; otherwise, false.</returns>
        [Pure]
        public static Boolean operator !=(AngleDegree first, AngleDegree second)
        {
            return Math.Abs(first.Degree - second.Degree) >= Double.Epsilon;
        }

        /// <summary>
        /// Indicates whether a specified <see cref="AngleDegree"/> is less than another specified <see cref="AngleDegree"/>.
        /// </summary>
        /// <param name="first">The first angle to compare.</param>
        /// <param name="second">The second angle to compare.</param>
        /// <returns>true if the value of a1 is less than the value of a2; otherwise, false.</returns>
        [Pure]
        public static Boolean operator <(AngleDegree first, AngleDegree second)
        {
            return first.Degree < second.Degree;
        }

        /// <summary>
        /// Indicates whether a specified <see cref="AngleDegree"/> is greater than another specified <see cref="AngleDegree"/>.
        /// </summary>
        /// <param name="first">The first angle to compare.</param>
        /// <param name="second">The second angle to compare.</param>
        /// <returns>true if the value of a1 is greater than the value of a2; otherwise, false.</returns>
        [Pure]
        public static Boolean operator >(AngleDegree first, AngleDegree second)
        {
            return first.Degree > second.Degree;
        }

        /// <summary>
        /// Indicates whether a specified <see cref="AngleDegree"/> is less than or equal to another specified <see cref="AngleDegree"/>.
        /// </summary>
        /// <param name="first">The first angle to compare.</param>
        /// <param name="second">The second angle to compare.</param>
        /// <returns>true if the value of a1 is less than or equal to the value of a2; otherwise, false.</returns>
        [Pure]
        public static Boolean operator <=(AngleDegree first, AngleDegree second)
        {
            return first.Degree <= second.Degree;
        }

        /// <summary>
        /// Indicates whether a specified <see cref="AngleDegree"/> is greater than or equal to another specified <see cref="AngleDegree"/>.
        /// </summary>
        /// <param name="first">The first angle to compare.</param>
        /// <param name="second">The second angle to compare.</param>
        /// <returns>true if the value of a1 is greater than or equal to the value of a2; otherwise, false.</returns>
        [Pure]
        public static Boolean operator >=(AngleDegree first, AngleDegree second)
        {
            return first.Degree >= second.Degree;
        }

        /// <summary>
        /// Adds two vectors. 
        /// </summary>
        /// <param name="first">Source angle.</param>
        /// <param name="second">Source angle.</param>
        /// <returns>Result of the addition.</returns>
        [Pure]
        public static AngleDegree operator +(AngleDegree first, AngleDegree second)
        {
            return new AngleDegree(first.Degree + second.Degree);
        }

        /// <summary>
        /// Negates an angle.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>Result of the negation.</returns>
        [Pure]
        public static AngleDegree operator -(AngleDegree angle)
        {
            return new AngleDegree(-angle.Degree);
        }

        /// <summary>
        /// Subtracts a angle from a angle.  
        /// </summary>
        /// <param name="first">Source angle.</param>
        /// <param name="second">Source angle.</param>
        /// <returns>Result of the subtraction.</returns>
        [Pure]
        public static AngleDegree operator -(AngleDegree first, AngleDegree second)
        {
            return new AngleDegree(first.Degree - second.Degree);
        }

        /// <summary>
        /// Multiplies a scalar by an angle value. 
        /// </summary>
        /// <param name="first">Scalar value.</param>
        /// <param name="second">Source angle.</param>
        /// <returns>Result of the multiplication.</returns>
        [Pure]
        public static AngleDegree operator *(Double first, AngleDegree second)
        {
            return new AngleDegree(first * second.Degree);
        }

        /// <summary>
        /// Divides a angle by a scalar value. 
        /// </summary>
        /// <param name="first">Source angle.</param>
        /// <param name="second">Scalar value.</param>
        /// <returns>Result of the division.</returns>
        [Pure]
        public static AngleDegree operator /(AngleDegree first, Double second)
        {
            return new AngleDegree(first.Degree / second);
        }

        /// <summary>
        /// Modulo a angle by a scalar value. 
        /// </summary>
        /// <param name="first">Source angle.</param>
        /// <param name="second">Scalar value.</param>
        /// <returns>Result of the division.</returns>
        [Pure]
        public static AngleDegree operator %(AngleDegree first, Double second)
        {
            return new AngleDegree(first.Degree % second);
        }

        /// <summary>
        /// Gets the amplitude of the angle degrees.
        /// </summary>
        private Double Degree { get; }

        public AngleDegree(Double degree)
        {
            Degree = degree;
        }

        /// <summary>
        /// Gets the value of the current Angle structure expressed degrees and minutes.
        /// </summary>
        public void Deconstruct(out Int32 degree, out Double minutes)
        {
            degree = (Int32) Degree;
            minutes = Math.Abs(Degree - degree) * 60;
        }

        /// <summary>
        /// Gets the value of the current Angle structure expressed degrees, minutes and seconds.
        /// </summary>
        public void Deconstruct(out Int32 degree, out Int32 minutes, out Double seconds)
        {
            degree = (Int32) Degree;

            Double decimalminutes = Math.Abs(Degree - degree) * 60;

            minutes = (Int32) decimalminutes;
            seconds = (decimalminutes - minutes) * 60;
        }

        [Pure]
        public Int32 CompareTo(AngleDegree other)
        {
            return Degree.CompareTo(other.Degree);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        [Pure]
        public override Int32 GetHashCode()
        {
            return Degree.GetHashCode();
        }

        /// <summary>
        /// Indicates whether two <see cref="AngleDegree"/> instances are equal.
        /// </summary>
        /// <param name="first">The first angle to compare.</param>
        /// <param name="second">The second angle to compare.</param>
        /// <returns>true if the values of a1 and a2 are equal; otherwise, false.</returns>
        [Pure]
        public static Boolean Equals(AngleDegree first, AngleDegree second)
        {
            return Math.Abs(first.Degree - second.Degree) < Double.Epsilon;
        }

        /// <summary>
        /// Indicates whether whether this instance is equal to a specified <see cref="AngleDegree"/> object.
        /// </summary>
        /// <param name="other">An <see cref="AngleDegree"/> to compare with this instance.</param>
        /// <returns>true if obj represents the same angle as this instance; otherwise, false.</returns>
        /// <remarks>This method implements the System.IEquatable&lt;T&gt; interface, and performs slightly better than <see cref="AngleDegree.Equals(object)"/> because it does not have to convert the obj parameter to an object.</remarks>
        [Pure]
        public Boolean Equals(AngleDegree other)
        {
            return Math.Abs(Degree - other.Degree) < Double.Epsilon;
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>true if value is a <see cref="AngleDegree"/> object that represents the same angle as the current <see cref="AngleDegree"/> structure; otherwise, false.</returns>
        [Pure]
        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                AngleDegree angle => Equals(angle),
                AngleRadian angle => Equals(angle.ToAngleDegree()),
                AngleGradian angle => Equals(angle.ToAngleDegree()),
                AngleRevolution angle => Equals(angle.ToAngleDegree()),
                _ => false
            };
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        [Pure]
        public override String ToString()
        {
            return Degree.ToString();
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        [Pure]
        public String ToString(IFormatProvider? provider)
        {
            return Degree.ToString(provider);
        }

        /// <summary>
        /// Converts the value of the current <see cref="AngleDegree"/> object to its equivalent string representation, using a specified format.
        /// </summary>
        /// <param name="format">A string that specifies the format to be used for the returned string.</param>
        /// <returns>A string representation of the value of the current <see cref="AngleDegree"/> object, the specified format.</returns>
        [Pure]
        public String ToString(String? format)
        {
            return Degree.ToString(format);
        }

        /// <summary>
        /// Converts the value of the current <see cref="AngleDegree"/> object to its equivalent string representation using the specified format and culture-specific format information.
        /// </summary>
        /// <param name="format">A standard or custom date and time format string.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>A string representation of value of the current <see cref="AngleDegree"/> object as specified by format and provider.</returns>
        [Pure]
        public String ToString(String? format, IFormatProvider? provider)
        {
            return Degree.ToString(format, provider);
        }
    }
}