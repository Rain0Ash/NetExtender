// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Tasks.Interfaces
{
    public interface IEventPausable : IPausable
    {
        event EmptyHandler Resumed;
        event EmptyHandler Paused;
    }

    public interface IPausable
    {
        public Boolean IsPaused { get; }

        public void Pause();
        public void Resume();
    }
}