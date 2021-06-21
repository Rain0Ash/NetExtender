// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Utils.Static
{
    public static class CompabilityUtils
    {
        public static Boolean IsRunningOnMono { get; } = Type.GetType("Mono.Runtime") is not null;
    }
}