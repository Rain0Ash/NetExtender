// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using NetExtender.Types.Trees;
using NetExtender.Types.Trees.Interfaces;
using Newtonsoft.Json;

namespace NetExtender.NewtonSoft.Types.Trees
{
    public sealed class DictionaryTreeTypeJsonConverter : JsonConverter
    {
        private static Type DictionaryTypeDifinition { get; } = typeof(Dictionary<,>);
        private static Type DictionaryTreeTypeDifinition { get; } = typeof(DictionaryTree<,>);
        private static Type IDictionaryTreeTypeDifinition { get; } = typeof(IDictionaryTree<,>);
        private static Type DictionaryTreeNodeTypeDifinition { get; } = typeof(DictionaryTreeNode<,>);
        private static Type EqualityComparerTypeDifinition { get; } = typeof(EqualityComparer<>);
        private static Type IEqualityComparerTypeDifinition { get; } = typeof(IEqualityComparer<>);
        
        private static ConcurrentDictionary<Type, Type> Cache { get; } = new ConcurrentDictionary<Type, Type>();

        public override Boolean CanWrite
        {
            get
            {
                return false;
            }
        }

        public override Boolean CanConvert(Type? objectType)
        {
            return false;
        }

        private static Type MakeGenericType(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            static Type Internal(Type type)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }

                try
                {
                    if (!type.IsGenericType)
                    {
                        return type;
                    }

                    Type definition = type.GetGenericTypeDefinition();

                    if (definition == IEqualityComparerTypeDifinition)
                    {
                        return EqualityComparerTypeDifinition.MakeGenericType(type.GetGenericArguments());
                    }

                    if (definition != DictionaryTreeTypeDifinition && definition != IDictionaryTreeTypeDifinition)
                    {
                        return type;
                    }

                    Type[] generic = type.GetGenericArguments();
                    return DictionaryTypeDifinition.MakeGenericType(generic[0], DictionaryTreeNodeTypeDifinition.MakeGenericType(generic[0], generic[1]));
                }
                catch (Exception exception)
                {
                    throw new JsonSerializationException($"Unable to construct type from generic {type}", exception);
                }
            }

            return Cache.GetOrAdd(type, Internal);
        }

        public override Object? ReadJson(JsonReader reader, Type type, Object? existingValue, JsonSerializer serializer)
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
            
            Object? @object = serializer.Deserialize(reader, MakeGenericType(type));

            if (@object is null)
            {
                return null;
            }

            type = @object.GetType();

            if (!type.IsGenericType)
            {
                return @object;
            }

            if (type.GetGenericTypeDefinition() != DictionaryTypeDifinition)
            {
                return @object;
            }

            Type[] generic = type.GetGenericArguments()[1].GetGenericArguments();
            Type tree = DictionaryTreeTypeDifinition.MakeGenericType(generic);

            ConstructorInfo? method = tree.GetConstructor(new[]
            {
                DictionaryTypeDifinition.MakeGenericType(generic[0], DictionaryTreeNodeTypeDifinition.MakeGenericType(generic)),
                IEqualityComparerTypeDifinition.MakeGenericType(generic[0])
            });

            return method?.Invoke(new[] { @object, null });
        }

        public override void WriteJson(JsonWriter writer, Object? value, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }
    }
}