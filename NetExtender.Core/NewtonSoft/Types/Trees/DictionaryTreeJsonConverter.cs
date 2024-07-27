// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using NetExtender.NewtonSoft.Utilities;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Trees;
using NetExtender.Types.Trees.Interfaces;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Serialization;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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

        private static KeyState ReadJsonKey(JsonTokenEntry token, NamingStrategy? strategy, out TKey key)
        {
            if (token.Current == strategy.NamingStrategy("Value", false))
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
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (serializer is null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }

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
                NamingStrategy? strategy = (serializer.ContractResolver as DefaultContractResolver)?.NamingStrategy;
                KeyState state = ReadJsonKey(token, strategy, out TKey key);

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

        public override void WriteJson(JsonWriter writer, Object? value, JsonSerializer serializer)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            if (serializer is null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }

            if (value is null)
            {
                writer.WriteNull();
                return;
            }

            if (value is not IDictionaryTree<TKey, TValue> node)
            {
                throw new JsonException();
            }

            NamingStrategy? strategy = (serializer.ContractResolver as DefaultContractResolver)?.NamingStrategy;
            WriteNode(writer, node.Node, strategy);
        }

        private static void WriteNode(JsonWriter writer, IDictionaryTreeNode<TKey, TValue> node, NamingStrategy? strategy)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

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
                writer.WritePropertyName(strategy.NamingStrategy("Value", false));
                writer.WriteValue(node.Value);
            }

            foreach ((TKey key, IDictionaryTreeNode<TKey, TValue>? child) in node)
            {
                writer.WritePropertyName(key.ToString()!, true);
                WriteNode(writer, child, strategy);
            }

            writer.WriteEndObject();
        }
    }

    public abstract class DictionaryTreeJsonConverter : JsonConverter
    {
        private static ConcurrentDictionary<Type, Boolean> Storage { get; } = new ConcurrentDictionary<Type, Boolean>();

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

        public sealed override Boolean CanConvert(Type? type)
        {
            return type is not null && Storage.GetOrAdd(type, IsConvertible);
        }
    }
}