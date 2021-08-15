// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Numerics
{
    public static partial class LogicUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsTrue(this Boolean? value)
        {
            return value.HasValue && value.Value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotTrue(this Boolean? value)
        {
            return !value.HasValue || !value.Value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsFalse(this Boolean? value)
        {
            return value.HasValue && !value.Value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotFalse(this Boolean? value)
        {
            return !value.HasValue || value.Value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNull(this Boolean? value)
        {
            return !value.HasValue;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotNull(this Boolean? value)
        {
            return value.HasValue;
        }
        
        /// <summary>
        /// Return EQ (==)
        /// </summary>
        /// <param name="first">First boolean</param>
        /// <param name="second">Second boolean</param>
        /// <returns><see cref="first"/> == <see cref="second"/></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Eq(Boolean first, Boolean second)
        {
            return first == second;
        }

        /// <summary>
        /// Return EQ (==)
        /// </summary>
        /// <param name="first">First boolean</param>
        /// <param name="second">Second boolean</param>
        /// <param name="third">Third boolean</param>
        /// <returns><see cref="first"/> == <see cref="second"/> == <see cref="third"/></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Eq(Boolean first, Boolean second, Boolean third)
        {
            return first == second == third;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Eq(Boolean first, Boolean second, Boolean third, params Boolean[] values)
        {
            return values.Prepend(second, third).All(boolean => boolean == first);
        }

        public static Boolean Eq(ReadOnlySpan<Boolean> values)
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
        /// <param name="first">First boolean</param>
        /// <param name="second">Second boolean</param>
        /// <returns><see cref="first"/> && <see cref="second"/></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean And(Boolean first, Boolean second)
        {
            return first && second;
        }

        /// <summary>
        /// Return AND (&&)
        /// </summary>
        /// <param name="first">First boolean</param>
        /// <param name="second">Second boolean</param>
        /// <param name="third">Third boolean</param>
        /// <returns><see cref="first"/> && <see cref="second"/> && <see cref="third"/></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean And(Boolean first, Boolean second, Boolean third)
        {
            return first && second && third;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean And(Boolean first, Boolean second, Boolean third, params Boolean[] values)
        {
            return values.Prepend(first, second, third).All(boolean => boolean);
        }
        
        public static Boolean And(ReadOnlySpan<Boolean> values)
        {
            return values.Length switch
            {
                0 => throw new IndexOutOfRangeException(),
                1 => values[0],
                2 => And(values[0], values[1]),
                3 => And(values[0], values[1], values[2]),
                _ => values.All(boolean => boolean)
            };
        }

        /// <summary>
        /// Return OR (||)
        /// </summary>
        /// <param name="first">First boolean</param>
        /// <param name="second">Second boolean</param>
        /// <returns><see cref="first"/> || <see cref="second"/></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Or(Boolean first, Boolean second)
        {
            return first || second;
        }

        /// <summary>
        /// Return OR (||)
        /// </summary>
        /// <param name="first">First boolean</param>
        /// <param name="second">Second boolean</param>
        /// <param name="third">Third boolean</param>
        /// <returns><see cref="first"/> || <see cref="second"/> || <see cref="third"/></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Or(Boolean first, Boolean second, Boolean third)
        {
            return first || second || third;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Or(Boolean first, Boolean second, Boolean third, params Boolean[] values)
        {
            return values.Prepend(first, second, third).Any(boolean => boolean);
        }
        
        public static Boolean Or(ReadOnlySpan<Boolean> values)
        {
            return values.Length switch
            {
                0 => throw new IndexOutOfRangeException(),
                1 => values[0],
                2 => Or(values[0], values[1]),
                3 => Or(values[0], values[1], values[2]),
                _ => values.Any(boolean => boolean)
            };
        }

        /// <summary>
        /// Return XOR (^)
        /// </summary>
        /// <param name="first">First boolean</param>
        /// <param name="second">Second boolean</param>
        /// <returns><see cref="first"/> ^ <see cref="second"/></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Xor(Boolean first, Boolean second)
        {
            return first ^ second;
        }

        /// <summary>
        /// Return XOR (^)
        /// </summary>
        /// <param name="first">First boolean</param>
        /// <param name="second">Second boolean</param>
        /// <param name="third">Third boolean</param>
        /// <returns><see cref="first"/> ^ <see cref="second"/> ^ <see cref="third"/></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Xor(Boolean first, Boolean second, Boolean third)
        {
            return first ^ second ^ third;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Xor(Boolean first, Boolean second, Boolean third, params Boolean[] values)
        {
            return values.Prepend(first, second, third).Aggregate((current, value) => current ^ value);
        }
        
        public static Boolean Xor(ReadOnlySpan<Boolean>  values)
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
        public static Boolean Impl(Boolean first, Boolean second)
        {
            return !first || second;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Impl(Boolean first, Boolean second, Boolean third)
        {
            return Impl(Impl(first, second), third);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Impl(Boolean first, Boolean second, Boolean third, params Boolean[] values)
        {
            return values.Prepend(first, second, third).Aggregate(Impl);
        }

        public static Boolean Impl(ReadOnlySpan<Boolean> values)
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
        public static Boolean Rimpl(Boolean first, Boolean second)
        {
            return first || !second;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Rimpl(Boolean first, Boolean second, Boolean third)
        {
            return Rimpl(Rimpl(first, second), third);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Rimpl(Boolean first, Boolean second, Boolean third, params Boolean[] values)
        {
            return values.Prepend(first, second, third).Aggregate(Rimpl);
        }
        
        public static Boolean Rimpl(ReadOnlySpan<Boolean> values)
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
        public static Boolean Neq(Boolean first, Boolean second)
        {
            return !Eq(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Neq(Boolean first, Boolean second, Boolean third)
        {
            return !Eq(first, second, third);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Neq(Boolean first, Boolean second, Boolean third, params Boolean[] values)
        {
            return !Eq(first, second, third, values);
        }
        
        public static Boolean Neq(ReadOnlySpan<Boolean> values)
        {
            return !Eq(values);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Nand(Boolean first, Boolean second)
        {
            return !And(first, second);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Nand(Boolean first, Boolean second, Boolean third)
        {
            return !And(first, second, third);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Nand(Boolean first, Boolean second, Boolean third, params Boolean[] values)
        {
            return !And(first, second, third, values);
        }
        
        public static Boolean Nand(ReadOnlySpan<Boolean> values)
        {
            return !And(values);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Nor(Boolean first, Boolean second)
        {
            return !Or(first, second);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Nor(Boolean first, Boolean second, Boolean third)
        {
            return !Or(first, second, third);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Nor(Boolean first, Boolean second, Boolean third, params Boolean[] values)
        {
            return !Or(first, second, third, values);
        }
        
        public static Boolean Nor(ReadOnlySpan<Boolean> values)
        {
            return !Or(values);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Xnor(Boolean first, Boolean second)
        {
            return !Xor(first, second);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Xnor(Boolean first, Boolean second, Boolean third)
        {
            return !Xor(first, second, third);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Xnor(Boolean first, Boolean second, Boolean third, params Boolean[] values)
        {
            return !Xor(first, second, third, values);
        }
        
        public static Boolean Xnor(ReadOnlySpan<Boolean> values)
        {
            return !Xor(values);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Nimpl(Boolean first, Boolean second)
        {
            return !Impl(first, second);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Nimpl(Boolean first, Boolean second, Boolean third)
        {
            return !Impl(first, second, third);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Nimpl(Boolean first, Boolean second, Boolean third, params Boolean[] values)
        {
            return !Impl(first, second, third, values);
        }
        
        public static Boolean Nimpl(ReadOnlySpan<Boolean> values)
        {
            return !Impl(values);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Nrimpl(Boolean first, Boolean second)
        {
            return !Rimpl(first, second);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Nrimpl(Boolean first, Boolean second, Boolean third)
        {
            return !Rimpl(first, second, third);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Nrimpl(Boolean first, Boolean second, Boolean third, params Boolean[] values)
        {
            return !Rimpl(first, second, third, values);
        }
        
        public static Boolean Nrimpl(ReadOnlySpan<Boolean> values)
        {
            return !Rimpl(values);
        }
    }
}