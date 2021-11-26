// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.Contracts;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Types.Numerics
{
    [Serializable]
    public readonly struct AngleRadian : IEquatable<AngleRadian>, IComparable<AngleRadian>, IFormattable
    {
        /// <summary>
        /// Represents the zero <see cref="AngleRadian"/> value (0 degrees).
        /// </summary>
        public static AngleRadian Zero { get; } = new AngleRadian(0);

        /// <summary>
        /// Represents the golden <see cref="AngleRadian"/> value (~137.508 degrees).
        /// </summary>
        public static AngleRadian Golden { get; } = new AngleRadian(Math.PI * (3 - MathUtilities.DoubleConstants.Sqrt5));

        /// <summary>
        /// Represents the smallest possible value of a <see cref="AngleRadian"/>.
        /// </summary>
        public static AngleRadian MinValue { get; } = new AngleRadian(Double.MinValue);

        /// <summary>
        /// Represents the largest possible value of a <see cref="AngleRadian"/>.
        /// </summary>
        public static AngleRadian MaxValue { get; } = new AngleRadian(Double.MaxValue);

        /// <summary>
        /// Represents the right <see cref="AngleRadian"/> value (90 degrees).
        /// </summary>
        public static AngleRadian Right { get; } = new AngleRadian(AngleUtilities.Radian.Right);

        /// <summary>
        /// Represents the straight <see cref="AngleRadian"/> value (180 degrees).
        /// </summary>
        public static AngleRadian Straight { get; } = new AngleRadian(AngleUtilities.Radian.Straight);

        /// <summary>
        /// Represents the full <see cref="AngleRadian"/> value (360 degrees).
        /// </summary>
        public static AngleRadian Full { get; } = new AngleRadian(AngleUtilities.Radian.Full);

        public static explicit operator Double(AngleRadian angle)
        {
            return angle.Radian;
        }

        public static implicit operator AngleRadian(Double radian)
        {
            return new AngleRadian(radian);
        }
        
        /// <summary>
        /// Indicates whether two <see cref="AngleRadian"/> instances are equal.
        /// </summary>
        /// <param name="first">The first angle to compare.</param>
        /// <param name="second">The second angle to compare.</param>
        /// <returns>true if the values of first and second are equal; otherwise, false.</returns>
        [Pure]
        public static Boolean operator ==(AngleRadian first, AngleRadian second)
        {
            return Math.Abs(first.Radian - second.Radian) < Double.Epsilon;
        }

        /// <summary>
        /// Indicates whether two <see cref="AngleRadian"/> instances are equal.
        /// </summary>
        /// <param name="first">The first angle to compare.</param>
        /// <param name="second">The second angle to compare.</param>
        /// <returns>true if the values of first and second are equal; otherwise, false.</returns>
        [Pure]
        public static Boolean operator !=(AngleRadian first, AngleRadian second)
        {
            return Math.Abs(first.Radian - second.Radian) >= Double.Epsilon;
        }
        
        /// <summary>
        /// Indicates whether a specified <see cref="AngleRadian"/> is less than another specified <see cref="AngleRadian"/>.
        /// </summary>
        /// <param name="first">The first angle to compare.</param>
        /// <param name="second">The second angle to compare.</param>
        /// <returns>true if the value of first is less than the value of second; otherwise, false.</returns>
        [Pure]
        public static Boolean operator <(AngleRadian first, AngleRadian second)
        {
            return first.Radian < second.Radian;
        }
        
        /// <summary>
        /// Indicates whether a specified <see cref="AngleRadian"/> is greater than another specified <see cref="AngleRadian"/>.
        /// </summary>
        /// <param name="first">The first angle to compare.</param>
        /// <param name="second">The second angle to compare.</param>
        /// <returns>true if the value of first is greater than the value of second; otherwise, false.</returns>
        [Pure]
        public static Boolean operator >(AngleRadian first, AngleRadian second)
        {
            return first.Radian > second.Radian;
        }

        /// <summary>
        /// Indicates whether a specified <see cref="AngleRadian"/> is less than or equal to another specified <see cref="AngleRadian"/>.
        /// </summary>
        /// <param name="first">The first angle to compare.</param>
        /// <param name="second">The second angle to compare.</param>
        /// <returns>true if the value of first is less than or equal to the value of second; otherwise, false.</returns>
        [Pure]
        public static Boolean operator <=(AngleRadian first, AngleRadian second)
        {
            return first.Radian <= second.Radian;
        }

        /// <summary>
        /// Indicates whether a specified <see cref="AngleRadian"/> is greater than or equal to another specified <see cref="AngleRadian"/>.
        /// </summary>
        /// <param name="first">The first angle to compare.</param>
        /// <param name="second">The second angle to compare.</param>
        /// <returns>true if the value of first is greater than or equal to the value of second; otherwise, false.</returns>
        [Pure]
        public static Boolean operator >=(AngleRadian first, AngleRadian second)
        {
            return first.Radian >= second.Radian;
        }
        
        /// <summary>
        /// Negates an angle.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>Result of the negation.</returns>
        [Pure]
        public static AngleRadian operator -(AngleRadian angle)
        {
            return new AngleRadian(-angle.Radian);
        }
        
        /// <summary>
        /// Subtracts a angle from a angle.  
        /// </summary>
        /// <param name="left">Source angle.</param>
        /// <param name="right">Source angle.</param>
        /// <returns>Result of the subtraction.</returns>
        [Pure]
        public static AngleRadian operator -(AngleRadian left, AngleRadian right)
        {
            return new AngleRadian(left.Radian - right.Radian);
        }

        /// <summary>
        /// Adds two vectors. 
        /// </summary>
        /// <param name="left">Source angle.</param>
        /// <param name="right">Source angle.</param>
        /// <returns>Result of the addition.</returns>
        [Pure]
        public static AngleRadian operator +(AngleRadian left, AngleRadian right)
        {
            return new AngleRadian(left.Radian + right.Radian);
        }

        /// <summary>
        /// Multiplies a scalar by an angle value. 
        /// </summary>
        /// <param name="left">Scalar value.</param>
        /// <param name="right">Source angle.</param>
        /// <returns>Result of the multiplication.</returns>
        [Pure]
        public static AngleRadian operator *(Double left, AngleRadian right)
        {
            return new AngleRadian(left * right.Radian);
        }

        /// <summary>
        /// Divides a angle by a scalar value. 
        /// </summary>
        /// <param name="left">Source angle.</param>
        /// <param name="right">Scalar value.</param>
        /// <returns>Result of the division.</returns>
        [Pure]
        public static AngleRadian operator /(AngleRadian left, Double right)
        {
            return new AngleRadian(left.Radian / right);
        }
        
        /// <summary>
        /// Modulo a angle by a scalar value. 
        /// </summary>
        /// <param name="first">Source angle.</param>
        /// <param name="second">Scalar value.</param>
        /// <returns>Result of the division.</returns>
        [Pure]
        public static AngleRadian operator %(AngleRadian first, Double second)
        {
            return new AngleRadian(first.Radian % second);
        }
        
        /// <summary>
        /// Gets the amplitude of the angle in radians.
        /// </summary>
        private Double Radian { get; }

        public AngleRadian(Double radian)
        {
            Radian = radian;
        }
        
        [Pure]
        public Int32 CompareTo(AngleRadian other)
        {
            return Radian.CompareTo(other.Radian);
        }

        /// <summary>
        /// Indicates whether two <see cref="AngleRadian"/> instances are equal.
        /// </summary>
        /// <param name="first">The first angle to compare.</param>
        /// <param name="second">The second angle to compare.</param>
        /// <returns>true if the values of first and second are equal; otherwise, false.</returns>
        [Pure]
        public static Boolean Equals(AngleRadian first, AngleRadian second)
        {
            return Math.Abs(first.Radian - second.Radian) < Double.Epsilon;
        }

        /// <summary>
        /// Indicates whether whether this instance is equal to a specified <see cref="AngleRadian"/> object.
        /// </summary>
        /// <param name="other">An <see cref="AngleRadian"/> to compare with this instance.</param>
        /// <returns>true if obj represents the same angle as this instance; otherwise, false.</returns>
        /// <remarks>This method implements the System.IEquatable&lt;T&gt; interface, and performs slightly better than <see cref="AngleRadian.Equals(object)"/> because it does not have to convert the obj parameter to an object.</remarks>
        [Pure]
        public Boolean Equals(AngleRadian other)
        {
            return Math.Abs(Radian - other.Radian) < Double.Epsilon;
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>true if value is a <see cref="AngleRadian"/> object that represents the same angle as the current <see cref="AngleRadian"/> structure; otherwise, false.</returns>
        public override Boolean Equals(Object? obj)
        {
            return obj switch
            {
                AngleRadian angle => Equals(angle),
                AngleDegree angle => Equals(angle.ToAngleRadian()),
                AngleGradian angle => Equals(angle.ToAngleRadian()),
                AngleRevolution angle => Equals(angle.ToAngleRadian()),
                _ => false,
            };
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override Int32 GetHashCode()
        {
            return Radian.GetHashCode();
        }
        
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override String ToString()
        {
            return Radian.ToString();
        }
        
        /// <summary>
        /// Converts the value of the current <see cref="AngleRadian"/> object to its equivalent string representation, using a specified format.
        /// </summary>
        /// <param name="format">A string that specifies the format to be used for the returned string.</param>
        /// <returns>A string representation of the value of the current <see cref="AngleRadian"/> object, in the specified format.</returns>
        [Pure]
        public String ToString(String? format)
        {
            return Radian.ToString(format);
        }

        /// <summary>
        /// Converts the value of the current <see cref="AngleRadian"/> object to its equivalent string representation using the specified format and culture-specific format information.
        /// </summary>
        /// <param name="format">A standard or custom date and time format string.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>A string representation of value of the current <see cref="AngleRadian"/> object as specified by format and provider.</returns>
        [Pure]
        public String ToString(String? format, IFormatProvider? provider)
        {
            return Radian.ToString(format, provider);
        }
    }
}
