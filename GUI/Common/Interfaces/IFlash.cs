// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.GUI.Common.Interfaces
{
    public interface IFlash
    {
        public Boolean NotifyFlash();

        public Boolean StartFlash(UInt32 count = UInt32.MaxValue);

        public Boolean StopFlash();
    }
}