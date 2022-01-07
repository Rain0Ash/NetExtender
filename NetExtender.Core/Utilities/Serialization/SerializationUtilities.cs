// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.Utilities.Serialization
{
    public static class SerializationUtilities
    {
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

            return info.GetValue(parameter, typeof(T)) is T result ? result : alternate;
        }
    }
}