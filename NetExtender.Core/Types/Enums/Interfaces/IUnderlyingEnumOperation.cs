// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Enums.Interfaces
{
    /// <summary>
    /// Provides underlying type specified operation interface.
    /// </summary>
    /// <typeparam name="T">Enum type</typeparam>
    internal interface IUnderlyingEnumOperation<T> where T : unmanaged, Enum
    {
        public Boolean IsContinuous { get; }
        public Boolean IsDefined(ref T value);
        public Boolean TryParse(String text, out T result);
        public EnumMember<T> GetMember(ref T value);
    }

    /// <summary>
    /// Provides underlying type specified operation interface.
    /// </summary>
    /// <typeparam name="T">Enum type</typeparam>
    /// <typeparam name="TUnderlying">Underlying enum type</typeparam>
    internal interface IUnderlyingEnumOperation<T, TUnderlying> : IUnderlyingEnumOperation<T> where T : unmanaged, Enum where TUnderlying : unmanaged
    {
        public Boolean IsDefined(ref TUnderlying value);
    }
}