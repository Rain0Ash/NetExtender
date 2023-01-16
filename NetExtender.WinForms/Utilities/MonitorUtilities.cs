// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using NetExtender.Types.Monitors;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Windows
{
    public static class MonitorUtilities
    {
#pragma warning disable 649
        internal struct Devmode
        {
            private const Int32 CCHDEVICENAME = 0x20;
            private const Int32 CCHFORMNAME = 0x20;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]

            public String dmDeviceName;
            public Int16 dmSpecVersion;
            public Int16 dmDriverVersion;
            public Int16 dmSize;
            public Int16 dmDriverExtra;
            public Int32 dmFields;
            public Int32 dmPositionX;
            public Int32 dmPositionY;
            public ScreenOrientation dmDisplayOrientation;
            public Int32 dmDisplayFixedOutput;
            public Int16 dmColor;
            public Int16 dmDuplex;
            public Int16 dmYResolution;
            public Int16 dmTTOption;
            public Int16 dmCollate;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public String dmFormName;

            public Int16 dmLogPixels;
            public Int32 dmBitsPerPel;
            public Int32 dmPelsWidth;
            public Int32 dmPelsHeight;
            public Int32 dmDisplayFlags;
            public Int32 dmDisplayFrequency;
            public Int32 dmICMMethod;
            public Int32 dmICMIntent;
            public Int32 dmMediaType;
            public Int32 dmDitherType;
            public Int32 dmReserved1;
            public Int32 dmReserved2;
            public Int32 dmPanningWidth;
            public Int32 dmPanningHeight;
        }
#pragma warning restore 649

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern Boolean EnumDisplaySettings(String lpszDeviceName, Int32 iModeNum, ref Devmode lpDevMode);

        public static Monitor GetMonitor(Int32 id)
        {
            return GetMonitor(Screen.AllScreens[id], id);
        }

        public static Monitor GetMonitor(Screen screen)
        {
            return GetMonitor(screen, Screen.AllScreens.IndexOf(screen));
        }

        public static Monitor GetMonitor(Screen screen, Int32 customID)
        {
            Devmode devmode = new Devmode { dmSize = (Int16) Marshal.SizeOf(typeof(Devmode)) };
            EnumDisplaySettings(screen.DeviceName, -1, ref devmode);

            return new Monitor(customID, screen.DeviceName, screen.Bounds, screen.WorkingArea, screen.Bounds, devmode);
        }

        public static Monitor GetPrimaryMonitor()
        {
            return GetMonitor(Screen.PrimaryScreen, 0);
        }

        public static Monitor[] GetMonitors()
        {
            return Screen.AllScreens.Select(screen => GetMonitor(screen)).ToArray();
        }
    }
}