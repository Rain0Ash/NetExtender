// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.Contracts;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Types.Numerics
{
    [Serializable]
    public readonly struct AngleGradian : IEquatable<AngleGradian>, IComparable<AngleGradian>, IFormattable
    {
        /// <summary>
        /// Represents the zero <see cref="AngleGradian"/> value (0 degrees). This field is read-only.
        /// </summary>
        public static AngleGradian Zero { get; } = new AngleGradian(0);

        /// <summary>
        /// Represents the smallest possible value of a <see cref="AngleGradian"/>. This field is read-only.
        /// </summary>
        public static AngleGradian MinValue { get; } = new AngleGradian(Double.MinValue);

        /// <summary>
        /// Represents the largest possible value of a <see cref="AngleGradian"/>. This field is read-only.
        /// </summary>
        public static AngleGradian MaxValue { get; } = new AngleGradian(Double.MaxValue);

        /// <summary>
        /// Represents the right <see cref="AngleGradian"/> value (90 degrees). This field is read-only.
        /// </summary>
        public static AngleGradian Right { get; } = new AngleGradian(AngleUtilities.Gradian.Right);

        /// <summary>
        /// Represents the straight <see cref="AngleGradian"/> value (180 degrees). This field is read-only.
        /// </summary>
        public static AngleGradian Straight { get; } = new AngleGradian(AngleUtilities.Gradian.Straight);

        /// <summary>
        /// Represents the full <see cref="AngleGradian"/> value (360 degrees). This field is read-only.
        /// </summary>
        public static AngleGradian Full { get; } = new AngleGradian(AngleUtilities.Gradian.Full);

        public static explicit operator Double(AngleGradian angle)
        {
            return angle.Gradian;
        }

        public static implicit operator AngleGradian(Double gradian)
        {
            return new AngleGradian(gradian);
        }

        /// <summary>
        /// Indicates whether two <see cref="AngleGradian"/> instances are equal.
        /// </summary>
        /// <param name="first">The first angle to compare.</param>
        /// <param name="second">The second angle to compare.</param>
        /// <returns>true if the values of first and second are equal; otherwise, false.</returns>
        [Pure]
        public static Boolean operator ==(AngleGradian first, AngleGradian second)
        {
            return Math.Abs(first.Gradian - second.Gradian) < Double.Epsilon;
        }

        /// <summary>
        /// Indicates whether two <see cref="AngleGradian"/> instances are equal.
        /// </summary>
        /// <param name="first">The first angle to compare.</param>
        /// <param name="second">The second angle to compare.</param>
        /// <returns>true if the values of first and second are equal; otherwise, false.</returns>
        [Pure]
        public static Boolean operator !=(AngleGradian first, AngleGradian second)
        {
            return Math.Abs(first.Gradian - second.Gradian) >= Double.Epsilon;
        }

        /// <summary>
        /// Indicates whether a specified <see cref="AngleGradian"/> is less than another specified <see cref="AngleGradian"/>.
        /// </summary>
        /// <param name="first">The first angle to compare.</param>
        /// <param name="second">The second angle to compare.</param>
        /// <returns>true if the value of first is less than the value of second; otherwise, false.</returns>
        [Pure]
        public static Boolean operator <(AngleGradian first, AngleGradian second)
        {
            return first.Gradian < second.Gradian;
        }

        /// <summary>
        /// Indicates whether a specified <see cref="AngleGradian"/> is greater than another specified <see cref="AngleGradian"/>.
        /// </summary>
        /// <param name="first">The first angle to compare.</param>
        /// <param name="second">The second angle to compare.</param>
        /// <returns>true if the value of first is greater than the value of second; otherwise, false.</returns>
        [Pure]
        public static Boolean operator >(AngleGradian first, AngleGradian second)
        {
            return first.Gradian > second.Gradian;
        }

        /// <summary>
        /// Indicates whether a specified <see cref="AngleGradian"/> is less than or equal to another specified <see cref="AngleGradian"/>.
        /// </summary>
        /// <param name="first">The first angle to compare.</param>
        /// <param name="second">The second angle to compare.</param>
        /// <returns>true if the value of first is less than or equal to the value of second; otherwise, false.</returns>
        [Pure]
        public static Boolean operator <=(AngleGradian first, AngleGradian second)
        {
            return first.Gradian <= second.Gradian;
        }

        /// <summary>
        /// Indicates whether a specified <see cref="AngleGradian"/> is greater than or equal to another specified <see cref="AngleGradian"/>.
        /// </summary>
        /// <param name="first">The first angle to compare.</param>
        /// <param name="second">The second angle to compare.</param>
        /// <returns>true if the value of first is greater than or equal to the value of second; otherwise, false.</returns>
        [Pure]
        public static Boolean operator >=(AngleGradian first, AngleGradian second)
        {
            return first.Gradian >= second.Gradian;
        }

        /// <summary>
        /// Adds two vectors. 
        /// </summary>
        /// <param name="first">Source angle.</param>
        /// <param name="second">Source angle.</param>
        /// <returns>Result of the addition.</returns>
        [Pure]
        public static AngleGradian operator +(AngleGradian first, AngleGradian second)
        {
            return new AngleGradian(first.Gradian + second.Gradian);
        }

        /// <summary>
        /// Negates an angle.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>Result of the negation.</returns>
        [Pure]
        public static AngleGradian operator -(AngleGradian angle)
        {
            return new AngleGradian(-angle.Gradian);
        }

        /// <summary>
        /// Subtracts a angle from a angle.  
        /// </summary>
        /// <param name="first">Source angle.</param>
        /// <param name="second">Source angle.</param>
        /// <returns>Result of the subtraction.</returns>
        [Pure]
        public static AngleGradian operator -(AngleGradian first, AngleGradian second)
        {
            return new AngleGradian(first.Gradian - second.Gradian);
        }

        /// <summary>
        /// Multiplies a scalar by an angle value. 
        /// </summary>
        /// <param name="first">Scalar value.</param>
        /// <param name="second">Source angle.</param>
        /// <returns>Result of the multiplication.</returns>
        [Pure]
        public static AngleGradian operator *(Double first, AngleGradian second)
        {
            return new AngleGradian(first * second.Gradian);
        }

        /// <summary>
        /// Divides a angle by a scalar value. 
        /// </summary>
        /// <param name="first">Source angle.</param>
        /// <param name="second">Scalar value.</param>
        /// <returns>Result of the division.</returns>
        [Pure]
        public static AngleGradian operator /(AngleGradian first, Double second)
        {
            return new AngleGradian(first.Gradian / second);
        }

        /// <summary>
        /// Modulo a angle by a scalar value. 
        /// </summary>
        /// <param name="first">Source angle.</param>
        /// <param name="second">Scalar value.</param>
        /// <returns>Result of the division.</returns>
        [Pure]
        public static AngleGradian operator %(AngleGradian first, Double second)
        {
            return new AngleGradian(first.Gradian % second);
        }

        /// <summary>
        /// Gets the amplitude of the angle degrees. This field is read-only.
        /// </summary>
        private Double Gradian { get; }

        public AngleGradian(Double gradian)
        {
            Gradian = gradian;
        }

        [Pure]
        public Int32 CompareTo(AngleGradian other)
        {
            return Gradian.CompareTo(other.Gradian);
        }

        /// <summary>
        /// Indicates whether two <see cref="AngleGradian"/> instances are equal.
        /// </summary>
        /// <param name="first">The first angle to compare.</param>
        /// <param name="second">The second angle to compare.</param>
        /// <returns>true if the values of first and second are equal; otherwise, false.</returns>
        [Pure]
        public static Boolean Equals(AngleGradian first, AngleGradian second)
        {
            return Math.Abs(first.Gradian - second.Gradian) < Double.Epsilon;
        }

        /// <summary>
        /// Indicates whether whether this instance is equal to a specified <see cref="AngleGradian"/> object.
        /// </summary>
        /// <param name="other">An <see cref="AngleGradian"/> to compare with this instance.</param>
        /// <returns>true if obj represents the same angle as this instance; otherwise, false.</returns>
        /// <remarks>This method implements the System.IEquatable&lt;T&gt; interface, and performs slightly better than <see cref="AngleGradian.Equals(object)"/> because it does not have to convert the obj parameter to an object.</remarks>
        [Pure]
        public Boolean Equals(AngleGradian other)
        {
            return Math.Abs(Gradian - other.Gradian) < Double.Epsilon;
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>true if value is a <see cref="AngleGradian"/> object that represents the same angle as the current <see cref="AngleGradian"/> structure; otherwise, false.</returns>
        [Pure]
        public override Boolean Equals(Object? obj)
        {
            return obj switch
            {
                AngleGradian angle => Equals(angle),
                AngleDegree angle => Equals(angle.ToAngleGradian()),
                AngleRadian angle => Equals(angle.ToAngleGradian()),
                AngleRevolution angle => Equals(angle.ToAngleGradian()),
                _ => false
            };
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        [Pure]
        public override Int32 GetHashCode()
        {
            return Gradian.GetHashCode();
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        [Pure]
        public override String ToString()
        {
            return Gradian.ToString();
        }

        /// <summary>
        /// Converts the value of the current <see cref="AngleGradian"/> object to its equivalent string representation, using a specified format.
        /// </summary>
        /// <param name="format">A string that specifies the format to be used for the returned string.</param>
        /// <returns>A string representation of the value of the current <see cref="AngleGradian"/> object, the specified format.</returns>
        [Pure]
        public String ToString(String? format)
        {
            return Gradian.ToString(format);
        }

        /// <summary>
        /// Converts the value of the current <see cref="AngleGradian"/> object to its equivalent string representation using the specified format and culture-specific format information.
        /// </summary>
        /// <param name="format">A standard or custom date and time format string.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>A string representation of value of the current <see cref="AngleGradian"/> object as specified by format and provider.</returns>
        [Pure]
        public String ToString(String? format, IFormatProvider? provider)
        {
            return Gradian.ToString(format, provider);
        }
    }
}
