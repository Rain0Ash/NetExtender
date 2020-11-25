// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#if !TOAST

namespace NetExtender.Notifications.Toasts.Mock
{
    public delegate void TypedEventHandler<in TNotify, in TArgs>(TNotify notify, TArgs args);
}

#endif