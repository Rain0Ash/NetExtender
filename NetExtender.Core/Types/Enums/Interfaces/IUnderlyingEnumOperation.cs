// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.Types.Enums.Interfaces
{
    /// <summary>
    /// Provides underlying type specified operation interface.
    /// </summary>
    /// <typeparam name="T">Enum type</typeparam>
    /// <typeparam name="TUnderlying">Underlying enum type</typeparam>
    internal interface IUnderlyingEnumOperation<T, in TUnderlying> : IUnderlyingEnumOperation<T> where T : unmanaged, Enum where TUnderlying : unmanaged
    {
        public Boolean IsDefined(TUnderlying value);
    }

    /// <summary>
    /// Provides underlying type specified operation interface.
    /// </summary>
    /// <typeparam name="T">Enum type</typeparam>
    internal interface IUnderlyingEnumOperation<T> where T : unmanaged, Enum
    {
        public Boolean IsContinuous { get; }
        public Boolean IsDefined(T value);
        public EnumMember<T> GetMember(T value);
        public Boolean TryGetMember(T value, [MaybeNullWhen(false)] out EnumMember<T> result);
        public Boolean TryParse(String value, out T result);
    }
}