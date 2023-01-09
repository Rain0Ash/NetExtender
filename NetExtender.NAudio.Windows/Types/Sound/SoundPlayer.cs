// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using NetExtender.NAudio.Types.Sound.Interfaces;
using NetExtender.Types.Disposable;

namespace NetExtender.NAudio.Types.Sound
{
    public class SoundPlayer : ISoundPlayer
    {
        public virtual Single Volume
        {
            get
            {
                return 1;
            }
        }

        protected DisposeCollection<IActiveSound> ActiveSounds { get; } = new DisposeCollection<IActiveSound> { Active = true };

        [return: NotNullIfNotNull("sound")]
        protected virtual IActiveSound? Create(IAudioSoundFile? sound)
        {
            return sound is not null ? new ActiveSound(sound, this) : null;
        }

        public virtual Boolean Play(IAudioSoundFile? sound)
        {
            if (sound is null)
            {
                return false;
            }

            ActiveSounds.Add(Create(sound).Play());
            return true;
        }

        public virtual void Stop()
        {
            ActiveSounds.Dispose();
        }

        public virtual Boolean Remove(IActiveSound? sound)
        {
            return sound is not null && ActiveSounds.Remove(sound);
        }
    }

    public class SoundPlayer<T> : SoundPlayer where T : class, ISoundPlayer, new()
    {
        public static T Default { get; } = new T();
    }
}