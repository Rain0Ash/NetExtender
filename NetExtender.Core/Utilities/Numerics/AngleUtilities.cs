// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Numerics;

namespace NetExtender.Utilities.Numerics
{
    /// <summary>
    /// The four regions divided by the x and y axis.
    /// </summary>
    public enum AngleQuadrant
    {
        /// <summary>
        /// The region where x and y are positive.
        /// </summary>
        First,
        /// <summary>
        /// The region where x is negative and y is positive.
        /// </summary>
        Second,
        /// <summary>
        /// The region where x and y are negative.
        /// </summary>
        Third,
        /// <summary>
        /// The region where x is positive and y is negative.
        /// </summary>
        Fourth
    }

    [SuppressMessage("ReSharper", "InvokeAsExtensionMethod")]
    public static class AngleUtilities
    {
        public static class Int32Degree
        {
            public const Int32 Zero = 0;
            public const Int32 Quarter = 90;
            public const Int32 Straight = 180;
            public const Int32 ThreeQuarter = 270;
            public const Int32 Full = 360;
        }
        
        public static class SingleDegree
        {
            public const Single Zero = 0F;
            public const Single Quarter = 90F;
            public const Single Straight = 180F;
            public const Single ThreeQuarter = 270F;
            public const Single Full = 360F;
        }
        
        public static class DoubleDegree
        {
            public const Double Zero = 0D;
            public const Double Quarter = 90D;
            public const Double Straight = 180D;
            public const Double ThreeQuarter = 270D;
            public const Double Full = 360D;
        }
        
        public static class DecimalDegree
        {
            public const Decimal Zero = 0M;
            public const Decimal Quarter = 90M;
            public const Decimal Straight = 180M;
            public const Decimal ThreeQuarter = 270M;
            public const Decimal Full = 360M;
        }

        public static class SingleRadian
        {
            public const Single Zero = 0.0F;
            public const Single Quarter = MathF.PI * 0.5F;
            public const Single Straight = MathF.PI;
            public const Single ThreeQuarter = MathF.PI * 1.5F;
            public const Single Full = MathF.PI * 2.0F;
        }

        public static class DoubleRadian
        {
            public const Double Zero = 0.0D;
            public const Double Quarter = Math.PI * 0.5D;
            public const Double Straight = Math.PI;
            public const Double ThreeQuarter = Math.PI * 1.5D;
            public const Double Full = Math.PI * 2.0D;
        }

        public static class DecimalRadian
        {
            public const Decimal Zero = 0.0M;
            public const Decimal Quarter = MathUtilities.DecimalConstants.PI * 0.5M;
            public const Decimal Straight = MathUtilities.DecimalConstants.PI;
            public const Decimal ThreeQuarter = MathUtilities.DecimalConstants.PI * 1.5M;
            public const Decimal Full = MathUtilities.DecimalConstants.PI * 2.0M;
        }
        
        public static class Int32Gradian
        {
            public const Int32 Zero = 0;
            public const Int32 Quarter = 100;
            public const Int32 Straight = 200;
            public const Int32 ThreeQuarter = 300;
            public const Int32 Full = 400;
        }

        public static class SingleGradian
        {
            public const Single Zero = 0F;
            public const Single Quarter = 100F;
            public const Single Straight = 200F;
            public const Single ThreeQuarter = 300F;
            public const Single Full = 400F;
        }

        public static class DoubleGradian
        {
            public const Double Zero = 0D;
            public const Double Quarter = 100D;
            public const Double Straight = 200D;
            public const Double ThreeQuarter = 300D;
            public const Double Full = 400D;
        }

        public static class DecimalGradian
        {
            public const Decimal Zero = 0M;
            public const Decimal Quarter = 100M;
            public const Decimal Straight = 200M;
            public const Decimal ThreeQuarter = 300M;
            public const Decimal Full = 400M;
        }

        public static class SingleRevolution
        {
            public const Single Zero = 0.0F;
            public const Single Quarter = 0.25F;
            public const Single Straight = 0.5F;
            public const Single ThreeQuarter = 0.75F;
            public const Single Full = 1.0F;
        }

        public static class DoubleRevolution
        {
            public const Double Zero = 0.0D;
            public const Double Quarter = 0.25D;
            public const Double Straight = 0.5D;
            public const Double ThreeQuarter = 0.75D;
            public const Double Full = 1.0D;
        }

        public static class DecimalRevolution
        {
            public const Decimal Zero = 0.0M;
            public const Decimal Quarter = 0.25M;
            public const Decimal Straight = 0.5M;
            public const Decimal ThreeQuarter = 0.75M;
            public const Decimal Full = 1.0M;
        }

        public static class SingleRelations
        {
            public const Single DegreeInRadian = SingleDegree.Full / SingleRadian.Full;
            public const Single GradianInRadian = SingleGradian.Full / SingleRadian.Full;
            public const Single GradianInDegree = SingleGradian.Full / SingleDegree.Full;
        }

        public static class DoubleRelations
        {
            public const Double DegreeInRadian = DoubleDegree.Full / DoubleRadian.Full;
            public const Double GradianInRadian = DoubleGradian.Full / DoubleRadian.Full;
            public const Double GradianInDegree = DoubleGradian.Full / DoubleDegree.Full;
        }

        public static class DecimalRelations
        {
            public const Decimal DegreeInRadian = DecimalDegree.Full / DecimalRadian.Full;
            public const Decimal GradianInRadian = DecimalGradian.Full / DecimalRadian.Full;
            public const Decimal GradianInDegree = DecimalGradian.Full / DecimalDegree.Full;
        }

        /// <summary>
        /// Returns an <see cref="AngleDegree"/> that represents the equivalent to the <see cref="AngleRadian"/>.
        /// </summary>
        /// <param name="angle">An angle in radians.</param>
        /// <returns>An object that represents value.</returns>
        [Pure]
        public static AngleDegree ToAngleDegree(this AngleRadian angle)
        {
            return new AngleDegree((Double) angle * DoubleRelations.DegreeInRadian);
        }

        /// <summary>
        /// Returns an <see cref="AngleDegree"/> that represents the equivalent to the <see cref="AngleGradian"/>.
        /// </summary>
        /// <param name="angle">An angle in gradians.</param>
        /// <returns>An object that represents value.</returns>
        [Pure]
        public static AngleDegree ToAngleDegree(this AngleGradian angle)
        {
            return new AngleDegree((Double) angle / DoubleRelations.GradianInDegree);
        }

        /// <summary>
        /// Returns an <see cref="AngleDegree"/> that represents the equivalent to the <see cref="AngleRevolution"/>.
        /// </summary>
        /// <param name="angle">An angle in revolutions.</param>
        /// <returns>An object that represents value.</returns>
        [Pure]
        public static AngleDegree ToAngleDegree(this AngleRevolution angle)
        {
            return new AngleDegree((Double) angle * DoubleDegree.Full);
        }

        /// <summary>
        /// Returns an <see cref="AngleRadian"/> that represents the equivalent to the <see cref="AngleDegree"/>.
        /// </summary>
        /// <param name="angle">An angle in degrees.</param>
        /// <returns>An object that represents value.</returns>
        [Pure]
        public static AngleRadian ToAngleRadian(this AngleDegree angle)
        {
            return new AngleRadian((Double) angle / DoubleRelations.DegreeInRadian);
        }

        /// <summary>
        /// Returns an <see cref="AngleRadian"/> that represents the equivalent to the <see cref="AngleGradian"/>.
        /// </summary>
        /// <param name="angle">An angle in gradians.</param>
        /// <returns>An object that represents value.</returns>
        [Pure]
        public static AngleRadian ToAngleRadian(this AngleGradian angle)
        {
            return new AngleRadian((Double) angle / DoubleRelations.GradianInRadian);
        }

        /// <summary>
        /// Returns an <see cref="AngleRadian"/> that represents the equivalent to the <see cref="AngleRevolution"/>.
        /// </summary>
        /// <param name="angle">An angle in revolutions.</param>
        /// <returns>An object that represents value.</returns>
        [Pure]
        public static AngleRadian ToAngleRadian(this AngleRevolution angle)
        {
            return new AngleRadian((Double) angle * DoubleRadian.Full);
        }

        /// <summary>
        /// Returns an <see cref="AngleGradian"/> that represents the equivalent to the <see cref="AngleRadian"/>.
        /// </summary>
        /// <param name="angle">An angle in radians.</param>
        /// <returns>An object that represents value.</returns>
        [Pure]
        public static AngleGradian ToAngleGradian(this AngleRadian angle)
        {
            return new AngleGradian((Double) angle * DoubleRelations.GradianInRadian);
        }

