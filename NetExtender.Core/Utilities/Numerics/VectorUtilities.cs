// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Numerics
{
    public static class VectorUtilities
    {
        /// <inheritdoc cref="Vector.IsHardwareAccelerated"/>
        public static Boolean IsHardwareAccelerated
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Vector.IsHardwareAccelerated;
            }
        }
        
        public static Vector<T> Create<T>(params T[] items) where T : struct
        {
            return new Vector<T>(items);
        }

        /// <inheritdoc cref="Vector.Abs{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> Abs<T>(this Vector<T> value) where T : struct
        {
            return Vector.Abs(value);
        }

        /// <inheritdoc cref="Vector.Add{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> Add<T>(this Vector<T> first, Vector<T> second) where T : struct
        {
            return Vector.Add(first, second);
        }

        /// <inheritdoc cref="Vector.Divide{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> Divide<T>(this Vector<T> first, Vector<T> second) where T : struct
        {
            return Vector.Divide(first, second);
        }

        /// <inheritdoc cref="Vector.Dot{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Dot<T>(this Vector<T> first, Vector<T> second) where T : struct
        {
            return Vector.Dot(first, second);
        }

        /// <inheritdoc cref="Vector.Equals{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> Equals<T>(this Vector<T> first, Vector<T> second) where T : struct
        {
            return Vector.Equals(first, second);
        }

        /// <inheritdoc cref="Vector.Equals{Int32}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<Int32> Equals(this Vector<Int32> first, Vector<Int32> second)
        {
            return Vector.Equals(first, second);
        }

        /// <inheritdoc cref="Vector.Equals{Int64}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<Int64> Equals(this Vector<Int64> first, Vector<Int64> second)
        {
            return Vector.Equals(first, second);
        }

        /// <inheritdoc cref="Vector.Equals{Single}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<Int32> Equals(this Vector<Single> first, Vector<Single> second)
        {
            return Vector.Equals(first, second);
        }

        /// <inheritdoc cref="Vector.Equals{Double}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<Int64> Equals(this Vector<Double> first, Vector<Double> second)
        {
            return Vector.Equals(first, second);
        }

        /// <inheritdoc cref="Vector.Max{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> Max<T>(this Vector<T> first, Vector<T> second) where T : struct
        {
            return Vector.Max(first, second);
        }

        /// <inheritdoc cref="Vector.Min{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> Min<T>(this Vector<T> first, Vector<T> second) where T : struct
        {
            return Vector.Min(first, second);
        }

        /// <inheritdoc cref="Vector.Multiply{T}(T,System.Numerics.Vector{T})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> Multiply<T>(T multiplier, Vector<T> value) where T : struct
        {
            return Vector.Multiply(multiplier, value);
        }

        /// <inheritdoc cref="Vector.Multiply{T}(System.Numerics.Vector{T},T)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> Multiply<T>(this Vector<T> value, T multiplier) where T : struct
        {
            return Vector.Multiply(value, multiplier);
        }

        /// <inheritdoc cref="Vector.Multiply{T}(System.Numerics.Vector{T},System.Numerics.Vector{T})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> Multiply<T>(this Vector<T> first, Vector<T> second) where T : struct
        {
            return Vector.Multiply(first, second);
        }

        /// <inheritdoc cref="Vector.Narrow(System.Numerics.Vector{Int16},System.Numerics.Vector{Int16})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<SByte> Narrow(this Vector<Int16> first, Vector<Int16> second)
        {
            return Vector.Narrow(first, second);
        }

        /// <inheritdoc cref="Vector.Narrow(System.Numerics.Vector{Int32},System.Numerics.Vector{Int32})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<Int16> Narrow(this Vector<Int32> first, Vector<Int32> second)
        {
            return Vector.Narrow(first, second);
        }

        /// <inheritdoc cref="Vector.Narrow(System.Numerics.Vector{Int64},System.Numerics.Vector{Int64})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<Int32> Narrow(this Vector<Int64> first, Vector<Int64> second)
        {
            return Vector.Narrow(first, second);
        }

        /// <inheritdoc cref="Vector.Narrow(System.Numerics.Vector{UInt16},System.Numerics.Vector{UInt16})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<Byte> Narrow(this Vector<UInt16> first, Vector<UInt16> second)
        {
            return Vector.Narrow(first, second);
        }

        /// <inheritdoc cref="Vector.Narrow(System.Numerics.Vector{UInt32},System.Numerics.Vector{UInt32})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<UInt16> Narrow(this Vector<UInt32> first, Vector<UInt32> second)
        {
            return Vector.Narrow(first, second);
        }

        /// <inheritdoc cref="Vector.Narrow(System.Numerics.Vector{UInt64},System.Numerics.Vector{UInt64})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<UInt32> Narrow(this Vector<UInt64> first, Vector<UInt64> second)
        {
            return Vector.Narrow(first, second);
        }

        /// <inheritdoc cref="Vector.Narrow(System.Numerics.Vector{Double},System.Numerics.Vector{Double})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<Single> Narrow(this Vector<Double> first, Vector<Double> second)
        {
            return Vector.Narrow(first, second);
        }

        /// <inheritdoc cref="Vector.Negate{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> Negate<T>(this Vector<T> value) where T : struct
        {
            return Vector.Negate(value);
        }

        /// <inheritdoc cref="Vector.Subtract{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> Substract<T>(this Vector<T> first, Vector<T> second) where T : struct
        {
            return Vector.Subtract(first, second);
        }

        /// <inheritdoc cref="Vector.Widen(System.Numerics.Vector{SByte},out System.Numerics.Vector{Int16},out System.Numerics.Vector{Int16})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Widen(this Vector<SByte> value, out Vector<Int16> first, out Vector<Int16> second)
        {
            Vector.Widen(value, out first, out second);
        }

        /// <inheritdoc cref="Vector.Widen(System.Numerics.Vector{Byte},out System.Numerics.Vector{UInt16},out System.Numerics.Vector{UInt16})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Widen(this Vector<Byte> value, out Vector<UInt16> first, out Vector<UInt16> second)
        {
            Vector.Widen(value, out first, out second);
        }

        /// <inheritdoc cref="Vector.Widen(System.Numerics.Vector{Int16},out System.Numerics.Vector{Int32},out System.Numerics.Vector{Int32})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Widen(this Vector<Int16> value, out Vector<Int32> first, out Vector<Int32> second)
        {
            Vector.Widen(value, out first, out second);
        }

        /// <inheritdoc cref="Vector.Widen(System.Numerics.Vector{UInt16},out System.Numerics.Vector{UInt32},out System.Numerics.Vector{UInt32})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Widen(this Vector<UInt16> value, out Vector<UInt32> first, out Vector<UInt32> second)
        {
            Vector.Widen(value, out first, out second);
        }

        /// <inheritdoc cref="Vector.Widen(System.Numerics.Vector{Int32},out System.Numerics.Vector{Int64},out System.Numerics.Vector{Int64})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Widen(this Vector<Int32> value, out Vector<Int64> first, out Vector<Int64> second)
        {
            Vector.Widen(value, out first, out second);
        }

        /// <inheritdoc cref="Vector.Widen(System.Numerics.Vector{UInt32},out System.Numerics.Vector{UInt64},out System.Numerics.Vector{UInt64})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Widen(this Vector<UInt32> value, out Vector<UInt64> first, out Vector<UInt64> second)
        {
            Vector.Widen(value, out first, out second);
        }

        /// <inheritdoc cref="Vector.Widen(System.Numerics.Vector{Single},out System.Numerics.Vector{Double},out System.Numerics.Vector{Double})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Widen(this Vector<Single> value, out Vector<Double> first, out Vector<Double> second)
        {
            Vector.Widen(value, out first, out second);
        }

        /// <inheritdoc cref="Vector.Xor{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> Xor<T>(this Vector<T> first, Vector<T> second) where T : struct
        {
            return Vector.Xor(first, second);
        }

        /// <inheritdoc cref="Vector.AndNot{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> AndNot<T>(this Vector<T> first, Vector<T> second) where T : struct
        {
            return Vector.AndNot(first, second);
        }

        /// <inheritdoc cref="Vector.BitwiseAnd{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> BitwiseAnd<T>(this Vector<T> first, Vector<T> second) where T : struct
        {
            return Vector.BitwiseAnd(first, second);
        }

        /// <inheritdoc cref="Vector.BitwiseOr{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> BitwiseOr<T>(this Vector<T> first, Vector<T> second) where T : struct
        {
            return Vector.BitwiseOr(first, second);
        }

        /// <inheritdoc cref="Vector.ConditionalSelect{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> ConditionalSelect<T>(this Vector<T> condition, Vector<T> first, Vector<T> second) where T : struct
        {
            return Vector.ConditionalSelect(condition, first, second);
        }

        /// <inheritdoc cref="Vector.ConditionalSelect{Int32}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<Single> ConditionalSelect(this Vector<Int32> condition, Vector<Single> first, Vector<Single> second)
        {
            return Vector.ConditionalSelect(condition, first, second);
        }

        /// <inheritdoc cref="Vector.ConditionalSelect{Int64}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<Double> ConditionalSelect(this Vector<Int64> condition, Vector<Double> first, Vector<Double> second)
        {
            return Vector.ConditionalSelect(condition, first, second);
        }

        /// <inheritdoc cref="Vector.EqualsAll{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean EqualsAll<T>(this Vector<T> first, Vector<T> second) where T : struct
        {
            return Vector.EqualsAll(first, second);
        }

        /// <inheritdoc cref="Vector.EqualsAny{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean EqualsAny<T>(this Vector<T> first, Vector<T> second) where T : struct
        {
            return Vector.EqualsAny(first, second);
        }

        /// <inheritdoc cref="Vector.GreaterThan{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> GreaterThan<T>(this Vector<T> first, Vector<T> second) where T : struct
        {
            return Vector.GreaterThan(first, second);
        }

        /// <inheritdoc cref="Vector.GreaterThan{Int32}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<Int32> GreaterThan(this Vector<Int32> first, Vector<Int32> second)
        {
            return Vector.GreaterThan(first, second);
        }

        /// <inheritdoc cref="Vector.GreaterThan{Int64}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<Int64> GreaterThan(this Vector<Int64> first, Vector<Int64> second)
        {
            return Vector.GreaterThan(first, second);
        }

        /// <inheritdoc cref="Vector.GreaterThan{Single}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<Int32> GreaterThan(this Vector<Single> first, Vector<Single> second)
        {
            return Vector.GreaterThan(first, second);
        }

        /// <inheritdoc cref="Vector.GreaterThan{Double}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<Int64> GreaterThan(this Vector<Double> first, Vector<Double> second)
        {
            return Vector.GreaterThan(first, second);
        }

        /// <inheritdoc cref="Vector.LessThan{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> LessThan<T>(this Vector<T> first, Vector<T> second) where T : struct
        {
            return Vector.LessThan(first, second);
        }

        /// <inheritdoc cref="Vector.LessThan{Int32}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<Int32> LessThan(this Vector<Int32> first, Vector<Int32> second)
        {
            return Vector.LessThan(first, second);
        }

        /// <inheritdoc cref="Vector.LessThan{Int64}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<Int64> LessThan(this Vector<Int64> first, Vector<Int64> second)
        {
            return Vector.LessThan(first, second);
        }

        /// <inheritdoc cref="Vector.LessThan{Single}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<Int32> LessThan(this Vector<Single> first, Vector<Single> second)
        {
            return Vector.LessThan(first, second);
        }

        /// <inheritdoc cref="Vector.LessThan{Double}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<Int64> LessThan(this Vector<Double> first, Vector<Double> second)
        {
            return Vector.LessThan(first, second);
        }

        /// <inheritdoc cref="Vector.GreaterThanOrEqual{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> GreaterThanOrEqual<T>(this Vector<T> first, Vector<T> second) where T : struct
        {
            return Vector.GreaterThanOrEqual(first, second);
        }

        /// <inheritdoc cref="Vector.GreaterThanOrEqual{Int32}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<Int32> GreaterThanOrEqual(this Vector<Int32> first, Vector<Int32> second)
        {
            return Vector.GreaterThanOrEqual(first, second);
        }

        /// <inheritdoc cref="Vector.GreaterThanOrEqual{Int64}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<Int64> GreaterThanOrEqual(this Vector<Int64> first, Vector<Int64> second)
        {
            return Vector.GreaterThanOrEqual(first, second);
        }

        /// <inheritdoc cref="Vector.GreaterThanOrEqual{Single}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<Int32> GreaterThanOrEqual(this Vector<Single> first, Vector<Single> second)
        {
            return Vector.GreaterThanOrEqual(first, second);
        }

        /// <inheritdoc cref="Vector.GreaterThanOrEqual{Double}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<Int64> GreaterThanOrEqual(this Vector<Double> first, Vector<Double> second)
        {
            return Vector.GreaterThanOrEqual(first, second);
        }
        /// <inheritdoc cref="Vector.LessThanOrEqual{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> LessThanOrEqual<T>(this Vector<T> first, Vector<T> second) where T : struct
        {
            return Vector.LessThanOrEqual(first, second);
        }

        /// <inheritdoc cref="Vector.LessThanOrEqual{Int32}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<Int32> LessThanOrEqual(this Vector<Int32> first, Vector<Int32> second)
        {
            return Vector.LessThanOrEqual(first, second);
        }

        /// <inheritdoc cref="Vector.LessThanOrEqual{Int64}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<Int64> LessThanOrEqual(this Vector<Int64> first, Vector<Int64> second)
        {
            return Vector.LessThanOrEqual(first, second);
        }

        /// <inheritdoc cref="Vector.LessThanOrEqual{Single}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<Int32> LessThanOrEqual(this Vector<Single> first, Vector<Single> second)
        {
            return Vector.LessThanOrEqual(first, second);
        }

        /// <inheritdoc cref="Vector.LessThanOrEqual{Double}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<Int64> LessThanOrEqual(this Vector<Double> first, Vector<Double> second)
        {
            return Vector.LessThanOrEqual(first, second);
        }

        /// <inheritdoc cref="Vector.OnesComplement{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> OnesComplement<T>(this Vector<T> value) where T : struct
        {
            return Vector.OnesComplement(value);
        }

        /// <inheritdoc cref="Vector.SquareRoot{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> SquareRoot<T>(this Vector<T> value) where T : struct
        {
            return Vector.SquareRoot(value);
        }

        /// <inheritdoc cref="Vector.AsVectorSByte{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<SByte> AsVectorSByte<T>(this Vector<T> value) where T : struct
        {
            return Vector.AsVectorSByte(value);
        }

        /// <inheritdoc cref="Vector.AsVectorByte{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<Byte> AsVectorByte<T>(this Vector<T> value) where T : struct
        {
            return Vector.AsVectorByte(value);
        }

        /// <inheritdoc cref="Vector.AsVectorInt16{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<Int16> AsVectorInt16<T>(this Vector<T> value) where T : struct
        {
            return Vector.AsVectorInt16(value);
        }

        /// <inheritdoc cref="Vector.AsVectorUInt16{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<UInt16> AsVectorUInt16<T>(this Vector<T> value) where T : struct
        {
            return Vector.AsVectorUInt16(value);
        }

        /// <inheritdoc cref="Vector.AsVectorInt32{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<Int32> AsVectorInt32<T>(this Vector<T> value) where T : struct
        {
            return Vector.AsVectorInt32(value);
        }

        /// <inheritdoc cref="Vector.AsVectorUInt32{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<UInt32> AsVectorUInt32<T>(this Vector<T> value) where T : struct
        {
            return Vector.AsVectorUInt32(value);
        }

        /// <inheritdoc cref="Vector.AsVectorInt64{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<Int64> AsVectorInt64<T>(this Vector<T> value) where T : struct
        {
            return Vector.AsVectorInt64(value);
        }

        /// <inheritdoc cref="Vector.AsVectorUInt64{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<UInt64> AsVectorUInt64<T>(this Vector<T> value) where T : struct
        {
            return Vector.AsVectorUInt64(value);
        }

        /// <inheritdoc cref="Vector.AsVectorSingle{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<Single> AsVectorSingle<T>(this Vector<T> value) where T : struct
        {
            return Vector.AsVectorSingle(value);
        }

        /// <inheritdoc cref="Vector.AsVectorDouble{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<Double> AsVectorDouble<T>(this Vector<T> value) where T : struct
        {
            return Vector.AsVectorDouble(value);
        }

        /// <inheritdoc cref="Vector.ConvertToInt32"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<Int32> ConvertToInt32(this Vector<Single> value)
        {
            return Vector.ConvertToInt32(value);
        }

        /// <inheritdoc cref="Vector.ConvertToUInt32"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<UInt32> ConvertToUInt32(this Vector<Single> value)
        {
            return Vector.ConvertToUInt32(value);
        }

        /// <inheritdoc cref="Vector.ConvertToInt64"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<Int64> ConvertToInt64(this Vector<Double> value)
        {
            return Vector.ConvertToInt64(value);
        }

        /// <inheritdoc cref="Vector.ConvertToUInt64"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<UInt64> ConvertToUInt64(this Vector<Double> value)
        {
            return Vector.ConvertToUInt64(value);
        }

        /// <inheritdoc cref="Vector.ConvertToSingle(System.Numerics.Vector{Int32})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<Single> ConvertToSingle(this Vector<Int32> value)
        {
            return Vector.ConvertToSingle(value);
        }

        /// <inheritdoc cref="Vector.ConvertToSingle(System.Numerics.Vector{UInt32})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<Single> ConvertToSingle(this Vector<UInt32> value)
        {
            return Vector.ConvertToSingle(value);
        }

        /// <inheritdoc cref="Vector.ConvertToDouble(System.Numerics.Vector{Int64})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<Double> ConvertToDouble(this Vector<Int64> value)
        {
            return Vector.ConvertToDouble(value);
        }

        /// <inheritdoc cref="Vector.ConvertToDouble(System.Numerics.Vector{UInt64})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<Double> ConvertToDouble(this Vector<UInt64> value)
        {
            return Vector.ConvertToDouble(value);
        }

        /// <inheritdoc cref="Vector.GreaterThanAll{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean GreaterThanAll<T>(this Vector<T> first, Vector<T> second) where T : struct
        {
            return Vector.GreaterThanAll(first, second);
        }

        /// <inheritdoc cref="Vector.LessThanAll{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean LessThanAll<T>(this Vector<T> first, Vector<T> second) where T : struct
        {
            return Vector.LessThanAll(first, second);
        }

        /// <inheritdoc cref="Vector.GreaterThanOrEqualAll{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean GreaterThanOrEqualAll<T>(this Vector<T> first, Vector<T> second) where T : struct
        {
            return Vector.GreaterThanOrEqualAll(first, second);
        }

        /// <inheritdoc cref="Vector.LessThanOrEqualAll{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean LessThanOrEqualAll<T>(this Vector<T> first, Vector<T> second) where T : struct
        {
            return Vector.LessThanOrEqualAll(first, second);
        }

        /// <inheritdoc cref="Vector.GreaterThanAny{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean GreaterThanAny<T>(this Vector<T> first, Vector<T> second) where T : struct
        {
            return Vector.GreaterThanAny(first, second);
        }

        /// <inheritdoc cref="Vector.LessThanAny{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean LessThanAny<T>(this Vector<T> first, Vector<T> second) where T : struct
        {
            return Vector.LessThanAny(first, second);
        }

        /// <inheritdoc cref="Vector.GreaterThanOrEqualAny{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean GreaterThanOrEqualAny<T>(this Vector<T> first, Vector<T> second) where T : struct
        {
            return Vector.GreaterThanOrEqualAny(first, second);
        }

        /// <inheritdoc cref="Vector.LessThanOrEqualAny{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean LessThanOrEqualAny<T>(this Vector<T> first, Vector<T> second) where T : struct
        {
            return Vector.LessThanOrEqualAny(first, second);
        }

        /// <inheritdoc cref="Vector2.Abs"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Abs(this Vector2 value)
        {
            return Vector2.Abs(value);
        }

        /// <inheritdoc cref="Vector2.Add"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Add(this Vector2 first, Vector2 second)
        {
            return Vector2.Add(first, second);
        }

        /// <inheritdoc cref="Vector2.Clamp"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Clamp(this Vector2 value, Vector2 min, Vector2 max)
        {
            return Vector2.Clamp(value, min, max);
        }

        /// <inheritdoc cref="Vector2.Divide(System.Numerics.Vector2,System.Numerics.Vector2)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Divide(this Vector2 first, Vector2 second)
        {
            return Vector2.Divide(first, second);
        }

        /// <inheritdoc cref="Vector2.Divide(System.Numerics.Vector2,Single)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Divide(this Vector2 value, Single divisor)
        {
            return Vector2.Divide(value, divisor);
        }

        /// <inheritdoc cref="Vector2.Dot"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Dot(this Vector2 first, Vector2 second)
        {
            return Vector2.Dot(first, second);
        }

        /// <inheritdoc cref="Vector2.Lerp"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Lerp(this Vector2 first, Vector2 second, Single amount)
        {
            return Vector2.Lerp(first, second, amount);
        }

        /// <inheritdoc cref="Vector2.Max"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Max(this Vector2 first, Vector2 second)
        {
            return Vector2.Max(first, second);
        }

        /// <inheritdoc cref="Vector2.Min"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Min(this Vector2 first, Vector2 second)
        {
            return Vector2.Min(first, second);
        }

        /// <inheritdoc cref="Vector2.Multiply(Single,System.Numerics.Vector2)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Multiply(Single multiplier, Vector2 value)
        {
            return Vector2.Multiply(multiplier, value);
        }

        /// <inheritdoc cref="Vector2.Multiply(System.Numerics.Vector2,Single)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Multiply(this Vector2 value, Single multiplier)
        {
            return Vector2.Multiply(value, multiplier);
        }

        /// <inheritdoc cref="Vector2.Multiply(System.Numerics.Vector2,System.Numerics.Vector2)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Multiply(this Vector2 first, Vector2 second)
        {
            return Vector2.Multiply(first, second);
        }

        /// <inheritdoc cref="Vector2.Negate"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Negate(this Vector2 value)
        {
            return Vector2.Negate(value);
        }

        /// <inheritdoc cref="Vector2.Normalize"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Normalize(this Vector2 value)
        {
            return Vector2.Normalize(value);
        }

        /// <inheritdoc cref="Vector2.Reflect"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Reflect(this Vector2 vector, Vector2 normal)
        {
            return Vector2.Reflect(vector, normal);
        }

        /// <inheritdoc cref="Vector2.Subtract"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Subtract(this Vector2 first, Vector2 second)
        {
            return Vector2.Subtract(first, second);
        }

        /// <inheritdoc cref="Vector2.DistanceSquared"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single DistanceSquared(this Vector2 first, Vector2 second)
        {
            return Vector2.DistanceSquared(first, second);
        }

        /// <inheritdoc cref="Vector2.SquareRoot"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 SquareRoot(this Vector2 value)
        {
            return Vector2.SquareRoot(value);
        }

        /// <inheritdoc cref="Vector2.Transform(System.Numerics.Vector2,System.Numerics.Matrix3x2)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Transform(this Vector2 position, Matrix3x2 matrix)
        {
            return Vector2.Transform(position, matrix);
        }

        /// <inheritdoc cref="Vector2.Transform(System.Numerics.Vector2,System.Numerics.Matrix4x4)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Transform(this Vector2 position, Matrix4x4 matrix)
        {
            return Vector2.Transform(position, matrix);
        }

        /// <inheritdoc cref="Vector2.Transform(System.Numerics.Vector2,System.Numerics.Quaternion)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Transform(this Vector2 value, Quaternion rotation)
        {
            return Vector2.Transform(value, rotation);
        }

        /// <inheritdoc cref="Vector2.TransformNormal(System.Numerics.Vector2,System.Numerics.Matrix3x2)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 TransformNormal(this Vector2 normal, Matrix3x2 matrix)
        {
            return Vector2.TransformNormal(normal, matrix);
        }

        /// <inheritdoc cref="Vector2.TransformNormal(System.Numerics.Vector2,System.Numerics.Matrix4x4)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 TransformNormal(this Vector2 normal, Matrix4x4 matrix)
        {
            return Vector2.TransformNormal(normal, matrix);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Rotation(this Vector2 value)
        {
            return MathF.Atan2(value.Y, value.X) * AngleUtilities.Degree.Single.Straight / MathF.PI;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Vector2 Rotate(this Vector2 value, Single degree)
        {
            Single sin = MathF.Sin(MathF.PI / AngleUtilities.Degree.Single.Straight * degree);
            Single cos = MathF.Cos(MathF.PI / AngleUtilities.Degree.Single.Straight * degree);
            return new Vector2(cos * value.X - sin * value.Y, sin * value.X + cos * value.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Rotate(this Vector2 first, Vector2 second)
        {
            return MathF.Atan2(second.Y - first.Y, second.X - first.X) * AngleUtilities.Degree.Single.Straight / MathF.PI;
        }

        /// <inheritdoc cref="Vector3.Abs"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Abs(this Vector3 value)
        {
            return Vector3.Abs(value);
        }

        /// <inheritdoc cref="Vector3.Add"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Add(this Vector3 first, Vector3 second)
        {
            return Vector3.Add(first, second);
        }

        /// <inheritdoc cref="Vector3.Clamp"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Clamp(this Vector3 value, Vector3 min, Vector3 max)
        {
            return Vector3.Clamp(value, min, max);
        }

        /// <inheritdoc cref="Vector3.Divide(System.Numerics.Vector3,System.Numerics.Vector3)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Divide(this Vector3 first, Vector3 second)
        {
            return Vector3.Divide(first, second);
        }

        /// <inheritdoc cref="Vector3.Divide(System.Numerics.Vector3,Single)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Divide(this Vector3 value, Single divisor)
        {
            return Vector3.Divide(value, divisor);
        }

        /// <inheritdoc cref="Vector3.Dot"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Dot(this Vector3 first, Vector3 second)
        {
            return Vector3.Dot(first, second);
        }

        /// <inheritdoc cref="Vector3.Lerp"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Lerp(this Vector3 first, Vector3 second, Single amount)
        {
            return Vector3.Lerp(first, second, amount);
        }

        /// <inheritdoc cref="Vector3.Max"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Max(this Vector3 first, Vector3 second)
        {
            return Vector3.Max(first, second);
        }

        /// <inheritdoc cref="Vector3.Min"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Min(this Vector3 first, Vector3 second)
        {
            return Vector3.Min(first, second);
        }

        /// <inheritdoc cref="Vector3.Multiply(Single,System.Numerics.Vector3)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Multiply(Single multiplier, Vector3 value)
        {
            return Vector3.Multiply(multiplier, value);
        }

        /// <inheritdoc cref="Vector3.Multiply(System.Numerics.Vector3,Single)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Multiply(this Vector3 value, Single multiplier)
        {
            return Vector3.Multiply(value, multiplier);
        }

        /// <inheritdoc cref="Vector3.Multiply(System.Numerics.Vector3,System.Numerics.Vector3)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Multiply(this Vector3 first, Vector3 second)
        {
            return Vector3.Multiply(first, second);
        }

        /// <inheritdoc cref="Vector3.Negate"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Negate(this Vector3 value)
        {
            return Vector3.Negate(value);
        }

        /// <inheritdoc cref="Vector3.Normalize"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Normalize(this Vector3 value)
        {
            return Vector3.Normalize(value);
        }

        /// <inheritdoc cref="Vector3.Reflect"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Reflect(this Vector3 vector, Vector3 normal)
        {
            return Vector3.Reflect(vector, normal);
        }

        /// <inheritdoc cref="Vector3.Subtract"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Subtract(this Vector3 first, Vector3 second)
        {
            return Vector3.Subtract(first, second);
        }

        /// <inheritdoc cref="Vector3.DistanceSquared"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single DistanceSquared(this Vector3 first, Vector3 second)
        {
            return Vector3.DistanceSquared(first, second);
        }

        /// <inheritdoc cref="Vector3.SquareRoot"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 SquareRoot(this Vector3 value)
        {
            return Vector3.SquareRoot(value);
        }

        /// <inheritdoc cref="Vector3.Transform(System.Numerics.Vector3,System.Numerics.Matrix4x4)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Transform(this Vector3 position, Matrix4x4 matrix)
        {
            return Vector3.Transform(position, matrix);
        }

        /// <inheritdoc cref="Vector3.Transform(System.Numerics.Vector3,System.Numerics.Quaternion)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Transform(this Vector3 value, Quaternion rotation)
        {
            return Vector3.Transform(value, rotation);
        }

        /// <inheritdoc cref="Vector3.TransformNormal(System.Numerics.Vector3,System.Numerics.Matrix4x4)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 TransformNormal(this Vector3 normal, Matrix4x4 matrix)
        {
            return Vector3.TransformNormal(normal, matrix);
        }

        /// <inheritdoc cref="Plane.CreateFromVertices"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Plane ToPlane(this Vector3 first, Vector3 second, Vector3 third)
        {
            return Plane.CreateFromVertices(first, second, third);
        }

        /// <inheritdoc cref="Quaternion.CreateFromAxisAngle"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion ToQuaternion(this Vector3 axis, Single angle)
        {
            return Quaternion.CreateFromAxisAngle(axis, angle);
        }

        /// <inheritdoc cref="Vector4.Abs"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Abs(this Vector4 value)
        {
            return Vector4.Abs(value);
        }

        /// <inheritdoc cref="Vector4.Add"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Add(this Vector4 first, Vector4 second)
        {
            return Vector4.Add(first, second);
        }

        /// <inheritdoc cref="Vector4.Clamp"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Clamp(this Vector4 value, Vector4 min, Vector4 max)
        {
            return Vector4.Clamp(value, min, max);
        }

        /// <inheritdoc cref="Vector4.Divide(System.Numerics.Vector4,System.Numerics.Vector4)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Divide(this Vector4 first, Vector4 second)
        {
            return Vector4.Divide(first, second);
        }

        /// <inheritdoc cref="Vector4.Divide(System.Numerics.Vector4,Single)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Divide(this Vector4 value, Single divisor)
        {
            return Vector4.Divide(value, divisor);
        }

        /// <inheritdoc cref="Vector4.Dot"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Dot(this Vector4 first, Vector4 second)
        {
            return Vector4.Dot(first, second);
        }

        /// <inheritdoc cref="Vector4.Lerp"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Lerp(this Vector4 first, Vector4 second, Single amount)
        {
            return Vector4.Lerp(first, second, amount);
        }

        /// <inheritdoc cref="Vector4.Max"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Max(this Vector4 first, Vector4 second)
        {
            return Vector4.Max(first, second);
        }

        /// <inheritdoc cref="Vector4.Min"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Min(this Vector4 first, Vector4 second)
        {
            return Vector4.Min(first, second);
        }

        /// <inheritdoc cref="Vector4.Multiply(Single,System.Numerics.Vector4)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Multiply(Single multiplier, Vector4 value)
        {
            return Vector4.Multiply(multiplier, value);
        }

        /// <inheritdoc cref="Vector4.Multiply(System.Numerics.Vector4,Single)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Multiply(this Vector4 value, Single multiplier)
        {
            return Vector4.Multiply(value, multiplier);
        }

        /// <inheritdoc cref="Vector4.Multiply(System.Numerics.Vector4,System.Numerics.Vector4)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Multiply(this Vector4 first, Vector4 second)
        {
            return Vector4.Multiply(first, second);
        }

        /// <inheritdoc cref="Vector4.Negate"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Negate(this Vector4 value)
        {
            return Vector4.Negate(value);
        }

        /// <inheritdoc cref="Vector4.Normalize"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Normalize(this Vector4 value)
        {
            return Vector4.Normalize(value);
        }

        /// <inheritdoc cref="Vector4.Subtract"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Subtract(this Vector4 first, Vector4 second)
        {
            return Vector4.Subtract(first, second);
        }

        /// <inheritdoc cref="Vector4.DistanceSquared"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single DistanceSquared(this Vector4 first, Vector4 second)
        {
            return Vector4.DistanceSquared(first, second);
        }

        /// <inheritdoc cref="Vector4.SquareRoot"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 SquareRoot(this Vector4 value)
        {
            return Vector4.SquareRoot(value);
        }

        /// <inheritdoc cref="Vector4.Transform(System.Numerics.Vector4,System.Numerics.Matrix4x4)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Transform(this Vector4 position, Matrix4x4 matrix)
        {
            return Vector4.Transform(position, matrix);
        }

        /// <inheritdoc cref="Vector4.Transform(System.Numerics.Vector4,System.Numerics.Quaternion)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Transform(this Vector4 value, Quaternion rotation)
        {
            return Vector4.Transform(value, rotation);
        }

        /// <inheritdoc cref="Quaternion.Add"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Add(this Quaternion first, Quaternion second)
        {
            return Quaternion.Add(first, second);
        }

        /// <inheritdoc cref="Quaternion.Concatenate"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Concatenate(this Quaternion first, Quaternion second)
        {
            return Quaternion.Concatenate(first, second);
        }

        /// <inheritdoc cref="Quaternion.Conjugate"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Conjugate(this Quaternion value)
        {
            return Quaternion.Conjugate(value);
        }

        /// <inheritdoc cref="Quaternion.Divide"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Divide(this Quaternion first, Quaternion second)
        {
            return Quaternion.Divide(first, second);
        }

        /// <inheritdoc cref="Quaternion.Dot"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Dot(this Quaternion first, Quaternion second)
        {
            return Quaternion.Dot(first, second);
        }

        /// <inheritdoc cref="Quaternion.Inverse"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Inverse(this Quaternion value)
        {
            return Quaternion.Inverse(value);
        }

        /// <inheritdoc cref="Quaternion.Lerp"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Lerp(this Quaternion first, Quaternion second, Single amount)
        {
            return Quaternion.Lerp(first, second, amount);
        }

        /// <inheritdoc cref="Quaternion.Multiply(System.Numerics.Quaternion,System.Numerics.Quaternion)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Multiply(this Quaternion first, Quaternion second)
        {
            return Quaternion.Multiply(first, second);
        }

        /// <inheritdoc cref="Quaternion.Multiply(System.Numerics.Quaternion,Single)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Multiply(this Quaternion value, Single multiplier)
        {
            return Quaternion.Multiply(value, multiplier);
        }

        /// <inheritdoc cref="Quaternion.Negate"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Negate(this Quaternion value)
        {
            return Quaternion.Negate(value);
        }

        /// <inheritdoc cref="Quaternion.Normalize"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Normalize(this Quaternion value)
        {
            return Quaternion.Normalize(value);
        }

        /// <inheritdoc cref="Quaternion.Slerp"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Slerp(this Quaternion first, Quaternion second, Single amount)
        {
            return Quaternion.Slerp(first, second, amount);
        }

        /// <inheritdoc cref="Quaternion.Subtract"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Subtract(this Quaternion first, Quaternion second)
        {
            return Quaternion.Subtract(first, second);
        }

        /// <inheritdoc cref="Quaternion.CreateFromRotationMatrix"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion ToQuaternion(this Matrix4x4 value)
        {
            return Quaternion.CreateFromRotationMatrix(value);
        }
    }
}