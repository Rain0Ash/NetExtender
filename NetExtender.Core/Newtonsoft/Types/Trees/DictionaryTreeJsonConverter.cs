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

namespace NetExtender.Newtonsoft.Types.Trees
{
    public sealed class DictionaryTreeJsonConverter<TKey, TValue> : DictionaryTreeJsonConverter where TKey : notnull
    {
        private enum KeyState
        {
            None,
            Key,
            Value
        }

        private static KeyState ReadKey(JsonTokenEntry token, ref SerializerOptions options, out TKey key)
        {
            if (token.Current != options.Strategy.Apply(nameof(IDictionaryTreeNode<TKey, TValue>.Value)))
            {
                // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
                return token.TryConvert(out key!) && key is not null ? KeyState.Key : KeyState.None;
            }

            key = default!;
            return KeyState.Value;
        }

        protected internal override Object? Read(in JsonReader reader, Type type, Object? value, ref SerializerOptions options)
        {
            SerializerOptions serializer = options;
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
                SerializerOptions options = serializer;
                KeyState state = ReadKey(token, ref options, out TKey key);

                switch (state)
                {
                    case KeyState.None:
                        throw new JsonSerializationException();
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
                if (!token.TryConvert(out TValue? result))
                {
                    throw new JsonSerializationException();
                }

                TKey key = keys[^1];
                dictionary.Add(key, result!, keys.SkipLast(1));
            }

            return dictionary;
        }

        protected internal override Boolean Write(in JsonWriter writer, Object? value, ref SerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNull();
                return true;
            }

            if (value is not IDictionaryTree<TKey, TValue> node)
            {
                throw new JsonSerializationException($"Value has type '{value.GetType().Name}', but expected '{typeof(IDictionaryTree<TKey, TValue>)}'.");
            }

            return WriteNode(writer, node.Node, ref options);
        }

        private static Boolean WriteNode(JsonWriter writer, IDictionaryTreeNode<TKey, TValue> node, ref SerializerOptions options)
        {
            if (node.IsEmpty)
            {
                writer.WriteNull();
                return true;
            }

            if (node is { TreeIsEmpty: true, HasValue: true })
            {
                writer.WriteValue(node.Value);
                return true;
            }

            writer.WriteStartObject();

            if (node.HasValue)
            {
                writer.WritePropertyName(nameof(IDictionaryTreeNode<TKey, TValue>.Value), options);
                writer.WriteValue(node.Value);
            }

            foreach ((TKey key, IDictionaryTreeNode<TKey, TValue> child) in node)
            {
                writer.WritePropertyName(key.ToString()!, true);
                WriteNode(writer, child, ref options);
            }

            writer.WriteEndObject();
            return true;
        }
    }

    public abstract class DictionaryTreeJsonConverter : NewtonsoftJsonConverter
    {
        private static ConcurrentDictionary<Type, Boolean> Storage { get; } = new ConcurrentDictionary<Type, Boolean>();

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

        private static Boolean IsConvertible(Type type)
        {
            return type.TryGetGenericTypeDefinition() == typeof(Dictionary<,>) || type.TryGetGenericTypeDefinitionInterfaces().Contains(typeof(IDictionaryTree<,>));
        }

        public sealed override Boolean CanConvert(Type? type)
        {
            return type is not null && Storage.GetOrAdd(type, IsConvertible);
        }
    }
}