        /// <summary>
        /// Returns an <see cref="AngleGradian"/> that represents the equivalent to the <see cref="AngleDegree"/>.
        /// </summary>
        /// <param name="angle">An angle in degrees.</param>
        /// <returns>An object that represents value.</returns>
        [Pure]
        public static AngleGradian ToAngleGradian(this AngleDegree angle)
        {
            return new AngleGradian((Double) angle * DoubleRelations.GradianInDegree);
        }

        /// <summary>
        /// Returns an <see cref="AngleGradian"/> that represents the equivalent to the <see cref="AngleRevolution"/>.
        /// </summary>
        /// <param name="angle">An angle in revolutions.</param>
        /// <returns>An object that represents value.</returns>
        [Pure]
        public static AngleGradian ToAngleGradian(this AngleRevolution angle)
        {
            return new AngleGradian((Double) angle * DoubleGradian.Full);
        }

        /// <summary>
        /// Returns an <see cref="AngleRevolution"/> that represents the equivalent to the <see cref="AngleDegree"/>.
        /// </summary>
        /// <param name="angle">An angle in degrees.</param>
        /// <returns>An object that represents value.</returns>
        [Pure]
        public static AngleRevolution ToAngleRevolution(this AngleDegree angle)
        {
            return new AngleRevolution((Double) angle / DoubleDegree.Full);
        }

        /// <summary>
        /// Returns an <see cref="AngleRevolution"/> that represents the equivalent to the <see cref="AngleGradian"/>.
        /// </summary>
        /// <param name="angle">An angle in degrees.</param>
        /// <returns>An object that represents value.</returns>
        [Pure]
        public static AngleRevolution ToAngleRevolution(this AngleGradian angle)
        {
            return new AngleRevolution((Double) angle / DoubleGradian.Full);
        }

