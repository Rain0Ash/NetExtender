// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Reflection;
using NetExtender.Types.Trees.Interfaces;
using Newtonsoft.Json;

namespace NetExtender.Types.Trees.Json
{
    public class DictionaryTreeJsonConverter : JsonConverter
    {
        private static Type DictionaryTypeDifinition { get; } = typeof(Dictionary<,>);
        private static Type DictionaryTreeTypeDifinition { get; } = typeof(DictionaryTree<,>);
        private static Type IDictionaryTreeTypeDifinition { get; } = typeof(IDictionaryTree<,>);
        private static Type DictionaryTreeNodeTypeDifinition { get; } = typeof(DictionaryTreeNode<,>);
        private static Type EqualityComparerTypeDifinition { get; } = typeof(EqualityComparer<>);
        private static Type IEqualityComparerTypeDifinition { get; } = typeof(IEqualityComparer<>);

        public override Boolean CanWrite
        {
            get
            {
                return false;
            }
        }

        public override Boolean CanConvert(Type objectType)
        {
            return false;
        }

        protected virtual Type MakeGenericType(Type objectType)
        {
            try
            {
                if (!objectType.IsGenericType)
                {
                    return objectType;
                }
                
                Type definition = objectType.GetGenericTypeDefinition();

                if (definition == IEqualityComparerTypeDifinition)
                {
                    return EqualityComparerTypeDifinition.MakeGenericType(objectType.GetGenericArguments());
                }

                if (definition == DictionaryTreeTypeDifinition || definition == IDictionaryTreeTypeDifinition)
                {
                    Type[] generic = objectType.GetGenericArguments();

                    return DictionaryTypeDifinition.MakeGenericType(generic[0], DictionaryTreeNodeTypeDifinition.MakeGenericType(generic[0], generic[1]));
                }

                return objectType;
            }
            catch (Exception exception)
            {
                throw new JsonSerializationException($"Unable to construct concrete type from generic {objectType}", exception);
            }
        }

        public override Object? ReadJson(JsonReader reader, Type objectType, Object? existingValue, JsonSerializer serializer)
        {
            Object? obj = serializer.Deserialize(reader, MakeGenericType(objectType));

            if (obj is null)
            {
                return null;
            }
            
            Type type = obj.GetType();

            if (!type.IsGenericType)
            {
                return obj;
            }

            if (type.GetGenericTypeDefinition() != DictionaryTypeDifinition)
            {
                return obj;
            }

            Type[] generic = type.GetGenericArguments()[1].GetGenericArguments();
            Type tree = DictionaryTreeTypeDifinition.MakeGenericType(generic);

            ConstructorInfo? method = tree.GetConstructor(new[]
            {
                DictionaryTypeDifinition.MakeGenericType(generic[0], DictionaryTreeNodeTypeDifinition.MakeGenericType(generic)),
                IEqualityComparerTypeDifinition.MakeGenericType(generic[0])
            });

            return method?.Invoke(new[] {obj, null});
        }

        public override void WriteJson(JsonWriter writer, Object? value, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }
    }
}