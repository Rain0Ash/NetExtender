// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NetExtender.Types.Numerics;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Numerics
{
    [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
    public static partial class LogicUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsTrue(this Boolean? value)
        {
            return value is true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotTrue(this Boolean? value)
        {
            return value is not true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsFalse(this Boolean? value)
        {
            return value is false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotFalse(this Boolean? value)
        {
            return value is not false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNull(this Boolean? value)
        {
            return value is null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotNull(this Boolean? value)
        {
            return value is not null;
        }

        /// <summary>
        /// Return NOT (!)
        /// </summary>
        /// <param name="value">Boolean</param>
        /// <returns>!<see cref="value"/></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Not(Boolean value)
        {
            return !value;
        }

        /// <summary>
        /// Return NOT (!)
        /// </summary>
        /// <param name="value">Boolean?</param>
        /// <returns>!<see cref="value"/></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean? Not(Boolean? value)
        {
            return !value;
        }

        /// <summary>
        /// Return NOT (!)
        /// </summary>
        /// <param name="value">Trilean</param>
        /// <returns>!<see cref="value"/></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Trilean Not(Trilean value)
        {
            return !value;
        }

        /// <summary>
        /// Return NOT (!)
        /// </summary>
        /// <param name="value">Trilean</param>
        /// <returns>!<see cref="value"/></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Not<T>(T value)
        {
            return Not(value.ToBoolean());
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
        /// <returns><see cref="first"/> == <see cref="second"/></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Eq<T>(T first, T second)
        {
            return Eq(first.ToBoolean(), second.ToBoolean());
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
        public static Boolean Eq(Boolean first, Boolean second, Boolean third, params Boolean[]? values)
        {
            if (second != first || third != first)
            {
                return false;
            }

            if (values is null)
            {
                return true;
            }

            foreach (Boolean value in values)
            {
                if (value != first)
                {
                    return false;
                }
            }

            return true;
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
        /// <returns><see cref="first"/> && <see cref="second"/></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean And<T>(T first, T second)
        {
            return And(first.ToBoolean(), second.ToBoolean());
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
        public static Boolean And(Boolean first, Boolean second, Boolean third, params Boolean[]? values)
        {
            if (!first || !second || !third)
            {
                return false;
            }

            if (values is null)
            {
                return true;
            }

            foreach (Boolean value in values)
            {
                if (!value)
                {
                    return false;
                }
            }

            return true;
        }

        public static Boolean And(ReadOnlySpan<Boolean> values)
        {
            return values.Length switch
            {
                0 => throw new IndexOutOfRangeException(),
                1 => values[0],
                2 => And(values[0], values[1]),
                3 => And(values[0], values[1], values[2]),
                _ => values.All(static boolean => boolean)
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
        /// <returns><see cref="first"/> || <see cref="second"/></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Or<T>(T first, T second)
        {
            return Or(first.ToBoolean(), second.ToBoolean());
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
        public static Boolean Or(Boolean first, Boolean second, Boolean third, params Boolean[]? values)
        {
            if (first || second || third)
            {
                return true;
            }

            if (values is null)
            {
                return false;
            }

            foreach (Boolean value in values)
            {
                if (value)
                {
                    return true;
                }
            }

            return false;
        }

        public static Boolean Or(ReadOnlySpan<Boolean> values)
        {
            return values.Length switch
            {
                0 => throw new IndexOutOfRangeException(),
                1 => values[0],
                2 => Or(values[0], values[1]),
                3 => Or(values[0], values[1], values[2]),
                _ => values.Any(static boolean => boolean)
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
        /// <returns><see cref="first"/> ^ <see cref="second"/></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Xor<T>(T first, T second)
        {
            return Xor(first.ToBoolean(), second.ToBoolean());
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
        public static Boolean Xor(Boolean first, Boolean second, Boolean third, params Boolean[]? values)
        {
            Boolean result = first ^ second ^ third;

            if (values is null)
            {
                return result;
            }

            foreach (Boolean value in values)
            {
                result ^= value;
            }

            return result;
        }

        public static Boolean Xor(ReadOnlySpan<Boolean> values)
        {
            return values.Length switch
            {
                0 => throw new IndexOutOfRangeException(),
                1 => false,
                2 => Xor(values[0], values[1]),
                3 => Xor(values[0], values[1], values[2]),
                _ => values.Aggregate(static (current, value) => current ^ value)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Impl(Boolean first, Boolean second)
        {
            return !first || second;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Impl<T>(T first, T second)
        {
            return Impl(first.ToBoolean(), second.ToBoolean());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Impl(Boolean first, Boolean second, Boolean third)
        {
            return Impl(Impl(first, second), third);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Impl(Boolean first, Boolean second, Boolean third, params Boolean[]? values)
        {
            Boolean current = !first || second;
            current = !current || third;

            if (values is null)
            {
                return current;
            }

            foreach (Boolean value in values)
            {
                current = !current || value;
            }

            return current;
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
        public static Boolean Rimpl<T>(T first, T second)
        {
            return Rimpl(first.ToBoolean(), second.ToBoolean());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Rimpl(Boolean first, Boolean second, Boolean third)
        {
            return Rimpl(Rimpl(first, second), third);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Rimpl(Boolean first, Boolean second, Boolean third, params Boolean[]? values)
        {
            Boolean current = first || !second;
            current = current || !third;

            if (values is null)
            {
                return current;
            }

            foreach (Boolean value in values)
            {
                current = current || !value;
            }

            return current;
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
        public static Boolean Neq<T>(T first, T second)
        {
            return Neq(first.ToBoolean(), second.ToBoolean());
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
        public static Boolean Nand<T>(T first, T second)
        {
            return Nand(first.ToBoolean(), second.ToBoolean());
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
        public static Boolean Nor<T>(T first, T second)
        {
            return Nor(first.ToBoolean(), second.ToBoolean());
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
        public static Boolean Xnor<T>(T first, T second)
        {
            return Xnor(first.ToBoolean(), second.ToBoolean());
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
        public static Boolean Nimpl<T>(T first, T second)
        {
            return Nimpl(first.ToBoolean(), second.ToBoolean());
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
        public static Boolean Nrimpl<T>(T first, T second)
        {
            return Nrimpl(first.ToBoolean(), second.ToBoolean());
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
        public static Trilean Eq(Trilean first, Trilean second, Trilean third, params Trilean[]? values)
        {
            if (second != first || third != first)
            {
                return false;
            }

            if (values is null)
            {
                return true;
            }

            foreach (Trilean value in values)
            {
                if (value != first)
                {
                    return false;
                }
            }

            return true;
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
        public static Trilean And(Trilean first, Trilean second, Trilean third, params Trilean[]? values)
        {
            if (!first)
            {
                return false;
            }

            if (!second)
            {
                return false;
            }

            if (!third)
            {
                return false;
            }

            if (values is null)
            {
                return true;
            }

            foreach (Trilean value in values)
            {
                if (!value)
                {
                    return false;
                }
            }

            return true;
        }

        public static Trilean And(ReadOnlySpan<Trilean> values)
        {
            return values.Length switch
            {
                0 => throw new IndexOutOfRangeException(),
                1 => values[0],
                2 => And(values[0], values[1]),
                3 => And(values[0], values[1], values[2]),
                _ => values.All(static trilean => trilean)
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
        public static Trilean Or(Trilean first, Trilean second, Trilean third, params Trilean[]? values)
        {
            if (first)
            {
                return true;
            }

            if (second)
            {
                return true;
            }

            if (third)
            {
                return true;
            }

            if (values is null)
            {
                return false;
            }

            foreach (Trilean value in values)
            {
                if (value)
                {
                    return true;
                }
            }

            return false;
        }

        public static Trilean Or(ReadOnlySpan<Trilean> values)
        {
            return values.Length switch
            {
                0 => throw new IndexOutOfRangeException(),
                1 => values[0],
                2 => Or(values[0], values[1]),
                3 => Or(values[0], values[1], values[2]),
                _ => values.Any(static trilean => trilean)
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
        public static Trilean Xor(Trilean first, Trilean second, Trilean third, params Trilean[]? values)
        {
            Trilean result = first ^ second ^ third;

            if (values is null)
            {
                return result;
            }

            foreach (Trilean value in values)
            {
                result ^= value;
            }

            return result;
        }

        public static Trilean Xor(ReadOnlySpan<Trilean> values)
        {
            return values.Length switch
            {
                0 => throw new IndexOutOfRangeException(),
                1 => false,
                2 => Xor(values[0], values[1]),
                3 => Xor(values[0], values[1], values[2]),
                _ => values.Aggregate(static (current, value) => current ^ value)
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
        public static Trilean Impl(Trilean first, Trilean second, Trilean third, params Trilean[]? values)
        {
            Trilean result = Impl(first, second, third);

            if (values is null)
            {
                return result;
            }

            foreach (Trilean value in values)
            {
                result = Impl(result, value);
            }

            return result;
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
        public static Trilean Rimpl(Trilean first, Trilean second, Trilean third, params Trilean[]? values)
        {
            Trilean result = Rimpl(first, second, third);

            if (values is null)
            {
                return result;
            }

            foreach (Trilean value in values)
            {
                result = Rimpl(result, value);
            }

            return result;
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