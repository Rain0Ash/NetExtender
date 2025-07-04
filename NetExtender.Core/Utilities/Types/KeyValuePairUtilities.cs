// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using NetExtender.Types.Entities;
using NetExtender.Utilities.Core;

namespace NetExtender.Utilities.Types
{
    public record KeyValuePairAccessor(Func<Object, Object?> Key, Func<Object, Object?> Value);
    
    public static class KeyValuePairUtilities
    {
        public static Type KeyValuePairType { get; } = typeof(KeyValuePair<,>);
        
        private static class KeyValuePairStorage
        {
            private static ConcurrentDictionary<Type, KeyValuePairAccessor> Storage { get; } = new ConcurrentDictionary<Type, KeyValuePairAccessor>();

            // ReSharper disable once MemberHidesStaticFromOuterClass
            public static Boolean TryGetAccessor(Type type, [MaybeNullWhen(false)] out KeyValuePairAccessor result)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }

                try
                {
                    result = Storage.GetOrAdd(type, CreateAccessor);
                    return true;
                }
                catch (Exception)
                {
                    result = null;
                    return false;
                }
            }

            private static KeyValuePairAccessor CreateAccessor(Type type)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }

                if (type.TryGetGenericTypeDefinition() != KeyValuePairType)
                {
                    throw new NotSupportedException();
                }

                ParameterExpression argument = Expression.Parameter(typeof(Object), nameof(argument));
                UnaryExpression convert = Expression.Convert(argument, type);

                Func<Object, Object?> key = Expression.Lambda<Func<Object, Object?>>(Expression.Convert(Expression.Property(convert, nameof(KeyValuePair<Any.Value, Any.Value>.Key)), typeof(Object)), argument).Compile();
                Func<Object, Object?> value = Expression.Lambda<Func<Object, Object?>>(Expression.Convert(Expression.Property(convert, nameof(KeyValuePair<Any.Value, Any.Value>.Value)), typeof(Object)), argument).Compile();

                return new KeyValuePairAccessor(key, value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetAccessor(Type type, [MaybeNullWhen(false)] out KeyValuePairAccessor result)
        {
            return KeyValuePairStorage.TryGetAccessor(type, out result);
        }

        public static Boolean TryGet(Object? @object, out Object? key, out Object? value)
        {
            if (@object is null || !TryGetAccessor(@object.GetType(), out KeyValuePairAccessor? accessor))
            {
                key = null;
                value = null;
                return false;
            }

            key = accessor.Key.Invoke(@object);
            value = accessor.Value.Invoke(@object);
            return true;
        }

        public static Boolean TryGetKey(Object? @object, out Object? key)
        {
            if (@object is null || !TryGetAccessor(@object.GetType(), out KeyValuePairAccessor? accessor))
            {
                key = null;
                return false;
            }

            key = accessor.Key.Invoke(@object);
            return true;
        }

        public static Boolean TryGetValue(Object? @object, out Object? value)
        {
            if (@object is null || !TryGetAccessor(@object.GetType(), out KeyValuePairAccessor? accessor))
            {
                value = null;
                return false;
            }

            value = accessor.Value.Invoke(@object);
            return true;
        }

        public static void Deconstruct(this DictionaryEntry entry, out Object key, out Object? value)
        {
            key = entry.Key;
            value = entry.Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KeyValuePair<TValue, TKey> Reverse<TKey, TValue>(this KeyValuePair<TKey, TValue> pair)
        {
            return new KeyValuePair<TValue, TKey>(pair.Value, pair.Key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KeyValuePair<TKey, TKey> FlattenByKey<TKey, TValue>(this KeyValuePair<TKey, KeyValuePair<TKey, TValue>> pair)
        {
            return FlattenByInnerKey(pair);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KeyValuePair<TKey, TInnerKey> FlattenByInnerKey<TKey, TInnerKey, TValue>(this KeyValuePair<TKey, KeyValuePair<TInnerKey, TValue>> pair)
        {
            return new KeyValuePair<TKey, TInnerKey>(pair.Key, pair.Value.Key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KeyValuePair<TKey, TValue> FlattenByValue<TKey, TValue>(this KeyValuePair<TKey, KeyValuePair<TKey, TValue>> pair)
        {
            return FlattenByInnerValue(pair);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KeyValuePair<TKey, TValue> FlattenByInnerValue<TKey, TInnerKey, TValue>(this KeyValuePair<TKey, KeyValuePair<TInnerKey, TValue>> pair)
        {
            return new KeyValuePair<TKey, TValue>(pair.Key, pair.Value.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DictionaryEntry ToDictionaryEntry<TKey, TValue>(this KeyValuePair<TKey, TValue> pair)
        {
            return new DictionaryEntry(pair.Key!, pair.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<DictionaryEntry> ToDictionaryEntries<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Select(ToDictionaryEntry);
        }
    }
}