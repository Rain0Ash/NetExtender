// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.Types.Numerics;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Numerics
{
    public static partial class LogicUtilities
    {
        /// <summary>
        /// Return EQ (==)
        /// </summary>
        /// <param name="first">First trilean</param>
        /// <param name="second">Second trilean</param>
        /// <returns><see cref="first"/> == <see cref="second"/></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Eq(Trilean first, Trilean second)
        {
            return first == second;
        }

        /// <summary>
        /// Return EQ (==)
        /// </summary>
        /// <param name="first">First trilean</param>
        /// <param name="second">Second trilean</param>
        /// <param name="third">Third trilean</param>
        /// <returns><see cref="first"/> == <see cref="second"/> == <see cref="third"/></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Eq(Trilean first, Trilean second, Trilean third)
        {
            return first == second && second == third;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Eq(Trilean first, Trilean second, Trilean third, params Trilean[] values)
        {
            return values.Prepend(second, third).All(trilean => trilean == first);
        }

        public static Trilean Eq(ReadOnlySpan<Trilean> values)
        {
            return values.Length switch
            {
                0 => throw new IndexOutOfRangeException(),
                1 => true,
                2 => Eq(values[0], values[1]),
                3 => Eq(values[0], values[1], values[2]),
                _ => values.AllSame()
            };
        }

        /// <summary>
        /// Return AND (&&)
        /// </summary>
        /// <param name="first">First trilean</param>
        /// <param name="second">Second trilean</param>
        /// <returns><see cref="first"/> && <see cref="second"/></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean And(Trilean first, Trilean second)
        {
            return first & second;
        }

        /// <summary>
        /// Return AND (&&)
        /// </summary>
        /// <param name="first">First trilean</param>
        /// <param name="second">Second trilean</param>
        /// <param name="third">Third trilean</param>
        /// <returns><see cref="first"/> && <see cref="second"/> && <see cref="third"/></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean And(Trilean first, Trilean second, Trilean third)
        {
            return first & second & third;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean And(Trilean first, Trilean second, Trilean third, params Trilean[] values)
        {
            return values.Prepend(first, second, third).All(trilean => trilean);
        }

        public static Trilean And(ReadOnlySpan<Trilean> values)
        {
            return values.Length switch
            {
                0 => throw new IndexOutOfRangeException(),
                1 => values[0],
                2 => And(values[0], values[1]),
                3 => And(values[0], values[1], values[2]),
                _ => values.All(trilean => trilean)
            };
        }

        /// <summary>
        /// Return OR (||)
        /// </summary>
        /// <param name="first">First trilean</param>
        /// <param name="second">Second trilean</param>
        /// <returns><see cref="first"/> || <see cref="second"/></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Or(Trilean first, Trilean second)
        {
            return first | second;
        }

        /// <summary>
        /// Return OR (||)
        /// </summary>
        /// <param name="first">First trilean</param>
        /// <param name="second">Second trilean</param>
        /// <param name="third">Third trilean</param>
        /// <returns><see cref="first"/> || <see cref="second"/> || <see cref="third"/></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Or(Trilean first, Trilean second, Trilean third)
        {
            return first | second | third;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Or(Trilean first, Trilean second, Trilean third, params Trilean[] values)
        {
            return values.Prepend(first, second, third).Any(trilean => trilean);
        }

        public static Trilean Or(ReadOnlySpan<Trilean> values)
        {
            return values.Length switch
            {
                0 => throw new IndexOutOfRangeException(),
                1 => values[0],
                2 => Or(values[0], values[1]),
                3 => Or(values[0], values[1], values[2]),
                _ => values.Any(trilean => trilean)
            };
        }

        /// <summary>
        /// Return XOR (^)
        /// </summary>
        /// <param name="first">First trilean</param>
        /// <param name="second">Second trilean</param>
        /// <returns><see cref="first"/> ^ <see cref="second"/></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Xor(Trilean first, Trilean second)
        {
            return first ^ second;
        }

        /// <summary>
        /// Return XOR (^)
        /// </summary>
        /// <param name="first">First trilean</param>
        /// <param name="second">Second trilean</param>
        /// <param name="third">Third trilean</param>
        /// <returns><see cref="first"/> ^ <see cref="second"/> ^ <see cref="third"/></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Xor(Trilean first, Trilean second, Trilean third)
        {
            return first ^ second ^ third;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Xor(Trilean first, Trilean second, Trilean third, params Trilean[] values)
        {
            return values.Prepend(first, second, third).Aggregate((current, value) => current ^ value);
        }

        public static Trilean Xor(ReadOnlySpan<Trilean> values)
        {
            return values.Length switch
            {
                0 => throw new IndexOutOfRangeException(),
                1 => false,
                2 => Xor(values[0], values[1]),
                3 => Xor(values[0], values[1], values[2]),
                _ => values.Aggregate((current, value) => current ^ value)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Impl(Trilean first, Trilean second)
        {
            return !first | second;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Impl(Trilean first, Trilean second, Trilean third)
        {
            return Impl(Impl(first, second), third);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Impl(Trilean first, Trilean second, Trilean third, params Trilean[] values)
        {
            return values.Prepend(first, second, third).Aggregate(Impl);
        }

        public static Trilean Impl(ReadOnlySpan<Trilean> values)
        {
            return values.Length switch
            {
                0 => throw new IndexOutOfRangeException(),
                1 => true,
                2 => Impl(values[0], values[1]),
                3 => Impl(values[0], values[1], values[2]),
                _ => values.Aggregate(Impl)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Rimpl(Trilean first, Trilean second)
        {
            return first | !second;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Rimpl(Trilean first, Trilean second, Trilean third)
        {
            return Rimpl(Rimpl(first, second), third);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Rimpl(Trilean first, Trilean second, Trilean third, params Trilean[] values)
        {
            return values.Prepend(first, second, third).Aggregate(Rimpl);
        }

        public static Trilean Rimpl(ReadOnlySpan<Trilean> values)
        {
            return values.Length switch
            {
                0 => throw new IndexOutOfRangeException(),
                1 => true,
                2 => Rimpl(values[0], values[1]),
                3 => Rimpl(values[0], values[1], values[2]),
                _ => values.Aggregate(Rimpl)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Neq(Trilean first, Trilean second)
        {
            return !Eq(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Neq(Trilean first, Trilean second, Trilean third)
        {
            return !Eq(first, second, third);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Neq(Trilean first, Trilean second, Trilean third, params Trilean[] values)
        {
            return !Eq(first, second, third, values);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Neq(ReadOnlySpan<Trilean> values)
        {
            return !Eq(values);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Nand(Trilean first, Trilean second)
        {
            return !And(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Nand(Trilean first, Trilean second, Trilean third)
        {
            return !And(first, second, third);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Nand(Trilean first, Trilean second, Trilean third, params Trilean[] values)
        {
            return !And(first, second, third, values);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Nand(ReadOnlySpan<Trilean> values)
        {
            return !And(values);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Nor(Trilean first, Trilean second)
        {
            return !Or(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Nor(Trilean first, Trilean second, Trilean third)
        {
            return !Or(first, second, third);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Nor(Trilean first, Trilean second, Trilean third, params Trilean[] values)
        {
            return !Or(first, second, third, values);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Nor(ReadOnlySpan<Trilean> values)
        {
            return !Or(values);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Xnor(Trilean first, Trilean second)
        {
            return !Xor(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Xnor(Trilean first, Trilean second, Trilean third)
        {
            return !Xor(first, second, third);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Xnor(Trilean first, Trilean second, Trilean third, params Trilean[] values)
        {
            return !Xor(first, second, third, values);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Xnor(ReadOnlySpan<Trilean> values)
        {
            return !Xor(values);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Nimpl(Trilean first, Trilean second)
        {
            return !Impl(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Nimpl(Trilean first, Trilean second, Trilean third)
        {
            return !Impl(first, second, third);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Nimpl(Trilean first, Trilean second, Trilean third, params Trilean[] values)
        {
            return !Impl(first, second, third, values);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Nimpl(ReadOnlySpan<Trilean> values)
        {
            return !Impl(values);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Nrimpl(Trilean first, Trilean second)
        {
            return !Rimpl(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Nrimpl(Trilean first, Trilean second, Trilean third)
        {
            return !Rimpl(first, second, third);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Nrimpl(Trilean first, Trilean second, Trilean third, params Trilean[] values)
        {
            return !Rimpl(first, second, third, values);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Nrimpl(ReadOnlySpan<Trilean> values)
        {
            return !Rimpl(values);
        }
    }
}