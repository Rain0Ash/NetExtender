// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Trees;
using NetExtender.Types.Trees.Interfaces;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Serialization;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;

namespace NetExtender.NewtonSoft.Types.Trees
{
    public sealed class DictionaryTreeJsonConverter<TKey, TValue> : DictionaryTreeJsonConverter where TKey : notnull
    {
        private enum KeyState
        {
            None,
            Key,
            Value
        }

        public override void WriteJson(JsonWriter writer, Object? value, JsonSerializer serializer)
        {
            if (value is null)
            {
                writer.WriteNull();
                return;
            }

            if (value is not IDictionaryTree<TKey, TValue> node)
            {
                throw new JsonException();
            }

            WriteNode(writer, node.Node);
        }

        private static void WriteNode(JsonWriter writer, IDictionaryTreeNode<TKey, TValue> node)
        {
            if (node.IsEmpty)
            {
                writer.WriteNull();
                return;
            }

            if (node.TreeIsEmpty && node.HasValue)
            {
                writer.WriteValue(node.Value);
                return;
            }

            writer.WriteStartObject();

            if (node.HasValue)
            {
                writer.WritePropertyName("Value");
                writer.WriteValue(node.Value);
            }

            foreach ((TKey key, IDictionaryTreeNode<TKey, TValue>? child) in node)
            {
                writer.WritePropertyName(key.ToString()!, true);
                WriteNode(writer, child);
            }

            writer.WriteEndObject();
        }

        private static KeyState ReadJsonKey(JsonTokenEntry token, out TKey key)
        {
            if (token.Current == "Value")
            {
                key = default!;
                return KeyState.Value;
            }

            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (token.TryConvert(out key!) && key is not null)
            {
                return KeyState.Key;
            }

            return KeyState.None;
        }

        public override Object? ReadJson(JsonReader reader, Type type, Object? value, JsonSerializer serializer)
        {
            DictionaryTree<TKey, TValue> dictionary = new DictionaryTree<TKey, TValue>();
            List<TKey> keys = new List<TKey>();

            reader.AsEnumerable()
                .CanBeNullable(out Boolean nullable)?
                .MustStartWith()
                .WithPropertyName(PropertyName)
                .WithValue(PropertyValue)
                .MustEndWith()
                .Evaluate();

            if (nullable)
            {
                return null;
            }

            void PropertyName(JsonTokenEntry token, String? name)
            {
                KeyState state = ReadJsonKey(token, out TKey key);

                switch (state)
                {
                    case KeyState.None:
                        throw new JsonException();
                    case KeyState.Value:
                        return;
                    case KeyState.Key:
                        break;
                    default:
                        throw new EnumUndefinedOrNotSupportedException<KeyState>(state, nameof(state), null);
                }

                if (keys.Count >= token.Depth)
                {
                    keys.RemoveRange(token.Depth - 1, keys.Count - token.Depth + 1);
                }

                keys.Add(key);
            }

            void PropertyValue(JsonTokenEntry token)
            {
                if (!token.TryConvert(out TValue? nodevalue))
                {
                    throw new JsonException();
                }

                TKey key = keys[^1];
                dictionary.Add(key, nodevalue!, keys.SkipLast(1));
            }

            return dictionary;
        }
    }

    public abstract class DictionaryTreeJsonConverter : JsonConverter
    {
        private static ConcurrentDictionary<Type, Boolean> Cache { get; } = new ConcurrentDictionary<Type, Boolean>();

        private static Boolean IsConvertible(Type type)
        {
            return type.TryGetGenericTypeDefinition() == typeof(Dictionary<,>) ||
                   type.TryGetGenericTypeDefinitionInterfaces().Contains(typeof(IDictionaryTree<,>));
        }

        public override Boolean CanRead
        {
            get
            {
                return true;
            }
        }

        public override Boolean CanWrite
        {
            get
            {
                return true;
            }
        }

        public sealed override Boolean CanConvert(Type type)
        {
            return Cache.GetOrAdd(type, IsConvertible);
        }
    }
}