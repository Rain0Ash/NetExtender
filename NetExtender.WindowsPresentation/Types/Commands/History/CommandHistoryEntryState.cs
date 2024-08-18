using System;

namespace NetExtender.WindowsPresentation.Types.Commands.History
{
    public enum CommandHistoryEntryState : Byte
    {
        None,
        Executed,
        Reverted
    }
}