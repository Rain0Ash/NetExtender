// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.AspNetCore.Windows.Services.Interfaces;

namespace NetExtender.AspNetCore.Windows.Services
{
    public sealed class WindowsServicePauseStateService : IWindowsServicePauseStateService
    {
        public Boolean IsPaused { get; private set; }

        public void Pause()
        {
            IsPaused = true;
        }

        public void Resume()
        {
            IsPaused = false;
        }
    }
}