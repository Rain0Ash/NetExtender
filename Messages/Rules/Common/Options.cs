// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Messages.Rules.Common
{
    [Flags]
    public enum ConsoleRuleOptions
    {
        None = 0,
        Hidden = 1,
        External = 2
    }
}