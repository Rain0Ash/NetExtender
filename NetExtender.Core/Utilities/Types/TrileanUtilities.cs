// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading.Tasks;
using NetExtender.Types.Numerics;

namespace NetExtender.Utilities.Types
{
    public static class TrileanUtilities
    {
        /// <summary>
        /// True task
        /// </summary>
        public static Task<Trilean> True { get; } = Task.FromResult(Trilean.True);

        /// <summary>
        /// Trit task
        /// </summary>
        public static Task<Trilean> Trit { get; } = Task.FromResult(Trilean.Trit);

        /// <summary>
        /// False task
        /// </summary>
        public static Task<Trilean> False { get; } = Task.FromResult(Trilean.False);
    }
}