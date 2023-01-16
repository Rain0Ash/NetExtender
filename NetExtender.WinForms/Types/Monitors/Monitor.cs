// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Windows.Forms;
using NetExtender.Utilities.Windows;
using NetExtender.Workstation.Interfaces;

namespace NetExtender.Types.Monitors
{
    public readonly struct Monitor : IScreen
    {
        public Int32 Id { get; }
        public String Name { get; }

        public Boolean Primary
        {
            get
            {
                return Devmode.dmPositionX == 0 && Devmode.dmPositionY == 0;
            }
        }

        public Rectangle Resolution { get; }
        public Rectangle WorkingArea { get; }

        public Rectangle Bounds { get; }

        public Int32 BitsPerPixel
        {
            get
            {
                return Devmode.dmBitsPerPel;
            }
        }

        public String DeviceName
        {
            get
            {
                return Devmode.dmDeviceName;
            }
        }

        public Int32 Frequency
        {
            get
            {
                return Devmode.dmDisplayFrequency;
            }
        }

        public ScreenOrientation Orientation
        {
            get
            {
                return Devmode.dmDisplayOrientation;
            }
        }

        public Boolean IsEmpty
        {
            get
            {
                return Name is null;
            }
        }

        private MonitorUtilities.Devmode Devmode { get; }

        internal Monitor(Int32 id, String name, Rectangle resolution, Rectangle workingArea, Rectangle bounds, MonitorUtilities.Devmode devmode)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Resolution = resolution;
            WorkingArea = workingArea;
            Bounds = bounds;
            Devmode = devmode;
        }
    }
}