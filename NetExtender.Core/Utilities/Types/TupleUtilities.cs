// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Types
{
    [SuppressMessage("ReSharper", "UseDeconstructionOnParameter")]
    public static class TupleUtilities
    {
        public static IEnumerable<T> AsEnumerable<T>(this Tuple<T> value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            yield return value.Item1;
        }
        
        public static IEnumerable<T> AsEnumerable<T>(this Tuple<T, T> value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            
            yield return value.Item1;
            yield return value.Item2;
        }
        
        public static IEnumerable<T> AsEnumerable<T>(this Tuple<T, T, T> value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            
            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
        }
        
        public static IEnumerable<T> AsEnumerable<T>(this Tuple<T, T, T, T> value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            
            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
            yield return value.Item4;
        }
        
        public static IEnumerable<T> AsEnumerable<T>(this Tuple<T, T, T, T, T> value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            
            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
            yield return value.Item4;
            yield return value.Item5;
        }
        
        public static IEnumerable<T> AsEnumerable<T>(this Tuple<T, T, T, T, T, T> value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            
            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
            yield return value.Item4;
            yield return value.Item5;
            yield return value.Item6;
        }
        
        public static IEnumerable<T> AsEnumerable<T>(this Tuple<T, T, T, T, T, T, T> value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            
            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
            yield return value.Item4;
            yield return value.Item5;
            yield return value.Item6;
            yield return value.Item7;
        }
        
#pragma warning disable CS8714
        public static IEnumerable<T> AsEnumerable<T>(this Tuple<T, T, T, T, T, T, T, T> value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            
            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
            yield return value.Item4;
            yield return value.Item5;
            yield return value.Item6;
            yield return value.Item7;
            yield return value.Rest;
        }
#pragma warning restore CS8714
        
        public static IEnumerable<T> AsEnumerable<T, TRest>(this Tuple<T, T, T, T, T, T, T, TRest> value) where TRest : ITuple
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            
            for (Int32 i = 0; i < value.Rest.Length; i++)
            {
                Object? item = value.Rest[i];

                if (item is not null && item is not T && !ConvertUtilities.IsChangeType<T>(item))
                {
                    throw new InvalidOperationException($"Rest value must contains only items of type {typeof(T).Name}");
                }
            }
            
            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
            yield return value.Item4;
            yield return value.Item5;
            yield return value.Item6;
            yield return value.Item7;

            for (Int32 i = 0; i < value.Rest.Length; i++)
            {
                Object? item = value.Rest[i];
                yield return item is not null ? item is T result ? result : ConvertUtilities.ChangeType<T>(item) : default!;
            }
        }
        
        public static IEnumerable<T> AsEnumerable<T>(this ValueTuple<T> value)
        {
            yield return value.Item1;
        }
        
        public static IEnumerable<T> AsEnumerable<T>(this ValueTuple<T, T> value)
        {
            yield return value.Item1;
            yield return value.Item2;
        }
        
        public static IEnumerable<T> AsEnumerable<T>(this ValueTuple<T, T, T> value)
        {
            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
        }
        
        public static IEnumerable<T> AsEnumerable<T>(this ValueTuple<T, T, T, T> value)
        {
            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
            yield return value.Item4;
        }
        
        public static IEnumerable<T> AsEnumerable<T>(this ValueTuple<T, T, T, T, T> value)
        {
            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
            yield return value.Item4;
            yield return value.Item5;
        }
        
        public static IEnumerable<T> AsEnumerable<T>(this ValueTuple<T, T, T, T, T, T> value)
        {
            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
            yield return value.Item4;
            yield return value.Item5;
            yield return value.Item6;
        }
        
        public static IEnumerable<T> AsEnumerable<T>(this ValueTuple<T, T, T, T, T, T, T> value)
        {
            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
            yield return value.Item4;
            yield return value.Item5;
            yield return value.Item6;
            yield return value.Item7;
        }
        
#pragma warning disable CS8714
        public static IEnumerable<T> AsEnumerable<T>(this ValueTuple<T, T, T, T, T, T, T, T> value) where T : struct
        {
            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
            yield return value.Item4;
            yield return value.Item5;
            yield return value.Item6;
            yield return value.Item7;
            yield return value.Rest;
        }
#pragma warning restore CS8714
        
        public static IEnumerable<T> AsEnumerable<T, TRest>(this ValueTuple<T, T, T, T, T, T, T, TRest> value) where TRest : struct, ITuple
        {
            for (Int32 i = 0; i < value.Rest.Length; i++)
            {
                Object? item = value.Rest[i];

                if (item is not null && item is not T && !ConvertUtilities.IsChangeType<T>(item))
                {
                    throw new InvalidOperationException($"Rest value must contains only items of type {typeof(T).Name}");
                }
            }
            
            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
            yield return value.Item4;
            yield return value.Item5;
            yield return value.Item6;
            yield return value.Item7;

            for (Int32 i = 0; i < value.Rest.Length; i++)
            {
                Object? item = value.Rest[i];
                yield return item is not null ? item is T result ? result : ConvertUtilities.ChangeType<T>(item) : default!;
            }
        }
        
        public static IEnumerable<Object?> AsEnumerable(this ITuple value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            for (Int32 i = 0; i < value.Length; i++)
            {
                yield return value[i];
            }
        }

        public static IEnumerable<T> AsEnumerable<T>(this ITuple value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            
            for (Int32 i = 0; i < value.Length; i++)
            {
                Object? item = value[i];

                if (item is not null && item is not T && !ConvertUtilities.IsChangeType<T>(item))
                {
                    throw new InvalidOperationException($"Rest value must contains only items of type {typeof(T).Name}");
                }
            }
            
            for (Int32 i = 0; i < value.Length; i++)
            {
                Object? item = value[i];
                yield return item is not null ? item is T result ? result : ConvertUtilities.ChangeType<T>(item) : default!;
            }
        }
        
        [return: NotNullIfNotNull("value")]
        public static ITuple? AsTuple<T>(this Tuple<T>? value)
        {
            return value;
        }
        
        [return: NotNullIfNotNull("value")]
        public static ITuple? AsTuple<T>(this Tuple<T, T>? value)
        {
            return value;
        }
        
        [return: NotNullIfNotNull("value")]
        public static ITuple? AsTuple<T>(this Tuple<T, T, T>? value)
        {
            return value;
        }
        
        [return: NotNullIfNotNull("value")]
        public static ITuple? AsTuple<T>(this Tuple<T, T, T, T>? value)
        {
            return value;
        }
        
        [return: NotNullIfNotNull("value")]
        public static ITuple? AsTuple<T>(this Tuple<T, T, T, T, T>? value)
        {
            return value;
        }
        
        [return: NotNullIfNotNull("value")]
        public static ITuple? AsTuple<T>(this Tuple<T, T, T, T, T, T>? value)
        {
            return value;
        }
        
        [return: NotNullIfNotNull("value")]
        public static ITuple? AsTuple<T>(this Tuple<T, T, T, T, T, T, T>? value)
        {
            return value;
        }
        
#pragma warning disable CS8714
        [return: NotNullIfNotNull("value")]
        public static ITuple? AsTuple<T>(this Tuple<T, T, T, T, T, T, T, T>? value)
        {
            return value;
        }
#pragma warning restore CS8714

        [return: NotNullIfNotNull("value")]
        public static ITuple? AsTuple<T, TRest>(this Tuple<T, T, T, T, T, T, T, TRest>? value) where TRest : ITuple
        {
            return value;
        }
        
        public static ITuple AsTuple<T>(this ValueTuple<T> value)
        {
            return value;
        }
        
        public static ITuple AsTuple<T>(this ValueTuple<T, T> value)
        {
            return value;
        }
        
        public static ITuple AsTuple<T>(this ValueTuple<T, T, T> value)
        {
            return value;
        }
        
        public static ITuple AsTuple<T>(this ValueTuple<T, T, T, T> value)
        {
            return value;
        }
        
        public static ITuple AsTuple<T>(this ValueTuple<T, T, T, T, T> value)
        {
            return value;
        }
        
        public static ITuple AsTuple<T>(this ValueTuple<T, T, T, T, T, T> value)
        {
            return value;
        }
        
        public static ITuple AsTuple<T>(this ValueTuple<T, T, T, T, T, T, T> value)
        {
            return value;
        }
        
#pragma warning disable CS8714
        public static ITuple AsTuple<T>(this ValueTuple<T, T, T, T, T, T, T, T> value) where T : struct
        {
            return value;
        }
#pragma warning restore CS8714

        public static ITuple AsTuple<T, TRest>(this ValueTuple<T, T, T, T, T, T, T, TRest> value) where TRest : struct, ITuple
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ITuple? AsTuple<T>(this IEnumerable<T> source, Boolean value)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            LinkedList<T> items = new LinkedList<T>(source);

            if (items.Count <= 0)
            {
                return null;
            }

            Int32 index = 0;
            Type[] types = new Type[items.Count];
            foreach (T item in items)
            {
                types[index++] = item?.GetType() ?? typeof(Object);
            }
            
            Func<Type[], Type> factory = value ? GenericTypeUtilities.CreateValueTupleType : GenericTypeUtilities.CreateTupleType;

            if (items.Count <= GenericTypeUtilities.TupleMaximumGeneric)
            {
                return (ITuple?) Activator.CreateInstance(factory(types), items.Cast<Object?>().ToArray());
            }

            Object? inner = null;

            Type[] typearray = new Type[GenericTypeUtilities.TupleMaximumGeneric + 1];
            Object?[] itemarray = new Object?[typearray.Length];
            foreach (Int32 count in types.Reverse().ZipChunk(items.Reverse().Cast<Object?>(), typearray, itemarray, GenericTypeUtilities.TupleMaximumGeneric))
            {
                Array.Reverse(typearray, 0, GenericTypeUtilities.TupleMaximumGeneric);
                Array.Reverse(itemarray, 0, GenericTypeUtilities.TupleMaximumGeneric);
                
                if (inner is not null && count >= GenericTypeUtilities.TupleMaximumGeneric)
                {
                    typearray[GenericTypeUtilities.TupleMaximumGeneric] = inner.GetType();
                    itemarray[GenericTypeUtilities.TupleMaximumGeneric] = inner;
                }

                Type type = factory(inner is not null && count >= GenericTypeUtilities.TupleMaximumGeneric ? typearray : typearray.Slice(0, count).ToArray());
                inner = Activator.CreateInstance(type, inner is not null && count >= GenericTypeUtilities.TupleMaximumGeneric ? itemarray : itemarray.Slice(0, count).ToArray());
            }

            return (ITuple?) inner;
        }
        
        public static ITuple? AsTuple(this IEnumerable source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return AsTuple(source.Cast<Object?>());
        }

        public static ITuple? AsTuple<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return AsTuple(source, false);
        }
        
        public static ITuple? AsValueTuple(this IEnumerable source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return AsValueTuple(source.Cast<Object?>());
        }

        public static ITuple? AsValueTuple<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return AsTuple(source, true);
        }

        public static KeyValuePair<TKey, TValue> ToPair<TKey, TValue>(this (TKey, TValue) value)
        {
            return new KeyValuePair<TKey, TValue>(value.Item1, value.Item2);
        }
    }
}