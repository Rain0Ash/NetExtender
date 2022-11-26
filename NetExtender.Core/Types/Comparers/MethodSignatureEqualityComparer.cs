// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Reflection;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Comparers
{
    public sealed class MethodSignatureEqualityComparer : EqualityComparer<MethodInfo>
    {
        public new static MethodSignatureEqualityComparer Default { get; } = new MethodSignatureEqualityComparer();

        private MethodSignatureEqualityComparer()
        {
        }

        private static Type ConvertType(ParameterInfo info)
        {
            return info.ParameterType;
        }

        private static Boolean TypeEquals(Type x, Type y)
        {
            if (x.IsGenericParameter ^ y.IsGenericParameter)
            {
                return false;
            }

            if (x.IsGenericParameter && y.IsGenericParameter)
            {
                return x.GenericParameterPosition == y.GenericParameterPosition;
            }

            if (x.IsGenericType ^ y.IsGenericType)
            {
                return false;
            }

            if (!x.IsGenericType && !y.IsGenericType)
            {
                return x == y;
            }

            Type xdefinition = x.GetGenericTypeDefinition();
            Type ydefenition = y.GetGenericTypeDefinition();

            if (xdefinition != ydefenition)
            {
                return false;
            }

            Type[] xgeneric = x.GetGenericArguments();
            Type[] ygeneric = y.GetGenericArguments();

            return xgeneric.Length == ygeneric.Length && xgeneric.All((type, index) => TypeEquals(type, ygeneric[index]));
        }

        public override Boolean Equals(MethodInfo? x, MethodInfo? y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (x is null || y is null)
            {
                return false;
            }

            if (x.Name != y.Name)
            {
                return false;
            }

            if (x.CallingConvention != y.CallingConvention)
            {
                return false;
            }

            if (x.IsGenericMethod ^ y.IsGenericMethod)
            {
                return false;
            }

            x = x.TryGetGenericMethodDefinition();
            y = y.TryGetGenericMethodDefinition();

            Type[] xgeneric = x.TryGetGenericArguments() ?? Array.Empty<Type>();
            Type[] ygeneric = y.TryGetGenericArguments() ?? Array.Empty<Type>();

            if (xgeneric.Length != ygeneric.Length)
            {
                return false;
            }

            if (!TypeEquals(x.ReturnParameter.ParameterType, y.ReturnParameter.ParameterType))
            {
                return false;
            }

            Type[] xparameters = Array.ConvertAll(x.GetParameters(), ConvertType);
            Type[] yparameters = Array.ConvertAll(y.GetParameters(), ConvertType);

            return xparameters.Length == yparameters.Length && xparameters.All((type, index) => TypeEquals(type, yparameters[index]));
        }

        private static Int32 GetTypeHashCode(Type type)
        {
            if (type.IsGenericParameter)
            {
                return type.GenericParameterPosition.GetHashCode();
            }

            if (!type.IsGenericType)
            {
                return type.GetHashCode();
            }

            HashCode code = new HashCode();
            Type generic = type.GetGenericTypeDefinition();
            code.Add(generic);
            code.AddRange(type.GetGenericArguments());

            return code.ToHashCode();
        }

        public override Int32 GetHashCode(MethodInfo? method)
        {
            if (method is null)
            {
                return 0;
            }

            HashCode hash = new HashCode();
            hash.Add(method.Name);
            hash.Add(method.CallingConvention);

            if (method.IsGenericMethod)
            {
                method = method.GetGenericMethodDefinition();
            }

            Type[]? arguments = method.TryGetGenericArguments();

            if (arguments is not null)
            {
                hash.AddRange(arguments);
            }

            hash.Add(method.ReturnParameter.ParameterType);

            Type[] types = Array.ConvertAll(method.GetParameters(), ConvertType);

            foreach (Type type in types)
            {
                if (type.IsGenericParameter)
                {
                    hash.Add(type.GenericParameterPosition);
                    continue;
                }

                if (!type.IsGenericType)
                {
                    hash.Add(type);
                    continue;
                }

                hash.Add(type.GetGenericTypeDefinition());
                hash.AddRange(type.GetGenericArguments());
            }

            hash.AddRange(types);
            return hash.ToHashCode();
        }
    }
}