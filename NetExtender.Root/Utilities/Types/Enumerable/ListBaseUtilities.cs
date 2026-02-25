using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using NetExtender.Exceptions;
using NetExtender.Types.Random.Interfaces;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Utilities.Types
{
    internal static class ListBaseUtilities
    {
        private static class ArrayAccessor<T>
        {
            public static Func<List<T>, T[]> Getter { get; }

            static ArrayAccessor()
            {
                const MethodAttributes attributes = MethodAttributes.Public | MethodAttributes.Static;
                DynamicMethod method = new DynamicMethod("get", attributes, CallingConventions.Standard, typeof(T[]), new[] { typeof(List<T>) }, typeof(ArrayAccessor<T>), true);
                ILGenerator il = method.GetILGenerator();
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldfld, typeof(List<T>).GetField("_items", BindingFlags.NonPublic | BindingFlags.Instance) ?? throw new NeverOperationException());
                il.Emit(OpCodes.Ret);
                Getter = (Func<List<T>, T[]>) method.CreateDelegate(typeof(Func<List<T>, T[]>));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static T[] InternalArray<T>(List<T> list)
        {
            if (list is null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            return ArrayAccessor<T>.Getter(list);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static T GetRandom<T>(this ImmutableArray<T> source)
        {
            return source.Length > 0 ? source[RandomUtilities.NextNonNegative(source.Length - 1)] : throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static T? GetRandomOrDefault<T>(this ImmutableArray<T> source)
        {
            return source.Length > 0 ? source[RandomUtilities.NextNonNegative(source.Length - 1)] : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static T GetRandomOrDefault<T>(this ImmutableArray<T> source, T alternate)
        {
            return source.Length > 0 ? source[RandomUtilities.NextNonNegative(source.Length - 1)] : alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static T GetRandomOrDefault<T>(this ImmutableArray<T> source, Func<T> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return source.Length > 0 ? source[RandomUtilities.NextNonNegative(source.Length - 1)] : alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static T GetRandom<T>(IList<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.Count > 0 ? collection[RandomUtilities.NextNonNegative(collection.Count - 1)] : throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static T? GetRandomOrDefault<T>(IList<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.Count > 0 ? collection[RandomUtilities.NextNonNegative(collection.Count - 1)] : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static T GetRandomOrDefault<T>(IList<T> collection, T alternate)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.Count > 0 ? collection[RandomUtilities.NextNonNegative(collection.Count - 1)] : alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static T GetRandomOrDefault<T>(IList<T> collection, Func<T> alternate)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return collection.Count > 0 ? collection[RandomUtilities.NextNonNegative(collection.Count - 1)] : alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Shuffle<T>(IList<T> collection)
        {
            Shuffle(collection, RandomUtilities.Generator);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        internal static void Shuffle<T>(IList<T> collection, Random random)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            for (Int32 i = 0; i < collection.Count; i++)
            {
                Int32 j = random.Next(i, collection.Count);
                (collection[i], collection[j]) = (collection[j], collection[i]);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Shuffle<T>(IList<T> collection, IRandom random)
        {
            Shuffle<T, IRandom>(collection, random);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        internal static void Shuffle<T, TRandom>(IList<T> collection, TRandom random) where TRandom : IRandom
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            for (Int32 i = 0; i < collection.Count; i++)
            {
                Int32 j = random.Next(i, collection.Count);
                (collection[i], collection[j]) = (collection[j], collection[i]);
            }
        }
    }
}