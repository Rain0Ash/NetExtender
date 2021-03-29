// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading.Tasks;

namespace NetExtender.Utils.Types
{
    public static class BooleanUtils
    {
        /// <summary>
        /// Cached true task
        /// </summary>
        public static Task<Boolean> True { get; } = Task.FromResult(true);
        
        /// <summary>
        /// Cached false task
        /// </summary>
        public static Task<Boolean> False { get; } = Task.FromResult(false);
    }
}