// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading.Tasks;
using NetExtender.UserInterface.WindowsPresentation.Types.Sounds;

namespace NetExtender.UserInterface.WindowsPresentation.Windows
{
    public abstract class SoundWindow : StartedWindow
    {
        public virtual WindowSoundsContainer Sounds
        {
            get
            {
                return WindowSoundsContainer.Default;
            }
        }

        public event WindowSoundEventHandler? Sound;

        protected SoundWindow()
        {
            BeginConstructorInit();
            Activated += OnActivated;
            Showed += OnShow;
            Closed += OnClosed;
            Sound += OnSound;
        }
        
        private void OnActivated(Object? sender, EventArgs args)
        {
            Sound?.Invoke(sender, new WindowSoundEventArgs(nameof(Activate)));
        }

        private void OnShow(Object? sender, EventArgs args)
        {
            Sound?.Invoke(sender, new WindowSoundEventArgs(nameof(Show)));
        }

        private void OnClosed(Object? sender, EventArgs args)
        {
            Sound?.Invoke(sender, new WindowSoundEventArgs(nameof(Close)));
        }

        private async void OnSound(Object? sender, WindowSoundEventArgs args)
        {
            if (!args.Handled)
            {
                await PlaySound(args.Sound);
            }
        }

        protected virtual async ValueTask PlaySound(String? sound)
        {
            await Sounds.Play(sound);
        }

        protected virtual async ValueTask PlaySound<T>(T sound) where T : unmanaged, Enum
        {
            await Sounds.Play(sound);
        }
    }
}