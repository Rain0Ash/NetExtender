// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.GUI.Common.Interfaces;
using NetExtender.Utils.GUI.Flash;

namespace NetExtender.GUI.WPF.Windows
{
    public abstract class FlashWindow : FixedWindow, IFlashWindow
    {
        public Boolean NotifyFlash()
        {
            return FlashUtils.Notify((IGUIHandle)this);
        }

        public Boolean StartFlash(UInt32 count = UInt32.MaxValue)
        {
            return FlashUtils.Start((IGUIHandle)this, count);
        }

        public Boolean StopFlash()
        {
            return FlashUtils.Stop((IGUIHandle)this);
        }
    }
}