// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.WindowsPresentation.Types.Commands.History
{
    [Flags]
    public enum CommandHistoryEntryOptions : Byte
    {
        None = 0,
        Blocking = 1
    }
}