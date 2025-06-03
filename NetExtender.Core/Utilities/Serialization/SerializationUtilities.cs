// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using NetExtender.Types.Exceptions;

namespace NetExtender.Utilities.Serialization
{
    public static class SerializationUtilities
    {
        private static Func<SerializationInfo, String, Type, Object?> GetValueNoThrow { get; }

        static SerializationUtilities()
        {
            const BindingFlags binding = BindingFlags.Instance | BindingFlags.NonPublic;
            MethodInfo method = typeof(SerializationInfo).GetMethod(nameof(GetValueNoThrow), binding) ?? throw new NeverOperationException();
            ParameterExpression instance = Expression.Parameter(typeof(SerializationInfo), nameof(instance));
            ParameterExpression name = Expression.Parameter(typeof(String), nameof(name));
            ParameterExpression type = Expression.Parameter(typeof(Type), nameof(type));
            MethodCallExpression expression = Expression.Call(instance, method, name, type);
            GetValueNoThrow = Expression.Lambda<Func<SerializationInfo, String, Type, Object?>>(expression, instance, name, type).Compile();
        }

        public static void GetObjectData<T>(T value, SerializationInfo info, StreamingContext context) where T : ISerializable
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            value.GetObjectData(info, context);
        }

        public static T GetValue<T>(this SerializationInfo info, String parameter)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (parameter is null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            return info.GetValue(parameter, typeof(T)) is T result ? result : throw new SerializationException();
        }

        public static T? GetValueOrDefault<T>(this SerializationInfo info, String parameter)
        {
            return GetValueOrDefault<T?>(info, parameter, default);
        }

        public static T GetValueOrDefault<T>(this SerializationInfo info, String parameter, T alternate)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (parameter is null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            return TryGetValue<T>(info, parameter, out T? result) ? result : alternate;
        }

        public static Boolean TryGetValue(this SerializationInfo info, String parameter, Type type, [MaybeNullWhen(false)] out Object result)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (parameter is null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return (result = GetValueNoThrow(info, parameter, type)) is not null;
        }

        public static Boolean TryGetValue<T>(this SerializationInfo info, String parameter, [MaybeNullWhen(false)] out T result)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (parameter is null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            if (TryGetValue(info, parameter, typeof(T), out Object? value) && value is T convert)
            {
                result = convert;
                return true;
            }

            result = default;
            return false;
        }
    }
}