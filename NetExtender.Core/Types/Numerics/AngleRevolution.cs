// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.Contracts;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Types.Numerics
{
    [Serializable]
    public readonly struct AngleRevolution : IEquatable<AngleRevolution>, IComparable<AngleRevolution>, IFormattable
    {
        /// <summary>
        /// Represents the zero <see cref="AngleRevolution"/> value (0 degrees).
        /// </summary>
        public static AngleRevolution Zero { get; } = new AngleRevolution(AngleUtilities.Revolution.Double.Zero);
        
        /// <summary>
        /// Represents the golden <see cref="AngleRevolution"/> value (~137.508 degrees).
        /// </summary>
        public static AngleRevolution Golden { get; } = AngleRadian.Golden.ToAngleRevolution();

        /// <summary>
        /// Represents the quarter <see cref="AngleRevolution"/> value (90 degrees).
        /// </summary>
        public static AngleRevolution Quarter { get; } = new AngleRevolution(AngleUtilities.Revolution.Double.Quarter);

        /// <summary>
        /// Represents the straight <see cref="AngleRevolution"/> value (180 degrees).
        /// </summary>
        public static AngleRevolution Straight { get; } = new AngleRevolution(AngleUtilities.Revolution.Double.Straight);
        
        /// <summary>
        /// Represents the three quarter <see cref="AngleRevolution"/> value (270 degrees).
        /// </summary>
        public static AngleRevolution ThreeQuarter { get; } = new AngleRevolution(AngleUtilities.Revolution.Double.ThreeQuarter);

        /// <summary>
        /// Represents the full <see cref="AngleRevolution"/> value (360 degrees).
        /// </summary>
        public static AngleRevolution Full { get; } = new AngleRevolution(AngleUtilities.Revolution.Double.Full);

        /// <summary>
        /// Represents the smallest possible value of a <see cref="AngleRevolution"/>.
        /// </summary>
        public static AngleRevolution MinValue { get; } = new AngleRevolution(Double.MinValue);

        /// <summary>
        /// Represents the largest possible value of a <see cref="AngleRevolution"/>.
        /// </summary>
        public static AngleRevolution MaxValue { get; } = new AngleRevolution(Double.MaxValue);

        public static explicit operator Double(AngleRevolution angle)
        {
            return angle.Revolution;
        }

        public static implicit operator AngleRevolution(Double revolution)
        {
            return new AngleRevolution(revolution);
        }

        /// <summary>
        /// Indicates whether two <see cref="AngleRevolution"/> instances are equal.
        /// </summary>
        /// <param name="first">The first angle to compare.</param>
        /// <param name="second">The second angle to compare.</param>
        /// <returns>true if the values of first and second are equal; otherwise, false.</returns>
        [Pure]
        public static Boolean operator ==(AngleRevolution first, AngleRevolution second)
        {
            return Math.Abs(first.Revolution - second.Revolution) < Double.Epsilon;
        }

        /// <summary>
        /// Indicates whether two <see cref="AngleRevolution"/> instances are equal.
        /// </summary>
        /// <param name="first">The first angle to compare.</param>
        /// <param name="second">The second angle to compare.</param>
        /// <returns>true if the values of first and second are equal; otherwise, false.</returns>
        [Pure]
        public static Boolean operator !=(AngleRevolution first, AngleRevolution second)
        {
            return Math.Abs(first.Revolution - second.Revolution) >= Double.Epsilon;
        }

        /// <summary>
        /// Indicates whether a specified <see cref="AngleRevolution"/> is less than another specified <see cref="AngleRevolution"/>.
        /// </summary>
        /// <param name="first">The first angle to compare.</param>
        /// <param name="second">The second angle to compare.</param>
        /// <returns>true if the value of first is less than the value of second; otherwise, false.</returns>
        [Pure]
        public static Boolean operator <(AngleRevolution first, AngleRevolution second)
        {
            return first.Revolution < second.Revolution;
        }

        /// <summary>
        /// Indicates whether a specified <see cref="AngleRevolution"/> is greater than another specified <see cref="AngleRevolution"/>.
        /// </summary>
        /// <param name="first">The first angle to compare.</param>
        /// <param name="second">The second angle to compare.</param>
        /// <returns>true if the value of first is greater than the value of second; otherwise, false.</returns>
        [Pure]
        public static Boolean operator >(AngleRevolution first, AngleRevolution second)
        {
            return first.Revolution > second.Revolution;
        }

        /// <summary>
        /// Indicates whether a specified <see cref="AngleRevolution"/> is less than or equal to another specified <see cref="AngleRevolution"/>.
        /// </summary>
        /// <param name="first">The first angle to compare.</param>
        /// <param name="second">The second angle to compare.</param>
        /// <returns>true if the value of first is less than or equal to the value of second; otherwise, false.</returns>
        [Pure]
        public static Boolean operator <=(AngleRevolution first, AngleRevolution second)
        {
            return first.Revolution <= second.Revolution;
        }

        /// <summary>
        /// Indicates whether a specified <see cref="AngleRevolution"/> is greater than or equal to another specified <see cref="AngleRevolution"/>.
        /// </summary>
        /// <param name="first">The first angle to compare.</param>
        /// <param name="second">The second angle to compare.</param>
        /// <returns>true if the value of first is greater than or equal to the value of second; otherwise, false.</returns>
        [Pure]
        public static Boolean operator >=(AngleRevolution first, AngleRevolution second)
        {
            return first.Revolution >= second.Revolution;
        }

        /// <summary>
        /// Adds two vectors. 
        /// </summary>
        /// <param name="first">Source angle.</param>
        /// <param name="second">Source angle.</param>
        /// <returns>Result of the addition.</returns>
        [Pure]
        public static AngleRevolution operator +(AngleRevolution first, AngleRevolution second)
        {
            return new AngleRevolution(first.Revolution + second.Revolution);
        }

        /// <summary>
        /// Negates an angle.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>Result of the negation.</returns>
        [Pure]
        public static AngleRevolution operator -(AngleRevolution angle)
        {
            return new AngleRevolution(-angle.Revolution);
        }

        /// <summary>
        /// Subtracts a angle from a angle.  
        /// </summary>
        /// <param name="first">Source angle.</param>
        /// <param name="second">Source angle.</param>
        /// <returns>Result of the subtraction.</returns>
        [Pure]
        public static AngleRevolution operator -(AngleRevolution first, AngleRevolution second)
        {
            return new AngleRevolution(first.Revolution - second.Revolution);
        }

        /// <summary>
        /// Multiplies a scalar by an angle value. 
        /// </summary>
        /// <param name="first">Scalar value.</param>
        /// <param name="second">Source angle.</param>
        /// <returns>Result of the multiplication.</returns>
        [Pure]
        public static AngleRevolution operator *(Double first, AngleRevolution second)
        {
            return new AngleRevolution(first * second.Revolution);
        }

        /// <summary>
        /// Divides a angle by a scalar value. 
        /// </summary>
        /// <param name="first">Source angle.</param>
        /// <param name="second">Scalar value.</param>
        /// <returns>Result of the division.</returns>
        [Pure]
        public static AngleRevolution operator /(AngleRevolution first, Double second)
        {
            return new AngleRevolution(first.Revolution / second);
        }

        /// <summary>
        /// Modulo a angle by a scalar value. 
        /// </summary>
        /// <param name="first">Source angle.</param>
        /// <param name="second">Scalar value.</param>
        /// <returns>Result of the division.</returns>
        [Pure]
        public static AngleRevolution operator %(AngleRevolution first, Double second)
        {
            return new AngleRevolution(first.Revolution % second);
        }

        /// <summary>
        /// Gets the amplitude of the angle degrees.
        /// </summary>
        private Double Revolution { get; }

        public AngleRevolution(Double degrees)
        {
            Revolution = degrees;
        }

        [Pure]
        public Int32 CompareTo(AngleRevolution other)
        {
            return Revolution.CompareTo(other.Revolution);
        }

        /// <summary>
        /// Indicates whether two <see cref="AngleRevolution"/> instances are equal.
        /// </summary>
        /// <param name="first">The first angle to compare.</param>
        /// <param name="second">The second angle to compare.</param>
        /// <returns>true if the values of first and second are equal; otherwise, false.</returns>
        [Pure]
        public static Boolean Equals(AngleRevolution first, AngleRevolution second)
        {
            return Math.Abs(first.Revolution - second.Revolution) < Double.Epsilon;
        }

        /// <summary>
        /// Indicates whether whether this instance is equal to a specified <see cref="AngleRevolution"/> object.
        /// </summary>
        /// <param name="other">An <see cref="AngleRevolution"/> to compare with this instance.</param>
        /// <returns>true if obj represents the same angle as this instance; otherwise, false.</returns>
        /// <remarks>This method implements the System.IEquatable&lt;T&gt; interface, and performs slightly better than <see cref="AngleRevolution.Equals(object)"/> because it does not have to convert the obj parameter to an object.</remarks>
        [Pure]
        public Boolean Equals(AngleRevolution other)
        {
            return Math.Abs(Revolution - other.Revolution) < Double.Epsilon;
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>true if value is a <see cref="AngleRevolution"/> object that represents the same angle as the current <see cref="AngleRevolution"/> structure; otherwise, false.</returns>
        [Pure]
        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                AngleRevolution angle => Equals(angle),
                AngleDegree angle => Equals(angle.ToAngleRevolution()),
                AngleRadian angle => Equals(angle.ToAngleRevolution()),
                AngleGradian angle => Equals(angle.ToAngleRevolution()),
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
            return Revolution.GetHashCode();
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        [Pure]
        public override String ToString()
        {
            return Revolution.ToString();
        }

        /// <summary>
        /// Converts the value of the current <see cref="AngleRevolution"/> object to its equivalent string representation, using a specified format.
        /// </summary>
        /// <param name="format">A string that specifies the format to be used for the returned string.</param>
        /// <returns>A string representation of the value of the current <see cref="AngleRevolution"/> object, the specified format.</returns>
        [Pure]
        public String ToString(String? format)
        {
            return Revolution.ToString(format);
        }

        /// <summary>
        /// Converts the value of the current <see cref="AngleRevolution"/> object to its equivalent string representation using the specified format and culture-specific format information.
        /// </summary>
        /// <param name="format">A standard or custom date and time format string.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>A string representation of value of the current <see cref="AngleRevolution"/> object as specified by format and provider.</returns>
        [Pure]
        public String ToString(String? format, IFormatProvider? provider)
        {
            return Revolution.ToString(format, provider);
        }
    }
}
