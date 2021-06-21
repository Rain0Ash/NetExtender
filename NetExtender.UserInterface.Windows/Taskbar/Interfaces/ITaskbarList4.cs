// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NetExtender.UserInterface.Windows.Taskbar.Interfaces
{
    [Guid("c43dc798-95d1-4bea-9030-bb99e2983a1a")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface ITaskbarList4
    {
        [MethodImpl(MethodImplOptions.PreserveSig)]
        void HrInit();

        [MethodImpl(MethodImplOptions.PreserveSig)]
        void AddTab(IntPtr hwnd);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        void DeleteTab(IntPtr hwnd);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        void ActivateTab(IntPtr hwnd);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        void SetActiveAlt(IntPtr hwnd);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        void MarkFullscreenWindow(IntPtr hwnd, Boolean fFullscreen);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        void SetProgressValue(IntPtr hwnd, UInt64 ullCompleted, UInt64 ullTotal);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        void SetProgressState(IntPtr hwnd, TaskbarProgressBarState tbpFlags);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        void RegisterTab(IntPtr hwndTab, IntPtr hwndMDI);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        void UnregisterTab(IntPtr hwndTab);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        void SetTabOrder(IntPtr hwndTab, IntPtr hwndInsertBefore);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        void SetTabActive(IntPtr hwndTab, IntPtr hwndInsertBefore, UInt32 dwReserved);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        TaskbarHResult ThumbBarAddButtons(IntPtr hwnd, UInt32 cButtons, ThumbButton[] pButtons);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        TaskbarHResult ThumbBarUpdateButtons(IntPtr hwnd, UInt32 cButtons, ThumbButton[] pButtons);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        void ThumbBarSetImageList(IntPtr hwnd, IntPtr himl);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        void SetOverlayIcon(IntPtr hwnd, IntPtr hIcon, String pszDescription);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        void SetThumbnailTooltip(IntPtr hwnd, String pszTip);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        void SetThumbnailClip(IntPtr hwnd, IntPtr prcClip);

        void SetTabProperties(IntPtr hwndTab, SetTabPropertiesOption stpFlags);
    }
}