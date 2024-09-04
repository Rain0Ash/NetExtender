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