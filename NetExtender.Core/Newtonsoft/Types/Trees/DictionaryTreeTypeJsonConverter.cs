// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using NetExtender.Types.Trees;
using NetExtender.Types.Trees.Interfaces;
using Newtonsoft.Json;

namespace NetExtender.Newtonsoft.Types.Trees
{
    public sealed class DictionaryTreeTypeJsonConverter : NewtonsoftJsonConverter
    {
        private static Type DictionaryTypeDefinition { get; } = typeof(Dictionary<,>);
        private static Type DictionaryTreeTypeDefinition { get; } = typeof(DictionaryTree<,>);
        private static Type IDictionaryTreeTypeDefinition { get; } = typeof(IDictionaryTree<,>);
        private static Type DictionaryTreeNodeTypeDefinition { get; } = typeof(DictionaryTreeNode<,>);
        private static Type EqualityComparerTypeDefinition { get; } = typeof(EqualityComparer<>);
        private static Type IEqualityComparerTypeDefinition { get; } = typeof(IEqualityComparer<>);
        
        private static ConcurrentDictionary<Type, Type> Cache { get; } = new ConcurrentDictionary<Type, Type>();

        public override Boolean CanWrite
        {
            get
            {
                return false;
            }
        }

        public override Boolean CanConvert(Type? type)
        {
            return false;
        }

        private static Type MakeGenericType(Type type)
        {
            static Type Core(Type type)
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

                    if (definition == IEqualityComparerTypeDefinition)
                    {
                        return EqualityComparerTypeDefinition.MakeGenericType(type.GetGenericArguments());
                    }

                    if (definition != DictionaryTreeTypeDefinition && definition != IDictionaryTreeTypeDefinition)
                    {
                        return type;
                    }

                    Type[] generic = type.GetGenericArguments();
                    return DictionaryTypeDefinition.MakeGenericType(generic[0], DictionaryTreeNodeTypeDefinition.MakeGenericType(generic[0], generic[1]));
                }
                catch (Exception exception)
                {
                    throw new JsonSerializationException($"Unable to construct type from generic {type}", exception);
                }
            }

            return Cache.GetOrAdd(type, Core);
        }

        protected internal override Object? Read(in JsonReader reader, Type type, Object? exist, ref SerializerOptions options)
        {
            Object? @object = options.Deserialize(reader, MakeGenericType(type));

            if (@object is null)
            {
                return null;
            }

            type = @object.GetType();

            if (!type.IsGenericType || type.GetGenericTypeDefinition() != DictionaryTypeDefinition)
            {
                return @object;
            }

            Type[] generic = type.GetGenericArguments()[1].GetGenericArguments();
            Type tree = DictionaryTreeTypeDefinition.MakeGenericType(generic);

            ConstructorInfo? method = tree.GetConstructor(new[]
            {
                DictionaryTypeDefinition.MakeGenericType(generic[0], DictionaryTreeNodeTypeDefinition.MakeGenericType(generic)),
                IEqualityComparerTypeDefinition.MakeGenericType(generic[0])
            });

            return method?.Invoke(new[] { @object, null });
        }

        protected internal override Boolean Write(in JsonWriter writer, Object? value, ref SerializerOptions options)
        {
            throw new NotSupportedException();
        }
    }
}