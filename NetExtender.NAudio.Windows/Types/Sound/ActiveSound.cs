// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NAudio.Wave;
using NetExtender.NAudio.Types.Sound.Interfaces;
using NetExtender.Utilities.NAudio;

namespace NetExtender.NAudio.Types.Sound
{
    public class ActiveSound : IActiveSound
    {
        public IAudioSoundFile Sound { get; }
        public ISoundPlayer Player { get; }
        public PlaybackState State { get; private set; }

        public virtual Single Volume
        {
            get
            {
                return Player.Volume * Sound.Volume / 4F;
            }
        }

        private AudioFileReader? Reader { get; set; }
        private WaveOutEvent? Device { get; set; }

        public ActiveSound(IAudioSoundFile sound, ISoundPlayer player)
        {
            Sound = sound ?? throw new ArgumentNullException(nameof(sound));
            Player = player ?? throw new ArgumentNullException(nameof(player));
            Reader = new AudioFileReader(Sound.Path);
            Device = new WaveOutEvent();
            Device.PlaybackStopped += Stop;
            State = PlaybackState.Paused;
        }

        public virtual IActiveSound Play()
        {
            if (Reader is null || Device is null)
            {
                throw new ObjectDisposedException(nameof(ActiveSound), $"Can't play disposed sound '{Sound.Path}'");
            }
            
            Device.Volume = Volume;
            
            switch (State)
            {
                case PlaybackState.Playing:
                    return this;
                case PlaybackState.Stopped:
                    throw new InvalidOperationException("Cannot play a stopped sound.");
                case PlaybackState.Paused:
                    Device.Play(Reader, Sound);
                    return this;
                default:
                    throw new ArgumentOutOfRangeException(nameof(State), State, null);
            }
        }

        public virtual void Stop()
        {
            State = PlaybackState.Stopped;
            Dispose();
        }

        private void Stop(Object? sender, StoppedEventArgs args)
        {
            Stop();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(Boolean disposing)
        {
            Reader?.Dispose();
            Device?.Dispose();
            Reader = null;
            Device = null;
            Player.Remove(this);
        }

        ~ActiveSound()
        {
            Dispose(false);
        }
    }
}