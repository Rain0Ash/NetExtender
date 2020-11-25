// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Reflection;
using NetExtender.Utils.Core;

namespace NetExtender.Utils.Types
{
    public static partial class GenericUtils
    {
        private static Object DeepCopyInternal(Object original, IDictionary<Object, Object> visited)
        {
            if (original is null)
            {
                return null;
            }

            Type typeToReflect = original.GetType();

            if (typeToReflect.IsPrimitive())
            {
                return original;
            }

            if (visited.ContainsKey(original))
            {
                return visited[original];
            }

            if (typeof(Delegate).IsAssignableFrom(typeToReflect))
            {
                return null;
            }

            Object cloneObject = MemberwiseCloneMethod.Invoke(original, null);

            if (typeToReflect.IsArray)
            {
                Type arrayType = typeToReflect.GetElementType();
                if (!arrayType.IsPrimitive())
                {
                    Array clonedArray = (Array) cloneObject;

                    clonedArray.ForEach((array, indices) => array.SetValue(DeepCopyInternal(clonedArray?.GetValue(indices), visited), indices));
                }
            }

            visited.Add(original, cloneObject);

            DeepCopyFields(original, visited, cloneObject, typeToReflect);

            RecursiveDeepCopyBaseTypePrivateFields(original, visited, cloneObject, typeToReflect);

            return cloneObject;
        }

        private static void DeepCopyFields(Object original, IDictionary<Object, Object> visited, Object cloneObject, IReflect typeToReflect,
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy, Func<FieldInfo, Boolean> filter = null)
        {
            foreach (FieldInfo fieldInfo in typeToReflect.GetFields(bindingFlags))
            {
                if (filter is not null && !filter(fieldInfo))
                {
                    continue;
                }

                if (fieldInfo.FieldType.IsPrimitive())
                {
                    continue;
                }

                Object originalFieldValue = fieldInfo.GetValue(original);
                Object clonedFieldValue = DeepCopyInternal(originalFieldValue, visited);

                fieldInfo.SetValue(cloneObject, clonedFieldValue);
            }
        }

        private static void RecursiveDeepCopyBaseTypePrivateFields(Object original, IDictionary<Object, Object> visited, Object clone, Type typeToReflect)
        {
            if (typeToReflect.BaseType is null)
            {
                return;
            }

            RecursiveDeepCopyBaseTypePrivateFields(original, visited, clone, typeToReflect.BaseType);
            DeepCopyFields(original, visited, clone, typeToReflect.BaseType, BindingFlags.Instance | BindingFlags.NonPublic, fieldInfo => fieldInfo.IsPrivate);
        }
    }
}