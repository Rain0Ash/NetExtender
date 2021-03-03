// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;

namespace NetExtender.Workstation
{
    public readonly struct Monitor
    {
        public Int32 ID { get; }
        public String Name { get; }
        public Rectangle Resolution { get; }
        public Rectangle WorkingArea { get; }
        public Rectangle Bounds { get; }

        public Hardware.DEVMODE DEVMODE { get; }

        public String DeviceName
        {
            get
            {
                return DEVMODE.dmDeviceName;
            }
        }

        public Int32 Frequency
        {
            get
            {
                return DEVMODE.dmDisplayFrequency;
            }
        }
        
        public Monitor(Int32 id, String name, Rectangle resolution, Rectangle workingArea, Rectangle bounds, Hardware.DEVMODE devmode)
        {
            ID = id;
            Name = name;
            Resolution = resolution;
            WorkingArea = workingArea;
            Bounds = bounds;
            DEVMODE = devmode;
        }
    }
}