        /// <summary>
        /// Returns an <see cref="AngleRevolution"/> that represents the equivalent to the <see cref="AngleRadian"/>.
        /// </summary>
        /// <param name="angle">An angle in degrees.</param>
        /// <returns>An object that represents value.</returns>
        [Pure]
        public static AngleRevolution ToAngleRevolution(this AngleRadian angle)
        {
            return new AngleRevolution((Double) angle / DoubleRadian.Full);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 AngleReduce(Int32 angle, Int32 full)
        {
            Int32 reduced = angle % full;
            if (reduced < 0)
            {
                reduced += full;
            }

            return reduced;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double AngleReduce(Double angle, Double full)
        {
            Double reduced = angle % full;
            if (reduced < 0)
            {
                reduced += full;
            }

            return reduced;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AngleQuadrant GetAngleQuadrant(Double angle, Double right, Double straight, Double full)
        {
            return GetAngleQuadrant(AngleReduce(angle, full), right, straight);
        }

        public static AngleQuadrant GetAngleQuadrant(Double angle, Double right, Double straight)
        {
            if (angle < right)
            {
                return AngleQuadrant.First;
            }

            if (angle < straight)
            {
                return AngleQuadrant.Second;
            }

            if (angle < straight + right)
            {
                return AngleQuadrant.Third;
            }

            return AngleQuadrant.Fourth;
        }

        public static Double GetAngleReference(Double angle, Double right, Double straight, Double full)
        {
            Double reduced = AngleReduce(angle, full);

            return GetAngleQuadrant(reduced, right, straight) switch
            {
                AngleQuadrant.First => reduced,
                AngleQuadrant.Second => straight - reduced,
                AngleQuadrant.Third => reduced - straight,
                AngleQuadrant.Fourth => full - reduced,
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double AngleLerp(Double first, Double second, Double lerp)
        {
            return (1 - lerp) * first + lerp * second;
        }

        /// <summary>
        /// Returns an <see cref="AngleDegree"/> that represents a specified number of degrees.
        /// </summary>
        /// <param name="degree">A number of degrees.</param>
        /// <returns>An object that represents value.</returns>
        [Pure]
        public static AngleDegree AngleFromDegree(Double degree)
        {
            return new AngleDegree(degree);
        }

        /// <summary>
        /// Returns an <see cref="AngleDegree"/> that represents a specified number of degrees.
        /// </summary>
        /// <param name="degree">The degrees parcel of the angle value.</param>
        /// <param name="minutes">The minutes parcel of the angle value.</param>
        /// <returns>An object that represents value.</returns>
        [Pure]
        public static AngleDegree AngleFromDegree(Int32 degree, Double minutes)
        {
            if (minutes < 0 || minutes >= 60)
            {
                throw new ArgumentOutOfRangeException(nameof(minutes), minutes, "Argument must be positive and less than 60.");
            }

            return degree < 0 ? new AngleDegree(degree - minutes / 60) : new AngleDegree(degree + minutes / 60);
        }

        /// <summary>
        /// Returns an <see cref="AngleDegree"/>> that represents a specified number of degrees.
        /// </summary>
        /// <param name="degree">The degrees parcel of the angle value.</param>
        /// <param name="minutes">The minutes parcel of the angle value.</param>
        /// <param name="seconds">The seconds parcel of the angle value.</param>
        /// <returns>An object that represents value.</returns>
        [Pure]
        public static AngleDegree AngleFromDegree(Int32 degree, Int32 minutes, Double seconds)
        {
            if (minutes < 0 || minutes >= 60)
            {
                throw new ArgumentOutOfRangeException(nameof(minutes), minutes, "Argument must be positive and less than 60.");
            }

            if (seconds < 0 || seconds >= 60)
            {
                throw new ArgumentOutOfRangeException(nameof(seconds), seconds, "Argument must be positive and less than 60.");
            }

            return degree < 0 ? new AngleDegree(degree - minutes / 60D - seconds / 3600) : new AngleDegree(degree + minutes / 60D + seconds / 3600);
        }

        /// <summary>
        /// Returns an <see cref="AngleRadian"/> that represents a angle with the specified number of radians.
        /// </summary>
        /// <param name="radian">A number of radians.</param>
        /// <returns>An object that represents value.</returns>
        [Pure]
        public static AngleRadian AngleFromRadian(Double radian)
        {
            return new AngleRadian(radian);
        }

        /// <summary>
        /// Returns an <see cref="AngleGradian"/> that represents a specified number of gradians.
        /// </summary>
        /// <param name="gradian">A number of gradians.</param>
        /// <returns>An object that represents value.</returns>
        [Pure]
        public static AngleGradian AngleFromGradian(Double gradian)
        {
            return new AngleGradian(gradian);
        }

        /// <summary>
        /// Returns an <see cref="AngleRevolution"/> that represents a specified number of revolutions.
        /// </summary>
        /// <param name="revolution">A number of revolutions.</param>
        /// <returns>An object that represents value.</returns>
        [Pure]
        public static AngleRevolution AngleFromRevolution(Double revolution)
        {
            return new AngleRevolution(revolution);
        }

        /// <summary>
        /// Returns the absolute value of the <see cref="AngleDegree"/>.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>
        /// An <see cref="AngleDegree"/>, x, such that <see cref="AngleDegree.Zero"/> &lt;= x &lt;= <see cref="AngleDegree.MaxValue"/>.
        /// </returns>
        [Pure]
        public static AngleDegree Abs(this AngleDegree angle)
        {
            return new AngleDegree(Math.Abs((Double) angle));
        }

        /// <summary>
        /// Returns the absolute value of the <see cref="AngleRadian"/>.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>
        /// An <see cref="AngleRadian"/>, x, such that <see cref="AngleRadian.Zero"/> &lt;= x &lt;= <see cref="AngleRadian.MaxValue"/>.
        /// </returns>
        [Pure]
        public static AngleRadian Abs(this AngleRadian angle)
        {
            return new AngleRadian(Math.Abs((Double) angle));
        }

        /// <summary>
        /// Returns the absolute value of the <see cref="AngleGradian"/>.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>
        /// An <see cref="AngleGradian"/>, x, such that <see cref="AngleGradian.Zero"/> &lt;= x &lt;= <see cref="AngleGradian.MaxValue"/>.
        /// </returns>
        [Pure]
        public static AngleGradian Abs(this AngleGradian angle)
        {
            return new AngleGradian(Math.Abs((Double) angle));
        }

        /// <summary>
        /// Returns the absolute value of the <see cref="AngleRevolution"/>.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>
        /// An <see cref="AngleRevolution"/>, x, such that <see cref="AngleRevolution.Zero"/> &lt;= x &lt;= <see cref="AngleRevolution.MaxValue"/>.
        /// </returns>
        [Pure]
        public static AngleRevolution Abs(this AngleRevolution angle)
        {
            return new AngleRevolution(Math.Abs((Double) angle));
        }

        /// <summary>
        /// Returns a value indicating the sign of an angle.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>A number that indicates the sign of value, -1 if value is less than zero, 0 if value equal to zero, 1 if value is grater than zero.</returns>
        [Pure]
        public static Int32 Sign(this AngleDegree angle)
        {
            return Math.Sign((Double) angle);
        }

        /// <summary>
        /// Returns a value indicating the sign of an angle.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>A number that indicates the sign of value, -1 if value is less than zero, 0 if value equal to zero, 1 if value is grater than zero.</returns>
        [Pure]
        public static Int32 Sign(this AngleRadian angle)
        {
            return Math.Sign((Double) angle);
        }

        /// <summary>
        /// Returns a value indicating the sign of an angle.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>A number that indicates the sign of value, -1 if value is less than zero, 0 if value equal to zero, 1 if value is grater than zero.</returns>
        [Pure]
        public static Int32 Sign(this AngleGradian angle)
        {
            return Math.Sign((Double) angle);
        }

        /// <summary>
        /// Returns a value indicating the sign of an angle.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>A number that indicates the sign of value, -1 if value is less than zero, 0 if value equal to zero, 1 if value is grater than zero.</returns>
        [Pure]
        public static Int32 Sign(this AngleRevolution angle)
        {
            return Math.Sign((Double) angle);
        }

        /// <summary>
        /// Returns the smaller of two angles.
        /// </summary>
        /// <param name="first">The first of two angles to compare.</param>
        /// <param name="second">The second of two angles to compare.</param>
        /// <returns>A reference to parameter first or second, whichever is smaller.</returns>
        [Pure]
        public static AngleDegree Min(this AngleDegree first, AngleDegree second)
        {
            return first <= second ? first : second;
        }

        /// <summary>
        /// Returns the smaller of two angles.
        /// </summary>
        /// <param name="first">The first of two angles to compare.</param>
        /// <param name="second">The second of two angles to compare.</param>
        /// <returns>A reference to parameter first or second, whichever is smaller.</returns>
        [Pure]
        public static AngleRadian Min(this AngleRadian first, AngleRadian second)
        {
            return first <= second ? first : second;
        }

        /// <summary>
        /// Returns the smaller of two angles.
        /// </summary>
        /// <param name="first">The first of two angles to compare.</param>
        /// <param name="second">The second of two angles to compare.</param>
        /// <returns>A reference to parameter first or right, whichever is smaller.</returns>
        [Pure]
        public static AngleGradian Min(this AngleGradian first, AngleGradian second)
        {
            return first <= second ? first : second;
        }

        /// <summary>
        /// Returns the smaller of two angles.
        /// </summary>
        /// <param name="first">The first of two angles to compare.</param>
        /// <param name="second">The second of two angles to compare.</param>
        /// <returns>A reference to parameter first or second, whichever is smaller.</returns>
        [Pure]
        public static AngleRevolution Min(this AngleRevolution first, AngleRevolution second)
        {
            return first <= second ? first : second;
        }

        /// <summary>
        /// Returns the largest of two angles.
        /// </summary>
        /// <param name="first">The first of two angles to compare.</param>
        /// <param name="second">The second of two angles to compare.</param>
        /// <returns>A reference to parameter first or second, whichever is larger.</returns>
        [Pure]
        public static AngleDegree Max(this AngleDegree first, AngleDegree second)
        {
            return first >= second ? first : second;
        }

        /// <summary>
        /// Returns the largest of two angles.
        /// </summary>
        /// <param name="first">The first of two angles to compare.</param>
        /// <param name="second">The second of two angles to compare.</param>
        /// <returns>A reference to parameter first or second, whichever is larger.</returns>
        [Pure]
        public static AngleRadian Max(this AngleRadian first, AngleRadian second)
        {
            return first >= second ? first : second;
        }

        /// <summary>
        /// Returns the largest of two angles.
        /// </summary>
        /// <param name="first">The first of two angles to compare.</param>
        /// <param name="second">The second of two angles to compare.</param>
        /// <returns>A reference to parameter first or second, whichever is larger.</returns>
        [Pure]
        public static AngleGradian Max(this AngleGradian first, AngleGradian second)
        {
            return first >= second ? first : second;
        }

        /// <summary>
        /// Returns the largest of two angles.
        /// </summary>
        /// <param name="first">The first of two angles to compare.</param>
        /// <param name="second">The second of two angles to compare.</param>
        /// <returns>A reference to parameter first or second, whichever is larger.</returns>
        [Pure]
        public static AngleRevolution Max(this AngleRevolution first, AngleRevolution second)
        {
            return first >= second ? first : second;
        }

        /// <summary>
        /// Reduce an angle between 0 and 2π.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns></returns>
        [Pure]
        public static AngleDegree AngleReduce(this AngleDegree angle)
        {
            return new AngleDegree(AngleDegreeReduce((Double) angle));
        }

        /// <summary>
        /// Reduce an angle between 0 and 2π.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns></returns>
        [Pure]
        public static AngleRadian AngleReduce(this AngleRadian angle)
        {
            return new AngleRadian(AngleRadianReduce((Double) angle));
        }

        /// <summary>
        /// Reduce an angle between 0 and 2π.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns></returns>
        [Pure]
        public static AngleGradian AngleReduce(this AngleGradian angle)
        {
            return new AngleGradian(AngleGradianReduce((Double) angle));
        }

        /// <summary>
        /// Reduce an angle between 0 and 2π.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns></returns>
        [Pure]
        public static AngleRevolution AngleReduce(this AngleRevolution angle)
        {
            return new AngleRevolution(AngleRevolutionReduce((Double) angle));
        }

        [Pure]
        public static Double AngleDegreeReduce(Double degree)
        {
            return AngleReduce(degree, DoubleDegree.Full);
        }

        [Pure]
        public static Double AngleRadianReduce(Double radian)
        {
            return AngleReduce(radian, DoubleRadian.Full);
        }

        [Pure]
        public static Double AngleGradianReduce(Double gradian)
        {
            return AngleReduce(gradian, DoubleGradian.Full);
        }

        [Pure]
        public static Double AngleRevolutionReduce(Double revolution)
        {
            return AngleReduce(revolution, DoubleRevolution.Full);
        }

        /// <summary>
        /// Returns the quadrant where the terminal side of the angle is when the standard position.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>The quadrant where the terminal side of the angle is when the standard position.</returns>
        [Pure]
        public static AngleQuadrant GetAngleQuadrant(this AngleDegree angle)
        {
            return GetAngleDegreeQuadrant((Double) angle);
        }

        /// <summary>
        /// Returns the quadrant where the terminal side of the angle is in when in the standard position.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>The quadrant where the terminal side of the angle is in when in the standard position.</returns>
        [Pure]
        public static AngleQuadrant GetAngleQuadrant(this AngleRadian angle)
        {
            return GetAngleRadianQuadrant((Double) angle);
        }

        /// <summary>
        /// Returns the quadrant where the terminal side of the angle is when the standard position.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>The quadrant where the terminal side of the angle is when the standard position.</returns>
        [Pure]
        public static AngleQuadrant GetAngleQuadrant(this AngleGradian angle)
        {
            return GetAngleGradianQuadrant((Double) angle);
        }

        /// <summary>
        /// Returns the quadrant where the terminal side of the angle is when the standard position.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>The quadrant where the terminal side of the angle is when the standard position.</returns>
        [Pure]
        public static AngleQuadrant GetQuadrant(this AngleRevolution angle)
        {
            return GetAngleGradianQuadrant((Double) angle);
        }

        [Pure]
        public static AngleQuadrant GetAngleDegreeQuadrant(Double degree)
        {
            return GetAngleQuadrant(degree, DoubleDegree.Quarter, DoubleDegree.Straight, DoubleDegree.Full);
        }

        [Pure]
        public static AngleQuadrant GetAngleRadianQuadrant(Double radian)
        {
            return GetAngleQuadrant(radian, DoubleRadian.Quarter, DoubleRadian.Straight, DoubleRadian.Full);
        }

        [Pure]
        public static AngleQuadrant GetAngleGradianQuadrant(Double gradian)
        {
            return GetAngleQuadrant(gradian, DoubleGradian.Quarter, DoubleGradian.Straight, DoubleGradian.Full);
        }

        [Pure]
        public static AngleQuadrant GetAngleRevolutionQuadrant(Double revolution)
        {
            return GetAngleQuadrant(revolution, DoubleRevolution.Quarter, DoubleRevolution.Straight, DoubleRevolution.Full);
        }

        /// <summary>
        /// Returns the reference angle.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>The reference angle.</returns>
        [Pure]
        public static AngleDegree GetAngleReference(this AngleDegree angle)
        {
            return new AngleDegree(GetAngleDegreeReference((Double) angle));
        }

        /// <summary>
        /// Returns the reference angle.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>The reference angle.</returns>
        [Pure]
        public static AngleRadian GetAngleReference(this AngleRadian angle)
        {
            return new AngleRadian(GetAngleRadianReference((Double) angle));
        }

        /// <summary>
        /// Returns the reference angle.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>The reference angle.</returns>
        [Pure]
        public static AngleGradian GetAngleReference(this AngleGradian angle)
        {
            return new AngleGradian(GetAngleGradianReference((Double) angle));
        }

        /// <summary>
        /// Returns the reference angle.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>The reference angle.</returns>
        [Pure]
        public static AngleRevolution GetAngleReference(this AngleRevolution angle)
        {
            return new AngleRevolution(GetAngleRevolutionReference((Double) angle));
        }

        [Pure]
        public static Double GetAngleDegreeReference(Double degree)
        {
            return GetAngleReference(degree, DoubleDegree.Quarter, DoubleDegree.Straight, DoubleDegree.Full);
        }

        [Pure]
        public static Double GetAngleRadianReference(Double radian)
        {
            return GetAngleReference(radian, DoubleRadian.Quarter, DoubleRadian.Straight, DoubleRadian.Full);
        }

        [Pure]
        public static Double GetAngleGradianReference(Double gradian)
        {
            return GetAngleReference(gradian, DoubleGradian.Quarter, DoubleGradian.Straight, DoubleGradian.Full);
        }

        [Pure]
        public static Double GetAngleRevolutionReference(Double revolution)
        {
            return GetAngleReference(revolution, DoubleRevolution.Quarter, DoubleRevolution.Straight, DoubleRevolution.Full);
        }

        /// <summary>
        /// Compares two <see cref="AngleDegree"/> values and returns an integer that indicates whether when both reduced the first value is shorter than, equal to, or longer than the second value.
        /// </summary>
        /// <param name="first">The first angle to compare.</param>
        /// <param name="second">The second angle to compare.</param>
        /// <returns></returns>
        [Pure]
        public static Int32 AngleReduceCompareTo(this AngleDegree first, AngleDegree second)
        {
            return AngleReduce(first).CompareTo(AngleReduce(second));
        }

        /// <summary>
        /// Compares two <see cref="AngleRadian"/> values and returns an integer that indicates whether when both reduced the first value is shorter than, equal to, or longer than the second value.
        /// </summary>
        /// <param name="first">The first angle to compare.</param>
        /// <param name="second">The second angle to compare.</param>
        /// <returns></returns>
        [Pure]
        public static Int32 AngleReduceCompareTo(this AngleRadian first, AngleRadian second)
        {
            return AngleReduce(first).CompareTo(AngleReduce(second));
        }

        /// <summary>
        /// Compares two <see cref="AngleGradian"/> values and returns an integer that indicates whether when both reduced the first value is shorter than, equal to, or longer than the second value.
        /// </summary>
        /// <param name="first">The first angle to compare.</param>
        /// <param name="second">The second angle to compare.</param>
        /// <returns></returns>
        [Pure]
        public static Int32 AngleReduceCompareTo(this AngleGradian first, AngleGradian second)
        {
            return AngleReduce(first).CompareTo(AngleReduce(second));
        }

        /// <summary>
        /// Compares two <see cref="AngleRevolution"/> values and returns an integer that indicates whether when both reduced the first value is shorter than, equal to, or longer than the second value.
        /// </summary>
        /// <param name="first">The first angle to compare.</param>
        /// <param name="second">The second angle to compare.</param>
        /// <returns></returns>
        [Pure]
        public static Int32 AngleReduceCompareTo(this AngleRevolution first, AngleRevolution second)
        {
            return AngleReduce(first).CompareTo(AngleReduce(second));
        }

        /// <summary>
        /// Indicates whether the specified angle is equal to Zero when reduced.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>true if the reduction of the absolute angle is zero; otherwise false.</returns>
        [Pure]
        public static Boolean IsAngleZero(this AngleDegree angle)
        {
            return Math.Abs((Double) angle % DoubleDegree.Full) < Double.Epsilon;
        }

        /// <summary>
        /// Indicates whether the specified angle is equal to Zero when reduced.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>true if the reduction of the absolute angle is zero; otherwise false.</returns>
        [Pure]
        public static Boolean IsAngleZero(this AngleRadian angle)
        {
            return Math.Abs((Double) angle % DoubleRadian.Full) < Double.Epsilon;
        }

        /// <summary>
        /// Indicates whether the specified angle is equal to Zero when reduced.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>true if the reduction of the absolute angle is zero; otherwise false.</returns>
        [Pure]
        public static Boolean IsAngleZero(this AngleGradian angle)
        {
            return Math.Abs((Double) angle % DoubleGradian.Full) < Double.Epsilon;
        }

        /// <summary>
        /// Indicates whether the specified angle is equal to Zero when reduced.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>true if the reduction of the absolute angle is zero; otherwise false.</returns>
        [Pure]
        public static Boolean IsAngleZero(this AngleRevolution angle)
        {
            return Math.Abs((Double) angle % DoubleRevolution.Full) < Double.Epsilon;
        }

        /// <summary>
        /// Indicates whether the specified angle is acute.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>true if the reduction of the absolute angle is greater than zero and less than 90 degrees; otherwise false.</returns>
        [Pure]
        public static Boolean IsAngleAcute(this AngleDegree angle)
        {
            return AngleDegreeReduce(Math.Abs((Double) angle)) is > 0 and < DoubleDegree.Quarter;
        }

        /// <summary>
        /// Indicates whether the specified angle is acute.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>true if the reduction of the absolute angle is greater than zero and less than 90 degrees; otherwise false.</returns>
        [Pure]
        public static Boolean IsAngleAcute(this AngleRadian angle)
        {
            return AngleRadianReduce(Math.Abs((Double) angle)) is > 0 and < DoubleRadian.Quarter;
        }

        /// <summary>
        /// Indicates whether the specified angle is acute.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>true if the reduction of the absolute angle is greater than zero and less than 90 degrees; otherwise false.</returns>
        [Pure]
        public static Boolean IsAngleAcute(this AngleGradian angle)
        {
            return AngleGradianReduce(Math.Abs((Double) angle)) is > 0 and < DoubleGradian.Quarter;
        }

        /// <summary>
        /// Indicates whether the specified angle is acute.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>true if the reduction of the absolute angle is greater than zero and less than 90 degrees; otherwise false.</returns>
        [Pure]
        public static Boolean IsAngleAcute(this AngleRevolution angle)
        {
            return AngleRevolutionReduce(Math.Abs((Double) angle)) is > 0 and < DoubleRevolution.Quarter;
        }

        /// <summary>
        /// Indicates whether the specified angle is right.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>true if the reduction of the absolute angle is 90 degrees; otherwise false.</returns>
        [Pure]
        public static Boolean IsAngleQuarter(this AngleDegree angle)
        {
            return Math.Abs(AngleDegreeReduce(Math.Abs((Double) angle)) - DoubleDegree.Quarter) < Double.Epsilon;
        }

        /// <summary>
        /// Indicates whether the specified angle is right.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>true if the reduction of the absolute angle is 90 degrees; otherwise false.</returns>
        [Pure]
        public static Boolean IsAngleQuarter(this AngleRadian angle)
        {
            return Math.Abs(AngleRadianReduce(Math.Abs((Double) angle)) - DoubleRadian.Quarter) < Double.Epsilon;
        }

        /// <summary>
        /// Indicates whether the specified angle is right.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>true if the reduction of the absolute angle is 90 degrees; otherwise false.</returns>
        [Pure]
        public static Boolean IsAngleQuarter(this AngleGradian angle)
        {
            return Math.Abs(AngleGradianReduce(Math.Abs((Double) angle)) - DoubleGradian.Quarter) < Double.Epsilon;
        }

        /// <summary>
        /// Indicates whether the specified angle is right.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>true if the reduction of the absolute angle is 90 degrees; otherwise false.</returns>
        [Pure]
        public static Boolean IsAngleQuarter(this AngleRevolution angle)
        {
            return Math.Abs(AngleRevolutionReduce(Math.Abs((Double) angle)) - DoubleRevolution.Quarter) < Double.Epsilon;
        }

        /// <summary>
        /// Indicates whether the specified angle is obtuse.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>true if the reduction of the absolute angle is greater than 90 degrees and less than 180 degrees; otherwise false.</returns>
        [Pure]
        public static Boolean IsAngleObtuse(this AngleDegree angle)
        {
            return AngleDegreeReduce(Math.Abs((Double) angle)) is > DoubleDegree.Quarter and < DoubleDegree.Straight;
        }

        /// <summary>
        /// Indicates whether the specified angle is obtuse.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>true if the reduction of the absolute angle is greater than 90 degrees and less than 180 degrees; otherwise false.</returns>
        [Pure]
        public static Boolean IsAngleObtuse(this AngleRadian angle)
        {
            return AngleRadianReduce(Math.Abs((Double) angle)) is > DoubleRadian.Quarter and < DoubleRadian.Straight;
        }

        /// <summary>
        /// Indicates whether the specified angle is obtuse.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>true if the reduction of the absolute angle is greater than 90 degrees and less than 180 degrees; otherwise false.</returns>
        [Pure]
        public static Boolean IsAngleObtuse(this AngleGradian angle)
        {
            return AngleGradianReduce(Math.Abs((Double) angle)) is > DoubleGradian.Quarter and < DoubleGradian.Straight;
        }

        /// <summary>
        /// Indicates whether the specified angle is obtuse.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>true if the reduction of the absolute angle is greater than 90 degrees and less than 180 degrees; otherwise false.</returns>
        [Pure]
        public static Boolean IsAngleObtuse(this AngleRevolution angle)
        {
            return AngleRevolutionReduce(Math.Abs((Double) angle)) is > DoubleRevolution.Quarter and < DoubleRevolution.Straight;
        }

        /// <summary>
        /// Indicates whether the specified angle is straight.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>true if the reduction of the absolute angle is 180 degrees; otherwise false.</returns>
        [Pure]
        public static Boolean IsAngleStraight(this AngleDegree angle)
        {
            return Math.Abs(AngleDegreeReduce(Math.Abs((Double) angle)) - DoubleDegree.Straight) < Double.Epsilon;
        }

        /// <summary>
        /// Indicates whether the specified angle is straight.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>true if the reduction of the absolute angle is 180 degrees; otherwise false.</returns>
        [Pure]
        public static Boolean IsAngleStraight(this AngleRadian angle)
        {
            return Math.Abs(AngleRadianReduce(Math.Abs((Double) angle)) - DoubleRadian.Straight) < Double.Epsilon;
        }

        /// <summary>
        /// Indicates whether the specified angle is straight.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>true if the reduction of the absolute angle is 180 degrees; otherwise false.</returns>
        [Pure]
        public static Boolean IsAngleStraight(this AngleGradian angle)
        {
            return Math.Abs(AngleGradianReduce(Math.Abs((Double) angle)) - DoubleGradian.Straight) < Double.Epsilon;
        }

        /// <summary>
        /// Indicates whether the specified angle is straight.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>true if the reduction of the absolute angle is 180 degrees; otherwise false.</returns>
        [Pure]
        public static Boolean IsAngleStraight(this AngleRevolution angle)
        {
            return Math.Abs(AngleRevolutionReduce(Math.Abs((Double) angle)) - DoubleRevolution.Straight) < Double.Epsilon;
        }

        /// <summary>
        /// Indicates whether the specified angle is reflex.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>true if the reduction of the absolute angle is greater than 180 degrees and less than 360 degrees; otherwise false.</returns>
        [Pure]
        public static Boolean IsAngleReflex(this AngleDegree angle)
        {
            return AngleDegreeReduce(Math.Abs((Double) angle)) > DoubleDegree.Straight;
        }

        /// <summary>
        /// Indicates whether the specified angle is reflex.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>true if the reduction of the absolute angle is greater than 180 degrees and less than 360 degrees; otherwise false.</returns>
        [Pure]
        public static Boolean IsAngleReflex(this AngleRadian angle)
        {
            return AngleRadianReduce(Math.Abs((Double) angle)) > DoubleRadian.Straight;
        }

        /// <summary>
        /// Indicates whether the specified angle is reflex.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>true if the reduction of the absolute angle is greater than 180 degrees and less than 360 degrees; otherwise false.</returns>
        [Pure]
        public static Boolean IsAngleReflex(this AngleGradian angle)
        {
            return AngleGradianReduce(Math.Abs((Double) angle)) > DoubleGradian.Straight;
        }

        /// <summary>
        /// Indicates whether the specified angle is reflex.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>true if the reduction of the absolute angle is greater than 180 degrees and less than 360 degrees; otherwise false.</returns>
        [Pure]
        public static Boolean IsAngleReflex(this AngleRevolution angle)
        {
            return AngleRevolutionReduce(Math.Abs((Double) angle)) > DoubleRevolution.Straight;
        }

        /// <summary>
        /// Indicates whether the specified angle is oblique.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>true if the angle is not right or a multiple of a right angle; otherwise false.</returns>
        [Pure]
        public static Boolean IsAngleOblique(this AngleDegree angle)
        {
            return Math.Abs((Double) angle % DoubleDegree.Quarter) >= Double.Epsilon;
        }

        /// <summary>
        /// Indicates whether the specified angle is oblique.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>true if the angle is not right or a multiple of a right angle; otherwise false.</returns>
        [Pure]
        public static Boolean IsAngleOblique(this AngleRadian angle)
        {
            return Math.Abs((Double) angle % DoubleRadian.Quarter) >= Double.Epsilon;
        }

        /// <summary>
        /// Indicates whether the specified angle is oblique.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>true if the angle is not right or a multiple of a right angle; otherwise false.</returns>
        [Pure]
        public static Boolean IsAngleOblique(this AngleGradian angle)
        {
            return Math.Abs((Double) angle % DoubleGradian.Quarter) >= Double.Epsilon;
        }

        /// <summary>
        /// Indicates whether the specified angle is oblique.
        /// </summary>
        /// <param name="angle">Source angle.</param>
        /// <returns>true if the angle is not right or a multiple of a right angle; otherwise false.</returns>
        [Pure]
        public static Boolean IsAngleOblique(this AngleRevolution angle)
        {
            return Math.Abs((Double) angle % DoubleRevolution.Quarter) >= Double.Epsilon;
        }

        /// <summary>
        /// Performs a linear interpolation.
        /// </summary>
        /// <param name="first">The first angle.</param>
        /// <param name="second">The second angle.</param>
        /// <param name="lerp">A value that linearly interpolates between the <see cref="first"/> parameter and the <see cref="second"/> parameter.</param>
        /// <returns>The result of the linear interpolation.</returns>
        [Pure]
        public static AngleDegree AngleLerp(this AngleDegree first, AngleDegree second, Double lerp)
        {
            return new AngleDegree(AngleLerp((Double) first, (Double) second, lerp));
        }

        /// <summary>
        /// Performs a linear interpolation.
        /// </summary>
        /// <param name="first">The first angle.</param>
        /// <param name="second">The second angle.</param>
        /// <param name="lerp">A value that linearly interpolates between the first parameter and the second parameter.</param>
        /// <returns>The result of the linear interpolation.</returns>
        [Pure]
        public static AngleRadian AngleLerp(this AngleRadian first, AngleRadian second, Double lerp)
        {
            return new AngleRadian(AngleLerp((Double) first, (Double) second, lerp));
        }

        /// <summary>
        /// Performs a linear interpolation.
        /// </summary>
        /// <param name="first">The first angle.</param>
        /// <param name="second">The second angle.</param>
        /// <param name="lerp">A value that linearly interpolates between the first parameter and the second parameter.</param>
        /// <returns>The result of the linear interpolation.</returns>
        [Pure]
        public static AngleGradian AngleLerp(this AngleGradian first, AngleGradian second, Double lerp)
        {
            return new AngleGradian(AngleLerp((Double) first, (Double) second, lerp));
        }

        /// <summary>
        /// Performs a linear interpolation.
        /// </summary>
        /// <param name="first">The first angle.</param>
        /// <param name="second">The second angle.</param>
        /// <param name="lerp">A value that linearly interpolates between the first parameter and the second parameter.</param>
        /// <returns>The result of the linear interpolation.</returns>
        [Pure]
        public static AngleRevolution AngleLerp(this AngleRevolution first, AngleRevolution second, Double lerp)
        {
            return new AngleRevolution(AngleLerp((Double) first, (Double) second, lerp));
        }

        /// <inheritdoc cref="MathUtilities.Sin(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sin(this AngleDegree value)
        {
            return MathUtilities.Sin((Double) value / DoubleRelations.DegreeInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Sin(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sin(this AngleRadian value)
        {
            return MathUtilities.Sin((Double) value);
        }

        /// <inheritdoc cref="MathUtilities.Sin(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sin(this AngleGradian value)
        {
            return MathUtilities.Sin((Double) value / DoubleRelations.GradianInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Sin(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sin(this AngleRevolution value)
        {
            return MathUtilities.Sin((Double) value * DoubleRadian.Full);
        }

        /// <inheritdoc cref="MathUtilities.Sinh(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sinh(this AngleDegree value)
        {
            return MathUtilities.Sinh((Double) value / DoubleRelations.DegreeInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Sinh(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sinh(this AngleRadian value)
        {
            return MathUtilities.Sinh((Double) value);
        }

        /// <inheritdoc cref="MathUtilities.Sinh(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sinh(this AngleGradian value)
        {
            return MathUtilities.Sinh((Double) value / DoubleRelations.GradianInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Sinh(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sinh(this AngleRevolution value)
        {
            return MathUtilities.Sinh((Double) value * DoubleRadian.Full);
        }

        /// <inheritdoc cref="MathUtilities.Asin(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Asin(this AngleDegree value)
        {
            return MathUtilities.Asin((Double) value / DoubleRelations.DegreeInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Asin(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Asin(this AngleRadian value)
        {
            return MathUtilities.Asin((Double) value);
        }

        /// <inheritdoc cref="MathUtilities.Asin(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Asin(this AngleGradian value)
        {
            return MathUtilities.Asin((Double) value / DoubleRelations.GradianInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Asin(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Asin(this AngleRevolution value)
        {
            return MathUtilities.Asin((Double) value * DoubleRadian.Full);
        }

        /// <inheritdoc cref="MathUtilities.Asinh(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Asinh(this AngleDegree value)
        {
            return MathUtilities.Asinh((Double) value / DoubleRelations.DegreeInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Asinh(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Asinh(this AngleRadian value)
        {
            return MathUtilities.Asinh((Double) value);
        }

        /// <inheritdoc cref="MathUtilities.Asinh(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Asinh(this AngleGradian value)
        {
            return MathUtilities.Asinh((Double) value / DoubleRelations.GradianInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Asinh(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Asinh(this AngleRevolution value)
        {
            return MathUtilities.Asinh((Double) value * DoubleRadian.Full);
        }

        /// <inheritdoc cref="MathUtilities.Cos(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Cos(this AngleDegree value)
        {
            return MathUtilities.Cos((Double) value / DoubleRelations.DegreeInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Cos(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Cos(this AngleRadian value)
        {
            return MathUtilities.Cos((Double) value);
        }

        /// <inheritdoc cref="MathUtilities.Cos(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Cos(this AngleGradian value)
        {
            return MathUtilities.Cos((Double) value / DoubleRelations.GradianInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Cos(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Cos(this AngleRevolution value)
        {
            return MathUtilities.Cos((Double) value * DoubleRadian.Full);
        }

        /// <inheritdoc cref="MathUtilities.Cosh(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Cosh(this AngleDegree value)
        {
            return MathUtilities.Cosh((Double) value / DoubleRelations.DegreeInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Cosh(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Cosh(this AngleRadian value)
        {
            return MathUtilities.Cosh((Double) value);
        }

        /// <inheritdoc cref="MathUtilities.Cosh(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Cosh(this AngleGradian value)
        {
            return MathUtilities.Cosh((Double) value / DoubleRelations.GradianInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Cosh(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Cosh(this AngleRevolution value)
        {
            return MathUtilities.Cosh((Double) value * DoubleRadian.Full);
        }

        /// <inheritdoc cref="MathUtilities.Acos(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acos(this AngleDegree value)
        {
            return MathUtilities.Acos((Double) value / DoubleRelations.DegreeInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Acos(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acos(this AngleRadian value)
        {
            return MathUtilities.Acos((Double) value);
        }

        /// <inheritdoc cref="MathUtilities.Acos(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acos(this AngleGradian value)
        {
            return MathUtilities.Acos((Double) value / DoubleRelations.GradianInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Acos(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acos(this AngleRevolution value)
        {
            return MathUtilities.Acos((Double) value * DoubleRadian.Full);
        }

        /// <inheritdoc cref="MathUtilities.Acosh(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acosh(this AngleDegree value)
        {
            return MathUtilities.Acosh((Double) value / DoubleRelations.DegreeInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Acosh(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acosh(this AngleRadian value)
        {
            return MathUtilities.Acosh((Double) value);
        }

        /// <inheritdoc cref="MathUtilities.Acosh(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acosh(this AngleGradian value)
        {
            return MathUtilities.Acosh((Double) value / DoubleRelations.GradianInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Acosh(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acosh(this AngleRevolution value)
        {
            return MathUtilities.Acosh((Double) value * DoubleRadian.Full);
        }

        /// <inheritdoc cref="MathUtilities.Tan(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Tan(this AngleDegree value)
        {
            return MathUtilities.Tan((Double) value / DoubleRelations.DegreeInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Tan(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Tan(this AngleRadian value)
        {
            return MathUtilities.Tan((Double) value);
        }

        /// <inheritdoc cref="MathUtilities.Tan(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Tan(this AngleGradian value)
        {
            return MathUtilities.Tan((Double) value / DoubleRelations.GradianInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Tan(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Tan(this AngleRevolution value)
        {
            return MathUtilities.Tan((Double) value * DoubleRadian.Full);
        }

        /// <inheritdoc cref="MathUtilities.Tanh(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Tanh(this AngleDegree value)
        {
            return MathUtilities.Tanh((Double) value / DoubleRelations.DegreeInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Tanh(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Tanh(this AngleRadian value)
        {
            return MathUtilities.Tanh((Double) value);
        }

        /// <inheritdoc cref="MathUtilities.Tanh(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Tanh(this AngleGradian value)
        {
            return MathUtilities.Tanh((Double) value / DoubleRelations.GradianInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Tanh(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Tanh(this AngleRevolution value)
        {
            return MathUtilities.Tanh((Double) value * DoubleRadian.Full);
        }

        /// <inheritdoc cref="MathUtilities.Atan(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Atan(this AngleDegree value)
        {
            return MathUtilities.Atan((Double) value / DoubleRelations.DegreeInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Atan(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Atan(this AngleRadian value)
        {
            return MathUtilities.Atan((Double) value);
        }

        /// <inheritdoc cref="MathUtilities.Atan(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Atan(this AngleGradian value)
        {
            return MathUtilities.Atan((Double) value / DoubleRelations.GradianInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Atan(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Atan(this AngleRevolution value)
        {
            return MathUtilities.Atan((Double) value * DoubleRadian.Full);
        }

        /// <inheritdoc cref="MathUtilities.Atan2(Double,Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Atan2(this AngleDegree value, Double point)
        {
            return MathUtilities.Atan2((Double) value / DoubleRelations.DegreeInRadian, point);
        }

        /// <inheritdoc cref="MathUtilities.Atan2(Double,Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Atan2(this AngleDegree value, AngleDegree angle)
        {
            return MathUtilities.Atan2((Double) value / DoubleRelations.DegreeInRadian, (Double) angle / DoubleRelations.DegreeInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Atan2(Double,Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Atan2(this AngleRadian value, Double point)
        {
            return MathUtilities.Atan2((Double) value, point);
        }

        /// <inheritdoc cref="MathUtilities.Atan2(Double,Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Atan2(this AngleRadian value, AngleRadian angle)
        {
            return MathUtilities.Atan2((Double) value, (Double) angle);
        }

        /// <inheritdoc cref="MathUtilities.Atan2(Double,Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Atan2(this AngleGradian value, Double point)
        {
            return MathUtilities.Atan2((Double) value / DoubleRelations.GradianInRadian, point);
        }

        /// <inheritdoc cref="MathUtilities.Atan2(Double,Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Atan2(this AngleGradian value, AngleGradian angle)
        {
            return MathUtilities.Atan2((Double) value / DoubleRelations.GradianInRadian, (Double) angle / DoubleRelations.GradianInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Atan2(Double,Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Atan2(this AngleRevolution value, Double point)
        {
            return MathUtilities.Atan2((Double) value * DoubleRadian.Full, point);
        }

        /// <inheritdoc cref="MathUtilities.Atan2(Double,Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Atan2(this AngleRevolution value, AngleRevolution angle)
        {
            return MathUtilities.Atan2((Double) value * DoubleRadian.Full, (Double) angle * DoubleRadian.Full);
        }

        /// <inheritdoc cref="MathUtilities.Atanh(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Atanh(this AngleDegree value)
        {
            return MathUtilities.Atanh((Double) value / DoubleRelations.DegreeInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Atanh(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Atanh(this AngleRadian value)
        {
            return MathUtilities.Atanh((Double) value);
        }

        /// <inheritdoc cref="MathUtilities.Atanh(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Atanh(this AngleGradian value)
        {
            return MathUtilities.Atanh((Double) value / DoubleRelations.GradianInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Atanh(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Atanh(this AngleRevolution value)
        {
            return MathUtilities.Atanh((Double) value * DoubleRadian.Full);
        }

        /// <inheritdoc cref="MathUtilities.Cot(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Cot(this AngleDegree value)
        {
            return MathUtilities.Cot((Double) value / DoubleRelations.DegreeInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Cot(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Cot(this AngleRadian value)
        {
            return MathUtilities.Cot((Double) value);
        }

        /// <inheritdoc cref="MathUtilities.Cot(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Cot(this AngleGradian value)
        {
            return MathUtilities.Cot((Double) value / DoubleRelations.GradianInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Cot(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Cot(this AngleRevolution value)
        {
            return MathUtilities.Cot((Double) value * DoubleRadian.Full);
        }

        /// <inheritdoc cref="MathUtilities.Coth(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Coth(this AngleDegree value)
        {
            return MathUtilities.Coth((Double) value / DoubleRelations.DegreeInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Coth(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Coth(this AngleRadian value)
        {
            return MathUtilities.Coth((Double) value);
        }

        /// <inheritdoc cref="MathUtilities.Coth(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Coth(this AngleGradian value)
        {
            return MathUtilities.Coth((Double) value / DoubleRelations.GradianInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Coth(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Coth(this AngleRevolution value)
        {
            return MathUtilities.Coth((Double) value * DoubleRadian.Full);
        }

        /// <inheritdoc cref="MathUtilities.Acot(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acot(this AngleDegree value)
        {
            return MathUtilities.Acot((Double) value / DoubleRelations.DegreeInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Acot(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acot(this AngleRadian value)
        {
            return MathUtilities.Acot((Double) value);
        }

        /// <inheritdoc cref="MathUtilities.Acot(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acot(this AngleGradian value)
        {
            return MathUtilities.Acot((Double) value / DoubleRelations.GradianInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Acot(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acot(this AngleRevolution value)
        {
            return MathUtilities.Acot((Double) value * DoubleRadian.Full);
        }

        /// <inheritdoc cref="MathUtilities.Acot2(Double,Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acot2(this AngleDegree value, Double point)
        {
            return MathUtilities.Acot2((Double) value / DoubleRelations.DegreeInRadian, point);
        }

        /// <inheritdoc cref="MathUtilities.Acot2(Double,Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acot2(this AngleDegree value, AngleDegree angle)
        {
            return MathUtilities.Acot2((Double) value / DoubleRelations.DegreeInRadian, (Double) angle / DoubleRelations.DegreeInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Acot2(Double,Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acot2(this AngleRadian value, Double point)
        {
            return MathUtilities.Acot2((Double) value, point);
        }

        /// <inheritdoc cref="MathUtilities.Acot2(Double,Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acot2(this AngleRadian value, AngleRadian angle)
        {
            return MathUtilities.Acot2((Double) value, (Double) angle);
        }

        /// <inheritdoc cref="MathUtilities.Acot2(Double,Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acot2(this AngleGradian value, Double point)
        {
            return MathUtilities.Acot2((Double) value / DoubleRelations.GradianInRadian, point);
        }

        /// <inheritdoc cref="MathUtilities.Acot2(Double,Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acot2(this AngleGradian value, AngleGradian angle)
        {
            return MathUtilities.Acot2((Double) value / DoubleRelations.GradianInRadian, (Double) angle / DoubleRelations.GradianInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Acot2(Double,Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acot2(this AngleRevolution value, Double point)
        {
            return MathUtilities.Acot2((Double) value * DoubleRadian.Full, point);
        }

        /// <inheritdoc cref="MathUtilities.Acot2(Double,Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acot2(this AngleRevolution value, AngleRevolution angle)
        {
            return MathUtilities.Acot2((Double) value * DoubleRadian.Full, (Double) angle * DoubleRadian.Full);
        }

        /// <inheritdoc cref="MathUtilities.Acoth(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acoth(this AngleDegree value)
        {
            return MathUtilities.Acoth((Double) value / DoubleRelations.DegreeInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Acoth(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acoth(this AngleRadian value)
        {
            return MathUtilities.Acoth((Double) value);
        }

        /// <inheritdoc cref="MathUtilities.Acoth(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acoth(this AngleGradian value)
        {
            return MathUtilities.Acoth((Double) value / DoubleRelations.GradianInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Acoth(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acoth(this AngleRevolution value)
        {
            return MathUtilities.Acoth((Double) value * DoubleRadian.Full);
        }

        /// <inheritdoc cref="MathUtilities.Sec(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sec(this AngleDegree value)
        {
            return MathUtilities.Sec((Double) value / DoubleRelations.DegreeInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Sec(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sec(this AngleRadian value)
        {
            return MathUtilities.Sec((Double) value);
        }

        /// <inheritdoc cref="MathUtilities.Sec(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sec(this AngleGradian value)
        {
            return MathUtilities.Sec((Double) value / DoubleRelations.GradianInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Sec(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sec(this AngleRevolution value)
        {
            return MathUtilities.Sec((Double) value * DoubleRadian.Full);
        }

        /// <inheritdoc cref="MathUtilities.Sech(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sech(this AngleDegree value)
        {
            return MathUtilities.Sech((Double) value / DoubleRelations.DegreeInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Sech(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sech(this AngleRadian value)
        {
            return MathUtilities.Sech((Double) value);
        }

        /// <inheritdoc cref="MathUtilities.Sech(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sech(this AngleGradian value)
        {
            return MathUtilities.Sech((Double) value / DoubleRelations.GradianInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Sech(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sech(this AngleRevolution value)
        {
            return MathUtilities.Sech((Double) value * DoubleRadian.Full);
        }

        /// <inheritdoc cref="MathUtilities.Asec(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Asec(this AngleDegree value)
        {
            return MathUtilities.Asec((Double) value / DoubleRelations.DegreeInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Asec(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Asec(this AngleRadian value)
        {
            return MathUtilities.Asec((Double) value);
        }

        /// <inheritdoc cref="MathUtilities.Asec(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Asec(this AngleGradian value)
        {
            return MathUtilities.Asec((Double) value / DoubleRelations.GradianInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Asec(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Asec(this AngleRevolution value)
        {
            return MathUtilities.Asec((Double) value * DoubleRadian.Full);
        }

        /// <inheritdoc cref="MathUtilities.Asech(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Asech(this AngleDegree value)
        {
            return MathUtilities.Asech((Double) value / DoubleRelations.DegreeInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Asech(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Asech(this AngleRadian value)
        {
            return MathUtilities.Asech((Double) value);
        }

        /// <inheritdoc cref="MathUtilities.Asech(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Asech(this AngleGradian value)
        {
            return MathUtilities.Asech((Double) value / DoubleRelations.GradianInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Asech(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Asech(this AngleRevolution value)
        {
            return MathUtilities.Asech((Double) value * DoubleRadian.Full);
        }

        /// <inheritdoc cref="MathUtilities.Csc(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Csc(this AngleDegree value)
        {
            return MathUtilities.Csc((Double) value / DoubleRelations.DegreeInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Csc(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Csc(this AngleRadian value)
        {
            return MathUtilities.Csc((Double) value);
        }

        /// <inheritdoc cref="MathUtilities.Csc(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Csc(this AngleGradian value)
        {
            return MathUtilities.Csc((Double) value / DoubleRelations.GradianInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Csc(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Csc(this AngleRevolution value)
        {
            return MathUtilities.Csc((Double) value * DoubleRadian.Full);
        }

        /// <inheritdoc cref="MathUtilities.Csch(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Csch(this AngleDegree value)
        {
            return MathUtilities.Csch((Double) value / DoubleRelations.DegreeInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Csch(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Csch(this AngleRadian value)
        {
            return MathUtilities.Csch((Double) value);
        }

        /// <inheritdoc cref="MathUtilities.Csch(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Csch(this AngleGradian value)
        {
            return MathUtilities.Csch((Double) value / DoubleRelations.GradianInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Csch(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Csch(this AngleRevolution value)
        {
            return MathUtilities.Csch((Double) value * DoubleRadian.Full);
        }

        /// <inheritdoc cref="MathUtilities.Acsc(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acsc(this AngleDegree value)
        {
            return MathUtilities.Acsc((Double) value / DoubleRelations.DegreeInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Acsc(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acsc(this AngleRadian value)
        {
            return MathUtilities.Acsc((Double) value);
        }

        /// <inheritdoc cref="MathUtilities.Acsc(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acsc(this AngleGradian value)
        {
            return MathUtilities.Acsc((Double) value / DoubleRelations.GradianInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Acsc(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acsc(this AngleRevolution value)
        {
            return MathUtilities.Acsc((Double) value * DoubleRadian.Full);
        }

        /// <inheritdoc cref="MathUtilities.Acsch(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acsch(this AngleDegree value)
        {
            return MathUtilities.Acsch((Double) value / DoubleRelations.DegreeInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Acsch(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acsch(this AngleRadian value)
        {
            return MathUtilities.Acsch((Double) value);
        }

        /// <inheritdoc cref="MathUtilities.Acsch(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acsch(this AngleGradian value)
        {
            return MathUtilities.Acsch((Double) value / DoubleRelations.GradianInRadian);
        }

        /// <inheritdoc cref="MathUtilities.Acsch(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acsch(this AngleRevolution value)
        {
            return MathUtilities.Acsch((Double) value * DoubleRadian.Full);
        }

        public static Double Trigonometry(this AngleDegree value, TrigonometryType type)
        {
            return type switch
            {
                TrigonometryType.Sin => Sin(value),
                TrigonometryType.Sinh => Sinh(value),
                TrigonometryType.Asin => Asin(value),
                TrigonometryType.Asinh => Asinh(value),
                TrigonometryType.Cos => Cos(value),
                TrigonometryType.Cosh => Cosh(value),
                TrigonometryType.Acos => Acos(value),
                TrigonometryType.Acosh => Acosh(value),
                TrigonometryType.Tan => Tan(value),
                TrigonometryType.Tanh => Tanh(value),
                TrigonometryType.Atan => Atan(value),
                TrigonometryType.Atanh => Atanh(value),
                TrigonometryType.Cot => Cot(value),
                TrigonometryType.Coth => Coth(value),
                TrigonometryType.Acot => Acot(value),
                TrigonometryType.Acoth => Acoth(value),
                TrigonometryType.Sec => Sec(value),
                TrigonometryType.Sech => Sech(value),
                TrigonometryType.Asec => Asec(value),
                TrigonometryType.Asech => Asech(value),
                TrigonometryType.Csc => Csc(value),
                TrigonometryType.Csch => Csch(value),
                TrigonometryType.Acsc => Acsc(value),
                TrigonometryType.Acsch => Acsch(value),
                _ => throw new EnumUndefinedOrNotSupportedException<TrigonometryType>(type, nameof(type), null)
            };
        }

        public static Double Trigonometry(this AngleRadian value, TrigonometryType type)
        {
            return type switch
            {
                TrigonometryType.Sin => Sin(value),
                TrigonometryType.Sinh => Sinh(value),
                TrigonometryType.Asin => Asin(value),
                TrigonometryType.Asinh => Asinh(value),
                TrigonometryType.Cos => Cos(value),
                TrigonometryType.Cosh => Cosh(value),
                TrigonometryType.Acos => Acos(value),
                TrigonometryType.Acosh => Acosh(value),
                TrigonometryType.Tan => Tan(value),
                TrigonometryType.Tanh => Tanh(value),
                TrigonometryType.Atan => Atan(value),
                TrigonometryType.Atanh => Atanh(value),
                TrigonometryType.Cot => Cot(value),
                TrigonometryType.Coth => Coth(value),
                TrigonometryType.Acot => Acot(value),
                TrigonometryType.Acoth => Acoth(value),
                TrigonometryType.Sec => Sec(value),
                TrigonometryType.Sech => Sech(value),
                TrigonometryType.Asec => Asec(value),
                TrigonometryType.Asech => Asech(value),
                TrigonometryType.Csc => Csc(value),
                TrigonometryType.Csch => Csch(value),
                TrigonometryType.Acsc => Acsc(value),
                TrigonometryType.Acsch => Acsch(value),
                _ => throw new EnumUndefinedOrNotSupportedException<TrigonometryType>(type, nameof(type), null)
            };
        }

        public static Double Trigonometry(this AngleGradian value, TrigonometryType type)
        {
            return type switch
            {
                TrigonometryType.Sin => Sin(value),
                TrigonometryType.Sinh => Sinh(value),
                TrigonometryType.Asin => Asin(value),
                TrigonometryType.Asinh => Asinh(value),
                TrigonometryType.Cos => Cos(value),
                TrigonometryType.Cosh => Cosh(value),
                TrigonometryType.Acos => Acos(value),
                TrigonometryType.Acosh => Acosh(value),
                TrigonometryType.Tan => Tan(value),
                TrigonometryType.Tanh => Tanh(value),
                TrigonometryType.Atan => Atan(value),
                TrigonometryType.Atanh => Atanh(value),
                TrigonometryType.Cot => Cot(value),
                TrigonometryType.Coth => Coth(value),
                TrigonometryType.Acot => Acot(value),
                TrigonometryType.Acoth => Acoth(value),
                TrigonometryType.Sec => Sec(value),
                TrigonometryType.Sech => Sech(value),
                TrigonometryType.Asec => Asec(value),
                TrigonometryType.Asech => Asech(value),
                TrigonometryType.Csc => Csc(value),
                TrigonometryType.Csch => Csch(value),
                TrigonometryType.Acsc => Acsc(value),
                TrigonometryType.Acsch => Acsch(value),
                _ => throw new EnumUndefinedOrNotSupportedException<TrigonometryType>(type, nameof(type), null)
            };
        }

        public static Double Trigonometry(this AngleRevolution value, TrigonometryType type)
        {
            return type switch
            {
                TrigonometryType.Sin => Sin(value),
                TrigonometryType.Sinh => Sinh(value),
                TrigonometryType.Asin => Asin(value),
                TrigonometryType.Asinh => Asinh(value),
                TrigonometryType.Cos => Cos(value),
                TrigonometryType.Cosh => Cosh(value),
                TrigonometryType.Acos => Acos(value),
                TrigonometryType.Acosh => Acosh(value),
                TrigonometryType.Tan => Tan(value),
                TrigonometryType.Tanh => Tanh(value),
                TrigonometryType.Atan => Atan(value),
                TrigonometryType.Atanh => Atanh(value),
                TrigonometryType.Cot => Cot(value),
                TrigonometryType.Coth => Coth(value),
                TrigonometryType.Acot => Acot(value),
                TrigonometryType.Acoth => Acoth(value),
                TrigonometryType.Sec => Sec(value),
                TrigonometryType.Sech => Sech(value),
                TrigonometryType.Asec => Asec(value),
                TrigonometryType.Asech => Asech(value),
                TrigonometryType.Csc => Csc(value),
                TrigonometryType.Csch => Csch(value),
                TrigonometryType.Acsc => Acsc(value),
                TrigonometryType.Acsch => Acsch(value),
                _ => throw new EnumUndefinedOrNotSupportedException<TrigonometryType>(type, nameof(type), null)
            };
        }
    }
}