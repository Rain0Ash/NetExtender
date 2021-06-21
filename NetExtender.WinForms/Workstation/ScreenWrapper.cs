// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Windows.Forms;
using Core.Workstation.Interfaces;
using NetExtender.Utils.Types;

namespace NetExtender.Workstation
{
    public sealed class ScreenWrapper : IScreen
    {
        [return: NotNullIfNotNull("screen")]
        public static implicit operator ScreenWrapper?(Screen? screen)
        {
            return screen is not null ? new ScreenWrapper(screen) : null;
        }
        
        [return: NotNullIfNotNull("wrapper")]
        public static implicit operator Screen?(ScreenWrapper? wrapper)
        {
            return wrapper?.Screen;
        }
        
        private Screen Screen { get; }

        public Int32 Id
        {
            get
            {
                return Screen.AllScreens.IndexOf(Screen);
            }
        }
        
        public String Name
        {
            get
            {
                return Screen.DeviceName;
            }
        }

        public Boolean Primary
        {
            get
            {
                return Screen.Primary;
            }
        }

        public Rectangle Bounds
        {
            get
            {
                return Screen.Bounds;
            }
        }

        public Rectangle WorkingArea
        {
            get
            {
                return Screen.WorkingArea;
            }
        }

        public Int32 BitsPerPixel
        {
            get
            {
                return Screen.BitsPerPixel;
            }
        }
        
        public ScreenWrapper(Screen screen)
        {
            Screen = screen ?? throw new ArgumentNullException(nameof(screen));
        }

        public override Boolean Equals(Object? obj)
        {
            return Screen.Equals(obj);
        }

        public override Int32 GetHashCode()
        {
            return Screen.GetHashCode();
        }

        public override String ToString()
        {
            return Screen.ToString();
        }
    }
}