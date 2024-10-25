// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;

namespace NetExtender.UserInterface.WindowsPresentation.Sounds
{
    public delegate void WindowSoundEventHandler(Object? sender, WindowSoundEventArgs args);
    
    public class WindowSoundEventArgs : HandledEventArgs
    {
        public String Sound { get; }

        public WindowSoundEventArgs(String sound)
        {
            Sound = sound ?? throw new ArgumentNullException(nameof(sound));
        }
        
        public WindowSoundEventArgs(String sound, Boolean handled)
            : base(handled)
        {
            Sound = sound ?? throw new ArgumentNullException(nameof(sound));
        }
    }
}