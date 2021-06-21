// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NetExtender.UserInterface
{
    public enum InterfaceCloseReason
    {
        None = 0,
        SystemShutdown = 1,
        WindowClosing = 2,
        UserClosing = 3,
        TaskManagerClosing = 4,
        OwnerClosing = 5,
        ApplicationExitCall = 6
    }
}